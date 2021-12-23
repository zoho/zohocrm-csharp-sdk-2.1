using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkWrite
{

	public class FieldMapping : Model
	{
		string apiName;
		int? index;
		string format;
		string findBy;
		Dictionary<string, object> defaultValue;
		string module;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string APIName
		{
			/// <summary>The method to get the aPIName</summary>
			/// <returns>string representing the apiName</returns>
			get
			{
				return  apiName;

			}
			/// <summary>The method to set the value to aPIName</summary>
			/// <param name="apiName">string</param>
			set
			{
				 apiName=value;

				 keyModified["api_name"] = 1;

			}
		}

		public int? Index
		{
			/// <summary>The method to get the index</summary>
			/// <returns>int? representing the index</returns>
			get
			{
				return  index;

			}
			/// <summary>The method to set the value to index</summary>
			/// <param name="index">int?</param>
			set
			{
				 index=value;

				 keyModified["index"] = 1;

			}
		}

		public string Format
		{
			/// <summary>The method to get the format</summary>
			/// <returns>string representing the format</returns>
			get
			{
				return  format;

			}
			/// <summary>The method to set the value to format</summary>
			/// <param name="format">string</param>
			set
			{
				 format=value;

				 keyModified["format"] = 1;

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

		public Dictionary<string, object> DefaultValue
		{
			/// <summary>The method to get the defaultValue</summary>
			/// <returns>Dictionary representing the defaultValue<String,Object></returns>
			get
			{
				return  defaultValue;

			}
			/// <summary>The method to set the value to defaultValue</summary>
			/// <param name="defaultValue">Dictionary<string,object></param>
			set
			{
				 defaultValue=value;

				 keyModified["default_value"] = 1;

			}
		}

		public string Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>string representing the module</returns>
			get
			{
				return  module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">string</param>
			set
			{
				 module=value;

				 keyModified["module"] = 1;

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