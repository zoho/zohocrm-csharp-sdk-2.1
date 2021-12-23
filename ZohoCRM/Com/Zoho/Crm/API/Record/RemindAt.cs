using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class RemindAt : Model
	{
		string alarm;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Alarm
		{
			/// <summary>The method to get the alarm</summary>
			/// <returns>string representing the alarm</returns>
			get
			{
				return  alarm;

			}
			/// <summary>The method to set the value to alarm</summary>
			/// <param name="alarm">string</param>
			set
			{
				 alarm=value;

				 keyModified["ALARM"] = 1;

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