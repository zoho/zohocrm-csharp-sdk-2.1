using Com.Zoho.Crm.API.Fields;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.CustomViews
{

	public class CustomView : Model
	{
		long? id;
		string name;
		string systemName;
		string displayValue;
		DateTimeOffset? createdTime;
		string accessType;
		string category;
		string sortBy;
		string sortOrder;
		int? favorite;
		bool? default1;
		bool? systemDefined;
		Criteria criteria;
		List<SharedTo> sharedTo;
		List<Field> fields;
		DateTimeOffset? modifiedTime;
		User modifiedBy;
		DateTimeOffset? lastAccessedTime;
		User createdBy;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public string SystemName
		{
			/// <summary>The method to get the systemName</summary>
			/// <returns>string representing the systemName</returns>
			get
			{
				return  systemName;

			}
			/// <summary>The method to set the value to systemName</summary>
			/// <param name="systemName">string</param>
			set
			{
				 systemName=value;

				 keyModified["system_name"] = 1;

			}
		}

		public string DisplayValue
		{
			/// <summary>The method to get the displayValue</summary>
			/// <returns>string representing the displayValue</returns>
			get
			{
				return  displayValue;

			}
			/// <summary>The method to set the value to displayValue</summary>
			/// <param name="displayValue">string</param>
			set
			{
				 displayValue=value;

				 keyModified["display_value"] = 1;

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

		public string AccessType
		{
			/// <summary>The method to get the accessType</summary>
			/// <returns>string representing the accessType</returns>
			get
			{
				return  accessType;

			}
			/// <summary>The method to set the value to accessType</summary>
			/// <param name="accessType">string</param>
			set
			{
				 accessType=value;

				 keyModified["access_type"] = 1;

			}
		}

		public string Category
		{
			/// <summary>The method to get the category</summary>
			/// <returns>string representing the category</returns>
			get
			{
				return  category;

			}
			/// <summary>The method to set the value to category</summary>
			/// <param name="category">string</param>
			set
			{
				 category=value;

				 keyModified["category"] = 1;

			}
		}

		public string SortBy
		{
			/// <summary>The method to get the sortBy</summary>
			/// <returns>string representing the sortBy</returns>
			get
			{
				return  sortBy;

			}
			/// <summary>The method to set the value to sortBy</summary>
			/// <param name="sortBy">string</param>
			set
			{
				 sortBy=value;

				 keyModified["sort_by"] = 1;

			}
		}

		public string SortOrder
		{
			/// <summary>The method to get the sortOrder</summary>
			/// <returns>string representing the sortOrder</returns>
			get
			{
				return  sortOrder;

			}
			/// <summary>The method to set the value to sortOrder</summary>
			/// <param name="sortOrder">string</param>
			set
			{
				 sortOrder=value;

				 keyModified["sort_order"] = 1;

			}
		}

		public int? Favorite
		{
			/// <summary>The method to get the favorite</summary>
			/// <returns>int? representing the favorite</returns>
			get
			{
				return  favorite;

			}
			/// <summary>The method to set the value to favorite</summary>
			/// <param name="favorite">int?</param>
			set
			{
				 favorite=value;

				 keyModified["favorite"] = 1;

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

		public bool? SystemDefined
		{
			/// <summary>The method to get the systemDefined</summary>
			/// <returns>bool? representing the systemDefined</returns>
			get
			{
				return  systemDefined;

			}
			/// <summary>The method to set the value to systemDefined</summary>
			/// <param name="systemDefined">bool?</param>
			set
			{
				 systemDefined=value;

				 keyModified["system_defined"] = 1;

			}
		}

		public Criteria Criteria
		{
			/// <summary>The method to get the criteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  criteria;

			}
			/// <summary>The method to set the value to criteria</summary>
			/// <param name="criteria">Instance of Criteria</param>
			set
			{
				 criteria=value;

				 keyModified["criteria"] = 1;

			}
		}

		public List<SharedTo> SharedTo
		{
			/// <summary>The method to get the sharedTo</summary>
			/// <returns>Instance of List<SharedTo></returns>
			get
			{
				return  sharedTo;

			}
			/// <summary>The method to set the value to sharedTo</summary>
			/// <param name="sharedTo">Instance of List<SharedTo></param>
			set
			{
				 sharedTo=value;

				 keyModified["shared_to"] = 1;

			}
		}

		public List<Field> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Instance of List<Field></returns>
			get
			{
				return  fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Instance of List<Field></param>
			set
			{
				 fields=value;

				 keyModified["fields"] = 1;

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

		public DateTimeOffset? LastAccessedTime
		{
			/// <summary>The method to get the lastAccessedTime</summary>
			/// <returns>DateTimeOffset? representing the lastAccessedTime</returns>
			get
			{
				return  lastAccessedTime;

			}
			/// <summary>The method to set the value to lastAccessedTime</summary>
			/// <param name="lastAccessedTime">DateTimeOffset?</param>
			set
			{
				 lastAccessedTime=value;

				 keyModified["last_accessed_time"] = 1;

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