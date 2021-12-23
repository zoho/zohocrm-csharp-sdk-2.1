using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class FileDetails : Model
	{
		string extn;
		bool? isPreviewAvailable;
		string downloadUrl;
		string deleteUrl;
		string entityId;
		string mode;
		string originalSizeByte;
		string previewUrl;
		string fileName;
		string fileId;
		string attachmentId;
		string fileSize;
		string creatorId;
		int? linkDocs;
		string delete;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Extn
		{
			/// <summary>The method to get the extn</summary>
			/// <returns>string representing the extn</returns>
			get
			{
				return  extn;

			}
			/// <summary>The method to set the value to extn</summary>
			/// <param name="extn">string</param>
			set
			{
				 extn=value;

				 keyModified["extn"] = 1;

			}
		}

		public bool? IsPreviewAvailable
		{
			/// <summary>The method to get the isPreviewAvailable</summary>
			/// <returns>bool? representing the isPreviewAvailable</returns>
			get
			{
				return  isPreviewAvailable;

			}
			/// <summary>The method to set the value to isPreviewAvailable</summary>
			/// <param name="isPreviewAvailable">bool?</param>
			set
			{
				 isPreviewAvailable=value;

				 keyModified["is_Preview_Available"] = 1;

			}
		}

		public string DownloadUrl
		{
			/// <summary>The method to get the downloadUrl</summary>
			/// <returns>string representing the downloadUrl</returns>
			get
			{
				return  downloadUrl;

			}
			/// <summary>The method to set the value to downloadUrl</summary>
			/// <param name="downloadUrl">string</param>
			set
			{
				 downloadUrl=value;

				 keyModified["download_Url"] = 1;

			}
		}

		public string DeleteUrl
		{
			/// <summary>The method to get the deleteUrl</summary>
			/// <returns>string representing the deleteUrl</returns>
			get
			{
				return  deleteUrl;

			}
			/// <summary>The method to set the value to deleteUrl</summary>
			/// <param name="deleteUrl">string</param>
			set
			{
				 deleteUrl=value;

				 keyModified["delete_Url"] = 1;

			}
		}

		public string EntityId
		{
			/// <summary>The method to get the entityId</summary>
			/// <returns>string representing the entityId</returns>
			get
			{
				return  entityId;

			}
			/// <summary>The method to set the value to entityId</summary>
			/// <param name="entityId">string</param>
			set
			{
				 entityId=value;

				 keyModified["entity_Id"] = 1;

			}
		}

		public string Mode
		{
			/// <summary>The method to get the mode</summary>
			/// <returns>string representing the mode</returns>
			get
			{
				return  mode;

			}
			/// <summary>The method to set the value to mode</summary>
			/// <param name="mode">string</param>
			set
			{
				 mode=value;

				 keyModified["mode"] = 1;

			}
		}

		public string OriginalSizeByte
		{
			/// <summary>The method to get the originalSizeByte</summary>
			/// <returns>string representing the originalSizeByte</returns>
			get
			{
				return  originalSizeByte;

			}
			/// <summary>The method to set the value to originalSizeByte</summary>
			/// <param name="originalSizeByte">string</param>
			set
			{
				 originalSizeByte=value;

				 keyModified["original_Size_Byte"] = 1;

			}
		}

		public string PreviewUrl
		{
			/// <summary>The method to get the previewUrl</summary>
			/// <returns>string representing the previewUrl</returns>
			get
			{
				return  previewUrl;

			}
			/// <summary>The method to set the value to previewUrl</summary>
			/// <param name="previewUrl">string</param>
			set
			{
				 previewUrl=value;

				 keyModified["preview_Url"] = 1;

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

				 keyModified["file_Name"] = 1;

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

				 keyModified["file_Id"] = 1;

			}
		}

		public string AttachmentId
		{
			/// <summary>The method to get the attachmentId</summary>
			/// <returns>string representing the attachmentId</returns>
			get
			{
				return  attachmentId;

			}
			/// <summary>The method to set the value to attachmentId</summary>
			/// <param name="attachmentId">string</param>
			set
			{
				 attachmentId=value;

				 keyModified["attachment_Id"] = 1;

			}
		}

		public string FileSize
		{
			/// <summary>The method to get the fileSize</summary>
			/// <returns>string representing the fileSize</returns>
			get
			{
				return  fileSize;

			}
			/// <summary>The method to set the value to fileSize</summary>
			/// <param name="fileSize">string</param>
			set
			{
				 fileSize=value;

				 keyModified["file_Size"] = 1;

			}
		}

		public string CreatorId
		{
			/// <summary>The method to get the creatorId</summary>
			/// <returns>string representing the creatorId</returns>
			get
			{
				return  creatorId;

			}
			/// <summary>The method to set the value to creatorId</summary>
			/// <param name="creatorId">string</param>
			set
			{
				 creatorId=value;

				 keyModified["creator_Id"] = 1;

			}
		}

		public int? LinkDocs
		{
			/// <summary>The method to get the linkDocs</summary>
			/// <returns>int? representing the linkDocs</returns>
			get
			{
				return  linkDocs;

			}
			/// <summary>The method to set the value to linkDocs</summary>
			/// <param name="linkDocs">int?</param>
			set
			{
				 linkDocs=value;

				 keyModified["link_Docs"] = 1;

			}
		}

		public string Delete
		{
			/// <summary>The method to get the delete</summary>
			/// <returns>string representing the delete</returns>
			get
			{
				return  delete;

			}
			/// <summary>The method to set the value to delete</summary>
			/// <param name="delete">string</param>
			set
			{
				 delete=value;

				 keyModified["_delete"] = 1;

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