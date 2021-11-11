using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.AssignmentRules;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations;

namespace Com.Zoho.Crm.Sample.AssignmentRules
{
    public class AssignmentRules
    {
        public static void GetAssignmentRules()
        {
            //Get instance of AssignmentRulesOperations Class
            AssignmentRulesOperations assignmentRulesOperations = new AssignmentRulesOperations();

            //Call GetAssignmentRules method
            APIResponse<API.AssignmentRules.ResponseHandler> response = assignmentRulesOperations.GetAssignmentRules();

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
                    API.AssignmentRules.ResponseHandler responseHandler = response.Object;

                    if (responseHandler is API.AssignmentRules.ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        API.AssignmentRules.ResponseWrapper responseWrapper = (API.AssignmentRules.ResponseWrapper)responseHandler;

                        //Get the list of obtained AssignmentRule instances
                        List<AssignmentRule> assignmentRules = responseWrapper.AssignmentRules;

                        foreach (AssignmentRule assignmentRule in assignmentRules)
                        {
                            //Get the  ModifiedTime of each AssignmentRule
                            Console.WriteLine("AssignmentRule ModifiedTime : "); Console.WriteLine(assignmentRule.ModifiedTime);

                            //Get the  createdTime of each AssignmentRule
                            Console.WriteLine("AssignmentRule CreatedTime : "); Console.WriteLine(assignmentRule.CreatedTime);

                            DefaultUser defaultAssignee = assignmentRule.DefaultAssignee;

                            if (defaultAssignee != null)
                            {
                                //Get the id of DefaultAssignee
                                Console.WriteLine("AssignmentRule DefaultAssignee Id : " + defaultAssignee.Id);

                                //Get the name of DefaultAssignee
                                Console.WriteLine("AssignmentRule DefaultAssignee Name : " + defaultAssignee.Name);
                            }

                            API.Modules.Module module = assignmentRule.Module;

                            if (module != null)
                            {
                                //Get the id of  Module
                                Console.WriteLine("AssignmentRule Module Id : " + module.Id);

                                //Get the apiName of  Module
                                Console.WriteLine("AssignmentRule Module APIName : " + module.APIName);
                            }

                            //Get the name of each AssignmentRule
                            Console.WriteLine("AssignmentRule Name : " + assignmentRule.Name);

                            User modifiedBy = assignmentRule.ModifiedBy;

                            if (modifiedBy != null)
                            {
                                //Get the id of ModifiedBy
                                Console.WriteLine("AssignmentRule ModifiedBy Id : " + modifiedBy.Id);

                                //Get the name of ModifiedBy
                                Console.WriteLine("AssignmentRule ModifiedBy Name : " + modifiedBy.Name);

                                //Get the email of ModifiedBy
                                Console.WriteLine("AssignmentRule ModifiedBy User-Email : " + modifiedBy.Email);
                            }

                            User createdBy = assignmentRule.CreatedBy;

                            if (createdBy != null)
                            {
                                //Get the id of CreatedBy
                                Console.WriteLine("AssignmentRule CreatedBy Id : " + createdBy.Id);

                                //Get the name of CreatedBy
                                Console.WriteLine("AssignmentRule CreatedBy Name : " + createdBy.Name);

                                //Get the email of CreatedBy
                                Console.WriteLine("AssignmentRule CreatedBy User-Email : " + createdBy.Email);
                            }

                            //Get the ID of each AssignmentRule
                            Console.WriteLine("AssignmentRule ID : " + assignmentRule.Id);

                            //Get the  description of each AssignmentRule
                            Console.WriteLine("AssignmentRule Description : " + assignmentRule.Description);
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is API.AssignmentRules.APIException)
                    {
                        //Get the received APIException instance
                        API.AssignmentRules.APIException exception = (API.AssignmentRules.APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status : " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code : " + exception.Code.Value);

                        Console.WriteLine("Details : ");

                        //Get the details map
                        foreach (KeyValuePair<string, Object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message : " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is : {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}) :", props.Length);

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

        public static void GetAssignmentRule(long ruleId)
        {
            //Get instance of AssignmentRulesOperations Class
            AssignmentRulesOperations assignmentRulesOperations = new AssignmentRulesOperations();

            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetAssignmentRuleParam.MODULE, "Leads");

            //Call GetAssignmentRules method
            APIResponse<API.AssignmentRules.ResponseHandler> response = assignmentRulesOperations.GetAssignmentRule(ruleId, paramInstance);

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
                    API.AssignmentRules.ResponseHandler responseHandler = response.Object;

                    if (responseHandler is API.AssignmentRules.ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        API.AssignmentRules.ResponseWrapper responseWrapper = (API.AssignmentRules.ResponseWrapper)responseHandler;

                        //Get the list of obtained AssignmentRule instances
                        List<AssignmentRule> assignmentRules = responseWrapper.AssignmentRules;

                        foreach (AssignmentRule assignmentRule in assignmentRules)
                        {
                            //Get the  modifiedTime of each AssignmentRule
                            Console.WriteLine("AssignmentRule ModifiedTime : "); Console.WriteLine(assignmentRule.ModifiedTime);

                            //Get the  createdTime of each AssignmentRule
                            Console.WriteLine("AssignmentRule CreatedTime : "); Console.WriteLine(assignmentRule.CreatedTime);

                            DefaultUser defaultAssignee = assignmentRule.DefaultAssignee;

                            if (defaultAssignee != null)
                            {
                                //Get the id of DefaultAssignee
                                Console.WriteLine("AssignmentRule DefaultAssignee Id : " + defaultAssignee.Id);

                                //Get the name of DefaultAssignee
                                Console.WriteLine("AssignmentRule DefaultAssignee Name : " + defaultAssignee.Name);
                            }

                            API.Modules.Module module = assignmentRule.Module;

                            if (module != null)
                            {
                                //Get the id of  Module
                                Console.WriteLine("AssignmentRule Module Id : " + module.Id);

                                //Get the apiName of  Module
                                Console.WriteLine("AssignmentRule Module APIName : " + module.APIName);
                            }

                            //Get the name of each AssignmentRule
                            Console.WriteLine("AssignmentRule Name : " + assignmentRule.Name);

                            User modifiedBy = assignmentRule.ModifiedBy;

                            if (modifiedBy != null)
                            {
                                //Get the id of ModifiedBy
                                Console.WriteLine("AssignmentRule ModifiedBy Id : " + modifiedBy.Id);

                                //Get the name of ModifiedBy
                                Console.WriteLine("AssignmentRule ModifiedBy Name : " + modifiedBy.Name);

                                //Get the email of ModifiedBy
                                Console.WriteLine("AssignmentRule ModifiedBy User-Email : " + modifiedBy.Email);
                            }

                            User createdBy = assignmentRule.CreatedBy;

                            if (createdBy != null)
                            {
                                //Get the id of CreatedBy
                                Console.WriteLine("AssignmentRule CreatedBy Id : " + createdBy.Id);

                                //Get the name of CreatedBy
                                Console.WriteLine("AssignmentRule CreatedBy Name : " + createdBy.Name);

                                //Get the email of CreatedBy
                                Console.WriteLine("AssignmentRule CreatedBy User-Email : " + createdBy.Email);
                            }

                            //Get the id of each AssignmentRule
                            Console.WriteLine("AssignmentRule Id : " + assignmentRule.Id);

                            //Get the  description of each AssignmentRule
                            Console.WriteLine("AssignmentRule Description : " + assignmentRule.Description);
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is API.AssignmentRules.APIException)
                    {
                        //Get the received APIException instance
                        API.AssignmentRules.APIException exception = (API.AssignmentRules.APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status : " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code : " + exception.Code.Value);

                        Console.WriteLine("Details : ");

                        //Get the details map
                        foreach (KeyValuePair<string, Object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message : " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is : {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}) :", props.Length);

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
