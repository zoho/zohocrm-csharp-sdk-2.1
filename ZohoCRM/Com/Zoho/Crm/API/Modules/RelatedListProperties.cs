using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Modules
{

	public class RelatedListProperties : Model
	{
		string sortBy;
		List<string> fields;
		string sortOrder;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string SortBy
		{
			/// <summary>The method to get the sortBy</summary>
			/// <returns>string representing the sortBy</returns>
			get
			{
				return  sortBy;

			}
			/// <summary>The method to set the value to sortBy</summary>
			/// <param name="sortBy">string</param>
			set
			{
				 sortBy=value;

				 keyModified["sort_by"] = 1;

			}
		}

		public List<string> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Instance of List<string></param>
			set
			{
				 fields=value;

				 keyModified["fields"] = 1;

			}
		}

		public string SortOrder
		{
			/// <summary>The method to get the sortOrder</summary>
			/// <returns>string representing the sortOrder</returns>
			get
			{
				return  sortOrder;

			}
			/// <summary>The method to set the value to sortOrder</summary>
			/// <param name="sortOrder">string</param>
			set
			{
				 sortOrder=value;

				 keyModified["sort_order"] = 1;

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