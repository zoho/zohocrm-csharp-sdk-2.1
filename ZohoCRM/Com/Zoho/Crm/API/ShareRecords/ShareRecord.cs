using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.ShareRecords
{

	public class ShareRecord : Model
	{
		bool? shareRelatedRecords;
		SharedThrough sharedThrough;
		DateTimeOffset? sharedTime;
		string permission;
		User sharedBy;
		User user;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? ShareRelatedRecords
		{
			/// <summary>The method to get the shareRelatedRecords</summary>
			/// <returns>bool? representing the shareRelatedRecords</returns>
			get
			{
				return  shareRelatedRecords;

			}
			/// <summary>The method to set the value to shareRelatedRecords</summary>
			/// <param name="shareRelatedRecords">bool?</param>
			set
			{
				 shareRelatedRecords=value;

				 keyModified["share_related_records"] = 1;

			}
		}

		public SharedThrough SharedThrough
		{
			/// <summary>The method to get the sharedThrough</summary>
			/// <returns>Instance of SharedThrough</returns>
			get
			{
				return  sharedThrough;

			}
			/// <summary>The method to set the value to sharedThrough</summary>
			/// <param name="sharedThrough">Instance of SharedThrough</param>
			set
			{
				 sharedThrough=value;

				 keyModified["shared_through"] = 1;

			}
		}

		public DateTimeOffset? SharedTime
		{
			/// <summary>The method to get the sharedTime</summary>
			/// <returns>DateTimeOffset? representing the sharedTime</returns>
			get
			{
				return  sharedTime;

			}
			/// <summary>The method to set the value to sharedTime</summary>
			/// <param name="sharedTime">DateTimeOffset?</param>
			set
			{
				 sharedTime=value;

				 keyModified["shared_time"] = 1;

			}
		}

		public string Permission
		{
			/// <summary>The method to get the permission</summary>
			/// <returns>string representing the permission</returns>
			get
			{
				return  permission;

			}
			/// <summary>The method to set the value to permission</summary>
			/// <param name="permission">string</param>
			set
			{
				 permission=value;

				 keyModified["permission"] = 1;

			}
		}

		public User SharedBy
		{
			/// <summary>The method to get the sharedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  sharedBy;

			}
			/// <summary>The method to set the value to sharedBy</summary>
			/// <param name="sharedBy">Instance of User</param>
			set
			{
				 sharedBy=value;

				 keyModified["shared_by"] = 1;

			}
		}

		public User User
		{
			/// <summary>The method to get the user</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  user;

			}
			/// <summary>The method to set the value to user</summary>
			/// <param name="user">Instance of User</param>
			set
			{
				 user=value;

				 keyModified["user"] = 1;

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