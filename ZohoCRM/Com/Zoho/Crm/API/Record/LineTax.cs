using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class LineTax : Model
	{
		double? percentage;
		string name;
		long? id;
		double? value;
		string displayName;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public double? Percentage
		{
			/// <summary>The method to get the percentage</summary>
			/// <returns>double? representing the percentage</returns>
			get
			{
				return  percentage;

			}
			/// <summary>The method to set the value to percentage</summary>
			/// <param name="percentage">double?</param>
			set
			{
				 percentage=value;

				 keyModified["percentage"] = 1;

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

		public double? Value
		{
			/// <summary>The method to get the value</summary>
			/// <returns>double? representing the value</returns>
			get
			{
				return  value;

			}
			/// <summary>The method to set the value to value</summary>
			/// <param name="value">double?</param>
			set
			{
				 this.value=value;

				 keyModified["value"] = 1;

			}
		}

		public string DisplayName
		{
			/// <summary>The method to get the displayName</summary>
			/// <returns>string representing the displayName</returns>
			get
			{
				return  displayName;

			}
			/// <summary>The method to set the value to displayName</summary>
			/// <param name="displayName">string</param>
			set
			{
				 displayName=value;

				 keyModified["display_name"] = 1;

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