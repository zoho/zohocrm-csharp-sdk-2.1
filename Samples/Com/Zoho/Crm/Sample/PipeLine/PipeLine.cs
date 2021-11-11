using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API.Pipeline;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;

namespace Com.Zoho.Crm.Sample.PipeLine
{
    public class PipeLine
    {
        public static void TransferAndDelete(long? layoutId)
        {
            //Get instance of PipelineOperations Class
            PipelineOperations pipelineOperations = new PipelineOperations(layoutId);

            TransferAndDeleteWrapper transferAndDeleteWrapper = new TransferAndDeleteWrapper();

            TransferPipeLine transferPipeLine = new TransferPipeLine();

            Pipeline pipeline = new Pipeline();

            pipeline.From = 34770616801;

            pipeline.To = 34770619541006;

            transferPipeLine.Pipeline = pipeline;

            Stage stage = new Stage();

            stage.From = 36523976817;

            stage.To = 36523976819;

            transferPipeLine.Stages = new List<Stage>() { stage };

            transferAndDeleteWrapper.TransferPipeline = new List<TransferPipeLine>() { transferPipeLine };

            //Call TransferAndDelete method
            APIResponse<TransferActionHandler> response = pipelineOperations.TransferAndDelete(transferAndDeleteWrapper);

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
                    TransferActionHandler transferActionHandler = response.Object;

                    if (transferActionHandler is TransferActionWrapper)
                    {
                        //Get the received TransferActionWrapper instance
                        TransferActionWrapper transferActionWrapper = (TransferActionWrapper)transferActionHandler;

                        //Get the list of obtained Pipeline instances
                        List<TransferActionResponse> actionResponses = transferActionWrapper.TransferPipeline;

                        foreach (TransferActionResponse actionResponse in actionResponses)
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
                    else if (transferActionHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)transferActionHandler;

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

        public static void GetPipelines(long? layoutId)
        {
            //Get instance of PipelineOperations Class
            PipelineOperations pipelineOperations = new PipelineOperations(layoutId);

            //Call GetPipelines method
            APIResponse<ResponseHandler> response = pipelineOperations.GetPipelines();

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

                        //Get the list of obtained Pipeline instances
                        List<Pipeline> pipelineList = responseWrapper.Pipeline;

                        foreach (Pipeline pipeline in pipelineList)
                        {
                            //Get the Id of each Pipeline
                            Console.WriteLine("Pipeline ID: " + pipeline.Id);

                            //Get the Default of each Pipeline
                            Console.WriteLine("Pipeline Default: " + pipeline.Default);

                            //Get the DisplayValue of each Pipeline
                            Console.WriteLine("Pipeline DisplayValue: " + pipeline.DisplayValue);

                            //Get the ActualValue of each Pipeline
                            Console.WriteLine("Pipeline Maps ActualValue: " + pipeline.ActualValue);

                            //Get the child available of each Pipeline
                            Console.WriteLine("Pipeline Child Available  : " + pipeline.ChildAvailable);
                            
                            API.Pipeline.Pipeline parent = pipeline.Parent;
                            
                            if(parent != null) 
                            {
                                //Get the ID of  parent
                                Console.WriteLine("Pipeline parent ID: " + parent.Id);
                            }

                            List<PickListValue> maps = pipeline.Maps;

                            if(maps != null)
                            {
                                foreach(PickListValue map in maps)
                                {
                                    //Get the Maps DisplayValue of each Pipeline
                                    Console.WriteLine("Pipeline Maps DisplayValue: " + map.DisplayValue);

                                    //Get the Maps SequenceNumber of each Pipeline
                                    Console.WriteLine("Pipeline Maps SequenceNumber: " + map.SequenceNumber);

                                    ForecastCategory forecastCategory = map.ForecastCategory;

                                    if(forecastCategory != null)
                                    {
                                        //Get the Maps ForecastCategory Name of each Pipeline
                                        Console.WriteLine("Pipeline Maps ForecastCategory Name: " + forecastCategory.Name);

                                        //Get the Maps ForecastCategory Id of each Pipeline
                                        Console.WriteLine("Pipeline Maps ForecastCategory Id: " + forecastCategory.Id);
                                    }

                                    //Get the Maps ActualValue of each Pipeline
                                    Console.WriteLine("Pipeline Maps ActualValue: " + map.ActualValue);

                                    //Get the Maps Id of each Pipeline
                                    Console.WriteLine("Pipeline Maps ID: " + map.Id);

                                    //Get the Maps ForecastType of each Pipeline
                                    Console.WriteLine("Pipeline Maps ForecastType: " + map.ForecastType);

                                    //Get PickListValue delete
							        Console.WriteLine("PickListValue delete" + map.Delete);
                                }
                            }
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

        public static void CreatePipelines(long? layoutId)
        {
            //Get instance of PipelineOperations Class
            PipelineOperations pipelineOperations = new PipelineOperations(layoutId);

            API.Pipeline.Pipeline pipeline  = new API.Pipeline.Pipeline();

		    pipeline.DisplayValue = "Adfasfsad1345677";

            PickListValue pickList = new PickListValue();

		    pickList.Id = 34770616805;

            pickList.DisplayValue = "Closed Won";

		    pickList.SequenceNumber = 1;

		    pipeline.Maps = new List<PickListValue>() { pickList };

            BodyWrapper bodyWrapper = new BodyWrapper();

		    bodyWrapper.Pipeline = new List<Pipeline>() { pipeline };

            //Call CreatePipelines method that takes BodyWrapper instance as parameter
            APIResponse<ActionHandler> response = pipelineOperations.CreatePipelines(bodyWrapper);

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
                        List<ActionResponse> actionResponses = actionWrapper.Pipeline;

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

        public static void UpdatePipelines(long? layoutId)
        {
            //Get instance of PipelineOperations Class
            PipelineOperations pipelineOperations = new PipelineOperations(layoutId);

            API.Pipeline.Pipeline pipeline = new API.Pipeline.Pipeline();

            pipeline.Id = 3477061012387002;

            pipeline.DisplayValue = "Qualification";

            PickListValue pickList = new PickListValue();

            pickList.Id = 34770616801;

            pickList.DisplayValue = "Closed Won";

            pickList.SequenceNumber = 1;

            pipeline.Maps = new List<PickListValue>() { pickList };

            BodyWrapper bodyWrapper = new BodyWrapper();

            bodyWrapper.Pipeline = new List<Pipeline>() { pipeline };

            //Call UpdatePipelines method that takes BodyWrapper instance as parameter
            APIResponse<ActionHandler> response = pipelineOperations.UpdatePipelines(bodyWrapper);

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
                        List<ActionResponse> actionResponses = actionWrapper.Pipeline;

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

        public static void GetPipeline(long? layoutId, long? pipelineId)
        {
            //Get instance of PipelineOperations Class
            PipelineOperations pipelineOperations = new PipelineOperations(layoutId);

            //Call GetPipeline method
            APIResponse< ResponseHandler > response = pipelineOperations.GetPipeline(pipelineId);

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

                        //Get the list of obtained Pipeline instances
                        List<Pipeline> pipelineList = responseWrapper.Pipeline;

                        foreach (Pipeline pipeline in pipelineList)
                        {
                            //Get the Id of each Pipeline
                            Console.WriteLine("Pipeline ID: " + pipeline.Id);

                            //Get the DisplayValue of each Pipeline
                            Console.WriteLine("Pipeline DisplayValue: " + pipeline.DisplayValue);

                            //Get the ActualValue of each Pipeline
                            Console.WriteLine("Pipeline Maps ActualValue: " + pipeline.ActualValue);

                            //Get the Default of each Pipeline
                            Console.WriteLine("Pipeline Default: " + pipeline.Default);

                            //Get the child available of each Pipeline
                            Console.WriteLine("Pipeline Child Available  : " + pipeline.ChildAvailable);

                            API.Pipeline.Pipeline parent = pipeline.Parent;
						
                            if(parent != null) 
                            {
                                //Get the ID of  parent
                                Console.WriteLine("Pipeline parent ID: " + parent.Id);
                            }

                            List<PickListValue> maps = pipeline.Maps;

                            if(maps != null)
                            {
                                foreach(PickListValue map in maps)
                                {
                                    //Get the Maps ActualValue of each Pipeline
                                    Console.WriteLine("Pipeline Maps ActualValue: " + map.ActualValue);

                                    //Get PickListValue delete
                                    Console.WriteLine("PickListValue Delete" + map.Delete);

                                    //Get the Maps DisplayValue of each Pipeline
                                    Console.WriteLine("Pipeline Maps DisplayValue: " + map.DisplayValue);

                                    ForecastCategory forecastCategory = map.ForecastCategory;

                                    if(forecastCategory != null)
                                    {
                                        //Get the Maps ForecastCategory Name of each Pipeline
                                        Console.WriteLine("Pipeline Maps ForecastCategory Name: " + forecastCategory.Name);

                                        //Get the Maps ForecastCategory Id of each Pipeline
                                        Console.WriteLine("Pipeline Maps ForecastCategory Id: " + forecastCategory.Id);
                                    }

                                    //Get the Maps ForecastType of each Pipeline
                                    Console.WriteLine("Pipeline Maps ForecastType: " + map.ForecastType);

                                    //Get the Maps Id of each Pipeline
                                    Console.WriteLine("Pipeline Maps Id: " + map.Id);

                                    //Get the Maps SequenceNumber of each Pipeline
                                    Console.WriteLine("Pipeline Maps SequenceNumber: " + map.SequenceNumber);
                                }
                            }
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

        public static void UpdatePipeline(long? layoutId, long? pipelineId)
        {
            //Get instance of PipelineOperations Class
            PipelineOperations pipelineOperations = new PipelineOperations(layoutId);

            API.Pipeline.Pipeline pipeline = new API.Pipeline.Pipeline();

            pipeline.DisplayValue = "Qualification";

            PickListValue pickList = new PickListValue();

		    pickList.Id = 34770616801;

            pickList.DisplayValue = "Adfasfsad112";

		    pickList.SequenceNumber = 1;

		    pipeline.Maps = new List<PickListValue>() { pickList };

            BodyWrapper bodyWrapper = new BodyWrapper();

		    bodyWrapper.Pipeline = new List<Pipeline>() { pipeline };

            //Call UpdatePipeline method that takes BodyWrapper instance as parameter
            APIResponse<ActionHandler> response = pipelineOperations.UpdatePipeline(pipelineId, bodyWrapper);

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
                        List<ActionResponse> actionResponses = actionWrapper.Pipeline;

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
