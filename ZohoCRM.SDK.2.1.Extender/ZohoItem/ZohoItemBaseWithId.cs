namespace ZohoCRM.SDK_2_1.Core.ZohoItem;

public static class ZohoItemBaseWithId
{
    // public static ZohoItemBaseWithId<T> ForCreating<T>(this T item) where T : ZohoItemBase => new(Maybe<long>.None, item);
    public static ZohoItemBaseWithId<T> ForTransferring<T>(this T item) where T : ZohoItemBase => new(item.ZohoId, item);
    // public static ZohoItemBaseWithId<T> ForUpdating<T>(this T item, long zohoId) where T : ZohoItemBase => new(zohoId, item);
}