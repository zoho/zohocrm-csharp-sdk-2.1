using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkRead
{

	public class CallBack : Model
	{
		string url;
		Choice<string> method;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Url
		{
			/// <summary>The method to get the url</summary>
			/// <returns>string representing the url</returns>
			get
			{
				return  url;

			}
			/// <summary>The method to set the value to url</summary>
			/// <param name="url">string</param>
			set
			{
				 url=value;

				 keyModified["url"] = 1;

			}
		}

		public Choice<string> Method
		{
			/// <summary>The method to get the method</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  method;

			}
			/// <summary>The method to set the value to method</summary>
			/// <param name="method">Instance of Choice<string></param>
			set
			{
				 method=value;

				 keyModified["method"] = 1;

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