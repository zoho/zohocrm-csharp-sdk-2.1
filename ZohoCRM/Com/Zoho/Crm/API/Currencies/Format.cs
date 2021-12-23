using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Currencies
{

	public class Format : Model
	{
		Choice<string> decimalSeparator;
		Choice<string> thousandSeparator;
		Choice<string> decimalPlaces;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Choice<string> DecimalSeparator
		{
			/// <summary>The method to get the decimalSeparator</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  decimalSeparator;

			}
			/// <summary>The method to set the value to decimalSeparator</summary>
			/// <param name="decimalSeparator">Instance of Choice<string></param>
			set
			{
				 decimalSeparator=value;

				 keyModified["decimal_separator"] = 1;

			}
		}

		public Choice<string> ThousandSeparator
		{
			/// <summary>The method to get the thousandSeparator</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  thousandSeparator;

			}
			/// <summary>The method to set the value to thousandSeparator</summary>
			/// <param name="thousandSeparator">Instance of Choice<string></param>
			set
			{
				 thousandSeparator=value;

				 keyModified["thousand_separator"] = 1;

			}
		}

		public Choice<string> DecimalPlaces
		{
			/// <summary>The method to get the decimalPlaces</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  decimalPlaces;

			}
			/// <summary>The method to set the value to decimalPlaces</summary>
			/// <param name="decimalPlaces">Instance of Choice<string></param>
			set
			{
				 decimalPlaces=value;

				 keyModified["decimal_places"] = 1;

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