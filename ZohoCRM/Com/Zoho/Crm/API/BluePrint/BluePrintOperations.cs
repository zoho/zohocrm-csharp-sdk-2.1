using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.BluePrint
{

	public class BluePrintOperations
	{
		string moduleAPIName;
		long? recordId;

		/// <summary>		/// Creates an instance of BluePrintOperations with the given parameters
		/// <param name="recordId">long?</param>
		/// <param name="moduleAPIName">string</param>
		
		public BluePrintOperations(long? recordId, string moduleAPIName)
		{
			 this.recordId=recordId;

			 this.moduleAPIName=moduleAPIName;


		}

		/// <summary>The method to get blueprint</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetBlueprint()
		{
			var handlerInstance=new CommonAPIHandler();

			var apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/");

			apiPath=string.Concat(apiPath,  moduleAPIName.ToString());

			apiPath=string.Concat(apiPath, "/");

			apiPath=string.Concat(apiPath,  recordId.ToString());

			apiPath=string.Concat(apiPath, "/actions/blueprint");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to update blueprint</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionResponse></returns>
		public APIResponse<ActionResponse> UpdateBlueprint(BodyWrapper request)
		{
			var handlerInstance=new CommonAPIHandler();

			var apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/");

			apiPath=string.Concat(apiPath,  moduleAPIName.ToString());

			apiPath=string.Concat(apiPath, "/");

			apiPath=string.Concat(apiPath,  recordId.ToString());

			apiPath=string.Concat(apiPath, "/actions/blueprint");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_PUT;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_UPDATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			return handlerInstance.APICall<ActionResponse>(typeof(ActionResponse), "application/json");


		}


	}
}