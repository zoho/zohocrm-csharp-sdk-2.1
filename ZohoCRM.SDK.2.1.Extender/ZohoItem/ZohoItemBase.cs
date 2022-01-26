using System;
using Com.Zoho.Crm.API.Record;
using CSharpFunctionalExtensions;
using ZohoCRM.SDK_2_1.Extender.BaseTypes.Operations;

namespace ZohoCRM.SDK_2_1.Extender.BaseTypes.ZohoItem;

public class ZohoItemBaseWithId<T> where T : ZohoItemBase
{
    public ZohoItemBaseWithId(Maybe<long> zohoId, T item)
    {
        _zohoId = zohoId;
        Item = item;

        if (_zohoId.HasValue && item.ZohoId.HasValue && _zohoId.Value != item.ZohoId.Value)
            throw new InvalidOperationException($"Existing and New ZohoId's can't be different: Existing [{item.ZohoId}], New [{zohoId.Value}]");
    }

    public ZohoItemBaseWithId(T item) : this(Maybe.None, item)
    {
    }

    public OperationTypeNeededInZohoEnum OperationTypeNeededInZoho => ZohoId
        .HasNoValue
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        .UseThenReturnSelf(hasNoValue =>
        {
            if (hasNoValue && Item.OperationTypeNeededInZoho != OperationTypeNeededInZohoEnum.Create)
                throw new InvalidOperationException("You must specify OperationTypeNeededInZohoEnum.Create for when ZohoId is missing");
        })
        ? OperationTypeNeededInZohoEnum.Create
        : Item.OperationTypeNeededInZoho == OperationTypeNeededInZohoEnum.Create // ZohoItemBase doesn't YET know that the record has already been updated
            ? OperationTypeNeededInZohoEnum.Update
            : Item.OperationTypeNeededInZoho;

    readonly Maybe<long> _zohoId;

    public Maybe<long> ZohoId => Item.ZohoId.HasValue ? Item.ZohoId.Value : _zohoId;
    public T Item { get; }

    public ZohoItemBaseWithId<T> SetZohoId(long zohoId) => new(zohoId, Item);
    public ZohoItemBaseWithId<T> UpdateItem(T item) => new(_zohoId, item);
    public ZohoItemBaseWithId<T> UpdateItem(Func<T, T> itemFunc) => new(_zohoId, itemFunc(Item));

    Record ZohoRecordInternal()
    {
        var record = new Record();
        if (_zohoId.HasValue)
            record.AddKeyValue("id", _zohoId.Value);

        return Item.CreateRecord(record);
    }

    public Record ZohoRecord => ZohoRecordInternal();
}

public abstract class ZohoItemBase
{
    public OperationTypeNeededInZohoEnum OperationTypeNeededInZoho => ZohoId
        .HasNoValue
        ? OperationTypeNeededInZohoEnum.Create
        : OperationTypeNeededInZohoEnum.Update;

    public abstract Maybe<long> ZohoId { get; }
    public abstract ZohoModules ZohoModule { get; }

    public abstract string RecordIdentifier { get; }

    public abstract Record CreateRecord(Record initialRecord);
}