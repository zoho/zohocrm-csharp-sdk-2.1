using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class MultiUserLookup : Model
	{
		string displayLabel;
		string linkingModule;
		string lookupApiname;
		string apiName;
		string connectedModule;
		string connectedlookupApiname;
		long? id;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string DisplayLabel
		{
			/// <summary>The method to get the displayLabel</summary>
			/// <returns>string representing the displayLabel</returns>
			get
			{
				return  displayLabel;

			}
			/// <summary>The method to set the value to displayLabel</summary>
			/// <param name="displayLabel">string</param>
			set
			{
				 displayLabel=value;

				 keyModified["display_label"] = 1;

			}
		}

		public string LinkingModule
		{
			/// <summary>The method to get the linkingModule</summary>
			/// <returns>string representing the linkingModule</returns>
			get
			{
				return  linkingModule;

			}
			/// <summary>The method to set the value to linkingModule</summary>
			/// <param name="linkingModule">string</param>
			set
			{
				 linkingModule=value;

				 keyModified["linking_module"] = 1;

			}
		}

		public string LookupApiname
		{
			/// <summary>The method to get the lookupApiname</summary>
			/// <returns>string representing the lookupApiname</returns>
			get
			{
				return  lookupApiname;

			}
			/// <summary>The method to set the value to lookupApiname</summary>
			/// <param name="lookupApiname">string</param>
			set
			{
				 lookupApiname=value;

				 keyModified["lookup_apiname"] = 1;

			}
		}

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

		public string ConnectedModule
		{
			/// <summary>The method to get the connectedModule</summary>
			/// <returns>string representing the connectedModule</returns>
			get
			{
				return  connectedModule;

			}
			/// <summary>The method to set the value to connectedModule</summary>
			/// <param name="connectedModule">string</param>
			set
			{
				 connectedModule=value;

				 keyModified["connected_module"] = 1;

			}
		}

		public string ConnectedlookupApiname
		{
			/// <summary>The method to get the connectedlookupApiname</summary>
			/// <returns>string representing the connectedlookupApiname</returns>
			get
			{
				return  connectedlookupApiname;

			}
			/// <summary>The method to set the value to connectedlookupApiname</summary>
			/// <param name="connectedlookupApiname">string</param>
			set
			{
				 connectedlookupApiname=value;

				 keyModified["connectedlookup_apiname"] = 1;

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