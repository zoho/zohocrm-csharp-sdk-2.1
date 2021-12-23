using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class SuccessfulConvert : Model, ConvertActionResponse
	{
		string contacts;
		string deals;
		string accounts;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Contacts
		{
			/// <summary>The method to get the contacts</summary>
			/// <returns>string representing the contacts</returns>
			get
			{
				return  contacts;

			}
			/// <summary>The method to set the value to contacts</summary>
			/// <param name="contacts">string</param>
			set
			{
				 contacts=value;

				 keyModified["Contacts"] = 1;

			}
		}

		public string Deals
		{
			/// <summary>The method to get the deals</summary>
			/// <returns>string representing the deals</returns>
			get
			{
				return  deals;

			}
			/// <summary>The method to set the value to deals</summary>
			/// <param name="deals">string</param>
			set
			{
				 deals=value;

				 keyModified["Deals"] = 1;

			}
		}

		public string Accounts
		{
			/// <summary>The method to get the accounts</summary>
			/// <returns>string representing the accounts</returns>
			get
			{
				return  accounts;

			}
			/// <summary>The method to set the value to accounts</summary>
			/// <param name="accounts">string</param>
			set
			{
				 accounts=value;

				 keyModified["Accounts"] = 1;

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