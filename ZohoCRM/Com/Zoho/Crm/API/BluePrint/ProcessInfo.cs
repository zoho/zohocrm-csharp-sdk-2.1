using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BluePrint
{

	public class ProcessInfo : Model
	{
		string fieldId;
		bool? isContinuous;
		string apiName;
		bool? continuous;
		string fieldLabel;
		string name;
		string columnName;
		string fieldValue;
		long? id;
		string fieldName;
		Escalation escalation;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string FieldId
		{
			/// <summary>The method to get the fieldId</summary>
			/// <returns>string representing the fieldId</returns>
			get
			{
				return  fieldId;

			}
			/// <summary>The method to set the value to fieldId</summary>
			/// <param name="fieldId">string</param>
			set
			{
				 fieldId=value;

				 keyModified["field_id"] = 1;

			}
		}

		public bool? IsContinuous
		{
			/// <summary>The method to get the isContinuous</summary>
			/// <returns>bool? representing the isContinuous</returns>
			get
			{
				return  isContinuous;

			}
			/// <summary>The method to set the value to isContinuous</summary>
			/// <param name="isContinuous">bool?</param>
			set
			{
				 isContinuous=value;

				 keyModified["is_continuous"] = 1;

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

		public bool? Continuous
		{
			/// <summary>The method to get the continuous</summary>
			/// <returns>bool? representing the continuous</returns>
			get
			{
				return  continuous;

			}
			/// <summary>The method to set the value to continuous</summary>
			/// <param name="continuous">bool?</param>
			set
			{
				 continuous=value;

				 keyModified["continuous"] = 1;

			}
		}

		public string FieldLabel
		{
			/// <summary>The method to get the fieldLabel</summary>
			/// <returns>string representing the fieldLabel</returns>
			get
			{
				return  fieldLabel;

			}
			/// <summary>The method to set the value to fieldLabel</summary>
			/// <param name="fieldLabel">string</param>
			set
			{
				 fieldLabel=value;

				 keyModified["field_label"] = 1;

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

		public string ColumnName
		{
			/// <summary>The method to get the columnName</summary>
			/// <returns>string representing the columnName</returns>
			get
			{
				return  columnName;

			}
			/// <summary>The method to set the value to columnName</summary>
			/// <param name="columnName">string</param>
			set
			{
				 columnName=value;

				 keyModified["column_name"] = 1;

			}
		}

		public string FieldValue
		{
			/// <summary>The method to get the fieldValue</summary>
			/// <returns>string representing the fieldValue</returns>
			get
			{
				return  fieldValue;

			}
			/// <summary>The method to set the value to fieldValue</summary>
			/// <param name="fieldValue">string</param>
			set
			{
				 fieldValue=value;

				 keyModified["field_value"] = 1;

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

		public string FieldName
		{
			/// <summary>The method to get the fieldName</summary>
			/// <returns>string representing the fieldName</returns>
			get
			{
				return  fieldName;

			}
			/// <summary>The method to set the value to fieldName</summary>
			/// <param name="fieldName">string</param>
			set
			{
				 fieldName=value;

				 keyModified["field_name"] = 1;

			}
		}

		public Escalation Escalation
		{
			/// <summary>The method to get the escalation</summary>
			/// <returns>Instance of Escalation</returns>
			get
			{
				return  escalation;

			}
			/// <summary>The method to set the value to escalation</summary>
			/// <param name="escalation">Instance of Escalation</param>
			set
			{
				 escalation=value;

				 keyModified["escalation"] = 1;

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