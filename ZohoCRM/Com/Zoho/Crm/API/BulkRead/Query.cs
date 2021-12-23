using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BulkRead
{

	public class Query : Model
	{
		Module module;
		string cvid;
		List<string> fields;
		int? page;
		Criteria criteria;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Module Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">Instance of Module</param>
			set
			{
				 module=value;

				 keyModified["module"] = 1;

			}
		}

		public string Cvid
		{
			/// <summary>The method to get the cvid</summary>
			/// <returns>string representing the cvid</returns>
			get
			{
				return  cvid;

			}
			/// <summary>The method to set the value to cvid</summary>
			/// <param name="cvid">string</param>
			set
			{
				 cvid=value;

				 keyModified["cvid"] = 1;

			}
		}

		public List<string> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Instance of List<string></param>
			set
			{
				 fields=value;

				 keyModified["fields"] = 1;

			}
		}

		public int? Page
		{
			/// <summary>The method to get the page</summary>
			/// <returns>int? representing the page</returns>
			get
			{
				return  page;

			}
			/// <summary>The method to set the value to page</summary>
			/// <param name="page">int?</param>
			set
			{
				 page=value;

				 keyModified["page"] = 1;

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