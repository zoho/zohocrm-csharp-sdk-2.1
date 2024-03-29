﻿using System;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using System.Reflection;

using Com.Zoho.Crm.API;

using Com.Zoho.Crm.API.Attachments;

using Com.Zoho.Crm.API.Layouts;

using Com.Zoho.Crm.API.Record;

using Com.Zoho.Crm.API.RelatedRecords;

using Com.Zoho.Crm.API.Tags;

using Com.Zoho.Crm.API.Users;

using Com.Zoho.Crm.API.Util;

using Newtonsoft.Json;

using static Com.Zoho.Crm.API.RelatedRecords.RelatedRecordsOperations;

using ActionHandler = Com.Zoho.Crm.API.RelatedRecords.ActionHandler;

using ActionResponse = Com.Zoho.Crm.API.RelatedRecords.ActionResponse;

using ActionWrapper = Com.Zoho.Crm.API.RelatedRecords.ActionWrapper;

using APIException = Com.Zoho.Crm.API.RelatedRecords.APIException;

using BodyWrapper = Com.Zoho.Crm.API.RelatedRecords.BodyWrapper;

using FileBodyWrapper = Com.Zoho.Crm.API.RelatedRecords.FileBodyWrapper;

using Info = Com.Zoho.Crm.API.Record.Info;

using ResponseHandler = Com.Zoho.Crm.API.RelatedRecords.ResponseHandler;

using ResponseWrapper = Com.Zoho.Crm.API.RelatedRecords.ResponseWrapper;

using SuccessResponse = Com.Zoho.Crm.API.RelatedRecords.SuccessResponse;

namespace Com.Zoho.Crm.Sample.RelatedRecords
{
    public class RelatedRecords
    {
        /// <summary>
        /// This method is used to get the related list records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to get related records.</param>
        /// <param name="RelatedRecordId">The ID of the record to be obtained.</param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        public static void GetRelatedRecords(string moduleAPIName, long recordId, string relatedListAPIName)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //long recordId = 34777002;
            //string relatedListAPIName = "module_api_name";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, recordId and moduleAPIName as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, null);

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            //paramInstance.Add(GetRelatedRecordsParam.PAGE, 1);

            //paramInstance.Add(GetRelatedRecordsParam.PER_PAGE, 2);

            //Get instance of HeaderMap Class
            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            //headerInstance.Add(GetRelatedRecordsHeader.IF_MODIFIED_SINCE, ifModifiedSince);

