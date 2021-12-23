using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class Private : Model
	{
		bool? restricted;
		bool? export;
		string type;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? Restricted
		{
			/// <summary>The method to get the restricted</summary>
			/// <returns>bool? representing the restricted</returns>
			get
			{
				return  restricted;

			}
			/// <summary>The method to set the value to restricted</summary>
			/// <param name="restricted">bool?</param>
			set
			{
				 restricted=value;

				 keyModified["restricted"] = 1;

			}
		}

		public bool? Export
		{
			/// <summary>The method to get the export</summary>
			/// <returns>bool? representing the export</returns>
			get
			{
				return  export;

			}
			/// <summary>The method to set the value to export</summary>
			/// <param name="export">bool?</param>
			set
			{
				 export=value;

				 keyModified["export"] = 1;

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