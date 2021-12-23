using Com.Zoho.Crm.API.CustomViews;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Territories
{

	public class Territory : Model
	{
		DateTimeOffset? createdTime;
		DateTimeOffset? modifiedTime;
		User manager;
		Criteria accountRuleCriteria;
		Criteria dealRuleCriteria;
		string name;
		User modifiedBy;
		string description;
		long? id;
		User createdBy;
		Territory reportingTo;
		string permissionType;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public DateTimeOffset? CreatedTime
		{
			/// <summary>The method to get the createdTime</summary>
			/// <returns>DateTimeOffset? representing the createdTime</returns>
			get
			{
				return  createdTime;

			}
			/// <summary>The method to set the value to createdTime</summary>
			/// <param name="createdTime">DateTimeOffset?</param>
			set
			{
				 createdTime=value;

				 keyModified["created_time"] = 1;

			}
		}

		public DateTimeOffset? ModifiedTime
		{
			/// <summary>The method to get the modifiedTime</summary>
			/// <returns>DateTimeOffset? representing the modifiedTime</returns>
			get
			{
				return  modifiedTime;

			}
			/// <summary>The method to set the value to modifiedTime</summary>
			/// <param name="modifiedTime">DateTimeOffset?</param>
			set
			{
				 modifiedTime=value;

				 keyModified["modified_time"] = 1;

			}
		}

		public User Manager
		{
			/// <summary>The method to get the manager</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  manager;

			}
			/// <summary>The method to set the value to manager</summary>
			/// <param name="manager">Instance of User</param>
			set
			{
				 manager=value;

				 keyModified["manager"] = 1;

			}
		}

		public Criteria AccountRuleCriteria
		{
			/// <summary>The method to get the accountRuleCriteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  accountRuleCriteria;

			}
			/// <summary>The method to set the value to accountRuleCriteria</summary>
			/// <param name="accountRuleCriteria">Instance of Criteria</param>
			set
			{
				 accountRuleCriteria=value;

				 keyModified["account_rule_criteria"] = 1;

			}
		}

		public Criteria DealRuleCriteria
		{
			/// <summary>The method to get the dealRuleCriteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  dealRuleCriteria;

			}
			/// <summary>The method to set the value to dealRuleCriteria</summary>
			/// <param name="dealRuleCriteria">Instance of Criteria</param>
			set
			{
				 dealRuleCriteria=value;

				 keyModified["deal_rule_criteria"] = 1;

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

		public User ModifiedBy
		{
			/// <summary>The method to get the modifiedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  modifiedBy;

			}
			/// <summary>The method to set the value to modifiedBy</summary>
			/// <param name="modifiedBy">Instance of User</param>
			set
			{
				 modifiedBy=value;

				 keyModified["modified_by"] = 1;

			}
		}

		public string Description
		{
			/// <summary>The method to get the description</summary>
			/// <returns>string representing the description</returns>
			get
			{
				return  description;

			}
			/// <summary>The method to set the value to description</summary>
			/// <param name="description">string</param>
			set
			{
				 description=value;

				 keyModified["description"] = 1;

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

		public Territory ReportingTo
		{
			/// <summary>The method to get the reportingTo</summary>
			/// <returns>Instance of Territory</returns>
			get
			{
				return  reportingTo;

			}
			/// <summary>The method to set the value to reportingTo</summary>
			/// <param name="reportingTo">Instance of Territory</param>
			set
			{
				 reportingTo=value;

				 keyModified["reporting_to"] = 1;

			}
		}

		public string PermissionType
		{
			/// <summary>The method to get the permissionType</summary>
			/// <returns>string representing the permissionType</returns>
			get
			{
				return  permissionType;

			}
			/// <summary>The method to set the value to permissionType</summary>
			/// <param name="permissionType">string</param>
			set
			{
				 permissionType=value;

				 keyModified["permission_type"] = 1;

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