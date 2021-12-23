using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.InventoryTemplates
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		List<InventoryTemplate> inventoryTemplates;
		Info info;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<InventoryTemplate> InventoryTemplates
		{
			/// <summary>The method to get the inventoryTemplates</summary>
			/// <returns>Instance of List<InventoryTemplate></returns>
			get
			{
				return  inventoryTemplates;

			}
			/// <summary>The method to set the value to inventoryTemplates</summary>
			/// <param name="inventoryTemplates">Instance of List<InventoryTemplate></param>
			set
			{
				 inventoryTemplates=value;

				 keyModified["inventory_templates"] = 1;

			}
		}

		public Info Info
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return  info;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="info">Instance of Info</param>
			set
			{
				 info=value;

				 keyModified["info"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if((( keyModified.ContainsKey(key))))
			{
				return  keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			 keyModified[key] = modification;


		}


	}
}