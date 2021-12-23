using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class BodyWrapper : Model
	{
		List<Record> data;
		List<string> trigger;
		List<string> duplicateCheckFields;
		string wfTrigger;
		string larId;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Record> Data
		{
			/// <summary>The method to get the data</summary>
			/// <returns>Instance of List<Record></returns>
			get
			{
				return  data;

			}
			/// <summary>The method to set the value to data</summary>
			/// <param name="data">Instance of List<Record></param>
			set
			{
				 data=value;

				 keyModified["data"] = 1;

			}
		}

		public List<string> Trigger
		{
			/// <summary>The method to get the trigger</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  trigger;

			}
			/// <summary>The method to set the value to trigger</summary>
			/// <param name="trigger">Instance of List<string></param>
			set
			{
				 trigger=value;

				 keyModified["trigger"] = 1;

			}
		}

		public List<string> DuplicateCheckFields
		{
			/// <summary>The method to get the duplicateCheckFields</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  duplicateCheckFields;

			}
			/// <summary>The method to set the value to duplicateCheckFields</summary>
			/// <param name="duplicateCheckFields">Instance of List<string></param>
			set
			{
				 duplicateCheckFields=value;

				 keyModified["duplicate_check_fields"] = 1;

			}
		}

		public string WfTrigger
		{
			/// <summary>The method to get the wfTrigger</summary>
			/// <returns>string representing the wfTrigger</returns>
			get
			{
				return  wfTrigger;

			}
			/// <summary>The method to set the value to wfTrigger</summary>
			/// <param name="wfTrigger">string</param>
			set
			{
				 wfTrigger=value;

				 keyModified["wf_trigger"] = 1;

			}
		}

		public string LarId
		{
			/// <summary>The method to get the larId</summary>
			/// <returns>string representing the larId</returns>
			get
			{
				return  larId;

			}
			/// <summary>The method to set the value to larId</summary>
			/// <param name="larId">string</param>
			set
			{
				 larId=value;

				 keyModified["lar_id"] = 1;

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