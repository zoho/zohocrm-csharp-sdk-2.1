using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Wizards
{

	public class Connection : Model
	{
		Button sourceButton;
		Screen targetScreen;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Button SourceButton
		{
			/// <summary>The method to get the sourceButton</summary>
			/// <returns>Instance of Button</returns>
			get
			{
				return  sourceButton;

			}
			/// <summary>The method to set the value to sourceButton</summary>
			/// <param name="sourceButton">Instance of Button</param>
			set
			{
				 sourceButton=value;

				 keyModified["source_button"] = 1;

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