using System;

using System.Collections.Generic;

using System.Reflection;

using Com.Zoho.Crm.API;

using Com.Zoho.Crm.API.InventoryTemplates;

using Com.Zoho.Crm.API.SendMail;

using Com.Zoho.Crm.API.Util;

using Newtonsoft.Json;

using APIException = Com.Zoho.Crm.API.SendMail.APIException;

using ResponseHandler = Com.Zoho.Crm.API.SendMail.ResponseHandler;

using ResponseWrapper = Com.Zoho.Crm.API.SendMail.ResponseWrapper;

namespace Com.Zoho.Crm.Sample.SendMail
{
    public class SendMail
    {
        public static void GetEmailAddresses()
        {
            //Get instance of SendMailOperations Class
            SendMailOperations sendMailOperations = new SendMailOperations();

            //Call GetEmailAddresses method
            APIResponse<ResponseHandler> response = sendMailOperations.GetEmailAddresses();

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

                        //Get the list of obtained FromAddress instances
                        List<API.SendMail.UserAddress> FromAddresses = responseWrapper.FromAddresses;

                        foreach (API.SendMail.UserAddress fromAddress in FromAddresses)
                        {
                            Console.WriteLine("UserName: " + fromAddress.UserName);

                            Console.WriteLine("Mail Type: " + fromAddress.Type);

                            Console.WriteLine("Mail : " + fromAddress.Email);

                            Console.WriteLine("Mail ID: " + fromAddress.Id);

                            Console.WriteLine("Mail Default: " + fromAddress.Default);
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

        public static void SendMailMethod(long? recordId, string moduleAPIName)
        {
            //Get instance of SendMailOperations Class
            SendMailOperations sendMailOperations = new SendMailOperations();

            Mail mail = new Mail();

            UserAddress from = new UserAddress();

            from.UserName = "abc";

            from.Email = "abc@zoho.com";

            mail.From = from;

            UserAddress to = new UserAddress();

            to.UserName = "abc1";

            to.Email = "abc1@gmail.com";

            mail.To = new List<UserAddress>() { to };

            mail.Subject = "Mail subject";

            mail.Content = "<br><a href=\"{ConsentForm.en_US}\" id=\"ConsentForm\" class=\"en_US\" target=\"_blank\">Consent form link</a><br><br><br><br><br><h3><span style=\"background-color: rgb(254, 255, 102)\">REGARDS,</span></h3><div><span style=\"background-color: rgb(254, 255, 102)\">AZ</span></div><div><span style=\"background-color: rgb(254, 255, 102)\">ADMIN</span></div> <div></div>";

            mail.ConsentEmail = false;

            mail.MailFormat = "html";

            InventoryTemplate template = new InventoryTemplate();

            template.Id = 347706179;

            mail.Template = template;

            BodyWrapper wrapper = new BodyWrapper();

            wrapper.Data = new List<Mail>() { mail };

            //Call SendMail method
            APIResponse<ActionHandler> response = sendMailOperations.SendMail(recordId, moduleAPIName, wrapper);

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
                                    Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
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
                                    Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
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
                            Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
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
