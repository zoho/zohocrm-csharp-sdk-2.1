using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Pipeline
{

	public class PipelineOperations
	{
		private long? layoutId;

		/// <summary>		/// Creates an instance of PipelineOperations with the given parameters
		/// <param name="layoutId">long?</param>
		
		public PipelineOperations(long? layoutId)
		{
			 this.layoutId=layoutId;


		}

		/// <summary>The method to transfer and delete</summary>
		/// <param name="request">Instance of TransferAndDeleteWrapper</param>
		/// <returns>Instance of APIResponse<TransferActionHandler></returns>
		public APIResponse<TransferActionHandler> TransferAndDelete(TransferAndDeleteWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/pipeline/actions/transfer");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_POST;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_CREATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			handlerInstance.AddParam(new Param<long?>("layout_id", "com.zoho.crm.api.Pipeline.TransferAndDeleteParam"),  this.layoutId);

			return handlerInstance.APICall<TransferActionHandler>(typeof(TransferActionHandler), "application/json");


		}

		/// <summary>The method to get pipelines</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetPipelines()
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/pipeline");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<long?>("layout_id", "com.zoho.crm.api.Pipeline.GetPipelinesParam"),  this.layoutId);

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to create pipelines</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> CreatePipelines(BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/pipeline");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_POST;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_CREATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			handlerInstance.AddParam(new Param<long?>("layout_id", "com.zoho.crm.api.Pipeline.CreatePipelinesParam"),  this.layoutId);

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}

		/// <summary>The method to update pipelines</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> UpdatePipelines(BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/pipeline");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_PUT;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_UPDATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			handlerInstance.AddParam(new Param<long?>("layout_id", "com.zoho.crm.api.Pipeline.UpdatePipelinesParam"),  this.layoutId);

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}

		/// <summary>The method to get pipeline</summary>
		/// <param name="pipelineId">long?</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetPipeline(long? pipelineId)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/pipeline/");

			apiPath=string.Concat(apiPath, pipelineId.ToString());

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<long?>("layout_id", "com.zoho.crm.api.Pipeline.GetPipelineParam"),  this.layoutId);

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to update pipeline</summary>
		/// <param name="pipelineId">long?</param>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> UpdatePipeline(long? pipelineId, BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/pipeline/");

			apiPath=string.Concat(apiPath, pipelineId.ToString());

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_PUT;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_UPDATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.AddParam(new Param<long?>("layout_id", "com.zoho.crm.api.Pipeline.UpdatePipelineParam"),  this.layoutId);

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}


		public static class TransferAndDeleteParam
		{
		}


		public static class GetPipelinesParam
		{
		}


		public static class CreatePipelinesParam
		{
		}


		public static class UpdatePipelinesParam
		{
		}


		public static class GetPipelineParam
		{
		}


		public static class UpdatePipelineParam
		{
		}

	}
}