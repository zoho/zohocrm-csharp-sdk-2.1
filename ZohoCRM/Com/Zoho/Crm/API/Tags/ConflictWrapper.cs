using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Tags
{

	public class ConflictWrapper : Model
	{
		string conflictId;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string ConflictId
		{
			/// <summary>The method to get the conflictId</summary>
			/// <returns>string representing the conflictId</returns>
			get
			{
				return  conflictId;

			}
			/// <summary>The method to set the value to conflictId</summary>
			/// <param name="conflictId">string</param>
			set
			{
				 conflictId=value;

				 keyModified["conflict_id"] = 1;

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