using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Wizards
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		List<Wizard> wizards;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Wizard> Wizards
		{
			/// <summary>The method to get the wizards</summary>
			/// <returns>Instance of List<Wizard></returns>
			get
			{
				return  wizards;

			}
			/// <summary>The method to set the value to wizards</summary>
			/// <param name="wizards">Instance of List<Wizard></param>
			set
			{
				 wizards=value;

				 keyModified["wizards"] = 1;

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