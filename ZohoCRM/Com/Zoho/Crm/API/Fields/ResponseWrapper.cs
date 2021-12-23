using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		List<Field> fields;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Field> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Instance of List<Field></returns>
			get
			{
				return  fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Instance of List<Field></param>
			set
			{
				 fields=value;

				 keyModified["fields"] = 1;

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