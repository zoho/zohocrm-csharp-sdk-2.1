using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Attachment = Com.Zoho.Crm.Sample.Attachments.Attachment;
using Com.Zoho.Crm.Sample.Initializer;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using Com.Zoho.Crm.API;
using static Com.Zoho.Crm.API.Modules.ModulesOperations;
using Newtonsoft.Json.Linq;
using Com.Zoho.API.Exception;
using Com.Zoho.Crm.Sample.AssignmentRules;
using Com.Zoho.Crm.Sample.EmailTemplates;
using Com.Zoho.Crm.Sample.FieldAttachments;
using Com.Zoho.Crm.Sample.InventoryTemplates;
using Com.Zoho.Crm.Sample.PipeLine;

namespace csharpsdksampleapplication
{
    class Test
    {
        static void Main(string[] args)
        {
            Initialize.SDKInitialize();

            AssignmentRule();

            Attachment();

            BluePrint();

            BulkRead();

            BulkWrite();

            ContactRoles();

            Currency();

            CustomView();

            EmailTemplates();

            FieldAttachments();

            Field();

            File();

            InventoryTemplates();

            Layout();

            Module();

            Note();

            Notification();

            Organization();

            Pipeline();

            Profile();

            Query();

            Record();

            RelatedList();

            RelatedRecords();

            Role();

            SendMail();

            ShareRecords();

            Tags();

            Tax();

            Territory();

            User();

            VariableGroup();

            Variable();

            Wizards();

            //Threading();

            //TestUpload();
        }

        public static void AssignmentRule()
        {
            long ruleId = 34770614353013;

            AssignmentRules.GetAssignmentRules();

            AssignmentRules.GetAssignmentRule(ruleId);
        }

