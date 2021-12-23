using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.SendMail;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.InventoryTemplates
{

	public class InventoryTemplate : Template, Model
	{
		string content;
		DateTimeOffset? createdTime;
		string subject;
		Module module;
		string type;
		User createdBy;
		DateTimeOffset? modifiedTime;
		DateTimeOffset? lastUsageTime;
		bool? associated;
		string name;
		User modifiedBy;
		string description;
		long? id;
		string editorMode;
		bool? favorite;
		InventoryTemplate folder;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Content
		{
			/// <summary>The method to get the content</summary>
			/// <returns>string representing the content</returns>
			get
			{
				return  content;

			}
			/// <summary>The method to set the value to content</summary>
			/// <param name="content">string</param>
			set
			{
				 content=value;

				 keyModified["content"] = 1;

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

		public string Subject
		{
			/// <summary>The method to get the subject</summary>
			/// <returns>string representing the subject</returns>
			get
			{
				return  subject;

			}
			/// <summary>The method to set the value to subject</summary>
			/// <param name="subject">string</param>
			set
			{
				 subject=value;

				 keyModified["subject"] = 1;

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

		public DateTimeOffset? LastUsageTime
		{
			/// <summary>The method to get the lastUsageTime</summary>
			/// <returns>DateTimeOffset? representing the lastUsageTime</returns>
			get
			{
				return  lastUsageTime;

			}
			/// <summary>The method to set the value to lastUsageTime</summary>
			/// <param name="lastUsageTime">DateTimeOffset?</param>
			set
			{
				 lastUsageTime=value;

				 keyModified["last_usage_time"] = 1;

			}
		}

		public bool? Associated
		{
			/// <summary>The method to get the associated</summary>
			/// <returns>bool? representing the associated</returns>
			get
			{
				return  associated;

			}
			/// <summary>The method to set the value to associated</summary>
			/// <param name="associated">bool?</param>
			set
			{
				 associated=value;

				 keyModified["associated"] = 1;

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

		public string EditorMode
		{
			/// <summary>The method to get the editorMode</summary>
			/// <returns>string representing the editorMode</returns>
			get
			{
				return  editorMode;

			}
			/// <summary>The method to set the value to editorMode</summary>
			/// <param name="editorMode">string</param>
			set
			{
				 editorMode=value;

				 keyModified["editor_mode"] = 1;

			}
		}

		public bool? Favorite
		{
			/// <summary>The method to get the favorite</summary>
			/// <returns>bool? representing the favorite</returns>
			get
			{
				return  favorite;

			}
			/// <summary>The method to set the value to favorite</summary>
			/// <param name="favorite">bool?</param>
			set
			{
				 favorite=value;

				 keyModified["favorite"] = 1;

			}
		}

		public InventoryTemplate Folder
		{
			/// <summary>The method to get the folder</summary>
			/// <returns>Instance of InventoryTemplate</returns>
			get
			{
				return  folder;

			}
			/// <summary>The method to set the value to folder</summary>
			/// <param name="folder">Instance of InventoryTemplate</param>
			set
			{
				 folder=value;

				 keyModified["folder"] = 1;

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