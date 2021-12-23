using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Tags
{

	public class RecordActionWrapper : Model, RecordActionHandler
	{
		List<RecordActionResponse> data;
		bool? wfScheduler;
		string successCount;
		int? lockedCount;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<RecordActionResponse> Data
		{
			/// <summary>The method to get the data</summary>
			/// <returns>Instance of List<RecordActionResponse></returns>
			get
			{
				return  data;

			}
			/// <summary>The method to set the value to data</summary>
			/// <param name="data">Instance of List<RecordActionResponse></param>
			set
			{
				 data=value;

				 keyModified["data"] = 1;

			}
		}

		public bool? WfScheduler
		{
			/// <summary>The method to get the wfScheduler</summary>
			/// <returns>bool? representing the wfScheduler</returns>
			get
			{
				return  wfScheduler;

			}
			/// <summary>The method to set the value to wfScheduler</summary>
			/// <param name="wfScheduler">bool?</param>
			set
			{
				 wfScheduler=value;

				 keyModified["wf_scheduler"] = 1;

			}
		}

		public string SuccessCount
		{
			/// <summary>The method to get the successCount</summary>
			/// <returns>string representing the successCount</returns>
			get
			{
				return  successCount;

			}
			/// <summary>The method to set the value to successCount</summary>
			/// <param name="successCount">string</param>
			set
			{
				 successCount=value;

				 keyModified["success_count"] = 1;

			}
		}

		public int? LockedCount
		{
			/// <summary>The method to get the lockedCount</summary>
			/// <returns>int? representing the lockedCount</returns>
			get
			{
				return  lockedCount;

			}
			/// <summary>The method to set the value to lockedCount</summary>
			/// <param name="lockedCount">int?</param>
			set
			{
				 lockedCount=value;

				 keyModified["locked_count"] = 1;

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