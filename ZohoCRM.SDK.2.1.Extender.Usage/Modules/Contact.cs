using System.Runtime.Versioning;
using Com.Zoho.Crm.API.Record;
using ZohoCRM.Modules.ModuleTypes;
using ZohoCRM.SDK._2._1.Tester.Console;

namespace ZohoCRM.SDK._2._1.Extender.Usage.Modules;

public class Contact : ZohoItemBase
{
    public Contact(string firstname, string lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }

    public string Firstname { get; }
    public string Lastname { get; }
    public Contact WithSurname(string surname) => new(Firstname, surname);

    public override ZohoModules ZohoModule => ZohoModules.Contacts;
    public override string RecordIdentifier => Firstname + " " + Lastname;

    public override Record CreateRecord(Record initialRecord)
        => initialRecord
            .AddFieldValue("First_Name", Firstname)
            .AddFieldValue("Last_Name", Lastname);
}