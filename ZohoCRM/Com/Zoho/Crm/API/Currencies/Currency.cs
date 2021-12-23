using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Currencies
{

	public class Currency : Model
	{
		string symbol;
		DateTimeOffset? createdTime;
		bool? isActive;
		string exchangeRate;
		Format format;
		User createdBy;
		bool? prefixSymbol;
		bool? isBase;
		DateTimeOffset? modifiedTime;
		string name;
		User modifiedBy;
		long? id;
		string isoCode;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Symbol
		{
			/// <summary>The method to get the symbol</summary>
			/// <returns>string representing the symbol</returns>
			get
			{
				return  symbol;

			}
			/// <summary>The method to set the value to symbol</summary>
			/// <param name="symbol">string</param>
			set
			{
				 symbol=value;

				 keyModified["symbol"] = 1;

			}
		}

		public DateTimeOffset? CreatedTime
		{
			/// <summary>The method to get the createdTime</summary>
			/// <returns>DateTimeOffset? representing the createdTime</returns>
			get
			{
				return  createdTime;

			}
			/// <summary>The method to set the value to createdTime</summary>
			/// <param name="createdTime">DateTimeOffset?</param>
			set
			{
				 createdTime=value;

				 keyModified["created_time"] = 1;

			}
		}

		public bool? IsActive
		{
			/// <summary>The method to get the isActive</summary>
			/// <returns>bool? representing the isActive</returns>
			get
			{
				return  isActive;

			}
			/// <summary>The method to set the value to isActive</summary>
			/// <param name="isActive">bool?</param>
			set
			{
				 isActive=value;

				 keyModified["is_active"] = 1;

			}
		}

		public string ExchangeRate
		{
			/// <summary>The method to get the exchangeRate</summary>
			/// <returns>string representing the exchangeRate</returns>
			get
			{
				return  exchangeRate;

			}
			/// <summary>The method to set the value to exchangeRate</summary>
			/// <param name="exchangeRate">string</param>
			set
			{
				 exchangeRate=value;

				 keyModified["exchange_rate"] = 1;

			}
		}

		public Format Format
		{
			/// <summary>The method to get the format</summary>
			/// <returns>Instance of Format</returns>
			get
			{
				return  format;

			}
			/// <summary>The method to set the value to format</summary>
			/// <param name="format">Instance of Format</param>
			set
			{
				 format=value;

				 keyModified["format"] = 1;

			}
		}

		public User CreatedBy
		{
			/// <summary>The method to get the createdBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  createdBy;

			}
			/// <summary>The method to set the value to createdBy</summary>
			/// <param name="createdBy">Instance of User</param>
			set
			{
				 createdBy=value;

				 keyModified["created_by"] = 1;

			}
		}

		public bool? PrefixSymbol
		{
			/// <summary>The method to get the prefixSymbol</summary>
			/// <returns>bool? representing the prefixSymbol</returns>
			get
			{
				return  prefixSymbol;

			}
			/// <summary>The method to set the value to prefixSymbol</summary>
			/// <param name="prefixSymbol">bool?</param>
			set
			{
				 prefixSymbol=value;

				 keyModified["prefix_symbol"] = 1;

			}
		}

		public bool? IsBase
		{
			/// <summary>The method to get the isBase</summary>
			/// <returns>bool? representing the isBase</returns>
			get
			{
				return  isBase;

			}
			/// <summary>The method to set the value to isBase</summary>
			/// <param name="isBase">bool?</param>
			set
			{
				 isBase=value;

				 keyModified["is_base"] = 1;

			}
		}

		public DateTimeOffset? ModifiedTime
		{
			/// <summary>The method to get the modifiedTime</summary>
			/// <returns>DateTimeOffset? representing the modifiedTime</returns>
			get
			{
				return  modifiedTime;

			}
			/// <summary>The method to set the value to modifiedTime</summary>
			/// <param name="modifiedTime">DateTimeOffset?</param>
			set
			{
				 modifiedTime=value;

				 keyModified["modified_time"] = 1;

			}
		}

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				return  name;

			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 name=value;

				 keyModified["name"] = 1;

			}
		}

		public User ModifiedBy
		{
			/// <summary>The method to get the modifiedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  modifiedBy;

			}
			/// <summary>The method to set the value to modifiedBy</summary>
			/// <param name="modifiedBy">Instance of User</param>
			set
			{
				 modifiedBy=value;

				 keyModified["modified_by"] = 1;

			}
		}

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 id=value;

				 keyModified["id"] = 1;

			}
		}

		public string IsoCode
		{
			/// <summary>The method to get the isoCode</summary>
			/// <returns>string representing the isoCode</returns>
			get
			{
				return  isoCode;

			}
			/// <summary>The method to set the value to isoCode</summary>
			/// <param name="isoCode">string</param>
			set
			{
				 isoCode=value;

				 keyModified["iso_code"] = 1;

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