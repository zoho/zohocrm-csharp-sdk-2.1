using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class Currency : Model
	{
		string roundingOption;
		int? precision;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string RoundingOption
		{
			/// <summary>The method to get the roundingOption</summary>
			/// <returns>string representing the roundingOption</returns>
			get
			{
				return  roundingOption;

			}
			/// <summary>The method to set the value to roundingOption</summary>
			/// <param name="roundingOption">string</param>
			set
			{
				 roundingOption=value;

				 keyModified["rounding_option"] = 1;

			}
		}

		public int? Precision
		{
			/// <summary>The method to get the precision</summary>
			/// <returns>int? representing the precision</returns>
			get
			{
				return  precision;

			}
			/// <summary>The method to set the value to precision</summary>
			/// <param name="precision">int?</param>
			set
			{
				 precision=value;

				 keyModified["precision"] = 1;

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