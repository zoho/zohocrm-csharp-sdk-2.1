using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.SendMail
{

	public class SendMailOperations
	{
		/// <summary>The method to get email addresses</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetEmailAddresses()
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/emails/actions/from_addresses");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to send mail</summary>
		/// <param name="recordId">long?</param>
		/// <param name="moduleAPIName">string</param>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> SendMail(long? recordId, string moduleAPIName, BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/");

			apiPath=string.Concat(apiPath, moduleAPIName.ToString());

			apiPath=string.Concat(apiPath, "/");

			apiPath=string.Concat(apiPath, recordId.ToString());

			apiPath=string.Concat(apiPath, "/actions/send_mail");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_POST;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_CREATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}


	}
}