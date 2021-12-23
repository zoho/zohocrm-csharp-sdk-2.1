using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Layouts
{

	public class Layout : Model
	{
		DateTimeOffset? createdTime;
		Dictionary<string, object> convertMapping;
		DateTimeOffset? modifiedTime;
		bool? visible;
		User createdFor;
		string name;
		User modifiedBy;
		List<Profile> profiles;
		long? id;
		User createdBy;
		List<Section> sections;
		int? status;
		int? displayType;
		bool? showBusinessCard;
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

		public Dictionary<string, object> ConvertMapping
		{
			/// <summary>The method to get the convertMapping</summary>
			/// <returns>Dictionary representing the convertMapping<String,Object></returns>
			get
			{
				return  convertMapping;

			}
			/// <summary>The method to set the value to convertMapping</summary>
			/// <param name="convertMapping">Dictionary<string,object></param>
			set
			{
				 convertMapping=value;

				 keyModified["convert_mapping"] = 1;

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

		public bool? Visible
		{
			/// <summary>The method to get the visible</summary>
			/// <returns>bool? representing the visible</returns>
			get
			{
				return  visible;

			}
			/// <summary>The method to set the value to visible</summary>
			/// <param name="visible">bool?</param>
			set
			{
				 visible=value;

				 keyModified["visible"] = 1;

			}
		}

		public User CreatedFor
		{
			/// <summary>The method to get the createdFor</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  createdFor;

			}
			/// <summary>The method to set the value to createdFor</summary>
			/// <param name="createdFor">Instance of User</param>
			set
			{
				 createdFor=value;

				 keyModified["created_for"] = 1;

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

		public int? Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>int? representing the status</returns>
			get
			{
				return  status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">int?</param>
			set
			{
				 status=value;

				 keyModified["status"] = 1;

			}
		}

		public int? DisplayType
		{
			/// <summary>The method to get the displayType</summary>
			/// <returns>int? representing the displayType</returns>
			get
			{
				return  displayType;

			}
			/// <summary>The method to set the value to displayType</summary>
			/// <param name="displayType">int?</param>
			set
			{
				 displayType=value;

				 keyModified["display_type"] = 1;

			}
		}

		public bool? ShowBusinessCard
		{
			/// <summary>The method to get the showBusinessCard</summary>
			/// <returns>bool? representing the showBusinessCard</returns>
			get
			{
				return  showBusinessCard;

			}
			/// <summary>The method to set the value to showBusinessCard</summary>
			/// <param name="showBusinessCard">bool?</param>
			set
			{
				 showBusinessCard=value;

				 keyModified["show_business_card"] = 1;

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