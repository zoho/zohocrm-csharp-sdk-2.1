using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using CSharpFunctionalExtensions;

namespace ZohoCRM.SDK_2_1.Core.Operations;

public static class RecordsParser
{
    public static Result<IEnumerable<RecordT>> ParseData(APIResponse<ResponseHandler> response)
    {
        Result<IEnumerable<RecordT>> Fail(string error) => Result.Failure<IEnumerable<RecordT>>(error);

        if (new List<int> { 204, 304 }.Contains(response.StatusCode))
            return Fail(response.StatusCode == 204 ? "No Content" : "Not Modified");

        if (!response.IsExpected) return Fail("!IsExpected");

        return response.Object switch
        {
            ResponseWrapper responseWrapper => Result.Success(responseWrapper.Data.Select(d => new RecordT(d))),
            APIException exception => Fail(exception.ToString()!),
            _ => Fail("Shouldn't be here!")
        };
    }

    public static Result<IEnumerable<Result<Record>>> ParseData(APIResponse<ActionHandler> response)
    {
        Result<Record> Fail(string error) => Result.Failure<Record>(error);
        Result<IEnumerable<Result<Record>>> FailLarge(string error) => Result.Failure<IEnumerable<Result<Record>>>(error);

        if (new List<int> { 204, 304 }.Contains(response.StatusCode))
            return FailLarge(response.StatusCode == 204 ? "No Content" : "Not Modified");

        if (!response.IsExpected) return FailLarge("!IsExpected");

        if (response.Object is APIException apiExceptionObject)
            return FailLarge(apiExceptionObject.Message.Value + " " + apiExceptionObject.Details.Select(di => $"{di.Key}:{di.Value}").StringJoinWithSemicolon());
        if (response.Object is not ActionWrapper actionWrapper) 
            return FailLarge("Shouldn't be here!");

        var result = actionWrapper.Data
            .Select(d =>
            {
                return d switch
                {
                    APIException apiException => Fail(apiException.Message.Value + " " + apiException.Details.Select(di => $"{di.Key}:{di.Value}").StringJoinWithSemicolon()),
                    SuccessResponse successResponse => long.Parse(successResponse.Details.Single(di => di.Key == "id").Value.ToString() ?? throw new InvalidOperationException())
                        .Use(t =>
                        {
                            var record = new Record();
                            record.AddFieldValue(Accounts.ID, t);
                            return record;
                        }),
                    _ => Fail("Unknown error occured")
                };
            });
        return Result.Success(result);
    }

    public class RecordT
    {
        public RecordT(Record record)
        {
            //Get the KeyValue map
            KeyValues = record.GetKeyValues().Select(k => (k.Key, k.Value)).ToList().AsReadOnly()!;
            NonEmptyKeyValues = KeyValues.OrderBy(kv => kv.Key).Where(k => k.Value != null).ToList().AsReadOnly();
            Record = record;
        }

        public Record Record { get; }
        public ReadOnlyCollection<(string Key, object? Value)> KeyValues { get; }
        public ReadOnlyCollection<(string key, object? value)> NonEmptyKeyValues { get; }
    }
}