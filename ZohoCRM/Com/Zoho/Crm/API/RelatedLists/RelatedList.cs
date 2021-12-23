using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.RelatedLists
{

	public class RelatedList : Model
	{
		long? id;
		string sequenceNumber;
		string displayLabel;
		string apiName;
		string module;
		string name;
		string action;
		string href;
		string type;
		string connectedmodule;
		string linkingmodule;
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

		public string SequenceNumber
		{
			/// <summary>The method to get the sequenceNumber</summary>
			/// <returns>string representing the sequenceNumber</returns>
			get
			{
				return  sequenceNumber;

			}
			/// <summary>The method to set the value to sequenceNumber</summary>
			/// <param name="sequenceNumber">string</param>
			set
			{
				 sequenceNumber=value;

				 keyModified["sequence_number"] = 1;

			}
		}

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

		public string Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>string representing the module</returns>
			get
			{
				return  module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">string</param>
			set
			{
				 module=value;

				 keyModified["module"] = 1;

			}
		}

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				return  name;

			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 name=value;

				 keyModified["name"] = 1;

			}
		}

		public string Action
		{
			/// <summary>The method to get the action</summary>
			/// <returns>string representing the action</returns>
			get
			{
				return  action;

			}
			/// <summary>The method to set the value to action</summary>
			/// <param name="action">string</param>
			set
			{
				 action=value;

				 keyModified[Constants.REQUEST_CATEGORY_ACTION] = 1;

			}
		}

		public string Href
		{
			/// <summary>The method to get the href</summary>
			/// <returns>string representing the href</returns>
			get
			{
				return  href;

			}
			/// <summary>The method to set the value to href</summary>
			/// <param name="href">string</param>
			set
			{
				 href=value;

				 keyModified["href"] = 1;

			}
		}

		public string Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>string representing the type</returns>
			get
			{
				return  type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">string</param>
			set
			{
				 type=value;

				 keyModified["type"] = 1;

			}
		}

		public string Connectedmodule
		{
			/// <summary>The method to get the connectedmodule</summary>
			/// <returns>string representing the connectedmodule</returns>
			get
			{
				return  connectedmodule;

			}
			/// <summary>The method to set the value to connectedmodule</summary>
			/// <param name="connectedmodule">string</param>
			set
			{
				 connectedmodule=value;

				 keyModified["connectedmodule"] = 1;

			}
		}

		public string Linkingmodule
		{
			/// <summary>The method to get the linkingmodule</summary>
			/// <returns>string representing the linkingmodule</returns>
			get
			{
				return  linkingmodule;

			}
			/// <summary>The method to set the value to linkingmodule</summary>
			/// <param name="linkingmodule">string</param>
			set
			{
				 linkingmodule=value;

				 keyModified["linkingmodule"] = 1;

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