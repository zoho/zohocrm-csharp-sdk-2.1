using Com.Zoho.Crm.API.CustomViews;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Wizards
{

	public class Button : Model
	{
		long? id;
		int? sequenceNumber;
		string displayLabel;
		Criteria criteria;
		Screen targetScreen;
		string type;
		string color;
		string shape;
		string backgroundColor;
		string visibility;
		Transition transition;
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

		public Criteria Criteria
		{
			/// <summary>The method to get the criteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  criteria;

			}
			/// <summary>The method to set the value to criteria</summary>
			/// <param name="criteria">Instance of Criteria</param>
			set
			{
				 criteria=value;

				 keyModified["criteria"] = 1;

			}
		}

		public Screen TargetScreen
		{
			/// <summary>The method to get the targetScreen</summary>
			/// <returns>Instance of Screen</returns>
			get
			{
				return  targetScreen;

			}
			/// <summary>The method to set the value to targetScreen</summary>
			/// <param name="targetScreen">Instance of Screen</param>
			set
			{
				 targetScreen=value;

				 keyModified["target_screen"] = 1;

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

		public string Color
		{
			/// <summary>The method to get the color</summary>
			/// <returns>string representing the color</returns>
			get
			{
				return  color;

			}
			/// <summary>The method to set the value to color</summary>
			/// <param name="color">string</param>
			set
			{
				 color=value;

				 keyModified["color"] = 1;

			}
		}

		public string Shape
		{
			/// <summary>The method to get the shape</summary>
			/// <returns>string representing the shape</returns>
			get
			{
				return  shape;

			}
			/// <summary>The method to set the value to shape</summary>
			/// <param name="shape">string</param>
			set
			{
				 shape=value;

				 keyModified["shape"] = 1;

			}
		}

		public string BackgroundColor
		{
			/// <summary>The method to get the backgroundColor</summary>
			/// <returns>string representing the backgroundColor</returns>
			get
			{
				return  backgroundColor;

			}
			/// <summary>The method to set the value to backgroundColor</summary>
			/// <param name="backgroundColor">string</param>
			set
			{
				 backgroundColor=value;

				 keyModified["background_color"] = 1;

			}
		}

		public string Visibility
		{
			/// <summary>The method to get the visibility</summary>
			/// <returns>string representing the visibility</returns>
			get
			{
				return  visibility;

			}
			/// <summary>The method to set the value to visibility</summary>
			/// <param name="visibility">string</param>
			set
			{
				 visibility=value;

				 keyModified["visibility"] = 1;

			}
		}

		public Transition Transition
		{
			/// <summary>The method to get the transition</summary>
			/// <returns>Instance of Transition</returns>
			get
			{
				return  transition;

			}
			/// <summary>The method to set the value to transition</summary>
			/// <param name="transition">Instance of Transition</param>
			set
			{
				 transition=value;

				 keyModified["transition"] = 1;

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