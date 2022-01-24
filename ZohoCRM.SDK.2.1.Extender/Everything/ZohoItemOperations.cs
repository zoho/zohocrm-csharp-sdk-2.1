using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using CSharpFunctionalExtensions;

namespace ZohoCRM.SDK_2_1.Extender.BaseTypes;

public static class ResultExtensions
{
    public static Result<TSuccess> Switch<T, TSuccess>(this Result<T> resultT, Func<T, TSuccess> successFunc, Func<string, string> failureFunc) =>
        resultT.IsSuccess switch
        {
            true => successFunc(resultT.Value),
            _ => resultT.MapError(failureFunc)
                .ConvertFailure<TSuccess>()
        };
}

public static class ZohoItemOperations
{
    static Result<ZohoItemBaseWithId<T>> UpdateCore<T>(this ZohoItemBaseWithId<T> zohoItemBase) where T : ZohoItemBase
    {
        if (!zohoItemBase.ZohoId.HasValue) throw new InvalidOperationException("Can't be updating zohoItemBase w/o zohoId specified");

        var parsedDataCore = ParseResult(zohoItemBase,
            tuple => () => tuple.ro.UpdateRecord(zohoItemBase.ZohoId.Value, zohoItemBase.Item.ZohoModule.ToString(), tuple.bw, tuple.hm));

        var updatedResult = parsedDataCore
            .OnFailureCompensate(error => error.Contains("the id given seems to be invalid")
                ? zohoItemBase.CreateCore()
                : parsedDataCore);

        return updatedResult;
    }


    static Result<ZohoItemBaseWithId<T>> CreateCore<T>(this ZohoItemBaseWithId<T> zohoItemBase, CreateOperationType createOperationType = CreateOperationType.CreateAndUpdateExisting) where T : ZohoItemBase
    {
        if (zohoItemBase.ZohoId.HasValue) throw new InvalidOperationException("Can't be CREATING zohoItemBase with zohoId specified");

        var parsedDataCore = ParseResult(zohoItemBase, tuple => () => tuple.ro.CreateRecords(zohoItemBase.Item.ZohoModule.ToString(), tuple.bw, tuple.hm))
            .Use(parsedDataCore =>
                parsedDataCore.IsSuccess
                    ? parsedDataCore
                    : parsedDataCore.IsFailure
                        ? ZohoApiErrorParser.ParseZohoApiError(parsedDataCore.Error)
                            .Use(altaIdParser => altaIdParser is { IsSuccess : true }
                                ? zohoItemBase.Item.ZohoModule.GetSingleRecord(altaIdParser.Value)
                                    .Use(existingRecord =>
                                        existingRecord switch
                                        {
                                            { IsSuccess: true } => createOperationType switch
                                            {
                                                CreateOperationType.CreateOrAcceptExisting => zohoItemBase.SetZohoId(existingRecord.Value.Record.Id!.Value),
                                                CreateOperationType.CreateAndUpdateExisting => ParseResult(zohoItemBase, tuple =>
                                                    () => tuple.ro.UpdateRecord(existingRecord.Value.Record.Id, zohoItemBase.Item.ZohoModule.ToString(), tuple.bw, tuple.hm)),
                                                _ => throw new ArgumentOutOfRangeException(nameof(createOperationType), createOperationType, null)
                                            },
                                            { IsFailure: true } => parsedDataCore
                                                .Combine(altaIdParser.ConvertFailure<ZohoItemBaseWithId<T>>())
                                                .ConvertFailure<ZohoItemBaseWithId<T>>(),
                                            _ => throw new ArgumentOutOfRangeException(nameof(existingRecord), existingRecord, null)
                                        }
                                    )
                                : altaIdParser is { IsFailure: true }
                                    ? parsedDataCore
                                        .Combine(altaIdParser.ConvertFailure<ZohoItemBaseWithId<T>>())
                                        .ConvertFailure<ZohoItemBaseWithId<T>>()
                                    : throw new ArgumentOutOfRangeException(nameof(altaIdParser), altaIdParser, null))
                        : throw new ArgumentOutOfRangeException());

        return parsedDataCore;
    }


    public enum CreateOperationType
    {
        CreateOrAcceptExisting,
        CreateAndUpdateExisting
    }


