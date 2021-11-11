using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.EmailTemplates;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations;
using static Com.Zoho.Crm.API.EmailTemplates.EmailTemplatesOperations;
using APIException = Com.Zoho.Crm.API.EmailTemplates.APIException;
using ResponseHandler = Com.Zoho.Crm.API.EmailTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EmailTemplates.ResponseWrapper;

namespace Com.Zoho.Crm.Sample.EmailTemplates
{
    public class EmailTemplate
    {
        public static void GetEmailTemplates(string moduleAPIName)
        {
            //Get instance of EmailTemplatesOperations Class
            EmailTemplatesOperations emailTemplatesOperations = new EmailTemplatesOperations();

            ParameterMap parameter = new ParameterMap();

            parameter.Add(GetEmailTemplatesParam.MODULE, moduleAPIName);

            //Call GetEmailTemplates method
            APIResponse<ResponseHandler> response = emailTemplatesOperations.GetEmailTemplates(parameter);

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
                        ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;

                        //Get the list of obtained EmailTemplate instances
                        List<API.EmailTemplates.EmailTemplate> emailTemplates = responseWrapper.EmailTemplates;

                        foreach (API.EmailTemplates.EmailTemplate emailTemplate in emailTemplates)
                        {
                            //Get the CreatedTime of each EmailTemplate
                            Console.WriteLine("EmailTemplate CreatedTime: " + emailTemplate.CreatedTime);

                            List<Attachment> attachments = emailTemplate.Attachments;

                            if(attachments != null)
                            {
                                foreach(Attachment attachment in attachments)
                                {
                                    //Get the Size of each Attachment
                                    Console.WriteLine("Attachment Size: " + attachment.Size);

                                    //Get the FileId of each Attachment
                                    Console.WriteLine("Attachment FileId: " + attachment.FileId);

                                    //Get the FileName of each Attachment
                                    Console.WriteLine("Attachment FileName: " + attachment.FileName);

                                    //Get the Id of each Attachment
                                    Console.WriteLine("Attachment ID: " + attachment.Id);
                                }
                            }

                            //Get the Subject of each EmailTemplate
                            Console.WriteLine("EmailTemplate Subject: " + emailTemplate.Subject);

                            API.Modules.Module module = emailTemplate.Module;

                            if(module != null)
                            {
                                //Get the Module Name of the EmailTemplate
                                Console.WriteLine("EmailTemplate Module Name : " + module.APIName);

                                //Get the Module Id of the EmailTemplate
                                Console.WriteLine("EmailTemplate Module Id : " + module.Id);
                            }

                            //Get the Type of each EmailTemplate
                            Console.WriteLine("EmailTemplate Type: " + emailTemplate.Type);

                            //Get the CreatedBy User instance of each EmailTemplate
                            User createdBy = emailTemplate.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the Id of the CreatedBy User
                                Console.WriteLine("EmailTemplate Created By User-ID: " + createdBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("EmailTemplate Created By User-Name: " + createdBy.Name);

                                Console.WriteLine("EmailTemplate Created By User-Email : " + createdBy.Email);
                            }

                            //Get the ModifiedTime of each EmailTemplate
                            Console.WriteLine("EmailTemplate ModifiedTime: " + emailTemplate.ModifiedTime);

                            //Get the Folder instance of each EmailTemplate
                            API.EmailTemplates.EmailTemplate folder = emailTemplate.Folder;

                            //Check if folder is not null
                            if(folder != null)
                            {
                                //Get the Id of the Folder
                                Console.WriteLine("EmailTemplate Folder Id: " + folder.Id);

                                //Get the Name of the Folder
                                Console.WriteLine("EmailTemplate Folder Name: " + folder.Name);
                            }

                            //Get the LastUsageTime of each EmailTemplate
                            Console.WriteLine("EmailTemplate LastUsageTime: " + emailTemplate.LastUsageTime);

                            //Get the Associated of each EmailTemplate
                            Console.WriteLine("EmailTemplate Associated: " + emailTemplate.Associated);

                            //Get the name of each EmailTemplate
                            Console.WriteLine("EmailTemplate Name: " + emailTemplate.Name);

                            //Get the ConsentLinked of each EmailTemplate
                            Console.WriteLine("EmailTemplate ConsentLinked: " + emailTemplate.ConsentLinked);

                            //Get the modifiedBy User instance of each EmailTemplate
                            User modifiedBy = emailTemplate.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the ModifiedBy User
                                Console.WriteLine("EmailTemplate Modified By User-ID: " + modifiedBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("EmailTemplate Modified By User-Name: " + modifiedBy.Name);

                                Console.WriteLine("EmailTemplate Modified By User-Email : " + modifiedBy.Email);
                            }

                            //Get the ID of each EmailTemplate
                            Console.WriteLine("EmailTemplate ID: " + emailTemplate.Id);

                            //Get the EditorMode each EmailTemplate
                            Console.WriteLine("EmailTemplate EditorMode: " + emailTemplate.EditorMode);

                            Console.WriteLine("EmailTemplate Content: " + emailTemplate.Content);

                            // Get the Description of each EmailTemplate
                            Console.WriteLine("EmailTemplate Description: " + emailTemplate.Description);

                            // Get the EditorMode of each EmailTemplate
						    Console.WriteLine("EmailTemplate EditorMode: " + emailTemplate.EditorMode);

                            //Get the Favorite of each EmailTemplate
                            Console.WriteLine("EmailTemplate Favorite: " + emailTemplate.Favorite);

                            // Get the Favourite of each EmailTemplate
                            Console.WriteLine("EmailTemplate Subject: " + emailTemplate.Subject);
                        }

                        API.Record.Info info = responseWrapper.Info;

                        Console.WriteLine("EmailTemplate Info PerPage : " + info.PerPage);

                        Console.WriteLine("EmailTemplate Info Count : " + info.Count);

                        Console.WriteLine("EmailTemplate Info Page : " + info.Page);

                        Console.WriteLine("EmailTemplate Info MoreRecords : " + info.MoreRecords);
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
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <h3> Get EmailTemplate</h3>
        /// This method is used to get a single Email Template
        /// </summary>
        /// <param name="emailTemplateID">The id of the Email Template</param>
        public static void GetEmailTemplateById(long emailTemplateID)
        {
            //Get instance of EmailTemplatesOperations Class
            EmailTemplatesOperations emailTemplatesOperations = new EmailTemplatesOperations();

            //Call GetEmailTemplateById method
            APIResponse<ResponseHandler> response = emailTemplatesOperations.GetEmailTemplateById(emailTemplateID);

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

                        //Get the list of obtained EmailTemplate instances
                        List<API.EmailTemplates.EmailTemplate> emailTemplates = responseWrapper.EmailTemplates;

                        foreach (API.EmailTemplates.EmailTemplate emailTemplate in emailTemplates)
                        {
                            //Get the CreatedTime of each EmailTemplate
                            Console.WriteLine("EmailTemplate CreatedTime: " + emailTemplate.CreatedTime);

                            List<Attachment> attachments = emailTemplate.Attachments;

                            if(attachments != null)
                            {
                                foreach(Attachment attachment in attachments)
                                {
                                    //Get the Size of each Attachment
                                    Console.WriteLine("EmailTemplate Attachment Size: " + attachment.Size);

                                    //Get the FileId of each Attachment
                                    Console.WriteLine("EmailTemplate Attachment FileId: " + attachment.FileId);

                                    //Get the FileName of each Attachment
                                    Console.WriteLine("EmailTemplate Attachment FileName: " + attachment.FileName);

                                    //Get the Id of each Attachment
                                    Console.WriteLine("EmailTemplate Attachment ID: " + attachment.Id);
                                }
                            }

                            //Get the Subject of each EmailTemplate
                            Console.WriteLine("EmailTemplate Subject: " + emailTemplate.Subject);

                            API.Modules.Module module = emailTemplate.Module;

                            if(module != null)
                            {
                                //Get the Module Name of the EmailTemplate
                                Console.WriteLine("EmailTemplate Module Name : " + module.APIName);

                                //Get the Module Id of the EmailTemplate
                                Console.WriteLine("EmailTemplate Module Id : " + module.Id);
                            }

                            //Get the Type of each EmailTemplate
                            Console.WriteLine("EmailTemplate Type: " + emailTemplate.Type);

                            //Get the CreatedBy User instance of each EmailTemplate
                            User createdBy = emailTemplate.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the Id of the CreatedBy User
                                Console.WriteLine("EmailTemplate Created By User-ID: " + createdBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("EmailTemplate Created By User-Name: " + createdBy.Name);

                                Console.WriteLine("EmailTemplate Created By User-Email : " + createdBy.Email);
                            }

                            //Get the ModifiedTime of each EmailTemplate
                            Console.WriteLine("EmailTemplate ModifiedTime: " + emailTemplate.ModifiedTime);

                            //Get the Folder instance of each EmailTemplate
                            API.EmailTemplates.EmailTemplate folder = emailTemplate.Folder;

                            //Check if folder is not null
                            if(folder != null)
                            {
                                //Get the Id of the Folder
                                Console.WriteLine("EmailTemplate Folder Id: " + folder.Id);

                                //Get the Name of the Folder
                                Console.WriteLine("EmailTemplate Folder Name: " + folder.Name);
                            }

                            //Get the LastUsageTime of each EmailTemplate
                            Console.WriteLine("EmailTemplate LastUsageTime: " + emailTemplate.LastUsageTime);

                            //Get the Associated of each EmailTemplate
                            Console.WriteLine("EmailTemplate Associated: " + emailTemplate.Associated);

                            //Get the name of each EmailTemplate
                            Console.WriteLine("EmailTemplate Name: " + emailTemplate.Name);

                            //Get the ConsentLinked of each EmailTemplate
                            Console.WriteLine("EmailTemplate ConsentLinked: " + emailTemplate.ConsentLinked);

                            //Get the modifiedBy User instance of each EmailTemplate
                            User modifiedBy = emailTemplate.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the ModifiedBy User
                                Console.WriteLine("EmailTemplate Modified By User-ID: " + modifiedBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("EmailTemplate Modified By User-Name: " + modifiedBy.Name);

                                Console.WriteLine("EmailTemplate Modified By User-Email : " + modifiedBy.Email);
                            }

                            //Get the ID of each EmailTemplate
                            Console.WriteLine("EmailTemplate Id: " + emailTemplate.Id);

                            //Get the EditorMode each EmailTemplate
                            Console.WriteLine("EmailTemplate EditorMode: " + emailTemplate.EditorMode);

                            //Get the Content of each EmailTemplate
                            Console.WriteLine("EmailTemplate Content: " + emailTemplate.Content);

                            // Get the Description of each EmailTemplate
                            Console.WriteLine("EmailTemplate Description: " + emailTemplate.Description);

                            // Get the EditorMode of each EmailTemplate
                            Console.WriteLine("EmailTemplate EditorMode: " + emailTemplate.EditorMode);

                            // Get the Favourite of each EmailTemplate
                            Console.WriteLine("EmailTemplate Favourite: " + emailTemplate.Favorite);
                        
                            // Get the Favourite of each EmailTemplate
                            Console.WriteLine("EmailTemplate Subject: " + emailTemplate.Subject);
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
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }
    }
}
