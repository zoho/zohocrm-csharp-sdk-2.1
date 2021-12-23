using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Wizards
{

	public class Wizard : Model
	{
		DateTimeOffset? createdTime;
		DateTimeOffset? modifiedTime;
		Module module;
		string name;
		User modifiedBy;
		List<Profile> profiles;
		bool? active;
		List<Container> containers;
		User createdBy;
		Wizard parentWizard;
		bool? draft;
		long? id;
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

		public Module Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">Instance of Module</param>
			set
			{
				 module=value;

				 keyModified["module"] = 1;

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

		public List<Profile> Profiles
		{
			/// <summary>The method to get the profiles</summary>
			/// <returns>Instance of List<Profile></returns>
			get
			{
				return  profiles;

			}
			/// <summary>The method to set the value to profiles</summary>
			/// <param name="profiles">Instance of List<Profile></param>
			set
			{
				 profiles=value;

				 keyModified["profiles"] = 1;

			}
		}

		public bool? Active
		{
			/// <summary>The method to get the active</summary>
			/// <returns>bool? representing the active</returns>
			get
			{
				return  active;

			}
			/// <summary>The method to set the value to active</summary>
			/// <param name="active">bool?</param>
			set
			{
				 active=value;

				 keyModified["active"] = 1;

			}
		}

		public List<Container> Containers
		{
			/// <summary>The method to get the containers</summary>
			/// <returns>Instance of List<Container></returns>
			get
			{
				return  containers;

			}
			/// <summary>The method to set the value to containers</summary>
			/// <param name="containers">Instance of List<Container></param>
			set
			{
				 containers=value;

				 keyModified["containers"] = 1;

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

		public Wizard ParentWizard
		{
			/// <summary>The method to get the parentWizard</summary>
			/// <returns>Instance of Wizard</returns>
			get
			{
				return  parentWizard;

			}
			/// <summary>The method to set the value to parentWizard</summary>
			/// <param name="parentWizard">Instance of Wizard</param>
			set
			{
				 parentWizard=value;

				 keyModified["parent_wizard"] = 1;

			}
		}

		public bool? Draft
		{
			/// <summary>The method to get the draft</summary>
			/// <returns>bool? representing the draft</returns>
			get
			{
				return  draft;

			}
			/// <summary>The method to set the value to draft</summary>
			/// <param name="draft">bool?</param>
			set
			{
				 draft=value;

				 keyModified["draft"] = 1;

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