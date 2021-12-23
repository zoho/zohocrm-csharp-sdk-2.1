using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Attachments
{

	public class Attachment : Model
	{
		User owner;
		DateTimeOffset? modifiedTime;
		string fileName;
		DateTimeOffset? createdTime;
		long? size;
		Record.Record parentId;
		bool? editable;
		string sharingPermission;
		string fileId;
		string type;
		string seModule;
		User modifiedBy;
		int? attachmentType;
		string state;
		string id;
		User createdBy;
		string linkUrl;
		string description;
		string category;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public User Owner
		{
			/// <summary>The method to get the owner</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  owner;

			}
			/// <summary>The method to set the value to owner</summary>
			/// <param name="owner">Instance of User</param>
			set
			{
				 owner=value;

				 keyModified["Owner"] = 1;

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

				 keyModified["Modified_Time"] = 1;

			}
		}

		public string FileName
		{
			/// <summary>The method to get the fileName</summary>
			/// <returns>string representing the fileName</returns>
			get
			{
				return  fileName;

			}
			/// <summary>The method to set the value to fileName</summary>
			/// <param name="fileName">string</param>
			set
			{
				 fileName=value;

				 keyModified["File_Name"] = 1;

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

				 keyModified["Created_Time"] = 1;

			}
		}

		public long? Size
		{
			/// <summary>The method to get the size</summary>
			/// <returns>long? representing the size</returns>
			get
			{
				return  size;

			}
			/// <summary>The method to set the value to size</summary>
			/// <param name="size">long?</param>
			set
			{
				 size=value;

				 keyModified["Size"] = 1;

			}
		}

		public Record.Record ParentId
		{
			/// <summary>The method to get the parentId</summary>
			/// <returns>Instance of Record</returns>
			get
			{
				return  parentId;

			}
			/// <summary>The method to set the value to parentId</summary>
			/// <param name="parentId">Instance of Record</param>
			set
			{
				 parentId=value;

				 keyModified["Parent_Id"] = 1;

			}
		}

		public bool? Editable
		{
			/// <summary>The method to get the editable</summary>
			/// <returns>bool? representing the editable</returns>
			get
			{
				return  editable;

			}
			/// <summary>The method to set the value to editable</summary>
			/// <param name="editable">bool?</param>
			set
			{
				 editable=value;

				 keyModified["$editable"] = 1;

			}
		}

		public string SharingPermission
		{
			/// <summary>The method to get the sharingPermission</summary>
			/// <returns>string representing the sharingPermission</returns>
			get
			{
				return  sharingPermission;

			}
			/// <summary>The method to set the value to sharingPermission</summary>
			/// <param name="sharingPermission">string</param>
			set
			{
				 sharingPermission=value;

				 keyModified["$sharing_permission"] = 1;

			}
		}

		public string FileId
		{
			/// <summary>The method to get the fileId</summary>
			/// <returns>string representing the fileId</returns>
			get
			{
				return  fileId;

			}
			/// <summary>The method to set the value to fileId</summary>
			/// <param name="fileId">string</param>
			set
			{
				 fileId=value;

				 keyModified["$file_id"] = 1;

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

				 keyModified["$type"] = 1;

			}
		}

		public string SeModule
		{
			/// <summary>The method to get the seModule</summary>
			/// <returns>string representing the seModule</returns>
			get
			{
				return  seModule;

			}
			/// <summary>The method to set the value to seModule</summary>
			/// <param name="seModule">string</param>
			set
			{
				 seModule=value;

				 keyModified["$se_module"] = 1;

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

				 keyModified["Modified_By"] = 1;

			}
		}

		public int? AttachmentType
		{
			/// <summary>The method to get the attachmentType</summary>
			/// <returns>int? representing the attachmentType</returns>
			get
			{
				return  attachmentType;

			}
			/// <summary>The method to set the value to attachmentType</summary>
			/// <param name="attachmentType">int?</param>
			set
			{
				 attachmentType=value;

				 keyModified["$attachment_type"] = 1;

			}
		}

		public string State
		{
			/// <summary>The method to get the state</summary>
			/// <returns>string representing the state</returns>
			get
			{
				return  state;

			}
			/// <summary>The method to set the value to state</summary>
			/// <param name="state">string</param>
			set
			{
				 state=value;

				 keyModified["$state"] = 1;

			}
		}

		public string Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>string representing the id</returns>
			get
			{
				return  id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">string</param>
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

				 keyModified["Created_By"] = 1;

			}
		}

		public string LinkUrl
		{
			/// <summary>The method to get the linkUrl</summary>
			/// <returns>string representing the linkUrl</returns>
			get
			{
				return  linkUrl;

			}
			/// <summary>The method to set the value to linkUrl</summary>
			/// <param name="linkUrl">string</param>
			set
			{
				 linkUrl=value;

				 keyModified["$link_url"] = 1;

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