            //Call GetRelatedRecords method that takes paramInstance, headerInstance as parameter
            APIResponse<ResponseHandler> response = relatedRecordsOperations.GetRelatedRecords(recordId, paramInstance, headerInstance);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if(new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if(responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;

                        //Get the obtained Record instance
                        List<API.Record.Record> records = responseWrapper.Data;

                        foreach(API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("RelatedRecord ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("RelatedRecord CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("RelatedRecord ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if(tags != null)
                            {
                                foreach(Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                }
                            }

                            //To get particular field value
                            Console.WriteLine("RelatedRecord Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("RelatedRecord KeyValues: " );

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
                                                    Console.WriteLine("RelatedRecord FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("RelatedRecord PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("RelatedRecord PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("RelatedRecord PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("RelatedRecord PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("RelatedRecord Participants Name: " + participant.Name);

                                                    Console.WriteLine("RelatedRecord Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("RelatedRecord Participants ID: " + participant.Id);

                                                    Console.WriteLine("RelatedRecord Participants Type: " + participant.Type);

                                                    Console.WriteLine("RelatedRecord Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("RelatedRecord Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {

                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Value: " + lineTax.Value);
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
                                            else if(dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>) dataList;

                                                foreach(Comment comment in comments)
                                                {
                                                    Console.WriteLine("RelatedRecord Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("RelatedRecord Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("RelatedRecord Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("RelatedRecord Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if(dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>) dataList;;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if(owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("RelatedRecord Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("RelatedRecord Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if(parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("RelatedRecord Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if(modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if(createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if(dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>) dataList;

                                                foreach(ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("RelatedRecord " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("RelatedRecord " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("RelatedRecord " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("RelatedRecord " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("RelatedRecord " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("RelatedRecord " + keyName + "FileId: " + imageUpload.FileId);
                                                }
                                            }
                                            else if (dataList[0] is API.Record.Record)
                                            {

                                                List<API.Record.Record> recordList = (List<API.Record.Record>)dataList;

                                                foreach (API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + entry1.Value);
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
                                            Console.WriteLine("RelatedRecord " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("RelatedRecord " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Email: " + user.Email);
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
                                            Console.WriteLine("RelatedRecord Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("RelatedRecord Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("RelatedRecord Consent Owner Email: " + owner.Email);
                                        }

                                        Console.WriteLine("RelatedRecord Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("RelatedRecord Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("RelatedRecord Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("RelatedRecord Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("RelatedRecord Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("RelatedRecord Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("RelatedRecord Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("RelatedRecord Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("RelatedRecord Consent DataProcessingBasis: " + consent.DataProcessingBasis);
                                    }
                                    else if (value is API.Record.Record)
                                    {
                                        API.Record.Record recordValue = (API.Record.Record)value;

                                        Console.WriteLine("RelatedRecord " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("RelatedRecord " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine("RelatedRecord " + keyName + ": " + prop.GetValue(value));
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }

                        //Get the object obtained Info instance
                        Info info = responseWrapper.Info;

                        //Check if info is not null
                        if(info != null)
                        {
                            //Get the PerPage of the Info
                            Console.WriteLine("RelatedRecord Info PerPage: " + info.PerPage);

                            //Get the Count of the Info
                            Console.WriteLine("RelatedRecord Info Count: " + info.Count);

                            //Get the Page of the Info
                            Console.WriteLine("RelatedRecord Info Page: " + info.Page);

                            //Get the MoreRecords of the Info
                            Console.WriteLine("RelatedRecord Info MoreRecords: " + info.MoreRecords);
                        }
                    }
                    //Check if the request returned an exception
                    else if(responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to update the relation between the records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to update related records.</param>
        /// <param name="RelatedRecordId">The ID of the record to be obtained.</param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        public static void UpdateRelatedRecords(string moduleAPIName, long recordId, string relatedListAPIName)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //long recordId = 347002;
            //string relatedListAPIName = "module_api_name";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, recordId and moduleAPIName as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, null);

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
            record1.AddKeyValue("id", 3477061012412001);

            record1.AddKeyValue("list_price", 50.56);

            //Add Record instance to the list
            records.Add(record1);

            //Get instance of Record Class
            API.Record.Record record2 = new API.Record.Record();

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record2.AddKeyValue("id", 3477061012255023);

            record2.AddKeyValue("list_price", 50.56);

            //Add Record instance to the list
            records.Add(record2);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call UpdateRelatedRecords method that takes BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecords(recordId, request);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to delete the association between modules and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to delink related records.</param>
        /// <param name="RelatedRecordId">The ID of the record</param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="relatedListIds">The List of related record IDs.</param>
        public static void DelinkRecords(string moduleAPIName, long recordId, string relatedListAPIName, List<string> relatedListIds)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //long recordId = 347702;
            //string relatedListAPIName = "module_api_name";
            //List<string> relatedListIds = new List<string>(){"34779001", "3477011"};

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, recordId and moduleAPIName as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, null);

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            foreach(string relatedListId in relatedListIds)
            {
                paramInstance.Add(DelinkRecordsParam.IDS, relatedListId);
            }

            //Call DelinkRecords method that takes paramInstance instance as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.DelinkRecords(recordId, paramInstance);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to get the related list records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to get related records.</param>
        /// <param name="externalValue"></param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        public static void GetRelatedRecordsUsingExternalId(string moduleAPIName, string externalValue, string relatedListAPIName)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //string externalValue = "34777002";
            //string relatedListAPIName = "module_api_name";
            string xExternal = "Products.Products_External";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, moduleAPIName and xExternal as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, xExternal);

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            //paramInstance.Add(GetRelatedRecordsParam.PAGE, 1);

            //paramInstance.Add(GetRelatedRecordsParam.PER_PAGE, 2);

            //Get instance of HeaderMap Class
            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            //headerInstance.Add(GetRelatedRecordsHeader.IF_MODIFIED_SINCE, ifModifiedSince);

            //Call GetRelatedRecordsUsingExternalId method that takes externalValue, paramInstance and headerInstance as parameter
            APIResponse<ResponseHandler> response = relatedRecordsOperations.GetRelatedRecordsUsingExternalId(externalValue, paramInstance, headerInstance);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if(new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if(responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;

                        //Get the obtained Record instance
                        List<API.Record.Record> records = responseWrapper.Data;

                        foreach(API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("RelatedRecord ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("RelatedRecord CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("RelatedRecord ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if(tags != null)
                            {
                                foreach(Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                }
                            }

                            //To get particular field value
                            Console.WriteLine("RelatedRecord Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("RelatedRecord KeyValues: " );

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
                                                    Console.WriteLine("RelatedRecord FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("RelatedRecord PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("RelatedRecord PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("RelatedRecord PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("RelatedRecord PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("RelatedRecord Participants Name: " + participant.Name);

                                                    Console.WriteLine("RelatedRecord Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("RelatedRecord Participants ID: " + participant.Id);

                                                    Console.WriteLine("RelatedRecord Participants Type: " + participant.Type);

                                                    Console.WriteLine("RelatedRecord Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("RelatedRecord Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {

                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Value: " + lineTax.Value);
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
                                            else if(dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>) dataList;

                                                foreach(Comment comment in comments)
                                                {
                                                    Console.WriteLine("RelatedRecord Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("RelatedRecord Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("RelatedRecord Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("RelatedRecord Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if(dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>) dataList;;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if(owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("RelatedRecord Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("RelatedRecord Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if(parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("RelatedRecord Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if(modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if(createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if(dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>) dataList;

                                                foreach(ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("RelatedRecord " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("RelatedRecord " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("RelatedRecord " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("RelatedRecord " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("RelatedRecord " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("RelatedRecord " + keyName + "FileId: " + imageUpload.FileId);
                                                }
                                            }
                                            else if (dataList[0] is API.Record.Record)
                                            {

                                                List<API.Record.Record> recordList = (List<API.Record.Record>)dataList;

                                                foreach (API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + entry1.Value);
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
                                            Console.WriteLine("RelatedRecord " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("RelatedRecord " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Email: " + user.Email);
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
                                            Console.WriteLine("RelatedRecord Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("RelatedRecord Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("RelatedRecord Consent Owner Email: " + owner.Email);
                                        }

                                        Console.WriteLine("RelatedRecord Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("RelatedRecord Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("RelatedRecord Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("RelatedRecord Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("RelatedRecord Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("RelatedRecord Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("RelatedRecord Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("RelatedRecord Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("RelatedRecord Consent DataProcessingBasis: " + consent.DataProcessingBasis);
                                    }
                                    else if (value is API.Record.Record)
                                    {
                                        API.Record.Record recordValue = (API.Record.Record)value;

                                        Console.WriteLine("RelatedRecord " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("RelatedRecord " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine("RelatedRecord " + keyName + ": " + prop.GetValue(value));
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }

                        //Get the object obtained Info instance
                        Info info = responseWrapper.Info;

                        //Check if info is not null
                        if(info != null)
                        {
                            //Get the PerPage of the Info
                            Console.WriteLine("RelatedRecord Info PerPage: " + info.PerPage);

                            //Get the Count of the Info
                            Console.WriteLine("RelatedRecord Info Count: " + info.Count);

                            //Get the Page of the Info
                            Console.WriteLine("RelatedRecord Info Page: " + info.Page);

                            //Get the MoreRecords of the Info
                            Console.WriteLine("RelatedRecord Info MoreRecords: " + info.MoreRecords);
                        }
                    }
                    //Check if the request returned an exception
                    else if(responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to update the relation between the records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to update related records.</param>
        /// <param name="externalValue"></param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        public static void UpdateRelatedRecordsUsingExternalId(string moduleAPIName, string externalValue, string relatedListAPIName)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //string externalValue = "347002";
            //string relatedListAPIName = "module_api_name";
            string xExternal = "Leads.External,Products.Products_External";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, moduleAPIName and xExternal as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, xExternal);

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
            record1.AddKeyValue("Products_External", "Products_External");

            record1.AddKeyValue("list_price", 50.56);

            //Add Record instance to the list
            records.Add(record1);

            //Get instance of Record Class
            API.Record.Record record2 = new API.Record.Record();

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */
            record2.AddKeyValue("Products_External", "AutomatedSDKExternal");

            record2.AddKeyValue("list_price", 50.56);

            //Add Record instance to the list
            records.Add(record2);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call UpdateRelatedRecordsUsingExternalId method that takes externalValue and BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecordsUsingExternalId(externalValue, request);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to delete the association between modules and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to delink related records.</param>
        /// <param name="externalValue"></param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="relatedListIds">The List of related record IDs.</param>
        public static void DeleteRelatedRecordsUsingExternalId(string moduleAPIName, string externalValue, string relatedListAPIName, List<string> relatedListIds)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //string externalValue = "347702";
            //string relatedListAPIName = "module_api_name";
            //List<string> relatedListIds = new List<string>(){"34779001", "3477011"};
            String xExternal = "Leads.External,Products.Products_External";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, moduleAPIName and xExternal as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, xExternal);

            //Get instance of ParameterMap Class
            ParameterMap paramInstance = new ParameterMap();

            foreach(string relatedListId in relatedListIds)
            {
                paramInstance.Add(DelinkRecordsParam.IDS, relatedListId);
            }

            //Call DeleteRelatedRecordsUsingExternalId method that takes externalValue and paramInstance instance as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.DeleteRelatedRecordsUsingExternalId(externalValue, paramInstance);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to get the single related list record and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to get related record.</param>
        /// <param name="recordId">The ID of the record to be get Related List.</param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="relatedListId">The ID of the related record.</param>
        /// <param name="destinationFolder">The absolute path of the destination folder to store the file</param>
        public static void GetRelatedRecord(string moduleAPIName, long recordId, string relatedListAPIName, long relatedListId, string destinationFolder)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //long recordId = 347002;
            //string relatedListAPIName = "module_api_name";
            //long relatedListId = 344115;
            //string destinationFolder = "/Users/user_name/Desktop";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, recordId and moduleAPIName as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, null);

            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetRelatedRecordHeader.IF_MODIFIED_SINCE, ifModifiedSince);

            //Call GetRelatedRecord method that takes relatedRecordId and headerInstance as parameter
            APIResponse<ResponseHandler> response = relatedRecordsOperations.GetRelatedRecord(relatedListId, recordId, headerInstance);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if(new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if(responseHandler is ResponseWrapper)
                    {
                        ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;

                        //Get the obtained Record instance
                        List<API.Record.Record> records = responseWrapper.Data;

                        foreach(API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("RelatedRecord ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("RelatedRecord CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("RelatedRecord ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if(tags != null)
                            {
                                foreach(Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                }
                            }

                            //To get particular field value
                            Console.WriteLine("RelatedRecord Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("RelatedRecord KeyValues: " );

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
                                                    Console.WriteLine("RelatedRecord FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("RelatedRecord PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("RelatedRecord PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("RelatedRecord PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("RelatedRecord PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("RelatedRecord Participants Name: " + participant.Name);

                                                    Console.WriteLine("RelatedRecord Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("RelatedRecord Participants ID: " + participant.Id);

                                                    Console.WriteLine("RelatedRecord Participants Type: " + participant.Type);

                                                    Console.WriteLine("RelatedRecord Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("RelatedRecord Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {
                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Value: " + lineTax.Value);
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
                                            else if(dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>) dataList;

                                                foreach(Comment comment in comments)
                                                {
                                                    Console.WriteLine("RelatedRecord Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("RelatedRecord Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("RelatedRecord Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("RelatedRecord Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if(dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>) dataList;;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if(owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("RelatedRecord Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("RelatedRecord Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if(parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("RelatedRecord Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if(modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if(createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if(dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>) dataList;

                                                foreach(Comment comment in comments)
                                                {
                                                    Console.WriteLine("RelatedRecord Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("RelatedRecord Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("RelatedRecord Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("RelatedRecord Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if(dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>) dataList;;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if(owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("RelatedRecord Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("RelatedRecord Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if(parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("RelatedRecord Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if(modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if(createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is API.Record.Record)
                                            {
                                                List<API.Record.Record> recordList = (List<API.Record.Record>)dataList;

                                                foreach (API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + entry1.Value);
                                                    }
                                                }
                                            }
                                            else if(dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>) dataList;

                                                foreach(ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("RelatedRecord " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("RelatedRecord " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("RelatedRecord " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("RelatedRecord " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("RelatedRecord " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("RelatedRecord " + keyName + "FileId: " + imageUpload.FileId);
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
                                            Console.WriteLine("RelatedRecord " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("RelatedRecord " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Email: " + user.Email);
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
                                        Console.WriteLine("RelatedRecord Consent Owner Name: " + owner.Name);

                                        //Get the ID of the owner User
                                        Console.WriteLine("RelatedRecord Consent Owner ID: " + owner.Id);

                                        //Get the Email of the owner User
                                        Console.WriteLine("RelatedRecord Consent Owner Email: " + owner.Email);
                                    }

                                    User consentCreatedBy = consent.CreatedBy;

                                    //Check if createdBy is not null
                                    if(consentCreatedBy != null)
                                    {
                                        //Get the name of the CreatedBy User
                                        Console.WriteLine("RelatedRecord Consent CreatedBy Name: " + consentCreatedBy.Name);

                                        //Get the ID of the CreatedBy User
                                        Console.WriteLine("RelatedRecord Consent CreatedBy ID: " + consentCreatedBy.Id);

                                        //Get the Email of the CreatedBy User
                                        Console.WriteLine("RelatedRecord Consent CreatedBy Email: " + consentCreatedBy.Email);
                                    }

                                    User consentModifiedBy = consent.ModifiedBy;

                                    //Check if createdBy is not null
                                    if(consentModifiedBy != null)
                                    {
                                        //Get the name of the ModifiedBy User
                                        Console.WriteLine("RelatedRecord Consent ModifiedBy Name: " + consentModifiedBy.Name);

                                        //Get the ID of the ModifiedBy User
                                        Console.WriteLine("RelatedRecord Consent ModifiedBy ID: " + consentModifiedBy.Id);

                                        //Get the Email of the ModifiedBy User
                                        Console.WriteLine("RelatedRecord Consent ModifiedBy Email: " + consentModifiedBy.Email);
                                    }

                                    Console.WriteLine("RelatedRecord Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                    Console.WriteLine("RelatedRecord Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                    Console.WriteLine("RelatedRecord Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                    Console.WriteLine("RelatedRecord Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                    Console.WriteLine("RelatedRecord Consent MailSentTime: " + consent.MailSentTime.ToString());

                                    Console.WriteLine("RelatedRecord Consent ConsentDate: " + consent.ConsentDate.ToString());

                                    Console.WriteLine("RelatedRecord Consent ConsentRemarks: " + consent.ConsentRemarks);

                                    Console.WriteLine("RelatedRecord Consent ConsentThrough: " + consent.ConsentThrough);

                                    Console.WriteLine("RelatedRecord Consent DataProcessingBasis: " + consent.DataProcessingBasis);

                                    //To get custom values
									Console.WriteLine("RelatedRecord Consent Lawful Reason: " + consent.GetKeyValue("Lawful_Reason"));
                                }
                                else if (value is API.Record.Record)
                                {
                                        API.Record.Record recordValue = (API.Record.Record)value;

                                        Console.WriteLine("RelatedRecord " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("RelatedRecord " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                }
                                else if (value.GetType().FullName.Contains("Choice"))
                                {
                                    Type type = value.GetType();

                                    PropertyInfo prop = type.GetProperty("Value");

                                    Console.WriteLine(keyName + ": " + prop.GetValue(value));
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
                    else if (responseHandler is FileBodyWrapper)
                    {
                        //Get object from response
                        FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)responseHandler;

                        //Get StreamWrapper instance from the returned FileBodyWrapper instance
                        StreamWrapper streamWrapper = fileBodyWrapper.File;

                        Stream file = streamWrapper.Stream;

                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);

                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
                        }
                    }
                    else if(responseHandler is APIException)
                    {
                        APIException exception = (APIException) responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to update the relation between the records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to update related record.</param>
        /// <param name="recordId">The ID of the record to be obtained.</param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="relatedListId">The ID of the related record.</param>
        public static void UpdateRelatedRecord(string moduleAPIName, long recordId, string relatedListAPIName, long relatedListId)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //long recordId = 347002;
            //string relatedListAPIName = "module_api_name";
            //long relatedListId = 34794115;

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, recordId and moduleAPIName as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, null);

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

            record1.AddKeyValue("list_price", 50.56);

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call UpdateRelatedRecord method that takes relatedRecordId and BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecord(relatedListId, recordId, request);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to delete the association between modules and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to delink related record.</param>
        /// <param name="recordId">The ID of the record to be obtained.</param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="relatedListId">The ID of the related record.</param>
        public static void DelinkRecord(string moduleAPIName, long recordId, string relatedListAPIName, long relatedListId)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //long recordId = 3477177002;
            //string relatedListAPIName = "module_api_name";
            //long relatedListId = 3115;

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, recordId and moduleAPIName as parameter
            RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, null);

            //Call DelinkRecord method that takes relatedListId as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.DelinkRecord(relatedListId, recordId);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to get the single related list record and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to get related record.</param>
        /// <param name="externalValue"></param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="externalFieldValue"></param>
        /// <param name="destinationFolder">The absolute path of the destination folder to store the file</param>
        public static void GetRelatedRecordUsingExternalId(string moduleAPIName, string externalValue, string relatedListAPIName, string externalFieldValue, string destinationFolder)
        {
            //example
            //string moduleAPIName = "module_api_name";
            //string externalValue = "34770615177002";
            //string relatedListAPIName = "module_api_name";
            //string externalFieldValue = "34770614994115";
            //string destinationFolder = "/Users/user_name/Desktop";
            string xExternal = "Leads.External,Products.Products_External";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName moduleAPIName as parameter
    	    RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, xExternal);

            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifModifiedSince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetRelatedRecordHeader.IF_MODIFIED_SINCE, ifModifiedSince);

            //Call GetRelatedRecordUsingExternalId method that takes externalFieldValue, externalValue and headerInstance as parameter
            APIResponse<ResponseHandler> response = relatedRecordsOperations.GetRelatedRecordUsingExternalId(externalFieldValue, externalValue, headerInstance);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if(new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if(responseHandler is ResponseWrapper)
                    {
                        ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;

                        //Get the obtained Record instance
                        List<API.Record.Record> records = responseWrapper.Data;

                        foreach(API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("RelatedRecord ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("RelatedRecord Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("RelatedRecord CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("RelatedRecord Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("RelatedRecord ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if(tags != null)
                            {
                                foreach(Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                }
                            }

                            //To get particular field value
                            Console.WriteLine("RelatedRecord Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("RelatedRecord KeyValues: " );

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
                                                    Console.WriteLine("RelatedRecord FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("RelatedRecord FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("RelatedRecord Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("RelatedRecord Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("RelatedRecord PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("RelatedRecord PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("RelatedRecord PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("RelatedRecord PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("RelatedRecord Participants Name: " + participant.Name);

                                                    Console.WriteLine("RelatedRecord Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("RelatedRecord Participants ID: " + participant.Id);

                                                    Console.WriteLine("RelatedRecord Participants Type: " + participant.Type);

                                                    Console.WriteLine("RelatedRecord Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("RelatedRecord Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {
                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("RelatedRecord ProductDetails LineTax Value: " + lineTax.Value);
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
                                            else if(dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>) dataList;

                                                foreach(Comment comment in comments)
                                                {
                                                    Console.WriteLine("RelatedRecord Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("RelatedRecord Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("RelatedRecord Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("RelatedRecord Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if(dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>) dataList;;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if(owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("RelatedRecord Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("RelatedRecord Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if(parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("RelatedRecord Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if(modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if(createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if(dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>) dataList;

                                                foreach(Comment comment in comments)
                                                {
                                                    Console.WriteLine("RelatedRecord Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("RelatedRecord Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("RelatedRecord Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("RelatedRecord Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if(dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>) dataList;;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if(owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("RelatedRecord Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("RelatedRecord Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("RelatedRecord Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if(parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("RelatedRecord Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("RelatedRecord Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if(modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("RelatedRecord Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if(createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("RelatedRecord Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("RelatedRecord Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is API.Record.Record)
                                            {
                                                List<API.Record.Record> recordList = (List<API.Record.Record>)dataList;

                                                foreach (API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + entry1.Value);
                                                    }
                                                }
                                            }
                                            else if(dataList[0] is ImageUpload)
                                            {
                                                List<ImageUpload> imageUploads = (List<ImageUpload>) dataList;

                                                foreach(ImageUpload imageUpload in imageUploads)
                                                {
                                                    Console.WriteLine("RelatedRecord " + keyName + "Description: " + imageUpload.Description);

                                                    Console.WriteLine("RelatedRecord " + keyName + "PreviewId: " + imageUpload.PreviewId);

                                                    Console.WriteLine("RelatedRecord " + keyName + "File_Name: " + imageUpload.FileName);

                                                    Console.WriteLine("RelatedRecord " + keyName + "State: " + imageUpload.State);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Size: " + imageUpload.Size);

                                                    Console.WriteLine("RelatedRecord " + keyName + "SequenceNumber: " + imageUpload.SequenceNumber);

                                                    Console.WriteLine("RelatedRecord " + keyName + "Id: " + imageUpload.Id);

                                                    Console.WriteLine("RelatedRecord " + keyName + "FileId: " + imageUpload.FileId);
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
                                            Console.WriteLine("RelatedRecord " + keyName + " ID: " + layout.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " Name: " + layout.Name);
                                        }
                                    }
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("RelatedRecord " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("RelatedRecord " + keyName + " User-Email: " + user.Email);
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
                                        Console.WriteLine("RelatedRecord Consent Owner Name: " + owner.Name);

                                        //Get the ID of the owner User
                                        Console.WriteLine("RelatedRecord Consent Owner ID: " + owner.Id);

                                        //Get the Email of the owner User
                                        Console.WriteLine("RelatedRecord Consent Owner Email: " + owner.Email);
                                    }

                                    User consentCreatedBy = consent.CreatedBy;

                                    //Check if createdBy is not null
                                    if(consentCreatedBy != null)
                                    {
                                        //Get the name of the CreatedBy User
                                        Console.WriteLine("RelatedRecord Consent CreatedBy Name: " + consentCreatedBy.Name);

                                        //Get the ID of the CreatedBy User
                                        Console.WriteLine("RelatedRecord Consent CreatedBy ID: " + consentCreatedBy.Id);

                                        //Get the Email of the CreatedBy User
                                        Console.WriteLine("RelatedRecord Consent CreatedBy Email: " + consentCreatedBy.Email);
                                    }

                                    User consentModifiedBy = consent.ModifiedBy;

                                    //Check if createdBy is not null
                                    if(consentModifiedBy != null)
                                    {
                                        //Get the name of the ModifiedBy User
                                        Console.WriteLine("RelatedRecord Consent ModifiedBy Name: " + consentModifiedBy.Name);

                                        //Get the ID of the ModifiedBy User
                                        Console.WriteLine("RelatedRecord Consent ModifiedBy ID: " + consentModifiedBy.Id);

                                        //Get the Email of the ModifiedBy User
                                        Console.WriteLine("RelatedRecord Consent ModifiedBy Email: " + consentModifiedBy.Email);
                                    }

                                    Console.WriteLine("RelatedRecord Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                    Console.WriteLine("RelatedRecord Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                    Console.WriteLine("RelatedRecord Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                    Console.WriteLine("RelatedRecord Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                    Console.WriteLine("RelatedRecord Consent MailSentTime: " + consent.MailSentTime.ToString());

                                    Console.WriteLine("RelatedRecord Consent ConsentDate: " + consent.ConsentDate.ToString());

                                    Console.WriteLine("RelatedRecord Consent ConsentRemarks: " + consent.ConsentRemarks);

                                    Console.WriteLine("RelatedRecord Consent ConsentThrough: " + consent.ConsentThrough);

                                    Console.WriteLine("RelatedRecord Consent DataProcessingBasis: " + consent.DataProcessingBasis);

                                    //To get custom values
									Console.WriteLine("RelatedRecord Consent Lawful Reason: " + consent.GetKeyValue("Lawful_Reason"));
                                }
                                else if (value is API.Record.Record)
                                {
                                        API.Record.Record recordValue = (API.Record.Record)value;

                                        Console.WriteLine("RelatedRecord " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("RelatedRecord " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                }
                                else if (value.GetType().FullName.Contains("Choice"))
                                {
                                    Type type = value.GetType();

                                    PropertyInfo prop = type.GetProperty("Value");

                                    Console.WriteLine(keyName + ": " + prop.GetValue(value));
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
                    else if (responseHandler is FileBodyWrapper)
                    {
                        //Get object from response
                        FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)responseHandler;

                        //Get StreamWrapper instance from the returned FileBodyWrapper instance
                        StreamWrapper streamWrapper = fileBodyWrapper.File;

                        Stream file = streamWrapper.Stream;

                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);

                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
                        }
                    }
                    else if(responseHandler is APIException)
                    {
                        APIException exception = (APIException) responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to update the relation between the records and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to update related record.</param>
        /// <param name="externalValue"></param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="externalFieldValue"></param>
        public static void UpdateRelatedRecordUsingExternalId(string moduleAPIName, string externalValue, string relatedListAPIName, string externalFieldValue)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //string externalValue = "34770615177002";
            //string relatedListAPIName = "module_api_name";
            //string externalFieldValue = "34770614994115";
            String xExternal = "Leads.External,Products.Products_External";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName, moduleAPIName and xExternal as parameter
    	    RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, xExternal);

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

            record1.AddKeyValue("list_price", 50.56);

            //Add Record instance to the list
            records.Add(record1);

            //Set the list to Records in BodyWrapper instance
            request.Data = records;

            //Call UpdateRelatedRecordUsingExternalId method that takes externalFieldValue, externalValue and BodyWrapper instance as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecordUsingExternalId(externalFieldValue, externalValue, request);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
        /// This method is used to delete the association between modules and print the response.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to delink related record.</param>
        /// <param name="externalValue"></param>
        /// <param name="relatedListAPIName">The API name of the related list. To get the API name of the related list.</param>
        /// <param name="externalFieldValue"></param>
        public static void DeleteRelatedRecordUsingExternalId(string moduleAPIName, string externalValue, string relatedListAPIName, string externalFieldValue)
        {
            //API Name of the module
            //string moduleAPIName = "module_api_name";
            //string externalValue = "34770615177002";
            //string relatedListAPIName = "module_api_name";
            //string externalFieldValue = "34770614994115";
            string xExternal = "Leads.External,Products.Products_External";

            //Get instance of RelatedRecordsOperations Class that takes relatedListAPIName and moduleAPIName as parameter
    	    RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName, xExternal);

            //Call DeleteRelatedRecordUsingExternalId method that takes externalFieldValue and externalValue as parameter.
            APIResponse<ActionHandler> response = relatedRecordsOperations.DeleteRelatedRecordUsingExternalId(externalFieldValue, externalValue);

            if(response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                //Check if expected response is received
                if(response.IsExpected)
                {
                    //Get object from response
                    ActionHandler actionHandler = response.Object;

                    if(actionHandler is ActionWrapper)
                    {
                        //Get the received ActionWrapper instance
                        ActionWrapper actionWrapper = (ActionWrapper) actionHandler;

                        //Get the list of obtained ActionResponse instances
                        List<ActionResponse> actionResponses = actionWrapper.Data;

                        foreach(ActionResponse actionResponse in actionResponses)
                        {
                            //Check if the request is successful
                            if(actionResponse is SuccessResponse)
                            {
                                //Get the received SuccessResponse instance
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + successResponse.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + successResponse.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in successResponse.Details)
                                {
                                    //Get each value in the map
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }

                                //Get the Message
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            //Check if the request returned an exception
                            else if(actionResponse is APIException)
                            {
                                //Get the received APIException instance
                                APIException exception = (APIException) actionResponse;

                                //Get the Status
                                Console.WriteLine("Status: " + exception.Status.Value);

                                //Get the Code
                                Console.WriteLine("Code: " + exception.Code.Value);

                                Console.WriteLine("Details: " );

                                //Get the details map
                                foreach(KeyValuePair<string, object> entry in exception.Details)
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
                    else if(actionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) actionHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
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
