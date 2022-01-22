using Com.Zoho.Crm.API.Record;
using CSharpFunctionalExtensions;
using ZohoCRM.Modules.ModuleTypes;

namespace ZohoCRM.SDK._2._1.Tester.Console;

public static class ZohoItemBaseWithId
{
    public static ZohoItemBaseWithId<T> ForCreating<T>(this T item) where T : ZohoItemBase => new(Maybe<long>.None, item);
    public static ZohoItemBaseWithId<T> ForUpdating<T>(this T item, long zohoId) where T : ZohoItemBase => new(zohoId, item);
}

public class ZohoItemBaseWithId<T> where T : ZohoItemBase
{
    public ZohoItemBaseWithId(Maybe<long> zohoId, T item)
    {
        ZohoId = zohoId;
        Item = item;
    }

    public OperationTypeNeededInZohoEnum OperationTypeNeededInZoho => ZohoId.HasValue
        ? OperationTypeNeededInZohoEnum.Update
        : OperationTypeNeededInZohoEnum.Create;

    public Maybe<long> ZohoId { get; }
    public T Item { get; }

    public ZohoItemBaseWithId<T> SetZohoId(long zohoId) => new ZohoItemBaseWithId<T>(zohoId, Item);
    public ZohoItemBaseWithId<T> UpdateItem(T item) => new ZohoItemBaseWithId<T>(ZohoId, item);
    public ZohoItemBaseWithId<T> UpdateItem(Func<T,T> itemFunc) => new ZohoItemBaseWithId<T>(ZohoId, itemFunc(Item));

    Record ZohoRecordInternal()
    {
        var record = new Record();
        if (ZohoId.HasValue)
            record.AddKeyValue("id", ZohoId.Value);

        return Item.CreateRecord(record);
    }

    public Record ZohoRecord => ZohoRecordInternal();
}

public abstract class ZohoItemBase
{
    public abstract ZohoModules ZohoModule { get; }

    public abstract string RecordIdentifier { get; }

    public abstract Record CreateRecord(Record initialRecord);
}