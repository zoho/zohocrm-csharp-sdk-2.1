using Com.Zoho.Crm.API.Record;
using CSharpFunctionalExtensions;
using ZohoCRM.SDK_2_1.Extender.BaseTypes;
using ZohoCRM.SDK_2_1.Extender.BaseTypes.Operations;
using ZohoCRM.SDK_2_1.Extender.BaseTypes.ZohoItem;

namespace ZohoCRM.SDK._2._1.Extender.Usage.Modules;

public class Contact : ZohoItemBase
{
    public Contact(string firstname, string lastname, Maybe<long> zohoId)
    {
        Firstname = firstname;
        Lastname = lastname;
        ZohoId = zohoId;
    }

    public string Firstname { get; }
    public string Lastname { get; }
    public Contact WithSurname(string surname) => new(Firstname, surname, ZohoId);

    public override Maybe<long> ZohoId { get; }
    public override ZohoModules ZohoModule => ZohoModules.Contacts;
    public override string RecordIdentifier => Firstname + " " + Lastname;

    public override Record CreateRecord(Record initialRecord)
        => initialRecord
            .AddFieldValue("First_Name", Firstname)
            .AddFieldValue("Last_Name", Lastname);
}