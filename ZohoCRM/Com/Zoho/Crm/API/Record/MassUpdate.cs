using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class MassUpdate : Model, MassUpdateResponse
	{
		Choice<string> status;
		int? failedCount;
		int? updatedCount;
		int? notUpdatedCount;
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

				 keyModified["Status"] = 1;

			}
		}

		public int? FailedCount
		{
			/// <summary>The method to get the failedCount</summary>
			/// <returns>int? representing the failedCount</returns>
			get
			{
				return  failedCount;

			}
			/// <summary>The method to set the value to failedCount</summary>
			/// <param name="failedCount">int?</param>
			set
			{
				 failedCount=value;

				 keyModified["Failed_Count"] = 1;

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

				 keyModified["Updated_Count"] = 1;

			}
		}

		public int? NotUpdatedCount
		{
			/// <summary>The method to get the notUpdatedCount</summary>
			/// <returns>int? representing the notUpdatedCount</returns>
			get
			{
				return  notUpdatedCount;

			}
			/// <summary>The method to set the value to notUpdatedCount</summary>
			/// <param name="notUpdatedCount">int?</param>
			set
			{
				 notUpdatedCount=value;

				 keyModified["Not_Updated_Count"] = 1;

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

				 keyModified["Total_Count"] = 1;

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