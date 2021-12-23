using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Taxes
{

	public class Preference : Model
	{
		bool? autoPopulateTax;
		bool? modifyTaxRates;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? AutoPopulateTax
		{
			/// <summary>The method to get the autoPopulateTax</summary>
			/// <returns>bool? representing the autoPopulateTax</returns>
			get
			{
				return  autoPopulateTax;

			}
			/// <summary>The method to set the value to autoPopulateTax</summary>
			/// <param name="autoPopulateTax">bool?</param>
			set
			{
				 autoPopulateTax=value;

				 keyModified["auto_populate_tax"] = 1;

			}
		}

		public bool? ModifyTaxRates
		{
			/// <summary>The method to get the modifyTaxRates</summary>
			/// <returns>bool? representing the modifyTaxRates</returns>
			get
			{
				return  modifyTaxRates;

			}
			/// <summary>The method to set the value to modifyTaxRates</summary>
			/// <param name="modifyTaxRates">bool?</param>
			set
			{
				 modifyTaxRates=value;

				 keyModified["modify_tax_rates"] = 1;

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