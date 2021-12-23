using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Tags
{

	public class MergeWrapper : Model
	{
		List<ConflictWrapper> tags;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<ConflictWrapper> Tags
		{
			/// <summary>The method to get the tags</summary>
			/// <returns>Instance of List<ConflictWrapper></returns>
			get
			{
				return  tags;

			}
			/// <summary>The method to set the value to tags</summary>
			/// <param name="tags">Instance of List<ConflictWrapper></param>
			set
			{
				 tags=value;

				 keyModified["tags"] = 1;

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