using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkWrite
{

	public class BulkWriteResponse : Model, ResponseWrapper
	{
		string status;
		string characterEncoding;
		List<Resource> resource;
		long? id;
		CallBack callback;
		Result result;
		User createdBy;
		string operation;
		DateTimeOffset? createdTime;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>string representing the status</returns>
			get
			{
				return  status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">string</param>
			set
			{
				 status=value;

				 keyModified["status"] = 1;

			}
		}

		public string CharacterEncoding
		{
			/// <summary>The method to get the characterEncoding</summary>
			/// <returns>string representing the characterEncoding</returns>
			get
			{
				return  characterEncoding;

			}
			/// <summary>The method to set the value to characterEncoding</summary>
			/// <param name="characterEncoding">string</param>
			set
			{
				 characterEncoding=value;

				 keyModified["character_encoding"] = 1;

			}
		}

		public List<Resource> Resource
		{
			/// <summary>The method to get the resource</summary>
			/// <returns>Instance of List<Resource></returns>
			get
			{
				return  resource;

			}
			/// <summary>The method to set the value to resource</summary>
			/// <param name="resource">Instance of List<Resource></param>
			set
			{
				 resource=value;

				 keyModified["resource"] = 1;

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

		public Result Result
		{
			/// <summary>The method to get the result</summary>
			/// <returns>Instance of Result</returns>
			get
			{
				return  result;

			}
			/// <summary>The method to set the value to result</summary>
			/// <param name="result">Instance of Result</param>
			set
			{
				 result=value;

				 keyModified["result"] = 1;

			}
		}

		public User CreatedBy
		{
			/// <summary>The method to get the createdBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  createdBy;

			}
			/// <summary>The method to set the value to createdBy</summary>
			/// <param name="createdBy">Instance of User</param>
			set
			{
				 createdBy=value;

				 keyModified["created_by"] = 1;

			}
		}

		public string Operation
		{
			/// <summary>The method to get the operation</summary>
			/// <returns>string representing the operation</returns>
			get
			{
				return  operation;

			}
			/// <summary>The method to set the value to operation</summary>
			/// <param name="operation">string</param>
			set
			{
				 operation=value;

				 keyModified["operation"] = 1;

			}
		}

		public DateTimeOffset? CreatedTime
		{
			/// <summary>The method to get the createdTime</summary>
			/// <returns>DateTimeOffset? representing the createdTime</returns>
			get
			{
				return  createdTime;

			}
			/// <summary>The method to set the value to createdTime</summary>
			/// <param name="createdTime">DateTimeOffset?</param>
			set
			{
				 createdTime=value;

				 keyModified["created_time"] = 1;

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