using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class Crypt : Model
	{
		string mode;
		string column;
		List<string> encfldids;
		string notify;
		string table;
		int? status;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Mode
		{
			/// <summary>The method to get the mode</summary>
			/// <returns>string representing the mode</returns>
			get
			{
				return  mode;

			}
			/// <summary>The method to set the value to mode</summary>
			/// <param name="mode">string</param>
			set
			{
				 mode=value;

				 keyModified["mode"] = 1;

			}
		}

		public string Column
		{
			/// <summary>The method to get the column</summary>
			/// <returns>string representing the column</returns>
			get
			{
				return  column;

			}
			/// <summary>The method to set the value to column</summary>
			/// <param name="column">string</param>
			set
			{
				 column=value;

				 keyModified["column"] = 1;

			}
		}

		public List<string> Encfldids
		{
			/// <summary>The method to get the encfldids</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  encfldids;

			}
			/// <summary>The method to set the value to encfldids</summary>
			/// <param name="encfldids">Instance of List<string></param>
			set
			{
				 encfldids=value;

				 keyModified["encFldIds"] = 1;

			}
		}

		public string Notify
		{
			/// <summary>The method to get the notify</summary>
			/// <returns>string representing the notify</returns>
			get
			{
				return  notify;

			}
			/// <summary>The method to set the value to notify</summary>
			/// <param name="notify">string</param>
			set
			{
				 notify=value;

				 keyModified["notify"] = 1;

			}
		}

		public string Table
		{
			/// <summary>The method to get the table</summary>
			/// <returns>string representing the table</returns>
			get
			{
				return  table;

			}
			/// <summary>The method to set the value to table</summary>
			/// <param name="table">string</param>
			set
			{
				 table=value;

				 keyModified["table"] = 1;

			}
		}

		public int? Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>int? representing the status</returns>
			get
			{
				return  status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">int?</param>
			set
			{
				 status=value;

				 keyModified["status"] = 1;

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