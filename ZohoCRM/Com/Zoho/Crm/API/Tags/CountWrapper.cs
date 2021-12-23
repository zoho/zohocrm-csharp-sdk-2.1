using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Tags
{

	public class CountWrapper : Model, CountHandler
	{
		string count;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Count
		{
			/// <summary>The method to get the count</summary>
			/// <returns>string representing the count</returns>
			get
			{
				return  count;

			}
			/// <summary>The method to set the value to count</summary>
			/// <param name="count">string</param>
			set
			{
				 count=value;

				 keyModified["count"] = 1;

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