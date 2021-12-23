using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BluePrint
{

	public class ValidationError : Model
	{
		string apiName;
		string infoMessage;
		string message;
		int? index;
		string parentAPIName;
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

		public string InfoMessage
		{
			/// <summary>The method to get the infoMessage</summary>
			/// <returns>string representing the infoMessage</returns>
			get
			{
				return  infoMessage;

			}
			/// <summary>The method to set the value to infoMessage</summary>
			/// <param name="infoMessage">string</param>
			set
			{
				 infoMessage=value;

				 keyModified["info_message"] = 1;

			}
		}

		public string Message
		{
			/// <summary>The method to get the message</summary>
			/// <returns>string representing the message</returns>
			get
			{
				return  message;

			}
			/// <summary>The method to set the value to message</summary>
			/// <param name="message">string</param>
			set
			{
				 message=value;

				 keyModified["message"] = 1;

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

		public string ParentAPIName
		{
			/// <summary>The method to get the parentAPIName</summary>
			/// <returns>string representing the parentAPIName</returns>
			get
			{
				return  parentAPIName;

			}
			/// <summary>The method to set the value to parentAPIName</summary>
			/// <param name="parentAPIName">string</param>
			set
			{
				 parentAPIName=value;

				 keyModified["parent_api_name"] = 1;

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