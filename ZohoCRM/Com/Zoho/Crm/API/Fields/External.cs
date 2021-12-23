using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class External : Model
	{
		bool? show;
		string type;
		bool? allowMultipleConfig;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? Show
		{
			/// <summary>The method to get the show</summary>
			/// <returns>bool? representing the show</returns>
			get
			{
				return  show;

			}
			/// <summary>The method to set the value to show</summary>
			/// <param name="show">bool?</param>
			set
			{
				 show=value;

				 keyModified["show"] = 1;

			}
		}

		public string Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>string representing the type</returns>
			get
			{
				return  type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">string</param>
			set
			{
				 type=value;

				 keyModified["type"] = 1;

			}
		}

		public bool? AllowMultipleConfig
		{
			/// <summary>The method to get the allowMultipleConfig</summary>
			/// <returns>bool? representing the allowMultipleConfig</returns>
			get
			{
				return  allowMultipleConfig;

			}
			/// <summary>The method to set the value to allowMultipleConfig</summary>
			/// <param name="allowMultipleConfig">bool?</param>
			set
			{
				 allowMultipleConfig=value;

				 keyModified["allow_multiple_config"] = 1;

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