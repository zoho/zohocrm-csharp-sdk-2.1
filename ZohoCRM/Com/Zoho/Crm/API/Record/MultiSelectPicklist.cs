using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class MultiSelectPicklist : Model
	{
		long? id;
		Dictionary<string, object> fieldname;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 id=value;

				 keyModified["id"] = 1;

			}
		}

		public Dictionary<string, object> Fieldname
		{
			/// <summary>The method to get the fieldname</summary>
			/// <returns>Dictionary representing the fieldname<String,Object></returns>
			get
			{
				return  fieldname;

			}
			/// <summary>The method to set the value to fieldname</summary>
			/// <param name="fieldname">Dictionary<string,object></param>
			set
			{
				 fieldname=value;

				 keyModified["fieldName"] = 1;

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