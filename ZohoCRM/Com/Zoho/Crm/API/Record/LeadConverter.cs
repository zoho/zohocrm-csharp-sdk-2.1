using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class LeadConverter : Model
	{
		bool? overwrite;
		bool? notifyLeadOwner;
		bool? notifyNewEntityOwner;
		string accounts;
		string contacts;
		string assignTo;
		Record deals;
		CarryOverTags carryOverTags;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? Overwrite
		{
			/// <summary>The method to get the overwrite</summary>
			/// <returns>bool? representing the overwrite</returns>
			get
			{
				return  overwrite;

			}
			/// <summary>The method to set the value to overwrite</summary>
			/// <param name="overwrite">bool?</param>
			set
			{
				 overwrite=value;

				 keyModified["overwrite"] = 1;

			}
		}

		public bool? NotifyLeadOwner
		{
			/// <summary>The method to get the notifyLeadOwner</summary>
			/// <returns>bool? representing the notifyLeadOwner</returns>
			get
			{
				return  notifyLeadOwner;

			}
			/// <summary>The method to set the value to notifyLeadOwner</summary>
			/// <param name="notifyLeadOwner">bool?</param>
			set
			{
				 notifyLeadOwner=value;

				 keyModified["notify_lead_owner"] = 1;

			}
		}

		public bool? NotifyNewEntityOwner
		{
			/// <summary>The method to get the notifyNewEntityOwner</summary>
			/// <returns>bool? representing the notifyNewEntityOwner</returns>
			get
			{
				return  notifyNewEntityOwner;

			}
			/// <summary>The method to set the value to notifyNewEntityOwner</summary>
			/// <param name="notifyNewEntityOwner">bool?</param>
			set
			{
				 notifyNewEntityOwner=value;

				 keyModified["notify_new_entity_owner"] = 1;

			}
		}

		public string Accounts
		{
			/// <summary>The method to get the accounts</summary>
			/// <returns>string representing the accounts</returns>
			get
			{
				return  accounts;

			}
			/// <summary>The method to set the value to accounts</summary>
			/// <param name="accounts">string</param>
			set
			{
				 accounts=value;

				 keyModified["Accounts"] = 1;

			}
		}

		public string Contacts
		{
			/// <summary>The method to get the contacts</summary>
			/// <returns>string representing the contacts</returns>
			get
			{
				return  contacts;

			}
			/// <summary>The method to set the value to contacts</summary>
			/// <param name="contacts">string</param>
			set
			{
				 contacts=value;

				 keyModified["Contacts"] = 1;

			}
		}

		public string AssignTo
		{
			/// <summary>The method to get the assignTo</summary>
			/// <returns>string representing the assignTo</returns>
			get
			{
				return  assignTo;

			}
			/// <summary>The method to set the value to assignTo</summary>
			/// <param name="assignTo">string</param>
			set
			{
				 assignTo=value;

				 keyModified["assign_to"] = 1;

			}
		}

		public Record Deals
		{
			/// <summary>The method to get the deals</summary>
			/// <returns>Instance of Record</returns>
			get
			{
				return  deals;

			}
			/// <summary>The method to set the value to deals</summary>
			/// <param name="deals">Instance of Record</param>
			set
			{
				 deals=value;

				 keyModified["Deals"] = 1;

			}
		}

		public CarryOverTags CarryOverTags
		{
			/// <summary>The method to get the carryOverTags</summary>
			/// <returns>Instance of CarryOverTags</returns>
			get
			{
				return  carryOverTags;

			}
			/// <summary>The method to set the value to carryOverTags</summary>
			/// <param name="carryOverTags">Instance of CarryOverTags</param>
			set
			{
				 carryOverTags=value;

				 keyModified["carry_over_tags"] = 1;

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