using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Currencies
{

	public class ActionWrapper : Model, ActionHandler
	{
		List<ActionResponse> currencies;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<ActionResponse> Currencies
		{
			/// <summary>The method to get the currencies</summary>
			/// <returns>Instance of List<ActionResponse></returns>
			get
			{
				return  currencies;

			}
			/// <summary>The method to set the value to currencies</summary>
			/// <param name="currencies">Instance of List<ActionResponse></param>
			set
			{
				 currencies=value;

				 keyModified["currencies"] = 1;

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