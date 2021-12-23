using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Currencies
{

	public class BaseCurrencyActionWrapper : Model, BaseCurrencyActionHandler
	{
		ActionResponse baseCurrency;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public ActionResponse BaseCurrency
		{
			/// <summary>The method to get the baseCurrency</summary>
			/// <returns>Instance of ActionResponse</returns>
			get
			{
				return  baseCurrency;

			}
			/// <summary>The method to set the value to baseCurrency</summary>
			/// <param name="baseCurrency">Instance of ActionResponse</param>
			set
			{
				 baseCurrency=value;

				 keyModified["base_currency"] = 1;

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