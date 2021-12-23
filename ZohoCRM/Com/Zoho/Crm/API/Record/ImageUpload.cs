using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class ImageUpload : Model
	{
		string description;
		string previewId;
		string encryptedId;
		string fileName;
		string state;
		string fileId;
		long? size;
		int? sequenceNumber;
		long? id;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

				 keyModified["Description"] = 1;

			}
		}

		public string PreviewId
		{
			/// <summary>The method to get the previewId</summary>
			/// <returns>string representing the previewId</returns>
			get
			{
				return  previewId;

			}
			/// <summary>The method to set the value to previewId</summary>
			/// <param name="previewId">string</param>
			set
			{
				 previewId=value;

				 keyModified["Preview_Id"] = 1;

			}
		}

		public string EncryptedId
		{
			/// <summary>The method to get the encryptedId</summary>
			/// <returns>string representing the encryptedId</returns>
			get
			{
				return  encryptedId;

			}
			/// <summary>The method to set the value to encryptedId</summary>
			/// <param name="encryptedId">string</param>
			set
			{
				 encryptedId=value;

				 keyModified["Encrypted_Id"] = 1;

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

				 keyModified["State"] = 1;

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

				 keyModified["File_Id"] = 1;

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

		public int? SequenceNumber
		{
			/// <summary>The method to get the sequenceNumber</summary>
			/// <returns>int? representing the sequenceNumber</returns>
			get
			{
				return  sequenceNumber;

			}
			/// <summary>The method to set the value to sequenceNumber</summary>
			/// <param name="sequenceNumber">int?</param>
			set
			{
				 sequenceNumber=value;

				 keyModified["Sequence_Number"] = 1;

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