using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Profiles
{

	public class Profile : Model
	{
		string displayLabel;
		DateTimeOffset? createdTime;
		DateTimeOffset? modifiedTime;
		List<PermissionDetail> permissionsDetails;
		string name;
		User modifiedBy;
		DefaultView defaultview;
		bool? default1;
		string description;
		long? id;
		bool? custom;
		User createdBy;
		List<Section> sections;
		bool? delete;
		string permissionType;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string DisplayLabel
		{
			/// <summary>The method to get the displayLabel</summary>
			/// <returns>string representing the displayLabel</returns>
			get
			{
				return  displayLabel;

			}
			/// <summary>The method to set the value to displayLabel</summary>
			/// <param name="displayLabel">string</param>
			set
			{
				 displayLabel=value;

				 keyModified["display_label"] = 1;

			}
		}

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

		public List<PermissionDetail> PermissionsDetails
		{
			/// <summary>The method to get the permissionsDetails</summary>
			/// <returns>Instance of List<PermissionDetail></returns>
			get
			{
				return  permissionsDetails;

			}
			/// <summary>The method to set the value to permissionsDetails</summary>
			/// <param name="permissionsDetails">Instance of List<PermissionDetail></param>
			set
			{
				 permissionsDetails=value;

				 keyModified["permissions_details"] = 1;

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

		public DefaultView Defaultview
		{
			/// <summary>The method to get the defaultview</summary>
			/// <returns>Instance of DefaultView</returns>
			get
			{
				return  defaultview;

			}
			/// <summary>The method to set the value to defaultview</summary>
			/// <param name="defaultview">Instance of DefaultView</param>
			set
			{
				 defaultview=value;

				 keyModified["_default_view"] = 1;

			}
		}

		public bool? Default
		{
			/// <summary>The method to get the default</summary>
			/// <returns>bool? representing the default1</returns>
			get
			{
				return  default1;

			}
			/// <summary>The method to set the value to default</summary>
			/// <param name="default1">bool?</param>
			set
			{
				 default1=value;

				 keyModified["default"] = 1;

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

		public bool? Custom
		{
			/// <summary>The method to get the custom</summary>
			/// <returns>bool? representing the custom</returns>
			get
			{
				return  custom;

			}
			/// <summary>The method to set the value to custom</summary>
			/// <param name="custom">bool?</param>
			set
			{
				 custom=value;

				 keyModified["custom"] = 1;

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

		public List<Section> Sections
		{
			/// <summary>The method to get the sections</summary>
			/// <returns>Instance of List<Section></returns>
			get
			{
				return  sections;

			}
			/// <summary>The method to set the value to sections</summary>
			/// <param name="sections">Instance of List<Section></param>
			set
			{
				 sections=value;

				 keyModified["sections"] = 1;

			}
		}

		public bool? Delete
		{
			/// <summary>The method to get the delete</summary>
			/// <returns>bool? representing the delete</returns>
			get
			{
				return  delete;

			}
			/// <summary>The method to set the value to delete</summary>
			/// <param name="delete">bool?</param>
			set
			{
				 delete=value;

				 keyModified["_delete"] = 1;

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