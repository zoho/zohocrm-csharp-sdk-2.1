using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkRead
{

	public class JobDetail : Model
	{
		long? id;
		string operation;
		Choice<string> state;
		Query query;
		User createdBy;
		DateTimeOffset? createdTime;
		Result result;
		string fileType;
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

		public Choice<string> State
		{
			/// <summary>The method to get the state</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  state;

			}
			/// <summary>The method to set the value to state</summary>
			/// <param name="state">Instance of Choice<string></param>
			set
			{
				 state=value;

				 keyModified["state"] = 1;

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

		public string FileType
		{
			/// <summary>The method to get the fileType</summary>
			/// <returns>string representing the fileType</returns>
			get
			{
				return  fileType;

			}
			/// <summary>The method to set the value to fileType</summary>
			/// <param name="fileType">string</param>
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