using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.InventoryTemplates;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations;
using static Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplatesOperations;
using APIException = Com.Zoho.Crm.API.InventoryTemplates.APIException;
using ResponseHandler = Com.Zoho.Crm.API.InventoryTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.InventoryTemplates.ResponseWrapper;

namespace Com.Zoho.Crm.Sample.InventoryTemplates
{
    public class InventoryTemplate
    {
        public static void GetInventoryTemplates(string moduleName)
        {
            // string moduleAPIName = "Quotes";
            string sortBy = "Modified_Time";
            string sortOrder = "desc";
            string category = "Created_By";

            //Get instance of InventoryTemplatesOperations Class
            InventoryTemplatesOperations inventoryTemplatesOperations = new InventoryTemplatesOperations(sortBy, sortOrder, category);

            ParameterMap parameter = new ParameterMap();

            parameter.Add(GetInventoryTemplatesParam.MODULE, moduleName);

            //Call GetInventoryTemplates method
            APIResponse<ResponseHandler> response = inventoryTemplatesOperations.GetInventoryTemplates(parameter);

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

                        //Get the list of obtained InventoryTemplate instances
                        List<API.InventoryTemplates.InventoryTemplate> inventoryTemplates = responseWrapper.InventoryTemplates;

                        foreach (API.InventoryTemplates.InventoryTemplate inventoryTemplate in inventoryTemplates)
                        {
                            //Get the CreatedTime of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate CreatedTime: " + inventoryTemplate.CreatedTime);

                            // Get the Subject of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Subject: " + inventoryTemplate.Subject);

                            API.Modules.Module module = inventoryTemplate.Module;

                            if(module != null)
                            {
                                //Get the Module Name of the InventoryTemplate
                                Console.WriteLine("InventoryTemplate Module Name : " + module.APIName);

                                //Get the Module Id of the InventoryTemplate
                                Console.WriteLine("InventoryTemplate Module Id : " + module.Id);
                            }

                            //Get the Type of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Type: " + inventoryTemplate.Type);

                            //Get the CreatedBy User instance of each InventoryTemplate
                            User createdBy = inventoryTemplate.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the Id of the CreatedBy User
                                Console.WriteLine("InventoryTemplate Created By User-ID: " + createdBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("InventoryTemplate Created By User-Name: " + createdBy.Name);
                            }

                            //Get the ModifiedTime of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate ModifiedTime: " + inventoryTemplate.ModifiedTime);

                            //Get the Folder instance of each InventoryTemplate
                            API.InventoryTemplates.InventoryTemplate folder = inventoryTemplate.Folder;

                            //Check if folder is not null
                            if(folder != null)
                            {
                                //Get the Id of the Folder
                                Console.WriteLine("InventoryTemplate Folder Id: " + folder.Id);

                                //Get the Name of the Folder
                                Console.WriteLine("InventoryTemplate Folder Name: " + folder.Name);
                            }

                            //Get the LastUsageTime of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate LastUsageTime: " + inventoryTemplate.LastUsageTime);

                            // Get the Associated of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Associated: " + inventoryTemplate.Associated);

                            //Get the name of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Name: " + inventoryTemplate.Name);

                            //Get the modifiedBy User instance of each InventoryTemplate
                            User modifiedBy = inventoryTemplate.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the ModifiedBy User
                                Console.WriteLine("InventoryTemplate Modified By User-ID: " + modifiedBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("InventoryTemplate Modified By User-Name: " + modifiedBy.Name);
                            }

                            //Get the ID of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Id: " + inventoryTemplate.Id);

                            //Get the EditorMode each InventoryTemplate
                            Console.WriteLine("InventoryTemplate EditorMode: " + inventoryTemplate.EditorMode);

                            Console.WriteLine("InventoryTemplate Content: " + inventoryTemplate.Content);

                            // Get the Description of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Description: " + inventoryTemplate.Description);

                            // Get the EditorMode of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate EditorMode: " + inventoryTemplate.EditorMode);

                            //Get the Favorite of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Favorite: " + inventoryTemplate.Favorite);
                        }

                        API.Record.Info info = responseWrapper.Info;

                        Console.WriteLine("InventoryTemplate Info PerPage : " + info.PerPage);

                        Console.WriteLine("InventoryTemplate Info Count : " + info.Count);

                        Console.WriteLine("InventoryTemplate Info Page : " + info.Page);

                        Console.WriteLine("InventoryTemplate Info MoreRecords : " + info.MoreRecords);
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

        public static void GetInventoryTemplateById(long inventoryTemplateID)
        {
            string sortBy = "modified_time";
            string sortOrder = "desc";
            string category = "created_by_me";

            //Get instance of InventoryTemplatesOperations Class
            InventoryTemplatesOperations inventoryTemplatesOperations = new InventoryTemplatesOperations(sortBy, sortOrder, category);

            //Call GetInventoryTemplateById method
            APIResponse<ResponseHandler> response = inventoryTemplatesOperations.GetInventoryTemplateById(inventoryTemplateID);

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

                        //Get the list of obtained InventoryTemplate instances
                        List<API.InventoryTemplates.InventoryTemplate> inventoryTemplates = responseWrapper.InventoryTemplates;

                        foreach (API.InventoryTemplates.InventoryTemplate inventoryTemplate in inventoryTemplates)
                        {
                            //Get the CreatedTime of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate CreatedTime: " + inventoryTemplate.CreatedTime);

                            // Get the Subject of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Subject: " + inventoryTemplate.Subject);

                            API.Modules.Module module = inventoryTemplate.Module;

                            if(module != null)
                            {
                                //Get the Module Name of the InventoryTemplate
                                Console.WriteLine("InventoryTemplate Module Name : " + module.APIName);

                                //Get the Module Id of the InventoryTemplate
                                Console.WriteLine("InventoryTemplate Module Id : " + module.Id);
                            }

                            //Get the Type of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Type: " + inventoryTemplate.Type);

                            //Get the CreatedBy User instance of each InventoryTemplate
                            User createdBy = inventoryTemplate.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the Id of the CreatedBy User
                                Console.WriteLine("InventoryTemplate Created By User-ID: " + createdBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("InventoryTemplate Created By User-Name: " + createdBy.Name);
                            }

                            //Get the ModifiedTime of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate ModifiedTime: " + inventoryTemplate.ModifiedTime);

                            //Get the Folder instance of each InventoryTemplate
                            API.InventoryTemplates.InventoryTemplate folder = inventoryTemplate.Folder;

                            //Check if folder is not null
                            if(folder != null)
                            {
                                //Get the Id of the Folder
                                Console.WriteLine("InventoryTemplate Folder Id: " + folder.Id);

                                //Get the Name of the Folder
                                Console.WriteLine("InventoryTemplate Folder Name: " + folder.Name);
                            }

                            //Get the LastUsageTime of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate LastUsageTime: " + inventoryTemplate.LastUsageTime);

                            // Get the Associated of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Associated: " + inventoryTemplate.Associated);

                            //Get the name of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Name: " + inventoryTemplate.Name);

                            //Get the modifiedBy User instance of each InventoryTemplate
                            User modifiedBy = inventoryTemplate.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the ID of the ModifiedBy User
                                Console.WriteLine("InventoryTemplate Modified By User-ID: " + modifiedBy.Id);

                                //Get the Name of the CreatedBy User
                                Console.WriteLine("InventoryTemplate Modified By User-Name: " + modifiedBy.Name);
                            }

                            //Get the ID of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Id: " + inventoryTemplate.Id);

                            //Get the EditorMode each InventoryTemplate
                            Console.WriteLine("InventoryTemplate EditorMode: " + inventoryTemplate.EditorMode);

                            Console.WriteLine("InventoryTemplate Content: " + inventoryTemplate.Content);

                            // Get the Description of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Description: " + inventoryTemplate.Description);

                            // Get the EditorMode of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate EditorMode: " + inventoryTemplate.EditorMode);

                            //Get the Favorite of each InventoryTemplate
                            Console.WriteLine("InventoryTemplate Favorite: " + inventoryTemplate.Favorite);
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
