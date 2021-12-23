using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class Reminder : Model
	{
		string period;
		string unit;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Period
		{
			/// <summary>The method to get the period</summary>
			/// <returns>string representing the period</returns>
			get
			{
				return  period;

			}
			/// <summary>The method to set the value to period</summary>
			/// <param name="period">string</param>
			set
			{
				 period=value;

				 keyModified["period"] = 1;

			}
		}

		public string Unit
		{
			/// <summary>The method to get the unit</summary>
			/// <returns>string representing the unit</returns>
			get
			{
				return  unit;

			}
			/// <summary>The method to set the value to unit</summary>
			/// <param name="unit">string</param>
			set
			{
				 unit=value;

				 keyModified["unit"] = 1;

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