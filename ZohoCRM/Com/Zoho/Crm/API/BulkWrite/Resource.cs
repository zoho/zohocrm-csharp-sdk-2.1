using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkWrite
{

	public class Resource : Model
	{
		Choice<string> status;
		Choice<string> type;
		Module module;
		string code;
		string fileId;
		bool? ignoreEmpty;
		string findBy;
		List<FieldMapping> fieldMappings;
		File file;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Choice<string> Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">Instance of Choice<string></param>
			set
			{
				 status=value;

				 keyModified["status"] = 1;

			}
		}

		public Choice<string> Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">Instance of Choice<string></param>
			set
			{
				 type=value;

				 keyModified["type"] = 1;

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

		public string Code
		{
			/// <summary>The method to get the code</summary>
			/// <returns>string representing the code</returns>
			get
			{
				return  code;

			}
			/// <summary>The method to set the value to code</summary>
			/// <param name="code">string</param>
			set
			{
				 code=value;

				 keyModified["code"] = 1;

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

				 keyModified["file_id"] = 1;

			}
		}

		public bool? IgnoreEmpty
		{
			/// <summary>The method to get the ignoreEmpty</summary>
			/// <returns>bool? representing the ignoreEmpty</returns>
			get
			{
				return  ignoreEmpty;

			}
			/// <summary>The method to set the value to ignoreEmpty</summary>
			/// <param name="ignoreEmpty">bool?</param>
			set
			{
				 ignoreEmpty=value;

				 keyModified["ignore_empty"] = 1;

			}
		}

		public string FindBy
		{
			/// <summary>The method to get the findBy</summary>
			/// <returns>string representing the findBy</returns>
			get
			{
				return  findBy;

			}
			/// <summary>The method to set the value to findBy</summary>
			/// <param name="findBy">string</param>
			set
			{
				 findBy=value;

				 keyModified["find_by"] = 1;

			}
		}

		public List<FieldMapping> FieldMappings
		{
			/// <summary>The method to get the fieldMappings</summary>
			/// <returns>Instance of List<FieldMapping></returns>
			get
			{
				return  fieldMappings;

			}
			/// <summary>The method to set the value to fieldMappings</summary>
			/// <param name="fieldMappings">Instance of List<FieldMapping></param>
			set
			{
				 fieldMappings=value;

				 keyModified["field_mappings"] = 1;

			}
		}

		public File File
		{
			/// <summary>The method to get the file</summary>
			/// <returns>FileInfo representing the file</returns>
			get
			{
				return  file;

			}
			/// <summary>The method to set the value to file</summary>
			/// <param name="file">FileInfo</param>
			set
			{
				 file=value;

				 keyModified["file"] = 1;

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