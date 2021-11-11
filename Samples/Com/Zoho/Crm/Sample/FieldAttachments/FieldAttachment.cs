using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Com.Zoho.Crm.API.FieldAttachments;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;

namespace Com.Zoho.Crm.Sample.FieldAttachments
{
    public class FieldAttachment
    {
		public static void GetFieldAttachments(string moduleAPIName, long recordId, long fieldsAttachmentId, string destinationFolder)
		{
			//example
			//string moduleAPIName = "Leads";
			//long recordId = 34770615177002;
			//long attachmentId = 34770615177023;
			//string destinationFolder = "/Users/user_name/Desktop";

			//Get instance of FieldAttachmentsOperations Class that takes moduleAPIName and recordId and fieldsAttachmentId as parameter
			FieldAttachmentsOperations fieldAttachmentsOperations = new FieldAttachmentsOperations(moduleAPIName, recordId, fieldsAttachmentId);

			//Call DownloadAttachment method that takes attachmentId as parameters
			APIResponse<ResponseHandler> response = fieldAttachmentsOperations.GetFieldAttachments();

			if (response != null)
			{
				//Get the status code from response
				Console.WriteLine("Status Code : " + response.StatusCode);

				if (response.StatusCode == 204)
				{
					Console.WriteLine("No Content");

					return;
				}

				//Check if expected response is received
				if (response.IsExpected)
				{
					//Get object from response
					ResponseHandler responseHandler = response.Object;

					if (responseHandler is FileBodyWrapper)
					{
						//Get the received FileBodyWrapper instance
						FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)responseHandler;

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
					else if (responseHandler is API.Attachments.APIException)
					{
						//Get the received APIException instance
						API.Attachments.APIException exception = (API.Attachments.APIException)responseHandler;

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
