using Com.Zoho.Crm.API.Attachments;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Notes
{

	public class Note : Model
	{
		User owner;
		DateTimeOffset? modifiedTime;
		List<Attachment> attachments;
		DateTimeOffset? createdTime;
		Record.Record parentId;
		bool? editable;
		string sharingPermission;
		string seModule;
		bool? isSharedToClient;
		User modifiedBy;
		string size;
		string state;
		bool? voiceNote;
		long? id;
		User createdBy;
		string noteTitle;
		string noteContent;
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

		public List<Attachment> Attachments
		{
			/// <summary>The method to get the attachments</summary>
			/// <returns>Instance of List<Attachment></returns>
			get
			{
				return  attachments;

			}
			/// <summary>The method to set the value to attachments</summary>
			/// <param name="attachments">Instance of List<Attachment></param>
			set
			{
				 attachments=value;

				 keyModified["$attachments"] = 1;

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

		public bool? IsSharedToClient
		{
			/// <summary>The method to get the isSharedToClient</summary>
			/// <returns>bool? representing the isSharedToClient</returns>
			get
			{
				return  isSharedToClient;

			}
			/// <summary>The method to set the value to isSharedToClient</summary>
			/// <param name="isSharedToClient">bool?</param>
			set
			{
				 isSharedToClient=value;

				 keyModified["$is_shared_to_client"] = 1;

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

		public string Size
		{
			/// <summary>The method to get the size</summary>
			/// <returns>string representing the size</returns>
			get
			{
				return  size;

			}
			/// <summary>The method to set the value to size</summary>
			/// <param name="size">string</param>
			set
			{
				 size=value;

				 keyModified["$size"] = 1;

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

		public bool? VoiceNote
		{
			/// <summary>The method to get the voiceNote</summary>
			/// <returns>bool? representing the voiceNote</returns>
			get
			{
				return  voiceNote;

			}
			/// <summary>The method to set the value to voiceNote</summary>
			/// <param name="voiceNote">bool?</param>
			set
			{
				 voiceNote=value;

				 keyModified["$voice_note"] = 1;

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

				 keyModified["Created_By"] = 1;

			}
		}

		public string NoteTitle
		{
			/// <summary>The method to get the noteTitle</summary>
			/// <returns>string representing the noteTitle</returns>
			get
			{
				return  noteTitle;

			}
			/// <summary>The method to set the value to noteTitle</summary>
			/// <param name="noteTitle">string</param>
			set
			{
				 noteTitle=value;

				 keyModified["Note_Title"] = 1;

			}
		}

		public string NoteContent
		{
			/// <summary>The method to get the noteContent</summary>
			/// <returns>string representing the noteContent</returns>
			get
			{
				return  noteContent;

			}
			/// <summary>The method to set the value to noteContent</summary>
			/// <param name="noteContent">string</param>
			set
			{
				 noteContent=value;

				 keyModified["Note_Content"] = 1;

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