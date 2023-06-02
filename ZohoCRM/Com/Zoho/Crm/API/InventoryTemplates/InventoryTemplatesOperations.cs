using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.InventoryTemplates
{

	public class InventoryTemplatesOperations
	{
		private string sortBy;
		private string sortOrder;
		private string category;

		/// <summary>		/// Creates an instance of InventoryTemplatesOperations with the given parameters
		/// <param name="sortBy">string</param>
		/// <param name="sortOrder">string</param>
		/// <param name="category">string</param>
		
		public InventoryTemplatesOperations(string sortBy, string sortOrder, string category)
		{
			 this.sortBy=sortBy;

			 this.sortOrder=sortOrder;

			 this.category=category;


		}

		/// <summary>The method to get inventory templates</summary>
		/// <param name="paramInstance">Instance of ParameterMap</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetInventoryTemplates(ParameterMap paramInstance)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/inventory_templates");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<string>("sort_by", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatesParam"),  this.sortBy);

			handlerInstance.AddParam(new Param<string>("sort_order", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatesParam"),  this.sortOrder);

			handlerInstance.AddParam(new Param<string>("category", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatesParam"),  this.category);

			handlerInstance.Param=paramInstance;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to get inventory template by id</summary>
		/// <param name="id">long?</param>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetInventoryTemplateById(long? id)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v2.1/settings/inventory_templates/");

			apiPath=string.Concat(apiPath, id.ToString());

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<string>("sort_by", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatebyIDParam"),  this.sortBy);

			handlerInstance.AddParam(new Param<string>("sort_order", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatebyIDParam"),  this.sortOrder);

			handlerInstance.AddParam(new Param<string>("category", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatebyIDParam"),  this.category);

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}


		public static class GetInventoryTemplatesParam
		{
			public static readonly Param<string> MODULE=new Param<string>("module", "com.zoho.crm.api.InventoryTemplates.GetInventoryTemplatesParam");
		}


		public static class GetInventoryTemplatebyIDParam
		{
		}

	}
}