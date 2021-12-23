using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Notification
{

	public class Info : Model
	{
		int? perPage;
		int? count;
		int? page;
		bool? moreRecords;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public int? PerPage
		{
			/// <summary>The method to get the perPage</summary>
			/// <returns>int? representing the perPage</returns>
			get
			{
				return  perPage;

			}
			/// <summary>The method to set the value to perPage</summary>
			/// <param name="perPage">int?</param>
			set
			{
				 perPage=value;

				 keyModified["per_page"] = 1;

			}
		}

		public int? Count
		{
			/// <summary>The method to get the count</summary>
			/// <returns>int? representing the count</returns>
			get
			{
				return  count;

			}
			/// <summary>The method to set the value to count</summary>
			/// <param name="count">int?</param>
			set
			{
				 count=value;

				 keyModified["count"] = 1;

			}
		}

		public int? Page
		{
			/// <summary>The method to get the page</summary>
			/// <returns>int? representing the page</returns>
			get
			{
				return  page;

			}
			/// <summary>The method to set the value to page</summary>
			/// <param name="page">int?</param>
			set
			{
				 page=value;

				 keyModified["page"] = 1;

			}
		}

		public bool? MoreRecords
		{
			/// <summary>The method to get the moreRecords</summary>
			/// <returns>bool? representing the moreRecords</returns>
			get
			{
				return  moreRecords;

			}
			/// <summary>The method to set the value to moreRecords</summary>
			/// <param name="moreRecords">bool?</param>
			set
			{
				 moreRecords=value;

				 keyModified["more_records"] = 1;

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