        public static void Attachment()
        {
            try
            {
                string moduleAPIName = "Leads";

                long recordId = 3477061011829018;

                long attachmentId = 3477061012177001;//34770617605001

                string absoluteFilePath = "/Users/Documents/file/download.png";

                string destinationFolder = "/Users/Documents/file";

                string attachmentURL = "https://5.imimg.com/data5/KJ/UP/MY-8655440/zoho-crm-500x500.png";

                List<long> attachmentIds = new List<long>() { 3477061012325001, 3477061012326001, 34770617605001 };

                Com.Zoho.Crm.Sample.Attachments.Attachment.UploadAttachments(moduleAPIName, recordId, absoluteFilePath);

                Com.Zoho.Crm.Sample.Attachments.Attachment.GetAttachments(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.Attachments.Attachment.DeleteAttachments(moduleAPIName, recordId, attachmentIds);

                Com.Zoho.Crm.Sample.Attachments.Attachment.DownloadAttachment(moduleAPIName, recordId, attachmentId, destinationFolder);

                Com.Zoho.Crm.Sample.Attachments.Attachment.DeleteAttachment(moduleAPIName, recordId, attachmentId);

                Com.Zoho.Crm.Sample.Attachments.Attachment.UploadLinkAttachments(moduleAPIName, recordId, attachmentURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void BluePrint()
        {
            try
            {
                string moduleAPIName = "Leads";

                long recordId = 34770614381002;

                long transitionId = 34770610173093;

                Com.Zoho.Crm.Sample.BluePrint.BluePrint.GetBlueprint(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.BluePrint.BluePrint.UpdateBlueprint(moduleAPIName, recordId, transitionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void BulkRead()
        {
            try
            {
                string moduleAPIName = "Leads";

                long jobId = 3477061012331;

                string destinationFolder = "/Users/Documents/file";

                Com.Zoho.Crm.Sample.BulkRead.BulkRead.CreateBulkReadJob(moduleAPIName);

                Com.Zoho.Crm.Sample.BulkRead.BulkRead.GetBulkReadJobDetails(jobId);

                Com.Zoho.Crm.Sample.BulkRead.BulkRead.DownloadResult(jobId, destinationFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void BulkWrite()
        {
            try
            {
                string absoluteFilePath = "/Users/Documents/CRM_SDK/Leads.zip";

                string orgID = "673573045";

                string moduleAPIName = "Leads";

                string fileId = "3477061012331001";

                long jobID = 3477061012345001;

                string downloadUrl = "https://download-accl.zoho.com/v2/crm/673573045/bulk-write/3477061012345001/3477061012345001.zip";

                string destinationFolder = "/Users/Documents/file";

                Com.Zoho.Crm.Sample.BulkWrite.BulkWrite.UploadFile(orgID, absoluteFilePath);

                Com.Zoho.Crm.Sample.BulkWrite.BulkWrite.CreateBulkWriteJob(moduleAPIName, fileId);

                Com.Zoho.Crm.Sample.BulkWrite.BulkWrite.GetBulkWriteJobDetails(jobID);

                Com.Zoho.Crm.Sample.BulkWrite.BulkWrite.DownloadBulkWriteResult(downloadUrl, destinationFolder);
            }
            catch (SDKException ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void ContactRoles()
        {
            try
            {
                long contactRoleId = 3477061011644007;

                List<string> contactRoleIds = new List<string>() { "3477061012212001", "3477061010404005", "3477061011644011", "34770619305", "3477061813705" };

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.GetContactRoles();

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.CreateContactRoles();

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.UpdateContactRoles();

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.DeleteContactRoles(contactRoleIds);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.GetContactRole(contactRoleId);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.UpdateContactRole(contactRoleId);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.DeleteContactRole(contactRoleId);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.GetAllContactRolesOfDeal(3477061010553012);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.GetContactRoleOfDeal(34770610358009, 3477061010553012);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.AddContactRoleToDeal(34770610358009, 3477061010553012);

                Com.Zoho.Crm.Sample.ContactRoles.ContactRoles.RemoveContactRoleFromDeal(34770610358009, 3477061010553012);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Currency()
        {
            try
            {
                long currencyId = 34770616008002;

                Com.Zoho.Crm.Sample.Currencies.Currency.GetCurrencies();

                Com.Zoho.Crm.Sample.Currencies.Currency.AddCurrencies();

                Com.Zoho.Crm.Sample.Currencies.Currency.UpdateCurrencies();

                Com.Zoho.Crm.Sample.Currencies.Currency.EnableMultipleCurrencies();

                Com.Zoho.Crm.Sample.Currencies.Currency.UpdateBaseCurrency();

                Com.Zoho.Crm.Sample.Currencies.Currency.GetCurrency(currencyId);

                Com.Zoho.Crm.Sample.Currencies.Currency.UpdateCurrency(currencyId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void CustomView()
        {
            try
            {
                string moduleAPIName = "Leads";

                long customID = 34770616814003;

                List<string> names = new List<string>() { "Products", "Tasks", "Vendors", "Calls", "Leads", "Deals", "Campaigns", "Quotes", "Invoices", "Attachments", "Price_Books", "Sales_Orders", "Contacts", "Solutions", "Events", "Purchase_Orders", "Accounts", "Cases", "Notes" };

                foreach (string name in names)
                {
                    Com.Zoho.Crm.Sample.CustomView.CustomView.GetCustomViews(name);
                }

                Com.Zoho.Crm.Sample.CustomView.CustomView.GetCustomViews(moduleAPIName);

                Com.Zoho.Crm.Sample.CustomView.CustomView.GetCustomView(moduleAPIName, customID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void EmailTemplates()
        {
		    long id = 347706179;

            EmailTemplate.GetEmailTemplates("Deals");

            EmailTemplate.GetEmailTemplateById(id);
        }

        public static void FieldAttachments()
        {
		    string destinationFolder = "/Users/Documents/file";

            FieldAttachment.GetFieldAttachments("Leads", 3477061011613002, 3477061011613032, destinationFolder);
        }

        public static void Field()
        {
            try
            {
                string moduleAPIName = "Deals";

                long fieldId = 34770612587;

                List<string> names = new List<string>() { "Products", "Tasks", "Vendors", "Calls", "Leads", "Deals", "Campaigns", "Quotes", "Invoices", "Attachments", "Price_Books", "Sales_Orders", "Contacts", "Solutions", "Events", "Purchase_Orders", "Accounts", "Cases", "Notes" };

                foreach (string name in names)
                {
                    Com.Zoho.Crm.Sample.Fields.Fields.GetFields(name);
                }

                Com.Zoho.Crm.Sample.Fields.Fields.GetFields(moduleAPIName);

                Com.Zoho.Crm.Sample.Fields.Fields.GetField(moduleAPIName, fieldId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void File()
        {
            try
            {
                string destinationFolder = "/Users/Documents/file";

                string id = "ae9c7cefa418aec1d6a5cc2d9ab35c32688a7b37833ed87e83124aa570be041f";

                Com.Zoho.Crm.Sample.File.File.UploadFiles();

                Com.Zoho.Crm.Sample.File.File.GetFile(id, destinationFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void InventoryTemplates()
        {
            long id = 34770610174009;

            InventoryTemplate.GetInventoryTemplates("Quotes");

            InventoryTemplate.GetInventoryTemplateById(id);
        }

        public static void Layout()
        {
            try
            {
                string moduleAPIName = "Calls";

                long layoutId = 34770610091005;

                List<string> names = new List<string>() { "Products", "Tasks", "Vendors", "Calls", "Leads", "Deals", "Campaigns", "Quotes", "Invoices", "Attachments", "Price_Books", "Sales_Orders", "Contacts", "Solutions", "Events", "Purchase_Orders", "Accounts", "Cases", "Notes" };

                foreach (string name in names)
                {
                    Com.Zoho.Crm.Sample.Layouts.Layout.GetLayouts(name);
                }

                Com.Zoho.Crm.Sample.Layouts.Layout.GetLayouts(moduleAPIName);

                Com.Zoho.Crm.Sample.Layouts.Layout.GetLayout(moduleAPIName, layoutId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Module()
        {
            try
            {
                string moduleAPIName = "apiName1";

                long moduleId = 34770610485367;

                Com.Zoho.Crm.Sample.Modules.Modules.GetModules();

                Com.Zoho.Crm.Sample.Modules.Modules.GetModule(moduleAPIName);

                Com.Zoho.Crm.Sample.Modules.Modules.UpdateModuleByAPIName(moduleAPIName);

                Com.Zoho.Crm.Sample.Modules.Modules.UpdateModuleById(moduleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Note()
        {
            try
            {
                List<long> notesId = new List<long>() { 34770619295006, 34770619295007, 34770619295008, 3477061010016003, 3477061010016004 };

                long noteId = 34770619295005;

                Com.Zoho.Crm.Sample.Notes.Note.GetNotes();

                Com.Zoho.Crm.Sample.Notes.Note.CreateNotes();

                Com.Zoho.Crm.Sample.Notes.Note.UpdateNotes();

                Com.Zoho.Crm.Sample.Notes.Note.DeleteNotes(notesId);

                Com.Zoho.Crm.Sample.Notes.Note.GetNote(noteId);

                Com.Zoho.Crm.Sample.Notes.Note.UpdateNote(noteId);

                Com.Zoho.Crm.Sample.Notes.Note.DeleteNote(noteId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Notification()
        {
            try
            {
                List<long> channelIds = new List<long>() { 1006800212, 1006800211 };

                Com.Zoho.Crm.Sample.Notification.Notification.EnableNotifications();

                Com.Zoho.Crm.Sample.Notification.Notification.GetNotificationDetails();

                Com.Zoho.Crm.Sample.Notification.Notification.UpdateNotifications();

                Com.Zoho.Crm.Sample.Notification.Notification.UpdateNotification();

                Com.Zoho.Crm.Sample.Notification.Notification.DisableNotifications(channelIds);

                Com.Zoho.Crm.Sample.Notification.Notification.DisableNotification();
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Organization()
        {
            try
            {
                string absoluteFilePath = "/Users/Documents/file/download.png";

                Com.Zoho.Crm.Sample.Organization.Organization.GetOrganization();

                Com.Zoho.Crm.Sample.Organization.Organization.UploadOrganizationPhoto(absoluteFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Pipeline()
        {
            long layoutId = 34770610091023;

            PipeLine.CreatePipelines(layoutId);

            PipeLine.UpdatePipelines(layoutId);

            PipeLine.GetPipelines(layoutId);

            PipeLine.GetPipeline(layoutId, 3477061012387002);

            PipeLine.UpdatePipeline(layoutId, 3477061012387002);

            PipeLine.TransferAndDelete(layoutId);
        }

        public static void Profile()
        {
            try
            {
                long profileId = 34770610026011;

                Com.Zoho.Crm.Sample.Profile.Profile.GetProfiles();

                Com.Zoho.Crm.Sample.Profile.Profile.GetProfile(profileId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Query()
        {
            try
            {
                Com.Zoho.Crm.Sample.Query.Query.GetRecords();
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Record()
        {
            try
            {
                string moduleAPIName = "leads";

                long recordId = 3477061012421002;

                string externalFieldValue = "External1";

                string destinationFolder = "/Users/Documents/file/";

                List<string> modules = new List<string>() { "products", "Tasks", "Vendors", "Calls", "Leads", "Deals", "Campaigns", "Quotes", "Invoices", "Attachments", "Price_Books", "Sales_Orders", "Contacts", "Solutions", "Events", "Purchase_Orders", "Accounts", "Cases", "Notes" };

                foreach (string module in modules)
                {
                    Com.Zoho.Crm.Sample.Record.Record.GetRecords(module);
                }

                string absoluteFilePath = "/Users/Documents/file/download.png";

                List<string> recordIds = new List<string>() { "TestExternalLead111", "34770616595015", "34770615908001" };

                string jobId = "3477061010035003";

                Com.Zoho.Crm.Sample.Record.Record.GetRecord(moduleAPIName, recordId, destinationFolder);

                Com.Zoho.Crm.Sample.Record.Record.UpdateRecord(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.Record.Record.DeleteRecord(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.Record.Record.GetRecordUsingExternalId(moduleAPIName, externalFieldValue, destinationFolder);

                Com.Zoho.Crm.Sample.Record.Record.UpdateRecordUsingExternalId(moduleAPIName, externalFieldValue);

                Com.Zoho.Crm.Sample.Record.Record.DeleteRecordUsingExternalId(moduleAPIName, externalFieldValue);

                Com.Zoho.Crm.Sample.Record.Record.GetRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.CreateRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.UpdateRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.DeleteRecords(moduleAPIName, recordIds);

                Com.Zoho.Crm.Sample.Record.Record.UpsertRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.GetDeletedRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.SearchRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.ConvertLead(recordId);

                Com.Zoho.Crm.Sample.Record.Record.UploadPhoto(moduleAPIName, recordId, absoluteFilePath);

                Com.Zoho.Crm.Sample.Record.Record.GetPhoto(moduleAPIName, recordId, destinationFolder);

                Com.Zoho.Crm.Sample.Record.Record.DeletePhoto(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.Record.Record.MassUpdateRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.GetMassUpdateStatus(moduleAPIName, jobId);

                Com.Zoho.Crm.Sample.Record.Record.GetRecordCount();

                Com.Zoho.Crm.Sample.Record.Record.AssignTerritoriesToMultipleRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.AssignTerritoryToRecord(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.Record.Record.RemoveTerritoriesFromMultipleRecords(moduleAPIName);

                Com.Zoho.Crm.Sample.Record.Record.RemoveTerritoriesFromRecord(moduleAPIName, recordId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void RelatedList()
        {
            try
            {
                string moduleAPIName = "Leads";

                long relatedListId = 34770613771;

                List<string> names = new List<string>() { "Products", "Tasks", "Vendors", "Calls", "Leads", "Deals", "Campaigns", "Quotes", "Invoices", "Attachments", "Price_Books", "Sales_Orders", "Contacts", "Solutions", "Events", "Purchase_Orders", "Accounts", "Cases" };

                foreach (string name in names)
                {
                    Com.Zoho.Crm.Sample.RelatedList.RelatedList.GetRelatedLists(name);
                }

                Com.Zoho.Crm.Sample.RelatedList.RelatedList.GetRelatedLists(moduleAPIName);

                Com.Zoho.Crm.Sample.RelatedList.RelatedList.GetRelatedList(moduleAPIName, relatedListId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void RelatedRecords()
        {
            try
            {
                string moduleAPIName = "leads";//"products";

                long recordId = 34770617606020;

                string relatedListAPIName = "products";

                long relatedRecordId = 3477061010041001;

                string destinationFolder = "/Users/Desktop/field/";

                List<string> relatedListIds = new List<string>(){ "3477061012412001", "Products_External", "AutomatedSDKExternal" };

                string externalValue = "Value32422";//"Products_External";

                string externalFieldValue = "Products_External";

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.GetRelatedRecords(moduleAPIName, recordId, relatedListAPIName);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.UpdateRelatedRecords(moduleAPIName, recordId, relatedListAPIName);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.DelinkRecords(moduleAPIName, recordId, relatedListAPIName, relatedListIds);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.GetRelatedRecordsUsingExternalId(moduleAPIName, externalValue, relatedListAPIName);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.UpdateRelatedRecordsUsingExternalId(moduleAPIName, externalValue, relatedListAPIName);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.DeleteRelatedRecordsUsingExternalId(moduleAPIName, externalValue, relatedListAPIName, relatedListIds);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.GetRelatedRecord(moduleAPIName, recordId, relatedListAPIName, relatedRecordId, destinationFolder);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.UpdateRelatedRecord(moduleAPIName, recordId, relatedListAPIName, relatedRecordId);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.DelinkRecord(moduleAPIName, recordId, relatedListAPIName, relatedRecordId);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.GetRelatedRecordUsingExternalId(moduleAPIName, externalValue, relatedListAPIName, externalFieldValue, destinationFolder);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.UpdateRelatedRecordUsingExternalId(moduleAPIName, externalValue, relatedListAPIName, externalFieldValue);

                Com.Zoho.Crm.Sample.RelatedRecords.RelatedRecords.DeleteRelatedRecordUsingExternalId(moduleAPIName, externalValue, relatedListAPIName, externalFieldValue);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Role()
        {
            try
            {
                long roleId = 34770610026008;

                Com.Zoho.Crm.Sample.Role.Role.GetRoles();

                Com.Zoho.Crm.Sample.Role.Role.GetRole(roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void SendMail()
        {
            Com.Zoho.Crm.Sample.SendMail.SendMail.GetEmailAddresses();

            Com.Zoho.Crm.Sample.SendMail.SendMail.SendMailMethod(35240335495063, "Leads");
        }

        public static void ShareRecords()
        {
            try
            {
                string moduleAPIName = "Leads";

                long recordId = 34770615623115L;

                Com.Zoho.Crm.Sample.ShareRecords.ShareRecords.GetSharedRecordDetails(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.ShareRecords.ShareRecords.ShareRecord(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.ShareRecords.ShareRecords.UpdateSharePermissions(moduleAPIName, recordId);

                Com.Zoho.Crm.Sample.ShareRecords.ShareRecords.RevokeSharedRecord(moduleAPIName, recordId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Tags()
        {
            try
            {
                string moduleAPIName = "Leads";

                long tagId = 34770619102018;

                long recordId = 34770615623115;

                List<string> tagNames = new List<string>() { "addtag1,addtag12" };

                List<long> recordIds = new List<long>() { 34770615623115, 34770619102018 };

                string conflictId = "34770619315001";

                Com.Zoho.Crm.Sample.Tags.Tag.GetTags(moduleAPIName);

                Com.Zoho.Crm.Sample.Tags.Tag.CreateTags(moduleAPIName);

                Com.Zoho.Crm.Sample.Tags.Tag.UpdateTags(moduleAPIName);

                Com.Zoho.Crm.Sample.Tags.Tag.UpdateTag(moduleAPIName, tagId);

                Com.Zoho.Crm.Sample.Tags.Tag.DeleteTag(tagId);

                Com.Zoho.Crm.Sample.Tags.Tag.MergeTags(tagId, conflictId);

                Com.Zoho.Crm.Sample.Tags.Tag.AddTagsToRecord(moduleAPIName, recordId, tagNames);

                Com.Zoho.Crm.Sample.Tags.Tag.RemoveTagsFromRecord(moduleAPIName, recordId, tagNames);

                Com.Zoho.Crm.Sample.Tags.Tag.AddTagsToMultipleRecords(moduleAPIName, recordIds, tagNames);

                Com.Zoho.Crm.Sample.Tags.Tag.RemoveTagsFromMultipleRecords(moduleAPIName, recordIds, tagNames);

                Com.Zoho.Crm.Sample.Tags.Tag.GetRecordCountForTag(moduleAPIName, tagId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Tax()
        {
            try
            {
                long taxId = 3477061010962013;

                List<long> taxIds = new List<long>() { 3477061012286006, 3477061011392, 3477061012455, 3477061012456 };

                Com.Zoho.Crm.Sample.Taxes.Tax.GetTaxes();

                Com.Zoho.Crm.Sample.Taxes.Tax.CreateTaxes();

                Com.Zoho.Crm.Sample.Taxes.Tax.UpdateTaxes();

                Com.Zoho.Crm.Sample.Taxes.Tax.DeleteTaxes(taxIds);

                Com.Zoho.Crm.Sample.Taxes.Tax.GetTax(taxId);

                Com.Zoho.Crm.Sample.Taxes.Tax.DeleteTax(taxId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Territory()
        {
            try
            {
                long territoryId = 34770613051397;

                Com.Zoho.Crm.Sample.Territories.Territory.GetTerritories();

                Com.Zoho.Crm.Sample.Territories.Territory.GetTerritory(territoryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }
        
        public static void User()
        {
            try
            {
                long userId = 3477061012449003;

                Com.Zoho.Crm.Sample.Users.User.GetUsers();

                Com.Zoho.Crm.Sample.Users.User.CreateUser();

                Com.Zoho.Crm.Sample.Users.User.UpdateUsers();

                Com.Zoho.Crm.Sample.Users.User.GetUser(userId);

                Com.Zoho.Crm.Sample.Users.User.UpdateUser(userId);

                Com.Zoho.Crm.Sample.Users.User.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void VariableGroup()
        {
            try
            {
                string variableGroupName = "General";

                long variableGroupId = 34770613089001;
                
                Com.Zoho.Crm.Sample.VariableGroups.VariableGroup.GetVariableGroups();

                Com.Zoho.Crm.Sample.VariableGroups.VariableGroup.GetVariableGroupById(variableGroupId);

                Com.Zoho.Crm.Sample.VariableGroups.VariableGroup.GetVariableGroupByAPIName(variableGroupName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Variable()
        {
            try
            {
                List<long> variableIds = new List<long>() { 3477061012192001, 34770617444003 };

                long variableId = 3477061010964014;

                string variableName = "Variable5521";

                Com.Zoho.Crm.Sample.Variables.Variable.GetVariables();

                Com.Zoho.Crm.Sample.Variables.Variable.CreateVariables();

                Com.Zoho.Crm.Sample.Variables.Variable.UpdateVariables();

                Com.Zoho.Crm.Sample.Variables.Variable.DeleteVariables(variableIds);

                Com.Zoho.Crm.Sample.Variables.Variable.GetVariableById(variableId);

                Com.Zoho.Crm.Sample.Variables.Variable.UpdateVariableById(variableId);

                Com.Zoho.Crm.Sample.Variables.Variable.DeleteVariable(variableId);

                Com.Zoho.Crm.Sample.Variables.Variable.GetVariableForAPIName(variableName);

                Com.Zoho.Crm.Sample.Variables.Variable.UpdateVariableByAPIName(variableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void Wizards()
        {
		    long wizardId = 34770619497009;

            string layoutId = "34770610091055";

            Com.Zoho.Crm.Sample.Wizards.Wizard.GetWizards();

            Com.Zoho.Crm.Sample.Wizards.Wizard.GetWizardById(wizardId, layoutId);
        }

        public static void Threading()
        {
            try
            {
                Com.Zoho.Crm.Sample.Threading.MultiUser.MultiThread.RunMultiThreadWithMultiUser();

                Com.Zoho.Crm.Sample.Threading.MultiUser.SingleThread.RunSingleThreadWithMultiUser();

                Com.Zoho.Crm.Sample.Threading.SingleUser.MultiThread.RunMultiThreadWithSingleUser();

                Com.Zoho.Crm.Sample.Threading.SingleUser.SingleThread.RunSingleThreadWithSingleUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public static void TestUpload()
        {
            string boundary = "----FILEBOUNDARY----";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.zohoapis.com/crm/v2/files");

            request.ContentType = "multipart/form-data; boundary=" + boundary;

            request.Method = "POST";

            request.Headers["Authorization"] = "Zoho-oauthtoken 1.xxxx.xxxx";

            request.KeepAlive = true;

            Stream memStream = new System.IO.MemoryStream();

            var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");

            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: application/octet-stream\r\n\r\n";

            StreamWrapper streamWrapper = new StreamWrapper("/Users/Desktop/test.html");

            StreamWrapper streamWrapper1 = new StreamWrapper("/Users/Desktop/test.html");

            List<StreamWrapper> streamWrapperList = new List<StreamWrapper>() { streamWrapper, streamWrapper1 };

            for (int i = 0; i < streamWrapperList.Count; i++)
            {
                StreamWrapper streamWrapperInstance = streamWrapperList[i];

                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                var header = string.Format(headerTemplate, "file", streamWrapperInstance.Name);

                var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);

                var buffer = new byte[1024];

                var bytesRead = 0;

                while ((bytesRead = streamWrapperInstance.Stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
            }

            memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);

            request.ContentLength = memStream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                memStream.Position = 0;

                byte[] tempBuffer = new byte[memStream.Length];

                memStream.Read(tempBuffer, 0, tempBuffer.Length);

                memStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Response == null) { throw; }

                response = (HttpWebResponse)e.Response;
            }

            HttpWebResponse responseEntity = response;

            string responsestring = new StreamReader(responseEntity.GetResponseStream()).ReadToEnd();

            responseEntity.Close();

            Console.WriteLine(responsestring);
        }
    }
}