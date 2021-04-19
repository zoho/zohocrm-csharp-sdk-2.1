using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class InventoryLineItems : Record , Model
	{

		public LineItemProduct ProductName
		{
			/// <summary>The method to get the productName</summary>
			/// <returns>Instance of LineItemProduct</returns>
			get
			{
				if((( this.GetKeyValue("Product_Name")) != (null)))
				{
					return (LineItemProduct) this.GetKeyValue("Product_Name");

				}
					return null;


			}
			/// <summary>The method to set the value to productName</summary>
			/// <param name="productName">Instance of LineItemProduct</param>
			set
			{
				 this.AddKeyValue("Product_Name", value);

			}
		}

		public Record ParentId
		{
			/// <summary>The method to get the parentId</summary>
			/// <returns>Instance of Record</returns>
			get
			{
				if((( this.GetKeyValue("parent_id")) != (null)))
				{
					return (Record) this.GetKeyValue("parent_id");

				}
					return null;


			}
			/// <summary>The method to set the value to parentId</summary>
			/// <param name="parentId">Instance of Record</param>
			set
			{
				 this.AddKeyValue("parent_id", value);

			}
		}

		public double? Quantity
		{
			/// <summary>The method to get the quantity</summary>
			/// <returns>double? representing the quantity</returns>
			get
			{
				if((( this.GetKeyValue("Quantity")) != (null)))
				{
					return (double?) this.GetKeyValue("Quantity");

				}
					return null;


			}
			/// <summary>The method to set the value to quantity</summary>
			/// <param name="quantity">double?</param>
			set
			{
				 this.AddKeyValue("Quantity", value);

			}
		}

		public string Discount
		{
			/// <summary>The method to get the discount</summary>
			/// <returns>string representing the discount</returns>
			get
			{
				if((( this.GetKeyValue("Discount")) != (null)))
				{
					return (string) this.GetKeyValue("Discount");

				}
					return null;


			}
			/// <summary>The method to set the value to discount</summary>
			/// <param name="discount">string</param>
			set
			{
				 this.AddKeyValue("Discount", value);

			}
		}

		public double? TotalAfterDiscount
		{
			/// <summary>The method to get the totalAfterDiscount</summary>
			/// <returns>double? representing the totalAfterDiscount</returns>
			get
			{
				if((( this.GetKeyValue("Total_After_Discount")) != (null)))
				{
					return (double?) this.GetKeyValue("Total_After_Discount");

				}
					return null;


			}
			/// <summary>The method to set the value to totalAfterDiscount</summary>
			/// <param name="totalAfterDiscount">double?</param>
			set
			{
				 this.AddKeyValue("Total_After_Discount", value);

			}
		}

		public double? NetTotal
		{
			/// <summary>The method to get the netTotal</summary>
			/// <returns>double? representing the netTotal</returns>
			get
			{
				if((( this.GetKeyValue("Net_Total")) != (null)))
				{
					return (double?) this.GetKeyValue("Net_Total");

				}
					return null;


			}
			/// <summary>The method to set the value to netTotal</summary>
			/// <param name="netTotal">double?</param>
			set
			{
				 this.AddKeyValue("Net_Total", value);

			}
		}

		public double? PriceBookName
		{
			/// <summary>The method to get the priceBookName</summary>
			/// <returns>double? representing the priceBookName</returns>
			get
			{
				if((( this.GetKeyValue("Price_Book_Name")) != (null)))
				{
					return (double?) this.GetKeyValue("Price_Book_Name");

				}
					return null;


			}
			/// <summary>The method to set the value to priceBookName</summary>
			/// <param name="priceBookName">double?</param>
			set
			{
				 this.AddKeyValue("Price_Book_Name", value);

			}
		}

		public double? Tax
		{
			/// <summary>The method to get the tax</summary>
			/// <returns>double? representing the tax</returns>
			get
			{
				if((( this.GetKeyValue("Tax")) != (null)))
				{
					return (double?) this.GetKeyValue("Tax");

				}
					return null;


			}
			/// <summary>The method to set the value to tax</summary>
			/// <param name="tax">double?</param>
			set
			{
				 this.AddKeyValue("Tax", value);

			}
		}

		public double? ListPrice
		{
			/// <summary>The method to get the listPrice</summary>
			/// <returns>double? representing the listPrice</returns>
			get
			{
				if((( this.GetKeyValue("List_Price")) != (null)))
				{
					return (double?) this.GetKeyValue("List_Price");

				}
					return null;


			}
			/// <summary>The method to set the value to listPrice</summary>
			/// <param name="listPrice">double?</param>
			set
			{
				 this.AddKeyValue("List_Price", value);

			}
		}

		public double? UnitPrice
		{
			/// <summary>The method to get the unitPrice</summary>
			/// <returns>double? representing the unitPrice</returns>
			get
			{
				if((( this.GetKeyValue("unit_price")) != (null)))
				{
					return (double?) this.GetKeyValue("unit_price");

				}
					return null;


			}
			/// <summary>The method to set the value to unitPrice</summary>
			/// <param name="unitPrice">double?</param>
			set
			{
				 this.AddKeyValue("unit_price", value);

			}
		}

		public double? QuantityInStock
		{
			/// <summary>The method to get the quantityInStock</summary>
			/// <returns>double? representing the quantityInStock</returns>
			get
			{
				if((( this.GetKeyValue("quantity_in_stock")) != (null)))
				{
					return (double?) this.GetKeyValue("quantity_in_stock");

				}
					return null;


			}
			/// <summary>The method to set the value to quantityInStock</summary>
			/// <param name="quantityInStock">double?</param>
			set
			{
				 this.AddKeyValue("quantity_in_stock", value);

			}
		}

		public double? Total
		{
			/// <summary>The method to get the total</summary>
			/// <returns>double? representing the total</returns>
			get
			{
				if((( this.GetKeyValue("Total")) != (null)))
				{
					return (double?) this.GetKeyValue("Total");

				}
					return null;


			}
			/// <summary>The method to set the value to total</summary>
			/// <param name="total">double?</param>
			set
			{
				 this.AddKeyValue("Total", value);

			}
		}

		public string Description
		{
			/// <summary>The method to get the description</summary>
			/// <returns>string representing the description</returns>
			get
			{
				if((( this.GetKeyValue("Description")) != (null)))
				{
					return (string) this.GetKeyValue("Description");

				}
					return null;


			}
			/// <summary>The method to set the value to description</summary>
			/// <param name="description">string</param>
			set
			{
				 this.AddKeyValue("Description", value);

			}
		}

		public List<LineTax> LineTax
		{
			/// <summary>The method to get the lineTax</summary>
			/// <returns>Instance of List<LineTax></returns>
			get
			{
				if((( this.GetKeyValue("Line_Tax")) != (null)))
				{
					return (List<LineTax>) this.GetKeyValue("Line_Tax");

				}
					return null;


			}
			/// <summary>The method to set the value to lineTax</summary>
			/// <param name="lineTax">Instance of List<LineTax></param>
			set
			{
				 this.AddKeyValue("Line_Tax", value);

			}
		}


	}
}