using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.RelatedLists
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		List<RelatedList> relatedLists;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<RelatedList> RelatedLists
		{
			/// <summary>The method to get the relatedLists</summary>
			/// <returns>Instance of List<RelatedList></returns>
			get
			{
				return  relatedLists;

			}
			/// <summary>The method to set the value to relatedLists</summary>
			/// <param name="relatedLists">Instance of List<RelatedList></param>
			set
			{
				 relatedLists=value;

				 keyModified["related_lists"] = 1;

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