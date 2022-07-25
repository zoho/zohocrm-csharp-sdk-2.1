using System.Diagnostics;
using CSharpFunctionalExtensions;
using ZohoCRM.SDK._2._1.Extender.Usage.Modules;
using ZohoCRM.SDK_2_1.Core;
using ZohoCRM.SDK_2_1.Core.Operations;
using ZohoCRM.SDK_2_1.Core.ZohoItem;
using ZohoCRM.SDK_2_1.Extender.BaseTypes;

Initialize.SdkInitialize(
    new ClientId("1000.VAG312OFJIAV50WN9MQG3ESTSJPC7L"),
    new ClientSecret("91afdc65d0a7f360479f5433f419cdacaa621e6dbc"),
    new GrantToken("1000.ba0a37f385079258466fd5b130981f41.c2852432466bb8f650e291462398e02a"),
    new SdkLogFileName("sdk.log"),
    new UserEmail("eurocreditcrm@gmail.com"),
    new ResourcesDirectory("Resources"),
    new TokenStorePath("tokenstore.path")
);

// var deleteResult = ZohoModules.Deals.DeleteAll();

var r = ZohoModules.Deals.GetSingleRecord(5183745000000354338);

var contact = new Contact("Petre", "Chitashvili", Maybe.None)
    .ForTransferring();

var contactSaved = contact.Save();
var contactSavedTwice = contactSaved.Value.UpdateItem(i => i.WithSurname("Birdman")).Save();

var lead = new Deal(contactSaved.Value, Maybe<long>.None).ForTransferring();
var leadSaved = lead.Save();

Debugger.Break();