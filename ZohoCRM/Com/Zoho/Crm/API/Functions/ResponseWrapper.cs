using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Functions
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		private string code;
		private Details details;
		private string message;

		private Dictionary<string, int?> keyModified = new Dictionary<string, int?>();

		public string Code
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return this.code;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="code">Instance of Info</param>
			set
			{
				this.code = value;

				this.keyModified["code"] = 1;

			}
		}

		public Details Details
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return this.details;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="details">Instance of Info</param>
			set
			{
				this.details = value;

				this.keyModified["details"] = 1;

			}
		}

		public string Message
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return this.message;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="message">Instance of Info</param>
			set
			{
				this.message = value;

				this.keyModified["message"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if (((this.keyModified.ContainsKey(key))))
			{
				return this.keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			this.keyModified[key] = modification;
		}

	}
}
