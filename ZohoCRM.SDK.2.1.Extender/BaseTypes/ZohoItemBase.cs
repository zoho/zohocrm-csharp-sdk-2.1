using System;
using Com.Zoho.Crm.API.Record;
using CSharpFunctionalExtensions;

namespace ZohoCRM.SDK_2_1.Extender.BaseTypes;

public static class ZohoItemBaseWithId
{
    public static ZohoItemBaseWithId<T> ForCreating<T>(this T item) where T : ZohoItemBase => new(Maybe<long>.None, item);
    public static ZohoItemBaseWithId<T> ForTransferring<T>(this T item) where T : ZohoItemBase => new(item.ZohoId, item);
    public static ZohoItemBaseWithId<T> ForUpdating<T>(this T item, long zohoId) where T : ZohoItemBase => new(zohoId, item);
}

public class ZohoItemBaseWithId<T> where T : ZohoItemBase
{
    public ZohoItemBaseWithId(Maybe<long> zohoId, T item)
    {
        _zohoId = zohoId;
        Item = item;

        if (_zohoId.HasValue && item.ZohoId.HasValue && _zohoId.Value != item.ZohoId.Value)
            throw new InvalidOperationException($"Existing and New ZohoId's can't be different: Existing [{item.ZohoId}], New [{zohoId.Value}]");
    }
    
    public ZohoItemBaseWithId(T item):this(Maybe.None, item)
    {
    }

    public OperationTypeNeededInZohoEnum OperationTypeNeededInZoho => _zohoId.HasValue
        ? OperationTypeNeededInZohoEnum.Update
        : OperationTypeNeededInZohoEnum.Create;

    readonly Maybe<long> _zohoId;
    
    public Maybe<long> ZohoId => Item.ZohoId.HasValue ? Item.ZohoId.Value : _zohoId;
    public T Item { get; }

    public ZohoItemBaseWithId<T> SetZohoId(long zohoId) => new ZohoItemBaseWithId<T>(zohoId, Item);
    public ZohoItemBaseWithId<T> UpdateItem(T item) => new ZohoItemBaseWithId<T>(_zohoId, item);
    public ZohoItemBaseWithId<T> UpdateItem(Func<T,T> itemFunc) => new ZohoItemBaseWithId<T>(_zohoId, itemFunc(Item));

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
    public abstract Maybe<long> ZohoId { get; }
    public abstract ZohoModules ZohoModule { get; }

    public abstract string RecordIdentifier { get; }

    public abstract Record CreateRecord(Record initialRecord);
}