    public static Result<ZohoItemBaseWithId<T>> Save<T>(this ZohoItemBaseWithId<T> zohoItemBase, CreateOperationType createOperationType = CreateOperationType.CreateAndUpdateExisting) where T : ZohoItemBase
    {
        var parsedData =
            zohoItemBase.OperationTypeNeededInZoho switch
            {
                OperationTypeNeededInZohoEnum.Create => CreateCore(zohoItemBase, createOperationType),
                OperationTypeNeededInZohoEnum.Update => UpdateCore(zohoItemBase),
                OperationTypeNeededInZohoEnum.IgnoreDueToError => Result.Failure<ZohoItemBaseWithId<T>>("IgnoredDueToError"),
                OperationTypeNeededInZohoEnum.LeaveUnchanged => zohoItemBase
                    // .ZohoRecord
                    .UseThenReturnSelf(zr =>
                    {
                        if (zr.ZohoId.HasNoValue)
                            throw new InvalidOperationException("Can't Leave unsaved record Unchanged");
                    }),
                _ => throw new ArgumentOutOfRangeException()
            };

        return parsedData.Map(pd =>
        {
            if (pd.ZohoId.HasNoValue)
            {
                Debugger.Break();
            }

            // Debug.Assert(pd.ZohoId.HasValue, "pd.Id != null");

            return zohoItemBase.SetZohoId(pd.ZohoId.Value);
        });
    }

    static Result<ZohoItemBaseWithId<T>> ParseResult<T>(ZohoItemBaseWithId<T> zohoItemBase, Func<(BodyWrapper bw, HeaderMap hm, RecordOperations ro), Func<APIResponse<ActionHandler>>> apiResponseHandler)
        where T : ZohoItemBase
    {
        if (!Initialize.IsInitialized) throw new InvalidOperationException("Please initialize the SDK first!");

        var bodyWrapper = new BodyWrapper();
        var recordOperations = new RecordOperations();
        var headerInstance2 = new HeaderMap();
        bodyWrapper.Data = new[] { zohoItemBase.ZohoRecord }.ToList();

        Result<Record> ParseResultCore(Func<APIResponse<ActionHandler>> parseFunc)
        {
            Result<TY> EnrichFailure<TY>(Result<TY> result)
                => result.IsSuccess ? result : Result.Failure<TY>($"{result.Error}\r\nError saving [{zohoItemBase.Item.ZohoModule}][{zohoItemBase.Item.RecordIdentifier}]");

            var parseFuncResult = Result.Try(parseFunc, e => e.ToString());

            return parseFuncResult
                    .Bind(response => RecordsParser.ParseData(response)
                        .Use(r => r.IsFailure
                            ? r.ConvertFailure<Record>()
                            : r.Value.Single()))
                    .Use(EnrichFailure)
                //.LogErrorToZoho(zohoItemBase)
                ;
        }

        var parsedResult = ParseResultCore(apiResponseHandler((bodyWrapper, headerInstance2, recordOperations)));

        var withBind = parsedResult
            // .Map(r => )
            .Map(r => zohoItemBase.SetZohoId(r.Id!.Value));

        return  withBind;
    }

    static class ZohoApiErrorParser
    {
        public static Result<long> ParseZohoApiError(string text)
        {
            var split = text.Split(';')
                    .Select(s => s.Split(':'))
                    .ToList()
                ;

            if (!split.Any()) return Result.Failure<long>($"No items [{text}]");

            var couplesOfTwo = split
                .Select(s => new { Key = s.First(), Value = s.Last() })
                .AsReadOnlyList();

            var first = couplesOfTwo.First();
            var second = couplesOfTwo.Skip(1).FirstOrDefault();
            if (first.Value == "Alta_ID" && second?.Key == "id")
                return long.TryParse(second.Value, out var parsedValue)
                    ? parsedValue
                    : Result.Failure<long>($"Couldn't convert [{second.Value}] to long");

            return Result.Failure<long>($"Couldn't get ID from [{text}]");
        }
    }

    public static Result DeleteAll(this ZohoModules module)
    {
        var recordOperations = new RecordOperations();

        Result<IEnumerable<RecordsParser.RecordT>> GetRecords() => module.GetRecords();

        var records = GetRecords();

        do
        {
            if (records.IsFailure) return records.ConvertFailure<Record>();

            $"Deleting {records.Value.Count()} {module} items".Dump();

            var headerInstance = new HeaderMap();

            var result = records
                .Value
                .Chunk(90)
                .Select(recordTs =>
                {
                    var paramInstance = new ParameterMap();

                    recordTs.ForEach(id => paramInstance.Add(RecordOperations.DeleteRecordsParam.IDS, id.Record.Id.ToString()));
                    paramInstance.Add(RecordOperations.DeleteRecordsParam.WF_TRIGGER, false);

                    return recordOperations.DeleteRecords(module.ToString(), paramInstance, headerInstance);
                }).ToList();

            records = GetRecords();
        } while (records.IsSuccess && records.Value.Any());


        return Result.Success();
    }

    public static Result DeleteSpecific(ZohoModules module, long recordId)
    {
        var recordOperations = new RecordOperations();

        var headerInstance = new HeaderMap();

        var paramInstance = new ParameterMap();

        paramInstance.Add(RecordOperations.DeleteRecordsParam.IDS, recordId.ToString());
        paramInstance.Add(RecordOperations.DeleteRecordsParam.WF_TRIGGER, false);

        var response = recordOperations.DeleteRecords(module.ToString(), paramInstance, headerInstance);

        return Result.Success();
    }
}

