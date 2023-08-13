using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Functions
{

	public class Details : Model
	{
		private string output;
		private string output_type;
		private string id;

		private Dictionary<string, int?> keyModified = new Dictionary<string, int?>();

		public string Id
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return this.id;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="id">Instance of Info</param>
			set
			{
				this.id = value;

				this.keyModified["id"] = 1;

			}
		}

		public string Output_type
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return this.output_type;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="output_type">Instance of Info</param>
			set
			{
				this.output_type = value;

				this.keyModified["output_type"] = 1;

			}
		}

		public string Output
		{
			/// <summary>The method to get the info</summary>
			/// <returns>Instance of Info</returns>
			get
			{
				return this.output;

			}
			/// <summary>The method to set the value to info</summary>
			/// <param name="output">Instance of Info</param>
			set
			{
				this.output = value;

				this.keyModified["output"] = 1;

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