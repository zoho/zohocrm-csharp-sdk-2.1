using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class ViewType : Model
	{
		bool? view;
		bool? edit;
		bool? create;
		bool? quickCreate;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? View
		{
			/// <summary>The method to get the view</summary>
			/// <returns>bool? representing the view</returns>
			get
			{
				return  view;

			}
			/// <summary>The method to set the value to view</summary>
			/// <param name="view">bool?</param>
			set
			{
				 view=value;

				 keyModified["view"] = 1;

			}
		}

		public bool? Edit
		{
			/// <summary>The method to get the edit</summary>
			/// <returns>bool? representing the edit</returns>
			get
			{
				return  edit;

			}
			/// <summary>The method to set the value to edit</summary>
			/// <param name="edit">bool?</param>
			set
			{
				 edit=value;

				 keyModified["edit"] = 1;

			}
		}

		public bool? Create
		{
			/// <summary>The method to get the create</summary>
			/// <returns>bool? representing the create</returns>
			get
			{
				return  create;

			}
			/// <summary>The method to set the value to create</summary>
			/// <param name="create">bool?</param>
			set
			{
				 create=value;

				 keyModified[Constants.REQUEST_CATEGORY_CREATE] = 1;

			}
		}

		public bool? QuickCreate
		{
			/// <summary>The method to get the quickCreate</summary>
			/// <returns>bool? representing the quickCreate</returns>
			get
			{
				return  quickCreate;

			}
			/// <summary>The method to set the value to quickCreate</summary>
			/// <param name="quickCreate">bool?</param>
			set
			{
				 quickCreate=value;

				 keyModified["quick_create"] = 1;

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