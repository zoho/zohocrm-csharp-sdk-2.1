using Com.Zoho.Crm.API.Fields;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Wizards
{

	public class Segment : Model
	{
		long? id;
		int? sequenceNumber;
		string displayLabel;
		string type;
		int? columnCount;
		List<Field> fields;
		List<Button> buttons;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public List<Button> Buttons
		{
			/// <summary>The method to get the buttons</summary>
			/// <returns>Instance of List<Button></returns>
			get
			{
				return  buttons;

			}
			/// <summary>The method to set the value to buttons</summary>
			/// <param name="buttons">Instance of List<Button></param>
			set
			{
				 buttons=value;

				 keyModified["buttons"] = 1;

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