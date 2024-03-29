﻿using System;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using System.Reflection;

using Com.Zoho.Crm.API;

using Com.Zoho.Crm.API.Attachments;

using Com.Zoho.Crm.API.Layouts;

using Com.Zoho.Crm.API.Record;

using Com.Zoho.Crm.API.Tags;

using Com.Zoho.Crm.API.Users;

using Com.Zoho.Crm.API.Util;

using Newtonsoft.Json;

using static Com.Zoho.Crm.API.Record.RecordOperations;

using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;

using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;

using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;

using APIException = Com.Zoho.Crm.API.Record.APIException;

using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;

using CountHandler = Com.Zoho.Crm.API.Record.CountHandler;

using CountWrapper = Com.Zoho.Crm.API.Record.CountWrapper;

using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;

using Info = Com.Zoho.Crm.API.Record.Info;

using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;

using ResponseWrapper = Com.Zoho.Crm.API.Record.ResponseWrapper;

using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;

namespace Com.Zoho.Crm.Sample.Record
{
    public class Record
    {
        /// <summary>
        /// This method is used to get a single record of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module.</param>
        /// <param name="recordId">The ID of the record to be obtained.</param>
        /// <param name="destinationFolder">The absolute path of the destination folder to store the file</param>
        public static void GetRecord(string moduleAPIName, long recordId, string destinationFolder)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //long recordId = 347276;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetRecordParam.APPROVED, "false");

            paramInstance.Add(GetRecordParam.CONVERTED, "false");

            List<string> fieldNames = new List<string>() { "Company", "Email" };

            foreach (string fieldName in fieldNames)
            {
                paramInstance.Add(GetRecordParam.FIELDS, fieldName);
            }

            DateTimeOffset startdatetime = new DateTimeOffset(new DateTime(2020, 10, 15, 12, 0, 1, DateTimeKind.Local));

            paramInstance.Add(GetRecordParam.STARTDATETIME, startdatetime);

            DateTimeOffset enddatetime = new DateTimeOffset(new DateTime(2020, 11, 15, 12, 0, 1, DateTimeKind.Local));

            paramInstance.Add(GetRecordParam.ENDDATETIME, enddatetime);

            paramInstance.Add(GetRecordParam.TERRITORY_ID, "34770613051357");

            paramInstance.Add(GetRecordParam.INCLUDE_CHILD, "true");

            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetRecordHeader.IF_MODIFIED_SINCE, ifmodifiedsince);

            headerInstance.Add(GetRecordHeader.X_EXTERNAL, "Leads.External");

