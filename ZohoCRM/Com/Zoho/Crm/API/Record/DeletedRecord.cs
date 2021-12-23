using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class DeletedRecord : Model
	{
		User deletedBy;
		long? id;
		string displayName;
		string type;
		User createdBy;
		DateTimeOffset? deletedTime;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public User DeletedBy
		{
			/// <summary>The method to get the deletedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  deletedBy;

			}
			/// <summary>The method to set the value to deletedBy</summary>
			/// <param name="deletedBy">Instance of User</param>
			set
			{
				 deletedBy=value;

				 keyModified["deleted_by"] = 1;

			}
		}

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 id=value;

				 keyModified["id"] = 1;

			}
		}

		public string DisplayName
		{
			/// <summary>The method to get the displayName</summary>
			/// <returns>string representing the displayName</returns>
			get
			{
				return  displayName;

			}
			/// <summary>The method to set the value to displayName</summary>
			/// <param name="displayName">string</param>
			set
			{
				 displayName=value;

				 keyModified["display_name"] = 1;

			}
		}

		public string Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>string representing the type</returns>
			get
			{
				return  type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">string</param>
			set
			{
				 type=value;

				 keyModified["type"] = 1;

			}
		}

		public User CreatedBy
		{
			/// <summary>The method to get the createdBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  createdBy;

			}
			/// <summary>The method to set the value to createdBy</summary>
			/// <param name="createdBy">Instance of User</param>
			set
			{
				 createdBy=value;

				 keyModified["created_by"] = 1;

			}
		}

		public DateTimeOffset? DeletedTime
		{
			/// <summary>The method to get the deletedTime</summary>
			/// <returns>DateTimeOffset? representing the deletedTime</returns>
			get
			{
				return  deletedTime;

			}
			/// <summary>The method to set the value to deletedTime</summary>
			/// <param name="deletedTime">DateTimeOffset?</param>
			set
			{
				 deletedTime=value;

				 keyModified["deleted_time"] = 1;

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