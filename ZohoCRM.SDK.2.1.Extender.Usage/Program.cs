using System.Diagnostics;
using CSharpFunctionalExtensions;
using ZohoCRM.SDK._2._1.Extender.Usage.Modules;
using ZohoCRM.SDK_2_1.Extender.BaseTypes.Everything;

Initialize.SdkInitialize(
    "1000.VAG312OFJIAV50WN9MQG3ESTSJPC7L",
    "91afdc65d0a7f360479f5433f419cdacaa621e6dbc",
    "1000.ba0a37f385079258466fd5b130981f41.c2852432466bb8f650e291462398e02a"
);

// var deleteResult = ZohoModules.Deals.DeleteAll();

var contact = new Contact("Petre", "Chitashvili", Maybe.None)
    .ForTransferring();

var contactSaved = contact.Save();
var contactSavedTwice = contactSaved.Value.UpdateItem(i => i.WithSurname("Birdman")).Save();

Debugger.Break();