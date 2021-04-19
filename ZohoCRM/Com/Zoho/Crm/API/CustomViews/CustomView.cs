using Com.Zoho.Crm.API.Fields;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.CustomViews
{

	public class CustomView : Model
	{
		private long? id;
		private string name;
		private string systemName;
		private string displayValue;
		private string accessType;
		private string category;
		private string sortBy;
		private string sortOrder;
		private int? favorite;
		private bool? default1;
		private bool? systemDefined;
		private Criteria criteria;
		private List<SharedTo> sharedTo;
		private List<Field> fields;
		private DateTimeOffset? modifiedTime;
		private User modifiedBy;
		private DateTimeOffset? lastAccessedTime;
		private User createdBy;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				return  this.name;

			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 this.name=value;

				 this.keyModified["name"] = 1;

			}
		}

		public string SystemName
		{
			/// <summary>The method to get the systemName</summary>
			/// <returns>string representing the systemName</returns>
			get
			{
				return  this.systemName;

			}
			/// <summary>The method to set the value to systemName</summary>
			/// <param name="systemName">string</param>
			set
			{
				 this.systemName=value;

				 this.keyModified["system_name"] = 1;

			}
		}

		public string DisplayValue
		{
			/// <summary>The method to get the displayValue</summary>
			/// <returns>string representing the displayValue</returns>
			get
			{
				return  this.displayValue;

			}
			/// <summary>The method to set the value to displayValue</summary>
			/// <param name="displayValue">string</param>
			set
			{
				 this.displayValue=value;

				 this.keyModified["display_value"] = 1;

			}
		}

		public string AccessType
		{
			/// <summary>The method to get the accessType</summary>
			/// <returns>string representing the accessType</returns>
			get
			{
				return  this.accessType;

			}
			/// <summary>The method to set the value to accessType</summary>
			/// <param name="accessType">string</param>
			set
			{
				 this.accessType=value;

				 this.keyModified["access_type"] = 1;

			}
		}

		public string Category
		{
			/// <summary>The method to get the category</summary>
			/// <returns>string representing the category</returns>
			get
			{
				return  this.category;

			}
			/// <summary>The method to set the value to category</summary>
			/// <param name="category">string</param>
			set
			{
				 this.category=value;

				 this.keyModified["category"] = 1;

			}
		}

		public string SortBy
		{
			/// <summary>The method to get the sortBy</summary>
			/// <returns>string representing the sortBy</returns>
			get
			{
				return  this.sortBy;

			}
			/// <summary>The method to set the value to sortBy</summary>
			/// <param name="sortBy">string</param>
			set
			{
				 this.sortBy=value;

				 this.keyModified["sort_by"] = 1;

			}
		}

		public string SortOrder
		{
			/// <summary>The method to get the sortOrder</summary>
			/// <returns>string representing the sortOrder</returns>
			get
			{
				return  this.sortOrder;

			}
			/// <summary>The method to set the value to sortOrder</summary>
			/// <param name="sortOrder">string</param>
			set
			{
				 this.sortOrder=value;

				 this.keyModified["sort_order"] = 1;

			}
		}

		public int? Favorite
		{
			/// <summary>The method to get the favorite</summary>
			/// <returns>int? representing the favorite</returns>
			get
			{
				return  this.favorite;

			}
			/// <summary>The method to set the value to favorite</summary>
			/// <param name="favorite">int?</param>
			set
			{
				 this.favorite=value;

				 this.keyModified["favorite"] = 1;

			}
		}

		public bool? Default
		{
			/// <summary>The method to get the default</summary>
			/// <returns>bool? representing the default1</returns>
			get
			{
				return  this.default1;

			}
			/// <summary>The method to set the value to default</summary>
			/// <param name="default1">bool?</param>
			set
			{
				 this.default1=value;

				 this.keyModified["default"] = 1;

			}
		}

		public bool? SystemDefined
		{
			/// <summary>The method to get the systemDefined</summary>
			/// <returns>bool? representing the systemDefined</returns>
			get
			{
				return  this.systemDefined;

			}
			/// <summary>The method to set the value to systemDefined</summary>
			/// <param name="systemDefined">bool?</param>
			set
			{
				 this.systemDefined=value;

				 this.keyModified["system_defined"] = 1;

			}
		}

		public Criteria Criteria
		{
			/// <summary>The method to get the criteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  this.criteria;

			}
			/// <summary>The method to set the value to criteria</summary>
			/// <param name="criteria">Instance of Criteria</param>
			set
			{
				 this.criteria=value;

				 this.keyModified["criteria"] = 1;

			}
		}

		public List<SharedTo> SharedTo
		{
			/// <summary>The method to get the sharedTo</summary>
			/// <returns>Instance of List<SharedTo></returns>
			get
			{
				return  this.sharedTo;

			}
			/// <summary>The method to set the value to sharedTo</summary>
			/// <param name="sharedTo">Instance of List<SharedTo></param>
			set
			{
				 this.sharedTo=value;

				 this.keyModified["shared_to"] = 1;

			}
		}

		public List<Field> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Instance of List<Field></returns>
			get
			{
				return  this.fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Instance of List<Field></param>
			set
			{
				 this.fields=value;

				 this.keyModified["fields"] = 1;

			}
		}

		public DateTimeOffset? ModifiedTime
		{
			/// <summary>The method to get the modifiedTime</summary>
			/// <returns>DateTimeOffset? representing the modifiedTime</returns>
			get
			{
				return  this.modifiedTime;

			}
			/// <summary>The method to set the value to modifiedTime</summary>
			/// <param name="modifiedTime">DateTimeOffset?</param>
			set
			{
				 this.modifiedTime=value;

				 this.keyModified["modified_time"] = 1;

			}
		}

		public User ModifiedBy
		{
			/// <summary>The method to get the modifiedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  this.modifiedBy;

			}
			/// <summary>The method to set the value to modifiedBy</summary>
			/// <param name="modifiedBy">Instance of User</param>
			set
			{
				 this.modifiedBy=value;

				 this.keyModified["modified_by"] = 1;

			}
		}

		public DateTimeOffset? LastAccessedTime
		{
			/// <summary>The method to get the lastAccessedTime</summary>
			/// <returns>DateTimeOffset? representing the lastAccessedTime</returns>
			get
			{
				return  this.lastAccessedTime;

			}
			/// <summary>The method to set the value to lastAccessedTime</summary>
			/// <param name="lastAccessedTime">DateTimeOffset?</param>
			set
			{
				 this.lastAccessedTime=value;

				 this.keyModified["last_accessed_time"] = 1;

			}
		}

		public User CreatedBy
		{
			/// <summary>The method to get the createdBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  this.createdBy;

			}
			/// <summary>The method to set the value to createdBy</summary>
			/// <param name="createdBy">Instance of User</param>
			set
			{
				 this.createdBy=value;

				 this.keyModified["created_by"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if((( this.keyModified.ContainsKey(key))))
			{
				return  this.keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			 this.keyModified[key] = modification;


		}


	}
}