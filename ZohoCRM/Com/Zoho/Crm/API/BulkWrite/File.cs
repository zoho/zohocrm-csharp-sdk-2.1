using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkWrite
{

	public class File : Model
	{
		Choice<string> status;
		string name;
		int? addedCount;
		int? skippedCount;
		int? updatedCount;
		int? totalCount;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Choice<string> Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">Instance of Choice<string></param>
			set
			{
				 status=value;

				 keyModified["status"] = 1;

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

		public int? AddedCount
		{
			/// <summary>The method to get the addedCount</summary>
			/// <returns>int? representing the addedCount</returns>
			get
			{
				return  addedCount;

			}
			/// <summary>The method to set the value to addedCount</summary>
			/// <param name="addedCount">int?</param>
			set
			{
				 addedCount=value;

				 keyModified["added_count"] = 1;

			}
		}

		public int? SkippedCount
		{
			/// <summary>The method to get the skippedCount</summary>
			/// <returns>int? representing the skippedCount</returns>
			get
			{
				return  skippedCount;

			}
			/// <summary>The method to set the value to skippedCount</summary>
			/// <param name="skippedCount">int?</param>
			set
			{
				 skippedCount=value;

				 keyModified["skipped_count"] = 1;

			}
		}

		public int? UpdatedCount
		{
			/// <summary>The method to get the updatedCount</summary>
			/// <returns>int? representing the updatedCount</returns>
			get
			{
				return  updatedCount;

			}
			/// <summary>The method to set the value to updatedCount</summary>
			/// <param name="updatedCount">int?</param>
			set
			{
				 updatedCount=value;

				 keyModified["updated_count"] = 1;

			}
		}

		public int? TotalCount
		{
			/// <summary>The method to get the totalCount</summary>
			/// <returns>int? representing the totalCount</returns>
			get
			{
				return  totalCount;

			}
			/// <summary>The method to set the value to totalCount</summary>
			/// <param name="totalCount">int?</param>
			set
			{
				 totalCount=value;

				 keyModified["total_count"] = 1;

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