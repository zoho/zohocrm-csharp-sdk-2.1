using System.Diagnostics;
using CSharpFunctionalExtensions;
using ZohoCRM.Modules.ModuleTypes;
using ZohoCRM.SDK._2._1.Extender.Usage.Modules;
using ZohoCRM.SDK._2._1.Tester.Console;

Initialize.SdkInitialize("1000.d7faf845efaf44b944952cbac77355b2.cb6555d7ee822bb3b5f1bfbdac69d303",
    "1000.VAG312OFJIAV50WN9MQG3ESTSJPC7L",
    "91afdc65d0a7f360479f5433f419cdacaa621e6dbc"
);

// var deleteResult = ZohoModules.Deals.DeleteAll();

var contact = new Contact("პეტ", "ჩიტ")
    .ForCreating();

var contactSaved = contact.Save();
var contactSavedTwice = contactSaved.Value.UpdateItem(i => i.WithSurname("ჩიტაშვილი")).Save();

Debugger.Break();