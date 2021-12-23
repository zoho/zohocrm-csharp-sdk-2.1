using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class HistoryTracking : Model
	{
		Module module;
		Field durationConfiguredField;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Module Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">Instance of Module</param>
			set
			{
				 module=value;

				 keyModified["module"] = 1;

			}
		}

		public Field DurationConfiguredField
		{
			/// <summary>The method to get the durationConfiguredField</summary>
			/// <returns>Instance of Field</returns>
			get
			{
				return  durationConfiguredField;

			}
			/// <summary>The method to set the value to durationConfiguredField</summary>
			/// <param name="durationConfiguredField">Instance of Field</param>
			set
			{
				 durationConfiguredField=value;

				 keyModified["duration_configured_field"] = 1;

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