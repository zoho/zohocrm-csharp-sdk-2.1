using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BluePrint
{

	public class Escalation : Model
	{
		int? days;
		string status;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public int? Days
		{
			/// <summary>The method to get the days</summary>
			/// <returns>int? representing the days</returns>
			get
			{
				return  days;

			}
			/// <summary>The method to set the value to days</summary>
			/// <param name="days">int?</param>
			set
			{
				 days=value;

				 keyModified["days"] = 1;

			}
		}

		public string Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>string representing the status</returns>
			get
			{
				return  status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">string</param>
			set
			{
				 status=value;

				 keyModified["status"] = 1;

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