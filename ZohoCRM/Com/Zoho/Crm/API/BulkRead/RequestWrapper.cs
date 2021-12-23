using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkRead
{

	public class RequestWrapper : Model
	{
		CallBack callback;
		Query query;
		Choice<string> fileType;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public CallBack Callback
		{
			/// <summary>The method to get the callback</summary>
			/// <returns>Instance of CallBack</returns>
			get
			{
				return  callback;

			}
			/// <summary>The method to set the value to callback</summary>
			/// <param name="callback">Instance of CallBack</param>
			set
			{
				 callback=value;

				 keyModified["callback"] = 1;

			}
		}

		public Query Query
		{
			/// <summary>The method to get the query</summary>
			/// <returns>Instance of Query</returns>
			get
			{
				return  query;

			}
			/// <summary>The method to set the value to query</summary>
			/// <param name="query">Instance of Query</param>
			set
			{
				 query=value;

				 keyModified["query"] = 1;

			}
		}

		public Choice<string> FileType
		{
			/// <summary>The method to get the fileType</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  fileType;

			}
			/// <summary>The method to set the value to fileType</summary>
			/// <param name="fileType">Instance of Choice<string></param>
			set
			{
				 fileType=value;

				 keyModified["file_type"] = 1;

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