using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Record
{

	public class PricingDetails : Record , Model
	{

		public double? ToRange
		{
			/// <summary>The method to get the toRange</summary>
			/// <returns>double? representing the toRange</returns>
			get
			{
				if((( GetKeyValue("to_range")) != (null)))
				{
					return (double?) GetKeyValue("to_range");

				}
					return null;


			}
			/// <summary>The method to set the value to toRange</summary>
			/// <param name="toRange">double?</param>
			set
			{
				 AddKeyValue("to_range", value);

			}
		}

		public double? Discount
		{
			/// <summary>The method to get the discount</summary>
			/// <returns>double? representing the discount</returns>
			get
			{
				if((( GetKeyValue("discount")) != (null)))
				{
					return (double?) GetKeyValue("discount");

				}
					return null;


			}
			/// <summary>The method to set the value to discount</summary>
			/// <param name="discount">double?</param>
			set
			{
				 AddKeyValue("discount", value);

			}
		}

		public double? FromRange
		{
			/// <summary>The method to get the fromRange</summary>
			/// <returns>double? representing the fromRange</returns>
			get
			{
				if((( GetKeyValue("from_range")) != (null)))
				{
					return (double?) GetKeyValue("from_range");

				}
					return null;


			}
			/// <summary>The method to set the value to fromRange</summary>
			/// <param name="fromRange">double?</param>
			set
			{
				 AddKeyValue("from_range", value);

			}
		}


	}
}