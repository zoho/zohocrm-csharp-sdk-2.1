using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class AutoNumber : Model
	{
		string prefix;
		string suffix;
		int? startNumber;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Prefix
		{
			/// <summary>The method to get the prefix</summary>
			/// <returns>string representing the prefix</returns>
			get
			{
				return  prefix;

			}
			/// <summary>The method to set the value to prefix</summary>
			/// <param name="prefix">string</param>
			set
			{
				 prefix=value;

				 keyModified["prefix"] = 1;

			}
		}

		public string Suffix
		{
			/// <summary>The method to get the suffix</summary>
			/// <returns>string representing the suffix</returns>
			get
			{
				return  suffix;

			}
			/// <summary>The method to set the value to suffix</summary>
			/// <param name="suffix">string</param>
			set
			{
				 suffix=value;

				 keyModified["suffix"] = 1;

			}
		}

		public int? StartNumber
		{
			/// <summary>The method to get the startNumber</summary>
			/// <returns>int? representing the startNumber</returns>
			get
			{
				return  startNumber;

			}
			/// <summary>The method to set the value to startNumber</summary>
			/// <param name="startNumber">int?</param>
			set
			{
				 startNumber=value;

				 keyModified["start_number"] = 1;

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