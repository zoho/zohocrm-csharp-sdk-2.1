using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Functions
{

	public class FunctionsOperation
	{
		/// <summary>The method to get records</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> Execute(string functionName, ParameterMap @params)
		{
			CommonAPIHandler handlerInstance = new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2/functions/");

			apiPath=string.Concat(apiPath, functionName);

			apiPath=string.Concat(apiPath, "/actions/execute");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_ACTION;

			@params.Add(new Param<string>("auth_type", null), "oauth");

			handlerInstance.Param = @params;

			handlerInstance.ContentType="application/json";

			handlerInstance.MandatoryChecker=false;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");

		}

	}
}