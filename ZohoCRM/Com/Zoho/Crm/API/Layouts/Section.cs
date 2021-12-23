using Com.Zoho.Crm.API.Fields;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Layouts
{

	public class Section : Model
	{
		string displayLabel;
		int? sequenceNumber;
		bool? issubformsection;
		int? tabTraversal;
		string apiName;
		int? columnCount;
		string name;
		string generatedType;
		List<Field> fields;
		Properties properties;
		string type;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string DisplayLabel
		{
			/// <summary>The method to get the displayLabel</summary>
			/// <returns>string representing the displayLabel</returns>
			get
			{
				return  displayLabel;

			}
			/// <summary>The method to set the value to displayLabel</summary>
			/// <param name="displayLabel">string</param>
			set
			{
				 displayLabel=value;

				 keyModified["display_label"] = 1;

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

				 keyModified["sequence_number"] = 1;

			}
		}

		public bool? Issubformsection
		{
			/// <summary>The method to get the issubformsection</summary>
			/// <returns>bool? representing the issubformsection</returns>
			get
			{
				return  issubformsection;

			}
			/// <summary>The method to set the value to issubformsection</summary>
			/// <param name="issubformsection">bool?</param>
			set
			{
				 issubformsection=value;

				 keyModified["isSubformSection"] = 1;

			}
		}

		public int? TabTraversal
		{
			/// <summary>The method to get the tabTraversal</summary>
			/// <returns>int? representing the tabTraversal</returns>
			get
			{
				return  tabTraversal;

			}
			/// <summary>The method to set the value to tabTraversal</summary>
			/// <param name="tabTraversal">int?</param>
			set
			{
				 tabTraversal=value;

				 keyModified["tab_traversal"] = 1;

			}
		}

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

		public int? ColumnCount
		{
			/// <summary>The method to get the columnCount</summary>
			/// <returns>int? representing the columnCount</returns>
			get
			{
				return  columnCount;

			}
			/// <summary>The method to set the value to columnCount</summary>
			/// <param name="columnCount">int?</param>
			set
			{
				 columnCount=value;

				 keyModified["column_count"] = 1;

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

		public string GeneratedType
		{
			/// <summary>The method to get the generatedType</summary>
			/// <returns>string representing the generatedType</returns>
			get
			{
				return  generatedType;

			}
			/// <summary>The method to set the value to generatedType</summary>
			/// <param name="generatedType">string</param>
			set
			{
				 generatedType=value;

				 keyModified["generated_type"] = 1;

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

		public Properties Properties
		{
			/// <summary>The method to get the properties</summary>
			/// <returns>Instance of Properties</returns>
			get
			{
				return  properties;

			}
			/// <summary>The method to set the value to properties</summary>
			/// <param name="properties">Instance of Properties</param>
			set
			{
				 properties=value;

				 keyModified["properties"] = 1;

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