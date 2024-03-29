﻿using System;
using System.Collections.Generic;
using System.Threading;
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.ContactRoles;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Logger;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.API.Authenticator.OAuthToken;
using APIException = Com.Zoho.Crm.API.ContactRoles.APIException;
using ResponseHandler = Com.Zoho.Crm.API.ContactRoles.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.ContactRoles.ResponseWrapper;
using SDKInitializer = Com.Zoho.Crm.API.Initializer;

namespace Com.Zoho.Crm.Sample.Threading.SingleUser
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiThread
    {
        public static void RunMultiThreadWithSingleUser()
        {
            Logger logger = new Logger.Builder()
                .Level(Logger.Levels.ALL)
                .FilePath("/Users/Documents/csharp_sdk_log.log")
                .Build();

            DataCenter.Environment env = USDataCenter.PRODUCTION;

            UserSignature user1 = new UserSignature("abc@zoho.com");

             //TokenStore tokenstore = new DBStore.Builder()
             //    .Host("host")
             //    .TableName("tablename")
             //    .Password("password")
             //    .Build();

            TokenStore tokenstore = new FileStore("/Users/Documents/csharp_sdk_token.txt");

            Token token1 = new OAuthToken.Builder()
                .ClientId("1.xxxx")
                .ClientSecret("xxxx")
                //.GrantToken("1.xxxx.xxxx")
                .RefreshToken("1.xxxx.xxxx")
                .RedirectURL("https://www.zoho.com")
                .Build();

            string resourcePath = "/Users/Documents";

            SDKConfig config = new SDKConfig.Builder()
            .AutoRefreshFields(true)
            .Build();

            new SDKInitializer.Builder()
                .User(user1)
                .Environment(env)
                .Token(token1)
                .Store(tokenstore)
                .SDKConfig(config)
                .ResourcePath(resourcePath)
                .Logger(logger)
                .Initialize();

            MultiThread multiThread1 = new MultiThread();

            Thread thread1 = new Thread(() => multiThread1.GetRecords("Quotes"));

            thread1.Start();

            Thread thread2 = new Thread(() => multiThread1.GetContactRoles());

            thread2.Start();

            thread1.Join();

            thread2.Join();
        }

        public void GetRecords(string moduleAPIName)
        {
            try
            {
                Console.WriteLine("Fetching Cr's for user - " + SDKInitializer.GetInitializer().User.Email);

                RecordOperations recordOperation = new RecordOperations();

                APIResponse<API.Record.ResponseHandler> response = recordOperation.GetRecords(moduleAPIName, null, null);

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
                        API.Record.ResponseHandler responseHandler = response.Object;

                        if (responseHandler is API.Record.ResponseWrapper)
                        {
                            //Get the received ResponseWrapper instance
                            API.Record.ResponseWrapper responseWrapper = (API.Record.ResponseWrapper)responseHandler;

                            //Get the list of obtained Record instances
                            List<API.Record.Record> records = responseWrapper.Data;

                            foreach (API.Record.Record record in records)
                            {
                                Console.WriteLine(JsonConvert.SerializeObject(record));
                            }
                        }
                        //Check if the request returned an exception
                        else if (responseHandler is API.Record.APIException)
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
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }

        public void GetContactRoles()
        {
            try
            {
                Console.WriteLine("Fetching Cr's for user - " + SDKInitializer.GetInitializer().User.Email);

                ContactRolesOperations contactRolesOperations = new ContactRolesOperations();

                APIResponse<ResponseHandler> response = contactRolesOperations.GetContactRoles();

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
                            List<ContactRole> contactRoles = responseWrapper.ContactRoles;

                            foreach (ContactRole contactRole in contactRoles)
                            {
                                Console.WriteLine(JsonConvert.SerializeObject(contactRole));
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
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
