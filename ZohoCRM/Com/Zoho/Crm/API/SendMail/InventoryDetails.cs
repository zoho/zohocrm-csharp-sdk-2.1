using Com.Zoho.Crm.API.InventoryTemplates;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SendMail
{

	public class InventoryDetails : Model
	{
		InventoryTemplate inventoryTemplate;
		string paperType;
		string viewType;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public InventoryTemplate InventoryTemplate
		{
			/// <summary>The method to get the inventoryTemplate</summary>
			/// <returns>Instance of InventoryTemplate</returns>
			get
			{
				return  inventoryTemplate;

			}
			/// <summary>The method to set the value to inventoryTemplate</summary>
			/// <param name="inventoryTemplate">Instance of InventoryTemplate</param>
			set
			{
				 inventoryTemplate=value;

				 keyModified["inventory_template"] = 1;

			}
		}

		public string PaperType
		{
			/// <summary>The method to get the paperType</summary>
			/// <returns>string representing the paperType</returns>
			get
			{
				return  paperType;

			}
			/// <summary>The method to set the value to paperType</summary>
			/// <param name="paperType">string</param>
			set
			{
				 paperType=value;

				 keyModified["paper_type"] = 1;

			}
		}

		public string ViewType
		{
			/// <summary>The method to get the viewType</summary>
			/// <returns>string representing the viewType</returns>
			get
			{
				return  viewType;

			}
			/// <summary>The method to set the value to viewType</summary>
			/// <param name="viewType">string</param>
			set
			{
				 viewType=value;

				 keyModified["view_type"] = 1;

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