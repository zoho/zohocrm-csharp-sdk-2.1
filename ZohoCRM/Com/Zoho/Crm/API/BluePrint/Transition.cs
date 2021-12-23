using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BluePrint
{

	public class Transition : Model
	{
		List<NextTransition> nextTransitions;
		Transition parentTransition;
		double? percentPartialSave;
		Record.Record data;
		string nextFieldValue;
		string name;
		bool? criteriaMatched;
		long? id;
		List<Field> fields;
		string criteriaMessage;
		string type;
		DateTimeOffset? executionTime;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<NextTransition> NextTransitions
		{
			/// <summary>The method to get the nextTransitions</summary>
			/// <returns>Instance of List<NextTransition></returns>
			get
			{
				return  nextTransitions;

			}
			/// <summary>The method to set the value to nextTransitions</summary>
			/// <param name="nextTransitions">Instance of List<NextTransition></param>
			set
			{
				 nextTransitions=value;

				 keyModified["next_transitions"] = 1;

			}
		}

		public Transition ParentTransition
		{
			/// <summary>The method to get the parentTransition</summary>
			/// <returns>Instance of Transition</returns>
			get
			{
				return  parentTransition;

			}
			/// <summary>The method to set the value to parentTransition</summary>
			/// <param name="parentTransition">Instance of Transition</param>
			set
			{
				 parentTransition=value;

				 keyModified["parent_transition"] = 1;

			}
		}

		public double? PercentPartialSave
		{
			/// <summary>The method to get the percentPartialSave</summary>
			/// <returns>double? representing the percentPartialSave</returns>
			get
			{
				return  percentPartialSave;

			}
			/// <summary>The method to set the value to percentPartialSave</summary>
			/// <param name="percentPartialSave">double?</param>
			set
			{
				 percentPartialSave=value;

				 keyModified["percent_partial_save"] = 1;

			}
		}

		public Record.Record Data
		{
			/// <summary>The method to get the data</summary>
			/// <returns>Instance of Record</returns>
			get
			{
				return  data;

			}
			/// <summary>The method to set the value to data</summary>
			/// <param name="data">Instance of Record</param>
			set
			{
				 data=value;

				 keyModified["data"] = 1;

			}
		}

		public string NextFieldValue
		{
			/// <summary>The method to get the nextFieldValue</summary>
			/// <returns>string representing the nextFieldValue</returns>
			get
			{
				return  nextFieldValue;

			}
			/// <summary>The method to set the value to nextFieldValue</summary>
			/// <param name="nextFieldValue">string</param>
			set
			{
				 nextFieldValue=value;

				 keyModified["next_field_value"] = 1;

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

		public bool? CriteriaMatched
		{
			/// <summary>The method to get the criteriaMatched</summary>
			/// <returns>bool? representing the criteriaMatched</returns>
			get
			{
				return  criteriaMatched;

			}
			/// <summary>The method to set the value to criteriaMatched</summary>
			/// <param name="criteriaMatched">bool?</param>
			set
			{
				 criteriaMatched=value;

				 keyModified["criteria_matched"] = 1;

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

		public string CriteriaMessage
		{
			/// <summary>The method to get the criteriaMessage</summary>
			/// <returns>string representing the criteriaMessage</returns>
			get
			{
				return  criteriaMessage;

			}
			/// <summary>The method to set the value to criteriaMessage</summary>
			/// <param name="criteriaMessage">string</param>
			set
			{
				 criteriaMessage=value;

				 keyModified["criteria_message"] = 1;

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

		public DateTimeOffset? ExecutionTime
		{
			/// <summary>The method to get the executionTime</summary>
			/// <returns>DateTimeOffset? representing the executionTime</returns>
			get
			{
				return  executionTime;

			}
			/// <summary>The method to set the value to executionTime</summary>
			/// <param name="executionTime">DateTimeOffset?</param>
			set
			{
				 executionTime=value;

				 keyModified["execution_time"] = 1;

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