using Com.Zoho.Crm.API.Record;
using CSharpFunctionalExtensions;
using ZohoCRM.SDK_2_1.Extender.BaseTypes;
using ZohoCRM.SDK_2_1.Extender.BaseTypes.Operations;
using ZohoCRM.SDK_2_1.Extender.BaseTypes.ZohoItem;

namespace ZohoCRM.SDK._2._1.Extender.Usage.Modules;

public class Deal : ZohoItemBase
{
    public Deal(ZohoItemBaseWithId<Contact> contact, Maybe<long> zohoId)
    {
        ZohoId = zohoId;
        Contact = contact;
    }

    public ZohoItemBaseWithId<Contact> Contact { get; }

    public override Maybe<long> ZohoId { get; }
    public override ZohoModules ZohoModule => ZohoModules.Deals;
    public override string RecordIdentifier => "-";

    public override Record CreateRecord(Record initialRecord)
        => initialRecord
            // .AddRecordValue("Contact_Name", Contact)
            .AddFieldValue("Deal_Name", Contact.Item.Firstname)
            .AddRecordValueByKeyValues("Contact_Name", Contact);
}