            //Call GetRecord method that takes recordID, moduleAPIName, paramInstance, and headerInstance  as parameter
            APIResponse<ResponseHandler> response = recordOperations.GetRecord(recordId, moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the list of obtained Record instances
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("Record ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("Record Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("Record Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if (tags != null)
                            {
                                foreach (Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                }
                            }

                            //To Get particular field value
                            Console.WriteLine("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("Record KeyValues: ");

                            //Get the KeyValue map
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                string keyName = entry.Key;

                                object value = entry.Value;

                                if (value != null)
                                {
                                    if (value is IList && ((IList)value).Count > 0)
                                    {
                                        IList dataList = (IList)value;

                                        if (dataList.Count > 0)
                                        {
                                            if (dataList[0] is FileDetails)
                                            {
                                                List<FileDetails> fileDetails = (List<FileDetails>)value;

                                                foreach (FileDetails fileDetail in fileDetails)
                                                {
                                                    //Get the Extn of each FileDetails
                                                    Console.WriteLine("Record FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("Record FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("Record FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("Record FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("Record FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("Record FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("Record FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("Record FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0].GetType().FullName.Contains("Choice"))
                                            {
                                                Console.WriteLine(keyName);

                                                Console.WriteLine("values");

                                                foreach (object choice in dataList)
                                                {
                                                    Type type = choice.GetType();

                                                    PropertyInfo prop = type.GetProperty("Value");

                                                    Console.WriteLine(prop.GetValue(choice));
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("Record PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("Record PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("Record PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("Record PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("Record Participants Name: " + participant.Name);

                                                    Console.WriteLine("Record Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("Record Participants ID: " + participant.Id);

                                                    Console.WriteLine("Record Participants Type: " + participant.Type);

                                                    Console.WriteLine("Record Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("Record Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {

                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("Record ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("Record ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("Record ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("Record ProductDetails LineTax Value: " + lineTax.Value);

                                                    Console.WriteLine("Record ProductDetails LineTax DisplayName: " + lineTax.DisplayName);
                                                }
                                            }
                                            else if (dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>)dataList;

                                                foreach (Comment comment in comments)
                                                {
                                                    Console.WriteLine("Record Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("Record Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("Record Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("Record Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if (dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>)dataList; ;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if (owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("Record Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("Record Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("Record Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("Record Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if (parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("Record Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("Record Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("Record Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("Record Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("Record Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("Record Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if (modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("Record Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("Record Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if (createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("Record Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>)dataList;

                                                foreach (ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("Record " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("Record " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("Record " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("Record " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("Record " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("Record " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("Record " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("Record " + keyName + "FileId: " + imageUpload.FileId);
                                                }
                                            }
                                            else if (dataList[0] is Com.Zoho.Crm.API.Record.Record)
                                            {
                                                List<Com.Zoho.Crm.API.Record.Record> recordList = (List<Com.Zoho.Crm.API.Record.Record>)dataList;

                                                foreach (Com.Zoho.Crm.API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + JsonConvert.SerializeObject(entry1.Value));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                            }
                                        }
                                    }
                                    else if (value is Consent)
                                    {
                                        Consent consent = (Consent)value;

                                        //Get the Owner User instance of each attachment
                                        User owner = consent.Owner;

                                        //Check if owner is not null
                                        if (owner != null)
                                        {
                                            //Get the name of the owner User
                                            Console.WriteLine("Record Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("Record Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("Record Consent Owner Email: " + owner.Email);
                                        }

                                        User consentCreatedBy = consent.CreatedBy;

                                        //Check if createdBy is not null
                                        if (consentCreatedBy != null)
                                        {
                                            //Get the name of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Name: " + consentCreatedBy.Name);

                                            //Get the ID of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy ID: " + consentCreatedBy.Id);

                                            //Get the Email of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Email: " + consentCreatedBy.Email);
                                        }

                                        User consentModifiedBy = consent.ModifiedBy;

                                        //Check if createdBy is not null
                                        if (consentModifiedBy != null)
                                        {
                                            //Get the name of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Name: " + consentModifiedBy.Name);

                                            //Get the ID of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy ID: " + consentModifiedBy.Id);

                                            //Get the Email of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Email: " + consentModifiedBy.Email);
                                        }

                                        Console.WriteLine("Record Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("Record Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("Record Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("Record Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("Record Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("Record Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("Record Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("Record Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("Record Consent DataProcessingBasis: " + consent.DataProcessingBasis);

                                        //To get custom values
                                        Console.WriteLine("Record Consent Lawful Reason: " + consent.GetKeyValue("Lawful_Reason"));
                                    }
                                    else if (value is Layout)
                                    {
                                        API.Layouts.Layout layout = (Layout)value;

                                        if (layout != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("Record " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("Record " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("Record " + keyName + " User-Email: " + user.Email);
                                        }
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine(keyName + ": " + prop.GetValue(value));
                                    }
                                    else if (value is RemindAt)
                                    {
                                        Console.WriteLine(keyName + ": " + ((RemindAt)value).Alarm);
                                    }
                                    else if (value is RecurringActivity)
                                    {
                                        Console.WriteLine(keyName);

                                        Console.WriteLine("RRULE" + ": " + ((RecurringActivity)value).Rrule);
                                    }
                                    else if (value is Com.Zoho.Crm.API.Record.Record)
                                    {
                                        Com.Zoho.Crm.API.Record.Record recordValue = (Com.Zoho.Crm.API.Record.Record)value;

                                        Console.WriteLine("Record " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("Record " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }
                    }
                    else if (responseHandler is API.Record.FileBodyWrapper)
                    {
                        //Get the received FileBodyWrapper instance
                        API.Record.FileBodyWrapper fileBodyWrapper = (API.Record.FileBodyWrapper)responseHandler;

                        //Get StreamWrapper instance from the returned FileBodyWrapper instance
                        StreamWrapper streamWrapper = fileBodyWrapper.File;

                        //Get Stream from the response
                        Stream file = streamWrapper.Stream;

                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);

                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to update a single record of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module.</param>
        /// <param name="recordId">The ID of the record to be updated.</param>
        public static void UpdateRecord(string moduleAPIName, long recordId)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //long recordId = 34777002;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record1.AddFieldValue(Leads.CITY, "City");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.COMPANY, "KKRNP");

            record1.AddKeyValue("Multi_Select_1", new List<Choice<string>>() { new Choice<string>("Test1"), new Choice<string>("Test2") });

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record1.AddKeyValue("Custom_field", "Value");

            record1.AddKeyValue("Custom_field_2", "value");

            record1.AddKeyValue("Date_Time_2", new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local)));

            record1.AddKeyValue("Date_1", new DateTime(2021, 1, 13).Date);

            List<FileDetails> fileDetails = new List<FileDetails>();

            FileDetails fileDetail1 = new FileDetails();

            fileDetail1.AttachmentId = "34770616072005";

            fileDetail1.Delete = null;

            fileDetails.Add(fileDetail1);

            FileDetails fileDetail2 = new FileDetails();

            fileDetail2.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c32ddd96f13c58ed3c8c701a7d9d1263077";

            fileDetails.Add(fileDetail2);

            FileDetails fileDetail3 = new FileDetails();

            fileDetail3.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c32155dbc7f8d3cd63c8548191c02435899";

            fileDetails.Add(fileDetail3);

            record1.AddKeyValue("File_Upload", fileDetails);

            API.Users.User recordOwner = new API.Users.User();

            recordOwner.Email = "abc@zoho.com";

            record1.AddKeyValue("Owner", recordOwner);

            //Used when GDPR is enabled
            Consent dataConsent = new Consent();

            dataConsent.ConsentRemarks = "Approved.";

            dataConsent.ConsentThrough = "Email";

            dataConsent.ContactThroughEmail = true;

            dataConsent.ContactThroughSocial = false;

            record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);

            List<API.Record.Record> subformList = new List<API.Record.Record>();

            API.Record.Record subform = new API.Record.Record();

            subform.AddKeyValue("Subform FieldAPIName", "FieldValue");

            subformList.Add(subform);

            record1.AddKeyValue("Subform_1", subformList);

            /** Following methods are being used only by Inventory modules */

            API.Record.Record dealName = new API.Record.Record();

            dealName.AddFieldValue(Deals.ID, 3477061012263008);

            record1.AddFieldValue(Sales_Orders.DEAL_NAME, dealName);

            API.Record.Record contactName = new API.Record.Record();

            contactName.AddFieldValue(Contacts.ID, 3477061012263005);

            record1.AddFieldValue(Purchase_Orders.CONTACT_NAME, contactName);

            API.Record.Record accountName = new API.Record.Record();

            accountName.AddKeyValue("name", "automatedAccount");

            record1.AddFieldValue(Quotes.ACCOUNT_NAME, accountName);

            List<API.Record.Record> inventoryLineItemList = new List<API.Record.Record>();

            API.Record.Record inventoryLineItem = new API.Record.Record();

            LineItemProduct lineItemProduct = new LineItemProduct();

            lineItemProduct.Id = 3477061012259001;

            //lineItemProduct.AddKeyValue("Products_External", "AutomatedSDKExternal");

            inventoryLineItem.AddKeyValue("Description", "asd");

            inventoryLineItem.AddKeyValue("Discount", "5");

            //API.Record.Record parentId = new API.Record.Record();

            //parentId.Id = 35240337331017;

            //inventoryLineItem.AddKeyValue("Parent_Id", 5);

            inventoryLineItem.AddKeyValue("Sequence_Number", 1);

            LineItemProduct lineitemProduct = new LineItemProduct();

            lineitemProduct.Id = 3477061012261;

            //lineitemProduct.AddKeyValue("Products_External", "ProductExternal");

            inventoryLineItem.AddKeyValue("Product_Name", lineItemProduct);

            inventoryLineItem.AddKeyValue("Sequence_Number", (long)1);

            inventoryLineItem.AddKeyValue("Quantity", 123.2);

            inventoryLineItem.AddKeyValue("Tax", 123.2);

            inventoryLineItemList.Add(inventoryLineItem);

            List<LineTax> productLineTaxes = new List<LineTax>();

            LineTax productLineTax = new LineTax();

            productLineTax.Name = "MyTax1134";

            productLineTax.Percentage = 20.0;

            productLineTaxes.Add(productLineTax);

            inventoryLineItem.AddKeyValue("Line_Tax", productLineTaxes);

            inventoryLineItemList.Add(inventoryLineItem);

            record1.AddKeyValue("Quoted_Items", inventoryLineItemList);

            List<LineTax> lineTaxes = new List<LineTax>();

            LineTax lineTax = new LineTax();

            lineTax.Name = "MyTax1134";

            lineTax.Percentage = 20.0;

            lineTaxes.Add(lineTax);

            record1.AddKeyValue("$line_tax", lineTaxes);

            /** End Inventory **/

            List<Tag> tagList = new List<Tag>();

            Tag tag = new Tag();

            tag.Name = "Testtask1";

            tagList.Add(tag);

            record1.Tag = tagList;

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            List<string> trigger = new List<string>();

            trigger.Add("approval");

            trigger.Add("workflow");

            trigger.Add("blueprint");

            request.Trigger = trigger;

            HeaderMap headerInstance = new HeaderMap();

            //headerInstance.Add(UpdateRecordHeader.X_EXTERNAL, "Quotes.Quoted_Items.Product_Name.Products_External");

            //Call UpdateRecord method that takes recordId, ModuleAPIName, BodyWrapper instance and headerInstance as parameter.
            APIResponse<ActionHandler> response = recordOperations.UpdateRecord(recordId, moduleAPIName, request, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    if (entry.Value is User)
                                    {
                                        User user = (User)entry.Value;

                                        //Get the Code
                                        Console.WriteLine("User Id: " + user.Id);

                                        //Get the Code
                                        Console.WriteLine("Use Name: " + user.Name);
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                    }
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to delete a single record of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module.</param>
        /// <param name="recordId">The ID of the record to be deleted.</param>
        public static void DeleteRecord(string moduleAPIName, long recordId)
        {
            //API Name of the module to delete record
            //string moduleAPIName = "module_api_name";
            //long recordId = 34702;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(DeleteRecordParam.WF_TRIGGER, false);

            HeaderMap headerInstance = new HeaderMap();

            //headerInstance.Add(DeleteRecordHeader.X_EXTERNAL, "Leads.External");

            //Call deleteRecord method that takes recordId, ModuleAPIName, paramInstance and headerInstance as parameter.
            APIResponse<ActionHandler> response = recordOperations.DeleteRecord(recordId, moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get a single record of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module.</param>
        /// <param name="externalFieldValue"></param>
        /// <param name="destinationFolder">The absolute path of the destination folder to store the file</param>
        public static void GetRecordUsingExternalId(string moduleAPIName, string externalFieldValue, string destinationFolder)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //string externalFieldValue = "347276";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetRecordParam.APPROVED, "false");

            paramInstance.Add(GetRecordParam.CONVERTED, "false");

            List<string> fieldNames = new List<string>() { "Company", "Email" };

            foreach (string fieldName in fieldNames)
            {
               paramInstance.Add(GetRecordParam.FIELDS, fieldName);
            }

            DateTimeOffset startdatetime = new DateTimeOffset(new DateTime(2020, 10, 15, 12, 0, 1, DateTimeKind.Local));

            paramInstance.Add(GetRecordParam.STARTDATETIME, startdatetime);

            DateTimeOffset enddatetime = new DateTimeOffset(new DateTime(2020, 11, 15, 12, 0, 1, DateTimeKind.Local));

            paramInstance.Add(GetRecordParam.ENDDATETIME, enddatetime);

            paramInstance.Add(GetRecordParam.TERRITORY_ID, "34770613051357");

            paramInstance.Add(GetRecordParam.INCLUDE_CHILD, "true");

            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetRecordHeader.IF_MODIFIED_SINCE, ifmodifiedsince);

            headerInstance.Add(GetRecordHeader.X_EXTERNAL, "Leads.External");

            //Call GetRecordUsingExternalId method that takes externalFieldValue, moduleAPIName, paramInstance, and headerInstance  as parameter
            APIResponse<ResponseHandler> response = recordOperations.GetRecordUsingExternalId(externalFieldValue, moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the list of obtained Record instances
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("Record ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("Record Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("Record Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if (tags != null)
                            {
                                foreach (Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                }
                            }

                            //To Get particular field value
                            Console.WriteLine("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("Record KeyValues: ");

                            //Get the KeyValue map
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                string keyName = entry.Key;

                                object value = entry.Value;

                                if (value != null)
                                {
                                    if (value is IList && ((IList)value).Count > 0)
                                    {
                                        IList dataList = (IList)value;

                                        if (dataList.Count > 0)
                                        {
                                            if (dataList[0] is FileDetails)
                                            {
                                                List<FileDetails> fileDetails = (List<FileDetails>)value;

                                                foreach (FileDetails fileDetail in fileDetails)
                                                {
                                                    //Get the Extn of each FileDetails
                                                    Console.WriteLine("Record FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("Record FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("Record FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("Record FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("Record FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("Record FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("Record FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("Record FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0].GetType().FullName.Contains("Choice"))
                                            {
                                                Console.WriteLine(keyName);

                                                Console.WriteLine("values");

                                                foreach (object choice in dataList)
                                                {
                                                    Type type = choice.GetType();

                                                    PropertyInfo prop = type.GetProperty("Value");

                                                    Console.WriteLine(prop.GetValue(choice));
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("Record PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("Record PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("Record PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("Record PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("Record Participants Name: " + participant.Name);

                                                    Console.WriteLine("Record Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("Record Participants ID: " + participant.Id);

                                                    Console.WriteLine("Record Participants Type: " + participant.Type);

                                                    Console.WriteLine("Record Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("Record Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {

                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("Record ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("Record ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("Record ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("Record ProductDetails LineTax Value: " + lineTax.Value);

                                                    Console.WriteLine("Record ProductDetails LineTax DisplayName: " + lineTax.DisplayName);
                                                }
                                            }
                                            else if (dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>)dataList;

                                                foreach (Comment comment in comments)
                                                {
                                                    Console.WriteLine("Record Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("Record Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("Record Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("Record Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if (dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>)dataList; ;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if (owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("Record Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("Record Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("Record Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("Record Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if (parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("Record Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("Record Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("Record Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("Record Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("Record Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("Record Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if (modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("Record Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("Record Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if (createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("Record Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>)dataList;

                                                foreach (ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("Record " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("Record " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("Record " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("Record " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("Record " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("Record " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("Record " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("Record " + keyName + "FileId: " + imageUpload.FileId);
                                                }
                                            }
                                            else if (dataList[0] is Com.Zoho.Crm.API.Record.Record)
                                            {
                                                List<Com.Zoho.Crm.API.Record.Record> recordList = (List<Com.Zoho.Crm.API.Record.Record>)dataList;

                                                foreach (Com.Zoho.Crm.API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + JsonConvert.SerializeObject(entry1.Value));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                            }
                                        }
                                    }
                                    else if (value is Consent)
                                    {
                                        Consent consent = (Consent)value;

                                        //Get the Owner User instance of each attachment
                                        User owner = consent.Owner;

                                        //Check if owner is not null
                                        if (owner != null)
                                        {
                                            //Get the name of the owner User
                                            Console.WriteLine("Record Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("Record Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("Record Consent Owner Email: " + owner.Email);
                                        }

                                        User consentCreatedBy = consent.CreatedBy;

                                        //Check if createdBy is not null
                                        if (consentCreatedBy != null)
                                        {
                                            //Get the name of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Name: " + consentCreatedBy.Name);

                                            //Get the ID of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy ID: " + consentCreatedBy.Id);

                                            //Get the Email of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Email: " + consentCreatedBy.Email);
                                        }

                                        User consentModifiedBy = consent.ModifiedBy;

                                        //Check if createdBy is not null
                                        if (consentModifiedBy != null)
                                        {
                                            //Get the name of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Name: " + consentModifiedBy.Name);

                                            //Get the ID of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy ID: " + consentModifiedBy.Id);

                                            //Get the Email of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Email: " + consentModifiedBy.Email);
                                        }

                                        Console.WriteLine("Record Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("Record Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("Record Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("Record Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("Record Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("Record Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("Record Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("Record Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("Record Consent DataProcessingBasis: " + consent.DataProcessingBasis);

                                        //To get custom values
                                        Console.WriteLine("Record Consent Lawful Reason: " + consent.GetKeyValue("Lawful_Reason"));
                                    }
                                    else if (value is Layout)
                                    {
                                        API.Layouts.Layout layout = (Layout)value;

                                        if (layout != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("Record " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("Record " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("Record " + keyName + " User-Email: " + user.Email);
                                        }
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine(keyName + ": " + prop.GetValue(value));
                                    }
                                    else if (value is RemindAt)
                                    {
                                        Console.WriteLine(keyName + ": " + ((RemindAt)value).Alarm);
                                    }
                                    else if (value is RecurringActivity)
                                    {
                                        Console.WriteLine(keyName);

                                        Console.WriteLine("RRULE" + ": " + ((RecurringActivity)value).Rrule);
                                    }
                                    else if (value is Com.Zoho.Crm.API.Record.Record)
                                    {
                                        Com.Zoho.Crm.API.Record.Record recordValue = (Com.Zoho.Crm.API.Record.Record)value;

                                        Console.WriteLine("Record " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("Record " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }
                    }
                    else if (responseHandler is API.Record.FileBodyWrapper)
                    {
                        //Get the received FileBodyWrapper instance
                        API.Record.FileBodyWrapper fileBodyWrapper = (API.Record.FileBodyWrapper)responseHandler;

                        //Get StreamWrapper instance from the returned FileBodyWrapper instance
                        StreamWrapper streamWrapper = fileBodyWrapper.File;

                        //Get Stream from the response
                        Stream file = streamWrapper.Stream;

                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);

                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to update a single record of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module.</param>
        /// <param name="externalFieldValue"></param>
        public static void UpdateRecordUsingExternalId(string moduleAPIName, string externalFieldValue)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //string externalFieldValue = "";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record1.AddFieldValue(Leads.CITY, "City");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.COMPANY, "KKRNP");

            record1.AddKeyValue("Multi_Select_1", new List<Choice<string>>() { new Choice<string>("Test1"), new Choice<string>("Test2") });

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record1.AddKeyValue("Custom_field", "Value");

            record1.AddKeyValue("Custom_field_2", "value");

            record1.AddKeyValue("Date_Time_2", new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local)));

            record1.AddKeyValue("Date_1", new DateTime(2021, 1, 13).Date);

            List<FileDetails> fileDetails = new List<FileDetails>();

            FileDetails fileDetail1 = new FileDetails();

            fileDetail1.AttachmentId = "34770616072005";

            fileDetail1.Delete = null;

            fileDetails.Add(fileDetail1);

            FileDetails fileDetail2 = new FileDetails();

            fileDetail2.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c325562658d8174404e150dab5b61d7e41b";

            fileDetails.Add(fileDetail2);

            FileDetails fileDetail3 = new FileDetails();

            fileDetail3.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c3211802ae6de725e23c780daf1a00f20a7";

            fileDetails.Add(fileDetail3);

            record1.AddKeyValue("File_Upload", fileDetails);

            API.Users.User recordOwner = new API.Users.User();

            recordOwner.Email = "abc@zoho.com";

            record1.AddKeyValue("Owner", recordOwner);

            //Used when GDPR is enabled
            Consent dataConsent = new Consent();

            dataConsent.ConsentRemarks = "Approved.";

            dataConsent.ConsentThrough = "Email";

            dataConsent.ContactThroughEmail = true;

            dataConsent.ContactThroughSocial = false;

            record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);

            List<API.Record.Record> subformList = new List<API.Record.Record>();

            API.Record.Record subform = new API.Record.Record();

            subform.AddKeyValue("Subform FieldAPIName", "FieldValue");

            subformList.Add(subform);

            record1.AddKeyValue("Subform Name", subformList);

            /** Following methods are being used only by Inventory modules */

            API.Record.Record dealName = new API.Record.Record();

            dealName.AddFieldValue(Deals.ID, 34770614995070);

            record1.AddFieldValue(Sales_Orders.DEAL_NAME, dealName);

            API.Record.Record contactName = new API.Record.Record();

            contactName.AddFieldValue(Contacts.ID, 34770614977055);

            record1.AddFieldValue(Purchase_Orders.CONTACT_NAME, contactName);

            API.Record.Record accountName = new API.Record.Record();

            accountName.AddKeyValue("name", "automatedAccount");

            record1.AddFieldValue(Quotes.ACCOUNT_NAME, accountName);

            List<API.Record.Record> inventoryLineItemList = new List<API.Record.Record>();

            API.Record.Record inventoryLineItem = new API.Record.Record();

            LineItemProduct lineItemProduct = new LineItemProduct();

            lineItemProduct.Id = 34770615356009;

            inventoryLineItem.AddKeyValue("Description", "asd");

            inventoryLineItem.AddKeyValue("Discount", "5");

            API.Record.Record parentId = new API.Record.Record();

            parentId.Id = 35240337331017;

            //inventoryLineItem.AddKeyValue("Parent_Id", 5);

            inventoryLineItem.AddKeyValue("Sequence_Number", 1);

            LineItemProduct lineitemProduct = new LineItemProduct();

            lineitemProduct.Id = 35240333659082;

            inventoryLineItem.AddKeyValue("Product_Name", lineItemProduct);

            inventoryLineItem.AddKeyValue("Sequence_Number", 1);

            inventoryLineItem.AddKeyValue("Quantity", 123.2);

            inventoryLineItem.AddKeyValue("Tax", 123.2);

            inventoryLineItemList.Add(inventoryLineItem);

            List<LineTax> productLineTaxes = new List<LineTax>();

            LineTax productLineTax = new LineTax();

            productLineTax.Name = "Sales Tax";

            productLineTax.Percentage = 20.0;

            productLineTaxes.Add(productLineTax);

            inventoryLineItem.AddKeyValue("line_tax", productLineTaxes);

            inventoryLineItemList.Add(inventoryLineItem);

            record1.AddKeyValue("Quoted_Items", inventoryLineItemList);

            List<LineTax> lineTaxes = new List<LineTax>();

            LineTax lineTax = new LineTax();

            lineTax.Name = "Sales Tax";

            lineTax.Percentage = 20.0;

            lineTaxes.Add(lineTax);

            record1.AddKeyValue("$line_tax", lineTaxes);

            /** End Inventory **/

            List<Tag> tagList = new List<Tag>();

            Tag tag = new Tag();

            tag.Name = "Testtask1";

            tagList.Add(tag);

            record1.Tag = tagList;

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            List<string> trigger = new List<string>();

            trigger.Add("approval");

            trigger.Add("workflow");

            trigger.Add("blueprint");

            request.Trigger = trigger;

            HeaderMap headerInstance = new HeaderMap();

            headerInstance.Add(UpdateRecordHeader.X_EXTERNAL, "Leads.External");

            //Call UpdateRecordUsingExternalId method that takes externalFieldValue, ModuleAPIName, BodyWrapper instance and headerInstance as parameter.
            APIResponse<ActionHandler> response = recordOperations.UpdateRecordUsingExternalId(externalFieldValue, moduleAPIName, request, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    if (entry.Value is User)
                                    {
                                        User user = (User)entry.Value;

                                        //Get the Code
                                        Console.WriteLine("User Id: " + user.Id);

                                        //Get the Code
                                        Console.WriteLine("Use Name: " + user.Name);
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                    }
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to delete a single record of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module.</param>
        /// <param name="externalFieldValue"></param>
        public static void DeleteRecordUsingExternalId(string moduleAPIName, string externalFieldValue)
        {
            //API Name of the module to delete record
            //string moduleAPIName = "Leads";
            //string externalFieldValue = "";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(DeleteRecordParam.WF_TRIGGER, false);

            HeaderMap headerInstance = new HeaderMap();

            headerInstance.Add(DeleteRecordHeader.X_EXTERNAL, "Leads.External");

            //Call DeleteRecordUsingExternalId method that takes recordId, ModuleAPIName, paramInstance and headerInstance as parameter.
            APIResponse<ActionHandler> response = recordOperations.DeleteRecordUsingExternalId(externalFieldValue, moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get all the records of a module and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to obtain records.</param>
        public static void GetRecords(string moduleAPIName)
        {
            //example
            //string moduleAPIName = "module_api_name";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetRecordsParam.APPROVED, "both");

            paramInstance.Add(GetRecordsParam.CONVERTED, "both");

            paramInstance.Add(GetRecordsParam.CVID, "3477061087501");

            List<string> ids = new List<string>() { "TestExternalLead111", "34770614352001" };

            foreach (string id in ids)
            {
                paramInstance.Add(GetRecordsParam.IDS, id);
            }

            paramInstance.Add(GetRecordsParam.UID, "34770615181008");

            List<string> fieldNames = new List<string>() { "Company", "Email" };

            foreach (string fieldName in fieldNames)
            {
                paramInstance.Add(GetRecordsParam.FIELDS, fieldName);
            }

            paramInstance.Add(GetRecordsParam.SORT_BY, "Email");

            paramInstance.Add(GetRecordsParam.SORT_ORDER, "desc");

            paramInstance.Add(GetRecordsParam.PAGE, 1);

            paramInstance.Add(GetRecordsParam.PER_PAGE, 1);

            DateTimeOffset startdatetime = new DateTimeOffset(new DateTime(2021, 11, 20, 10, 0, 01, DateTimeKind.Local));

            paramInstance.Add(GetRecordsParam.STARTDATETIME, startdatetime);

            DateTimeOffset enddatetime = new DateTimeOffset(new DateTime(2022, 12, 20, 10, 0, 01, DateTimeKind.Local));

            paramInstance.Add(GetRecordsParam.ENDDATETIME, enddatetime);

            paramInstance.Add(GetRecordsParam.TERRITORY_ID, "34770613051357");

            paramInstance.Add(GetRecordsParam.INCLUDE_CHILD, "true");

            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2019, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetRecordsHeader.IF_MODIFIED_SINCE, ifmodifiedsince);

            //headerInstance.Add(GetRecordsHeader.X_EXTERNAL, "Leads.External");

            //Call GetRecords method that takes moduleAPIName, paramInstance and headerInstance  as parameter.
            APIResponse<ResponseHandler> response = recordOperations.GetRecords(moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get the object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the obtained Record instances
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("Record ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("Record Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("Record Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if (tags != null)
                            {
                                foreach (Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                }
                            }

                            //To Get particular field value
                            Console.WriteLine("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("Record KeyValues: ");

                            //Get the KeyValue map
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                string keyName = entry.Key;

                                Object value = entry.Value;

                                if (value != null)
                                {
                                    if (value is IList && ((IList)value).Count > 0)
                                    {
                                        IList dataList = (IList)value;

                                        if (dataList.Count > 0)
                                        {
                                            if (dataList[0] is FileDetails)
                                            {
                                                List<FileDetails> fileDetails = (List<FileDetails>)value;

                                                foreach (FileDetails fileDetail in fileDetails)
                                                {
                                                    //Get the Extn of each FileDetails
                                                    Console.WriteLine("Record FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("Record FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("Record FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("Record FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("Record FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("Record FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("Record FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("Record FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("Record PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("Record PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("Record PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("Record PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("Record Participants Name: " + participant.Name);

                                                    Console.WriteLine("Record Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("Record Participants ID: " + participant.Id);

                                                    Console.WriteLine("Record Participants Type: " + participant.Type);

                                                    Console.WriteLine("Record Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("Record Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {
                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("Record ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("Record ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("Record ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("Record ProductDetails LineTax Value: " + lineTax.Value);

                                                    Console.WriteLine("Record ProductDetails LineTax DisplayName: " + lineTax.DisplayName);
                                                }
                                            }
                                            else if (dataList[0].GetType().FullName.Contains("Choice"))
                                            {
                                                Console.WriteLine(keyName);

                                                Console.WriteLine("values");

                                                foreach (object choice in dataList)
                                                {
                                                    Type type = choice.GetType();

                                                    PropertyInfo prop = type.GetProperty("Value");

                                                    Console.WriteLine(prop.GetValue(choice));
                                                }
                                            }
                                            else if (dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>)dataList;

                                                foreach (Comment comment in comments)
                                                {
                                                    Console.WriteLine("Record Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("Record Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("Record Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("Record Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if (dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>)dataList; ;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if (owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("Record Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("Record Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("Record Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("Record Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if (parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("Record Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("Record Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("Record Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("Record Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("Record Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("Record Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if (modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("Record Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("Record Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if (createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("Record Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>)dataList;

                                                foreach (ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("Record " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("Record " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("Record " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("Record " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("Record " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("Record " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("Record " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("Record " + keyName + "FileId: " + imageUpload.FileId);
                                                }
                                            }
                                            else if (dataList[0] is Com.Zoho.Crm.API.Record.Record)
                                            {
                                                List<Com.Zoho.Crm.API.Record.Record> recordList = (List<Com.Zoho.Crm.API.Record.Record>)dataList;

                                                foreach (Com.Zoho.Crm.API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + JsonConvert.SerializeObject(entry1.Value));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                            }
                                        }
                                    }
                                    else if (value is Layout)
                                    {
                                        Layout layout = (Layout)value;

                                        if (layout != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("Record " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("Record " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("Record " + keyName + " User-Email: " + user.Email);
                                        }
                                    }
                                    else if (value is Consent)
                                    {
                                        Consent consent = (Consent)value;

                                        Console.WriteLine("Record Consent ID: " + consent.Id);

                                        //Get the Owner User instance of each attachment
                                        User owner = consent.Owner;

                                        //Check if owner is not null
                                        if (owner != null)
                                        {
                                            //Get the name of the owner User
                                            Console.WriteLine("Record Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("Record Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("Record Consent Owner Email: " + owner.Email);
                                        }

                                        User consentCreatedBy = consent.CreatedBy;

                                        //Check if createdBy is not null
                                        if (consentCreatedBy != null)
                                        {
                                            //Get the name of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Name: " + consentCreatedBy.Name);

                                            //Get the ID of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy ID: " + consentCreatedBy.Id);

                                            //Get the Email of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Email: " + consentCreatedBy.Email);
                                        }

                                        User consentModifiedBy = consent.ModifiedBy;

                                        //Check if createdBy is not null
                                        if (consentModifiedBy != null)
                                        {
                                            //Get the name of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Name: " + consentModifiedBy.Name);

                                            //Get the ID of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy ID: " + consentModifiedBy.Id);

                                            //Get the Email of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Email: " + consentModifiedBy.Email);
                                        }

                                        Console.WriteLine("Record Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("Record Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("Record Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("Record Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("Record Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("Record Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("Record Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("Record Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("Record Consent DataProcessingBasis: " + consent.DataProcessingBasis);

                                        //To get custom values
                                        Console.WriteLine("Record Consent Lawful Reason: " + consent.GetKeyValue("Lawful_Reason"));
                                    }
                                    else if (value is Com.Zoho.Crm.API.Record.Record)
                                    {
                                        Com.Zoho.Crm.API.Record.Record recordValue = (Com.Zoho.Crm.API.Record.Record)value;

                                        Console.WriteLine("Record " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("Record " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine(keyName + ": " + prop.GetValue(value));
                                    }
                                    else if (value is RemindAt)
                                    {
                                        Console.WriteLine(keyName + ": " + ((RemindAt)value).Alarm);
                                    }
                                    else if (value is RecurringActivity)
                                    {
                                        Console.WriteLine(keyName);

                                        Console.WriteLine("RRULE" + ": " + ((RecurringActivity)value).Rrule);
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }

                        //Get the Object obtained Info instance
                        Info info = responseWrapper.Info;

                        //Check if info is not null
                        if (info != null)
                        {
                            if (info.PerPage != null)
                            {
                                //Get the PerPage of the Info
                                Console.WriteLine("Record Info PerPage: " + info.PerPage);
                            }

                            if (info.Count != null)
                            {
                                //Get the Count of the Info
                                Console.WriteLine("Record Info Count: " + info.Count);
                            }

                            if (info.Page != null)
                            {
                                //Get the Page of the Info
                                Console.WriteLine("Record Info Page: " + info.Page);
                            }

                            if (info.MoreRecords != null)
                            {
                                //Get the MoreRecords of the Info
                                Console.WriteLine("Record Info MoreRecords: " + info.MoreRecords);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to create records of a module and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to create records.</param>
        public static void CreateRecords(string moduleAPIName)
        {
            //API Name of the module to create records
            //string moduleAPIName = "module_api_name";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper bodyWrapper = new BodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record1.AddFieldValue(Leads.CITY, "City");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record1.AddFieldValue(Leads.COMPANY, "KKRNP");

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record1.AddKeyValue("External", "Value32422");

            record1.AddKeyValue("Custom_field_2", "value");

            record1.AddKeyValue("Date_Time_2", new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local)));

            record1.AddKeyValue("Date_1", new DateTime(2021, 1, 13).Date);

            record1.AddKeyValue("Subject", "AutomatedSDK");

            List<FileDetails> fileDetails = new List<FileDetails>();

            FileDetails fileDetail1 = new FileDetails();

            fileDetail1.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c32653b9ed5cc5ea81131aa4f67ea3c6871";

            fileDetails.Add(fileDetail1);

            FileDetails fileDetail2 = new FileDetails();

            fileDetail2.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c32c6a3eee9153b1c54528fd97486365542";

            fileDetails.Add(fileDetail2);

            FileDetails fileDetail3 = new FileDetails();

            fileDetail3.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c323aee85624ea8e1481150b7dc1edead07";

            fileDetails.Add(fileDetail3);

            record1.AddKeyValue("File_Upload", fileDetails);

            ImageUpload imageUpload1 = new ImageUpload();

            imageUpload1.EncryptedId = "ae9c7cefa418aec1d6a5cc2d9ab35c32ac761e7fd00680013b9565585a59646d";

            ImageUpload imageUpload2 = new ImageUpload();

            imageUpload2.EncryptedId = "ae9c7cefa418aec1d6a5cc2d9ab35c32e1bee118a548f80b05e8b1e6e134c8fa";

            record1.AddKeyValue("Image_Upload", new List<ImageUpload>() { imageUpload1, imageUpload2 });

            List<API.Record.Record> subformList = new List<API.Record.Record>();

            API.Record.Record subform = new API.Record.Record();

            subform.AddKeyValue("DOB", "sep");

            subform.AddKeyValue("Name1", "test");

            subformList.Add(subform);

            record1.AddKeyValue("Subform_1", subformList);

            API.Users.User recordOwner = new API.Users.User();

            recordOwner.Email = "abc@zoho.com";

            record1.AddKeyValue("Owner", recordOwner);

            //Used when GDPR is enabled
            Consent dataConsent = new Consent();

            dataConsent.ConsentRemarks = "Approved.";

            dataConsent.ConsentThrough = "Email";

            dataConsent.ContactThroughEmail = true;

            dataConsent.ContactThroughSocial = false;

            record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);

            //Products

            record1.AddFieldValue(Products.PRODUCT_NAME, "Product_Name");

            Com.Zoho.Crm.API.Record.Record vendorName = new Com.Zoho.Crm.API.Record.Record();

            vendorName.AddFieldValue(Vendors.ID, 34770617247001);

            record1.AddFieldValue(Products.VENDOR_NAME, vendorName);

            record1.AddFieldValue(Products.PRODUCT_CATEGORY, new Choice<string>("Software"));

            record1.AddFieldValue(Products.PRODUCT_ACTIVE, true);

            record1.AddFieldValue(Products.SALES_START_DATE, new DateTime(2020, 10, 12));

            record1.AddFieldValue(Products.SALES_END_DATE, new DateTime(2020, 12, 12));

            record1.AddFieldValue(Products.TAXABLE, true);

            List<Tax> taxes = new List<Tax>();

            Tax tax = new Tax();

            tax.Value = "MyT2ax1134 - 15.0 %";

            taxes.Add(tax);

            tax = new Tax();

            tax.Value = "MyTax22 - 10.0 %";

            taxes.Add(tax);

            tax = new Tax();

            tax.Value = "MyTax1123 - 10.0 %";

            taxes.Add(tax);

            tax = new Tax();

            tax.Value = "MyTax1134 - 15.0 %";

            taxes.Add(tax);

            record1.AddFieldValue(Products.TAX, taxes);

            /** Following methods are being used only by Inventory modules */

            Com.Zoho.Crm.API.Record.Record dealName = new Com.Zoho.Crm.API.Record.Record();

            dealName.AddFieldValue(Deals.ID, 3477061012263008);

            record1.AddFieldValue(Sales_Orders.DEAL_NAME, dealName);

            Com.Zoho.Crm.API.Record.Record contactName = new Com.Zoho.Crm.API.Record.Record();

            contactName.AddFieldValue(Contacts.ID, 3477061012263005);

            record1.AddFieldValue(Purchase_Orders.CONTACT_NAME, contactName);

            Com.Zoho.Crm.API.Record.Record accountName = new Com.Zoho.Crm.API.Record.Record();

            accountName.AddKeyValue("name", "automatedAccount");

            record1.AddFieldValue(Quotes.ACCOUNT_NAME, accountName);

            List<API.Record.Record> inventoryLineItemList = new List<API.Record.Record>();

            API.Record.Record inventoryLineItem = new API.Record.Record();

            LineItemProduct lineItemProduct = new LineItemProduct();

            lineItemProduct.Id = 3477061012259001;

            //lineItemProduct.AddKeyValue("Products_External", "AutomatedSDKExternal");

            inventoryLineItem.AddKeyValue("Product_Name", lineItemProduct);

            inventoryLineItem.AddKeyValue("Quantity", 1.5);

            inventoryLineItem.AddKeyValue("Description", "productDescription");

            inventoryLineItem.AddKeyValue("ListPrice", 10.0);

            inventoryLineItem.AddKeyValue("Discount", "5.0");

            inventoryLineItem.AddKeyValue("Discount", "5.25%");

            List<LineTax> productLineTaxes = new List<LineTax>();

            LineTax productLineTax = new LineTax();

            productLineTax.Name = "MyTax1134";

            productLineTax.Percentage = 20.0;

            productLineTaxes.Add(productLineTax);

            inventoryLineItem.AddKeyValue("Line_Tax", productLineTaxes);

            inventoryLineItemList.Add(inventoryLineItem);

            record1.AddKeyValue("Quoted_Items", inventoryLineItemList);

            // record1.AddKeyValue("Invoiced_Items", inventoryLineItemList);

            // record1.AddKeyValue("Purchase_Items", inventoryLineItemList);

            // record1.AddKeyValue("Ordered_Items", inventoryLineItemList);

            List<LineTax> lineTaxes = new List<LineTax>();

            LineTax lineTax = new LineTax();

            lineTax.Name = "MyTax1134";

            lineTax.Percentage = 20.0;

            lineTaxes.Add(lineTax);

            record1.AddKeyValue("$line_tax", lineTaxes);

            /** End Inventory **/

            /** Following methods are being used only by Activity modules */

            //Tasks

            record1.AddFieldValue(Tasks.DESCRIPTION, "Test Task");

            record1.AddKeyValue("Currency", new Choice<string>("INR"));

            RemindAt remindAt = new RemindAt();

            remindAt.Alarm = "FREQ=NONE;ACTION=EMAIL;TRIGGER=-P1D;TRIGGER_TIME=11:06";

            record1.AddFieldValue(Tasks.REMIND_AT, remindAt);

            Com.Zoho.Crm.API.Record.Record whoId = new Com.Zoho.Crm.API.Record.Record();

            whoId.Id = 3477061012263005;

            record1.AddFieldValue(Tasks.WHO_ID, whoId);

            record1.AddFieldValue(Tasks.STATUS, new Choice<string>("In Progress"));

            record1.AddFieldValue(Tasks.DUE_DATE, new DateTime(2022, 1, 13));

            record1.AddFieldValue(Tasks.PRIORITY, new Choice<string>("High"));

            record1.AddKeyValue("$se_module", "Accounts");

            record1.AddFieldValue(Tasks.SUBJECT, "Email");

            Com.Zoho.Crm.API.Record.Record whatId = new Com.Zoho.Crm.API.Record.Record();

            whatId.Id = 3477061012263002;

            record1.AddFieldValue(Tasks.WHAT_ID, whatId);

            /** Recurring Activity can be provided in any activity module*/

            RecurringActivity recurringActivity = new RecurringActivity();

            recurringActivity.Rrule = "FREQ=DAILY;INTERVAL=1;UNTIL=2022-11-10;DTSTART=2022-11-10";

            record1.AddFieldValue(Events.RECURRING_ACTIVITY, recurringActivity);

            //Events
            record1.AddFieldValue(Events.DESCRIPTION, "Test Events");

            DateTimeOffset startDateTime = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            record1.AddFieldValue(Events.START_DATETIME, startDateTime);

            List<Participants> participants = new List<Participants>();

            Participants participant1 = new Participants();

            participant1.Participant = "abc@zoho.com";

            participant1.Type = "email";

            participant1.Id = 34770615902017;

            participants.Add(participant1);

            Participants participant2 = new Participants();

            participant2.AddKeyValue("participant", "3477061012402002");

            participant2.AddKeyValue("type", "lead");

            participants.Add(participant2);

            record1.AddFieldValue(Events.PARTICIPANTS, participants);

            record1.AddKeyValue("$send_notification", true);

            record1.AddFieldValue(Events.EVENT_TITLE, "New Automated Event");

            DateTimeOffset enddatetime = new DateTimeOffset(new DateTime(2020, 09, 15, 12, 0, 0, DateTimeKind.Local));

            record1.AddFieldValue(Events.END_DATETIME, enddatetime);

            DateTimeOffset remindAt1 = new DateTimeOffset(new DateTime(2020, 08, 15, 12, 0, 0, DateTimeKind.Local));

            record1.AddFieldValue(Events.REMIND_AT, remindAt1);

            record1.AddFieldValue(Events.CHECK_IN_STATUS, "PLANNED");

            record1.AddKeyValue("$se_module", "Leads");

            Com.Zoho.Crm.API.Record.Record whatId1 = new Com.Zoho.Crm.API.Record.Record();

            whatId1.Id = 3477061012018002;

            record1.AddFieldValue(Events.WHAT_ID, whatId1);

            /** End Activity **/

            /** Following methods are being used only by Price_Books modules */

            List<PricingDetails> pricingDetails = new List<PricingDetails>();

            PricingDetails pricingDetail1 = new PricingDetails();

            pricingDetail1.FromRange = 1.0;

            pricingDetail1.ToRange = 5.0;

            pricingDetail1.Discount = 2.0;

            pricingDetails.Add(pricingDetail1);

            PricingDetails pricingDetail2 = new PricingDetails();

            pricingDetail2.AddKeyValue("from_range", 6.0);

            pricingDetail2.AddKeyValue("to_range", 11.0);

            pricingDetail2.AddKeyValue("discount", 3.0);

            pricingDetails.Add(pricingDetail2);

            record1.AddFieldValue(Price_Books.PRICING_DETAILS, pricingDetails);

            record1.AddKeyValue("Email", "abc@zoho.com");

            record1.AddFieldValue(Price_Books.DESCRIPTION, "TEST");

            record1.AddFieldValue(Price_Books.PRICE_BOOK_NAME, "book_name");

            record1.AddFieldValue(Price_Books.PRICING_MODEL, new Choice<string>("-None-"));

            List<Tag> tagList = new List<Tag>();

            Tag tag = new Tag();

            tag.Name = "Testtask";

            tagList.Add(tag);

            record1.Tag = tagList;

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            bodyWrapper.Data = records;

            List<string> trigger = new List<string>();

            trigger.Add("approval");

            trigger.Add("workflow");

            trigger.Add("blueprint");

            bodyWrapper.Trigger = trigger;

            //bodyWrapper.LarId = "3477061087515";

            HeaderMap headerInstance = new HeaderMap();

            //headerInstance.Add(CreateRecordsHeader.X_EXTERNAL, "Quotes.Quoted_Items.Product_Name.Products_External");

            //Call CreateRecords method that takes moduleAPIName and BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = recordOperations.CreateRecords(moduleAPIName, bodyWrapper, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to update the records of a module with ID and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to update records.</param>
        public static void UpdateRecords(string moduleAPIName)
        {
            //API Name of the module to create records
            //string moduleAPIName = "Leads";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            record1.Id = 3477061012397012;

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record1.AddFieldValue(Leads.CITY, "City");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record1.AddFieldValue(Leads.COMPANY, "zoho");

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record1.AddKeyValue("Custom_field", "Value");

            record1.AddKeyValue("Custom_field_2", "value");

            //Used when GDPR is enabled
            Consent dataConsent = new Consent();

            dataConsent.ConsentRemarks = "Approved.";

            dataConsent.ConsentThrough = "Email";

            dataConsent.ContactThroughEmail = true;

            dataConsent.ContactThroughSocial = false;

            record1.AddKeyValue("Data_Processing_Basis_Details", dataConsent);

            API.Users.User recordOwner = new API.Users.User();

            recordOwner.Email = "abc@zoho.com";

            record1.AddKeyValue("Owner", recordOwner);

            /** Following methods are being used only by Inventory modules */

            API.Record.Record dealName = new API.Record.Record();

            dealName.AddFieldValue(Deals.ID, 3477061011383007);

            record1.AddFieldValue(Sales_Orders.DEAL_NAME, dealName);

            API.Record.Record contactName = new API.Record.Record();

            contactName.AddFieldValue(Contacts.ID, 3477061011853001);

            record1.AddFieldValue(Purchase_Orders.CONTACT_NAME, contactName);

            API.Record.Record accountName = new API.Record.Record();

            accountName.AddKeyValue("name", "automatedAccount");

            record1.AddFieldValue(Quotes.ACCOUNT_NAME, accountName);

            List<API.Record.Record> inventoryLineItemList = new List<API.Record.Record>();

            API.Record.Record inventoryLineItem = new API.Record.Record();

            LineItemProduct lineItemProduct = new LineItemProduct();

            //lineItemProduct.Id = 3477061011217004;

            lineItemProduct.AddKeyValue("Products_External", "TestExternalLead121");

            inventoryLineItem.AddKeyValue("Description", "asd");

            inventoryLineItem.AddKeyValue("Discount", "5");

            API.Record.Record parentId = new API.Record.Record();

            parentId.Id = 35240337331017;

            inventoryLineItem.AddKeyValue("Parent_Id", parentId);

            LineItemProduct lineitemProduct = new LineItemProduct();

            lineitemProduct.Id = 35240333659082;

            inventoryLineItem.AddKeyValue("Product_Name", lineItemProduct);

            inventoryLineItem.AddKeyValue("Sequence_Number", 1);

            inventoryLineItem.AddKeyValue("Quantity", 123.2);

            inventoryLineItem.AddKeyValue("Tax", 123.2);

            inventoryLineItemList.Add(inventoryLineItem);

            List<LineTax> productLineTaxes = new List<LineTax>();

            LineTax productLineTax = new LineTax();

            productLineTax.Name = "MyTax1134";

            productLineTax.Percentage = 20.0;

            productLineTaxes.Add(productLineTax);

            inventoryLineItem.AddKeyValue("Line_Tax", productLineTaxes);

            inventoryLineItemList.Add(inventoryLineItem);

            record1.AddKeyValue("Quoted_Items", inventoryLineItemList);

            List<LineTax> lineTaxes = new List<LineTax>();

            LineTax lineTax = new LineTax();

            lineTax.Name = "MyTax1134";

            lineTax.Percentage = 20.0;

            lineTaxes.Add(lineTax);

            record1.AddKeyValue("$line_tax", lineTaxes);
            /** End Inventory **/

            //Add Record instance to the list
            records.Add(record1);

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();

            record2.Id = 3477061012392;

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record2.AddFieldValue(Leads.CITY, "City");

            record2.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record2.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record2.AddFieldValue(Leads.COMPANY, "KKRNP");

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record2.AddKeyValue("Custom_field", "Value");

            record2.AddKeyValue("Custom_field_2", "value");

            //Add Record instance to the list
            records.Add(record2);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            List<string> trigger = new List<string>();

            trigger.Add("approval");

            trigger.Add("workflow");

            trigger.Add("blueprint");

            request.Trigger = trigger;

            HeaderMap headerInstance = new HeaderMap();

            //headerInstance.Add(UpdateRecordsHeader.X_EXTERNAL, "Quotes.Quoted_Items.Product_Name.Products_External");

            //Call UpdateRecords method that takes moduleAPIName annd BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = recordOperations.UpdateRecords(moduleAPIName, request, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained Attachment instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to delete records of a module and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to delete records.</param>
        /// <param name="recordIds">The List of the record IDs to be deleted.</param>
        public static void DeleteRecords(string moduleAPIName, List<string> recordIds)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";

            //List<string> recordIds = new List<string>(){"347708033", "347717", "3478001"};

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            foreach (string id in recordIds)
            {
                paramInstance.Add(DeleteRecordsParam.IDS, id);
            }

            paramInstance.Add(DeleteRecordsParam.WF_TRIGGER, false);


            HeaderMap headerInstance = new HeaderMap();

            // headerInstance.Add(DeleteRecordsHeader.X_EXTERNAL, "Leads.External");

            //Call DeleteRecords method that takes moduleAPIName and paramInstance as parameter.
            APIResponse<ActionHandler> response = recordOperations.DeleteRecords(moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to Upsert records of a module and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to upsert records.</param>
        public static void UpsertRecords(string moduleAPIName)
        {
            //API Name of the module to create records
            //string moduleAPIName = "module_api_name";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record1.AddFieldValue(Leads.CITY, "City");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record1.AddFieldValue(Leads.COMPANY, "Company1");

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record1.AddKeyValue("Custom_field", "Value");

            record1.AddKeyValue("Custom_field_2", "value");

            //Add Record instance to the list
            records.Add(record1);

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record2.AddFieldValue(Leads.CITY, "City");

            record2.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record2.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record2.AddFieldValue(Leads.COMPANY, "Company12");

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record2.AddKeyValue("Custom_field", "Value");

            record2.AddKeyValue("Custom_field_2", "value");

            //Add Record instance to the list
            records.Add(record2);

            List<string> duplicateCheckFields = new List<string>() { "City", "Last_Name", "First_Name" };

            request.DuplicateCheckFields = duplicateCheckFields;

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            HeaderMap headerInstance = new HeaderMap();

            headerInstance.Add(UpsertRecordsHeader.X_EXTERNAL, "Leads.External");

            //Call upsertRecords method that takes moduleAPIName and BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = recordOperations.UpsertRecords(moduleAPIName, request, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get the deleted records of a module and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to get the deleted records.</param>
        public static void GetDeletedRecords(string moduleAPIName)
        {
            //example
            //string moduleAPIName = "module_api_name";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetDeletedRecordsParam.TYPE, "permanent");//all, recycle, permanent

            paramInstance.Add(GetDeletedRecordsParam.PAGE, 1);

            paramInstance.Add(GetDeletedRecordsParam.PER_PAGE, 2);

            //Get instance of HeaderMap Class
            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetDeletedRecordsHeader.IF_MODIFIED_SINCE, ifModifiedSince);

            //Call GetDeletedRecords method that takes moduleAPIName, headerInstance and paramInstance as parameter
            APIResponse<DeletedRecordsHandler> response = recordOperations.GetDeletedRecords(moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get the object from response
                    DeletedRecordsHandler deletedRecordsHandler = response.Object;

                    if (deletedRecordsHandler is DeletedRecordsWrapper)
                    {
                        //Get the received DeletedRecordsWrapper instance
                        DeletedRecordsWrapper deletedRecordsWrapper = (DeletedRecordsWrapper)deletedRecordsHandler;

                        //Get the list of obtained DeletedRecord instances
                        List<DeletedRecord> deletedRecords = deletedRecordsWrapper.Data;

                        foreach (DeletedRecord deletedRecord in deletedRecords)
                        {
                            //Get the deletedBy User instance of each DeletedRecord
                            User deletedBy = deletedRecord.DeletedBy;

                            //Check if deletedBy is not null
                            if (deletedBy != null)
                            {
                                //Get the name of the deletedBy User
                                Console.WriteLine("DeletedRecord Deleted By User-Name: " + deletedBy.Name);

                                //Get the ID of the deletedBy User
                                Console.WriteLine("DeletedRecord Deleted By User-ID: " + deletedBy.Id);
                            }

                            //Get the ID of each DeletedRecord
                            Console.WriteLine("DeletedRecord ID: " + deletedRecord.Id);

                            //Get the DisplayName of each DeletedRecord
                            Console.WriteLine("DeletedRecord DisplayName: " + deletedRecord.DisplayName);

                            //Get the Type of each DeletedRecord
                            Console.WriteLine("DeletedRecord Type: " + deletedRecord.Type);

                            //Get the createdBy User instance of each DeletedRecord
                            User createdBy = deletedRecord.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the name of the createdBy User
                                Console.WriteLine("DeletedRecord Created By User-Name: " + createdBy.Name);

                                //Get the ID of the createdBy User
                                Console.WriteLine("DeletedRecord Created By User-ID: " + createdBy.Id);
                            }

                            //Get the DeletedTime of each DeletedRecord
                            Console.WriteLine("DeletedRecord DeletedTime: " + deletedRecord.DeletedTime);
                        }

                        Info info = deletedRecordsWrapper.Info;

                        //Check if info is not null
                        if (info != null)
                        {
                            if (info.PerPage != null)
                            {
                                //Get the PerPage of the Info
                                Console.WriteLine("Record Info PerPage: " + info.PerPage);
                            }

                            if (info.Count != null)
                            {
                                //Get the Count of the Info
                                Console.WriteLine("Record Info Count: " + info.Count);
                            }

                            if (info.Page != null)
                            {
                                //Get the Page of the Info
                                Console.WriteLine("Record Info Page: " + info.Page);
                            }

                            if (info.MoreRecords != null)
                            {
                                //Get the MoreRecords of the Info
                                Console.WriteLine("Record Info MoreRecords: " + info.MoreRecords);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (deletedRecordsHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)deletedRecordsHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to search records of a module and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to obtain records.</param>
        public static void SearchRecords(string moduleAPIName)
        {
            //example
            //string moduleAPIName = "module_api_name";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(SearchRecordsParam.CRITERIA, "((Last_Name:starts_with:Last Name) and (Company:starts_with:fasf\\(123\\) K))");

            //paramInstance.Add(SearchRecordsParam.CRITERIA, "(External:in:TestExternal678901)");

            paramInstance.Add(SearchRecordsParam.EMAIL, "abc@zoho.com");

            paramInstance.Add(SearchRecordsParam.PHONE, "234567890");

            paramInstance.Add(SearchRecordsParam.WORD, "First Name Last Name");

            paramInstance.Add(SearchRecordsParam.CONVERTED, "both");

            paramInstance.Add(SearchRecordsParam.APPROVED, "both");

            paramInstance.Add(SearchRecordsParam.PAGE, 1);

            paramInstance.Add(SearchRecordsParam.PER_PAGE, 2);

            HeaderMap headerInstance = new HeaderMap();

            //headerInstance.Add(SearchRecordsHeader.X_EXTERNAL, "Leads.External");

            //Call SearchRecords method that takes moduleAPIName, ParameterMap Instance and headerInstance as parameter
            APIResponse<ResponseHandler> response = recordOperations.SearchRecords(moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get the object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the obtained Record instance
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("Record ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("Record Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("Record Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if (tags != null)
                            {
                                foreach (Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                }
                            }

                            //To Get particular field value
                            Console.WriteLine("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("Record KeyValues: ");

                            //Get the KeyValue map
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                string keyName = entry.Key;

                                Object value = entry.Value;

                                if (value != null)
                                {
                                    if (value is IList && ((IList)value).Count > 0)
                                    {
                                        IList dataList = (IList)value;

                                        if (dataList.Count > 0)
                                        {
                                            if (dataList[0] is FileDetails)
                                            {
                                                List<FileDetails> fileDetails = (List<FileDetails>)value;

                                                foreach (FileDetails fileDetail in fileDetails)
                                                {
                                                    //Get the Extn of each FileDetails
                                                    Console.WriteLine("Record FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("Record FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("Record FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("Record FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("Record FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("Record FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("Record FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("Record FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("Record PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("Record PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("Record PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("Record PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("Record Participants Name: " + participant.Name);

                                                    Console.WriteLine("Record Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("Record Participants ID: " + participant.Id);

                                                    Console.WriteLine("Record Participants Type: " + participant.Type);

                                                    Console.WriteLine("Record Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("Record Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {
                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("Record ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("Record ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("Record ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("Record ProductDetails LineTax Value: " + lineTax.Value);

                                                    Console.WriteLine("Record ProductDetails LineTax DisplayName: " + lineTax.DisplayName);
                                                }
                                            }
                                            else if (dataList[0].GetType().FullName.Contains("Choice"))
                                            {
                                                Console.WriteLine(keyName);

                                                Console.WriteLine("values");

                                                foreach (object choice in dataList)
                                                {
                                                    Type type = choice.GetType();

                                                    PropertyInfo prop = type.GetProperty("Value");

                                                    Console.WriteLine(prop.GetValue(choice));
                                                }
                                            }
                                            else if (dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>)dataList;

                                                foreach (Comment comment in comments)
                                                {
                                                    Console.WriteLine("Record Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("Record Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("Record Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("Record Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if (dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>)dataList; ;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if (owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("Record Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("Record Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("Record Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("Record Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if (parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("Record Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("Record Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("Record Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("Record Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("Record Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("Record Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if (modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("Record Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("Record Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if (createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("Record Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>)dataList;

                                                foreach (ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("Record " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("Record " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("Record " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("Record " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("Record " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("Record " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("Record " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("Record " + keyName + "FileId: " + imageUpload.FileId);
                                                }
                                            }
                                            else if (dataList[0] is Com.Zoho.Crm.API.Record.Record)
                                            {
                                                List<Com.Zoho.Crm.API.Record.Record> recordList = (List<Com.Zoho.Crm.API.Record.Record>)dataList;

                                                foreach (Com.Zoho.Crm.API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + JsonConvert.SerializeObject(entry1.Value));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                            }
                                        }
                                    }
                                    else if (value is Layout)
                                    {
                                        Layout layout = (Layout)value;

                                        if (layout != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("Record " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("Record " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("Record " + keyName + " User-Email: " + user.Email);
                                        }
                                    }
                                    else if (value is Consent)
                                    {
                                        Consent consent = (Consent)value;

                                        //Get the Owner User instance of each attachment
                                        User owner = consent.Owner;

                                        //Check if owner is not null
                                        if (owner != null)
                                        {
                                            //Get the name of the owner User
                                            Console.WriteLine("Record Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("Record Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("Record Consent Owner Email: " + owner.Email);
                                        }

                                        User consentCreatedBy = consent.CreatedBy;

                                        //Check if createdBy is not null
                                        if (consentCreatedBy != null)
                                        {
                                            //Get the name of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Name: " + consentCreatedBy.Name);

                                            //Get the ID of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy ID: " + consentCreatedBy.Id);

                                            //Get the Email of the CreatedBy User
                                            Console.WriteLine("Record Consent CreatedBy Email: " + consentCreatedBy.Email);
                                        }

                                        User consentModifiedBy = consent.ModifiedBy;

                                        //Check if createdBy is not null
                                        if (consentModifiedBy != null)
                                        {
                                            //Get the name of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Name: " + consentModifiedBy.Name);

                                            //Get the ID of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy ID: " + consentModifiedBy.Id);

                                            //Get the Email of the ModifiedBy User
                                            Console.WriteLine("Record Consent ModifiedBy Email: " + consentModifiedBy.Email);
                                        }

                                        Console.WriteLine("Record Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("Record Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("Record Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("Record Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("Record Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("Record Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("Record Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("Record Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("Record Consent DataProcessingBasis: " + consent.DataProcessingBasis);

                                        //To get custom values
                                        Console.WriteLine("Record Consent Lawful Reason: " + consent.GetKeyValue("Lawful_Reason"));
                                    }
                                    else if (value is Com.Zoho.Crm.API.Record.Record)
                                    {
                                        Com.Zoho.Crm.API.Record.Record recordValue = (Com.Zoho.Crm.API.Record.Record)value;

                                        Console.WriteLine("Record " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("Record " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine(keyName + ": " + prop.GetValue(value));
                                    }
                                    else if (value is RemindAt)
                                    {
                                        Console.WriteLine(keyName + ": " + ((RemindAt)value).Alarm);
                                    }
                                    else if (value is RecurringActivity)
                                    {
                                        Console.WriteLine(keyName);

                                        Console.WriteLine("RRULE" + ": " + ((RecurringActivity)value).Rrule);
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }

                        //Get the Object obtained Info instance
                        Info info = responseWrapper.Info;

                        //Check if info is not null
                        if (info != null)
                        {
                            if (info.PerPage != null)
                            {
                                //Get the PerPage of the Info
                                Console.WriteLine("Record Info PerPage: " + info.PerPage);
                            }

                            if (info.Count != null)
                            {
                                //Get the Count of the Info
                                Console.WriteLine("Record Info Count: " + info.Count);
                            }

                            if (info.Page != null)
                            {
                                //Get the Page of the Info
                                Console.WriteLine("Record Info Page: " + info.Page);
                            }

                            if (info.MoreRecords != null)
                            {
                                //Get the MoreRecords of the Info
                                Console.WriteLine("Record Info MoreRecords: " + info.MoreRecords);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to Convert Lead record and print the response.
        /// </summary>
        /// <param name="leadId">The ID of the Lead to be converted.</param>
        public static void ConvertLead(long leadId)
        {
            //API Name of the module
            //long leadId = 347276;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ConvertBodyWrapper Class that will contain the request body
            ConvertBodyWrapper request = new ConvertBodyWrapper();

            //List of LeadConverter instances
            List<LeadConverter> data = new List<LeadConverter>();

            //Get instance of LeadConverter Class
            LeadConverter record1 = new LeadConverter();

            record1.Overwrite = true;

            record1.NotifyLeadOwner = true;

            record1.NotifyNewEntityOwner = true;

            record1.Accounts = "3477061012263002";

            record1.Contacts = "3477061012263005";

            //record1.AssignTo = "3477061173021";

            Com.Zoho.Crm.API.Record.Record deals = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            deals.AddFieldValue(Deals.DEAL_NAME, "deal_name");

            deals.AddFieldValue(Deals.DESCRIPTION, "deals description");

            deals.AddFieldValue(Deals.CLOSING_DATE, new DateTime(2021, 1, 13));

            deals.AddFieldValue(Deals.STAGE, new Choice<string>("Closed Won"));

            deals.AddFieldValue(Deals.AMOUNT, 50.7);

            deals.AddKeyValue("Pipeline", new Choice<string>("Qualification"));

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            deals.AddKeyValue("Custom_field", "Value");

            deals.AddKeyValue("Custom_field_2", "value");

            List<Tag> tagList = new List<Tag>();

            Tag tag = new Tag();

            tag.Name = "TestDeals";

            tagList.Add(tag);

            deals.Tag = tagList;

            record1.Deals = deals;

            //Add Record instance to the list
            data.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = data;

            //Call ConvertLead method that takes leadId and ConvertBodyWrapper instance as parameter.
            APIResponse<ConvertActionHandler> response = recordOperations.ConvertLead(leadId, request);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ConvertActionHandler convertActionHandler = response.Object;

                    if (convertActionHandler is ConvertActionWrapper)
                    {
                        //Get the received ConvertActionWrapper instance
                        ConvertActionWrapper convertActionWrapper = (ConvertActionWrapper)convertActionHandler;

                        //Get the list of obtained ConvertActionResponse instances
                        List<ConvertActionResponse> convertActionResponses = convertActionWrapper.Data;

                        foreach (ConvertActionResponse convertActionResponse in convertActionResponses)
                        {
                            //Check if the request is successful
                            if (convertActionResponse is SuccessfulConvert)
                            {
                                //Get the received SuccessfulConvert instance
                                SuccessfulConvert successfulConvert = (SuccessfulConvert)convertActionResponse;

                                //Get the Accounts ID of  Record
                                Console.WriteLine("LeadConvert Accounts ID: " + successfulConvert.Accounts);

                                //Get the Contacts ID of  Record
                                Console.WriteLine("LeadConvert Contacts ID: " + successfulConvert.Contacts);

                                //Get the Deals ID of  Record
                                Console.WriteLine("LeadConvert Deals ID: " + successfulConvert.Deals);
                            }
                            //Check if the request returned an exception
                            else if (convertActionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)convertActionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (convertActionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)convertActionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to download a photo associated with a module.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module</param>
        /// <param name="recordId">The ID of the record to be obtained.</param>
        /// <param name="destinationFolder">The absolute path of the destination folder to store the photo.</param>
        public static void GetPhoto(string moduleAPIName, long recordId, string destinationFolder)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //long recordId = 34772;
            //string destinationFolder = "/Users/user_name/Desktop";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Call GetPhoto method that takes recordId and moduleAPIName as parameters
            APIResponse<DownloadHandler> response = recordOperations.GetPhoto(recordId, moduleAPIName);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    DownloadHandler downloadHandler = response.Object;

                    if (downloadHandler is FileBodyWrapper)
                    {
                        //Get object from response
                        FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)downloadHandler;

                        //Get StreamWrapper instance from the returned FileBodyWrapper instance
                        StreamWrapper streamWrapper = fileBodyWrapper.File;

                        Stream file = streamWrapper.Stream;

                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);

                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
                        }
                    }
                    //Check if the request returned an exception
                    else if (downloadHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)downloadHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to attach a photo to a record. You must include the photo in the request with content type as multipart/form data.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module</param>
        /// <param name="recordId">The ID of the record to be obtained.</param>
        /// <param name="absoluteFilePath">The absolute file path of the file to be uploads</param>
        public static void UploadPhoto(string moduleAPIName, long recordId, string absoluteFilePath)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //long recordId = 3477002;
            //string absoluteFilePath = "/Users/use_name/Desktop/image.png";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of FileBodyWrapper class that will contain the request file
            FileBodyWrapper fileBodyWrapper = new FileBodyWrapper();

            //Get instance of StreamWrapper class that takes absolute path of the file to be attached as parameter
            StreamWrapper streamWrapper = new StreamWrapper(absoluteFilePath);

            //Set file to the FileBodyWrapper instance
            fileBodyWrapper.File = streamWrapper;

            //Call UploadPhoto method that takes recordId, moduleAPIName and FileBodyWrapper instance  as parameter
            APIResponse<FileHandler> response = recordOperations.UploadPhoto(recordId, moduleAPIName, fileBodyWrapper);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    FileHandler fileHandler = response.Object;

                    //Check if the request is successful
                    if (fileHandler is SuccessResponse)
                    {
                        //Get the received SuccessResponse instance
                        SuccessResponse successResponse = (SuccessResponse)fileHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + successResponse.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + successResponse.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + successResponse.Message.Value);
                    }
                    //Check if the request returned an exception
                    else if (fileHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)fileHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to delete a photo from a record in a module.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the record's module</param>
        /// <param name="recordId">The ID of the record to be obtained.</param>
        public static void DeletePhoto(string moduleAPIName, long recordId)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //long recordId = 3477002;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Call DeletePhoto method that takes recordId and moduleAPIName as parameter
            APIResponse<FileHandler> response = recordOperations.DeletePhoto(recordId, moduleAPIName);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    FileHandler fileHandler = response.Object;

                    //Check if the request is successful
                    if (fileHandler is SuccessResponse)
                    {
                        //Get the received SuccessResponse instance
                        SuccessResponse successResponse = (SuccessResponse)fileHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + successResponse.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + successResponse.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + successResponse.Message.Value);
                    }
                    //Check if the request returned an exception
                    else if (fileHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)fileHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to update the values of specific fields for multiple records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to obtain records.</param>
        public static void MassUpdateRecords(string moduleAPIName)
        {
            //API Name of the module to massUpdateRecords
            //string moduleAPIName = "module_api_name";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of MassUpdateBodyWrapper Class that will contain the request body
            MassUpdateBodyWrapper request = new MassUpdateBodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record1.AddKeyValue("City", "Value");

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //request.Cvid = "3477061087501";

            List<string> ids = new List<string>() { "3477061012421001" };

            request.Ids = ids;

            //		Territory territory = new Territory();
            //
            //		territory.Id = 34770615922192;
            //
            //		territory.IncludeChild = true;
            //
            //		request.Territory = territory;

            request.OverWrite = true;

            //Call MassUpdateRecords method that takes ModuleAPIName and BodyWrapper instance as parameter.
            APIResponse<MassUpdateActionHandler> response = recordOperations.MassUpdateRecords(moduleAPIName, request);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    MassUpdateActionHandler massUpdateActionHandler = response.Object;

                    if (massUpdateActionHandler is MassUpdateActionWrapper)
                    {
                        //Get the received MassUpdateActionWrapper instance
                        MassUpdateActionWrapper massUpdateActionWrapper = (MassUpdateActionWrapper)massUpdateActionHandler;

                        //Get the list of obtained MassUpdateActionResponse instances
                        List<MassUpdateActionResponse> massUpdateActionResponses = massUpdateActionWrapper.Data;

                        foreach (MassUpdateActionResponse massUpdateActionResponse in massUpdateActionResponses)
                        {
                            //Check if the request is successful
                            if (massUpdateActionResponse is MassUpdateSuccessResponse)
                            {
                                //Get the received MassUpdateSuccessResponse instance
                                MassUpdateSuccessResponse massUpdateSuccessResponse = (MassUpdateSuccessResponse)massUpdateActionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + massUpdateSuccessResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + massUpdateSuccessResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in massUpdateSuccessResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + massUpdateSuccessResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (massUpdateActionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)massUpdateActionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (massUpdateActionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)massUpdateActionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get the status of the mass update job scheduled previously and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to obtain records.</param>
        /// <param name="jobId">The ID of the job from the response of Mass Update Records.</param>
        public static void GetMassUpdateStatus(string moduleAPIName, string jobId)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //string jobId = "347002";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetMassUpdateStatusParam.JOB_ID, jobId);

            //Call GetMassUpdateStatus method that takes moduleAPIName and paramInstance as parameter
            APIResponse<MassUpdateResponseHandler> response = recordOperations.GetMassUpdateStatus(moduleAPIName, paramInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    MassUpdateResponseHandler massUpdateResponseHandler = response.Object;

                    if (massUpdateResponseHandler is MassUpdateResponseWrapper)
                    {
                        //Get the received MassUpdateResponseWrapper instance
                        MassUpdateResponseWrapper massUpdateResponseWrapper = (MassUpdateResponseWrapper)massUpdateResponseHandler;

                        //Get the list of obtained MassUpdateResponse instances
                        List<MassUpdateResponse> massUpdateResponses = massUpdateResponseWrapper.Data;

                        foreach (MassUpdateResponse massUpdateResponse in massUpdateResponses)
                        {
                            //Check if the request is successful
                            if (massUpdateResponse is MassUpdate)
                            {
                                //Get the received MassUpdate instance
                                MassUpdate massUpdate = (MassUpdate)massUpdateResponse;

                                //Get the Status of each MassUpdate
                                Console.WriteLine("MassUpdate Status: " + massUpdate.Status.Value);

                                //Get the FailedCount of each MassUpdate
                                Console.WriteLine("MassUpdate FailedCount: " + massUpdate.FailedCount);

                                //Get the UpdatedCount of each MassUpdate
                                Console.WriteLine("MassUpdate UpdatedCount: " + massUpdate.UpdatedCount);

                                //Get the NotUpdatedCount of each MassUpdate
                                Console.WriteLine("MassUpdate NotUpdatedCount: " + massUpdate.NotUpdatedCount);

                                //Get the TotalCount of each MassUpdate
                                Console.WriteLine("MassUpdate TotalCount: " + massUpdate.TotalCount);

                            }
                            //Check if the request returned an exception
                            else if (massUpdateResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)massUpdateResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (massUpdateResponseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)massUpdateResponseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void GetRecordCount()
        {
            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            String moduleAPIName = "Leads";

            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(RecordOperations.RecordCountParam.PHONE, "(990) -0");

            APIResponse<CountHandler> response = recordOperations.RecordCount(moduleAPIName, paramInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    CountHandler countHandler = response.Object;

                    if (countHandler is CountWrapper)
                    {
                        //Get the received CountWrapper instance
                        CountWrapper countWrapper = (CountWrapper)countHandler;

                        //Get the Count of Tag
                        Console.WriteLine("Record Count: " + countWrapper.Count);
                    }
                    //Check if the request returned an exception
                    else if (countHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)countHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void AssignTerritoriesToMultipleRecords(string moduleAPIName)
        {
            //API Name of the module to assignTerritoriesToMultipleRecords
            //string moduleAPIName = "Leads";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();


            //List of Record instances
            List<API.Record.Record> records = new List<API.Record.Record>();

            //Get instance of Record Class
            API.Record.Record record1 = new API.Record.Record();

            record1.Id = 3477061012107002;

            /*
             * Call addKeyValue method that takes two arguments
             * 1 -> A string that is the Field's API Name
             * 2 -> Value
             */

            API.Record.Territory territory = new API.Record.Territory();

            territory.Id = 34770613051397;

            record1.AddKeyValue("Territories", new List<API.Record.Territory>() { territory });

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call AssignTerritoriesToMultipleRecords method that takes ModuleAPIName and  BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = recordOperations.AssignTerritoriesToMultipleRecords(moduleAPIName, request);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void AssignTerritoryToRecord(string moduleAPIName, long id)
        {
            //API Name of the module to assignTerritoryToRecord
            //string moduleAPIName = "Leads";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<API.Record.Record> records = new List<API.Record.Record>();

            //Get instance of Record Class
            API.Record.Record record1 = new API.Record.Record();

            /*
             * Call addKeyValue method that takes two arguments
             * 1 -> A string that is the Field's API Name
             * 2 -> Value
             */

            API.Record.Territory territory = new API.Record.Territory();

            territory.Id = 34770613051397;

            record1.AddKeyValue("Territories", new List<API.Record.Territory>() { territory });

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call AssignTerritoryToRecord method that takes ModuleAPIName, id and  BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = recordOperations.AssignTerritoryToRecord(moduleAPIName, id, request);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                ///Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void RemoveTerritoriesFromMultipleRecords(string moduleAPIName)
        {
            //example
            //string moduleAPIName = "Leads";
            //long recordId = 34770615177002;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<API.Record.Record> records = new List<API.Record.Record>();

            //Get instance of Record Class
            API.Record.Record record1 = new API.Record.Record();

            record1.Id = 3477061012107002;

            /*
             * Call addKeyValue method that takes two arguments
             * 1 -> A string that is the Field's API Name
             * 2 -> Value
             */

            API.Record.Territory territory = new API.Record.Territory();

            territory.Id = 34770613051397;

            record1.AddKeyValue("Territories", new List<API.Record.Territory>() { territory });

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call RemoveTerritoriesFromMultipleRecords method that takes moduleAPIName and BodyWrapper instance as parameter
            APIResponse<ActionHandler> response = recordOperations.RemoveTerritoriesFromMultipleRecords(moduleAPIName, request);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void RemoveTerritoriesFromRecord(string moduleAPIName, long id)
        {
            //example
            //string moduleAPIName = "Leads";
            //long recordId = 34770615177002L;

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper request = new BodyWrapper();

            //List of Record instances
            List<API.Record.Record> records = new List<API.Record.Record>();

            //Get instance of Record Class
            API.Record.Record record1 = new API.Record.Record();

            /*
             * Call addKeyValue method that takes two arguments
             * 1 -> A string that is the Field's API Name
             * 2 -> Value
             */

            API.Record.Territory territory = new API.Record.Territory();

            territory.Id = 34770613051397;

            record1.AddKeyValue("Territories", new List<API.Record.Territory>() { territory });

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call RemoveTerritoriesFromRecord method that takes moduleAPIName, recordId and BodyWrapper instance as parameter
            APIResponse<ActionHandler> response = recordOperations.RemoveTerritoriesFromRecord(moduleAPIName, id, request);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if (actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if (actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if (actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: ");

                                //Get the details map
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }
    }
}
