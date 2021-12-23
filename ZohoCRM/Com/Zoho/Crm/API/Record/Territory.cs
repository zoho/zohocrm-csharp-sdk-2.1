using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class Territory : Model
	{
		string assigned;
		string name;
		long? id;
		DateTimeOffset? assignedTime;
		User assignedBy;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Assigned
		{
			/// <summary>The method to get the assigned</summary>
			/// <returns>string representing the assigned</returns>
			get
			{
				return  assigned;

			}
			/// <summary>The method to set the value to assigned</summary>
			/// <param name="assigned">string</param>
			set
			{
				 assigned=value;

				 keyModified["$assigned"] = 1;

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

				 keyModified["Name"] = 1;

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

		public DateTimeOffset? AssignedTime
		{
			/// <summary>The method to get the assignedTime</summary>
			/// <returns>DateTimeOffset? representing the assignedTime</returns>
			get
			{
				return  assignedTime;

			}
			/// <summary>The method to set the value to assignedTime</summary>
			/// <param name="assignedTime">DateTimeOffset?</param>
			set
			{
				 assignedTime=value;

				 keyModified["$assigned_time"] = 1;

			}
		}

		public User AssignedBy
		{
			/// <summary>The method to get the assignedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  assignedBy;

			}
			/// <summary>The method to set the value to assignedBy</summary>
			/// <param name="assignedBy">Instance of User</param>
			set
			{
				 assignedBy=value;

				 keyModified["$assigned_by"] = 1;

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