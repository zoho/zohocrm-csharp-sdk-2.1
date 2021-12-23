using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Record
{

	public class LineItemProduct : Record , Model
	{

		public string ProductCode
		{
			/// <summary>The method to get the productCode</summary>
			/// <returns>string representing the productCode</returns>
			get
			{
				if((( GetKeyValue("Product_Code")) != (null)))
				{
					return (string) GetKeyValue("Product_Code");

				}
					return null;


			}
			/// <summary>The method to set the value to productCode</summary>
			/// <param name="productCode">string</param>
			set
			{
				 AddKeyValue("Product_Code", value);

			}
		}

		public string Currency
		{
			/// <summary>The method to get the currency</summary>
			/// <returns>string representing the currency</returns>
			get
			{
				if((( GetKeyValue("Currency")) != (null)))
				{
					return (string) GetKeyValue("Currency");

				}
					return null;


			}
			/// <summary>The method to set the value to currency</summary>
			/// <param name="currency">string</param>
			set
			{
				 AddKeyValue("Currency", value);

			}
		}

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				if((( GetKeyValue("name")) != (null)))
				{
					return (string) GetKeyValue("name");

				}
					return null;


			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 AddKeyValue("name", value);

			}
		}


	}
}