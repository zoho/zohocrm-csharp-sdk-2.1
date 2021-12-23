using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Roles
{

	public class Role : Model
	{
		string displayLabel;
		User forecastManager;
		bool? shareWithPeers;
		string name;
		string description;
		long? id;
		User reportingTo;
		bool? adminUser;
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

		public User ForecastManager
		{
			/// <summary>The method to get the forecastManager</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  forecastManager;

			}
			/// <summary>The method to set the value to forecastManager</summary>
			/// <param name="forecastManager">Instance of User</param>
			set
			{
				 forecastManager=value;

				 keyModified["forecast_manager"] = 1;

			}
		}

		public bool? ShareWithPeers
		{
			/// <summary>The method to get the shareWithPeers</summary>
			/// <returns>bool? representing the shareWithPeers</returns>
			get
			{
				return  shareWithPeers;

			}
			/// <summary>The method to set the value to shareWithPeers</summary>
			/// <param name="shareWithPeers">bool?</param>
			set
			{
				 shareWithPeers=value;

				 keyModified["share_with_peers"] = 1;

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

		public string Description
		{
			/// <summary>The method to get the description</summary>
			/// <returns>string representing the description</returns>
			get
			{
				return  description;

			}
			/// <summary>The method to set the value to description</summary>
			/// <param name="description">string</param>
			set
			{
				 description=value;

				 keyModified["description"] = 1;

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

		public User ReportingTo
		{
			/// <summary>The method to get the reportingTo</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  reportingTo;

			}
			/// <summary>The method to set the value to reportingTo</summary>
			/// <param name="reportingTo">Instance of User</param>
			set
			{
				 reportingTo=value;

				 keyModified["reporting_to"] = 1;

			}
		}

		public bool? AdminUser
		{
			/// <summary>The method to get the adminUser</summary>
			/// <returns>bool? representing the adminUser</returns>
			get
			{
				return  adminUser;

			}
			/// <summary>The method to set the value to adminUser</summary>
			/// <param name="adminUser">bool?</param>
			set
			{
				 adminUser=value;

				 keyModified["admin_user"] = 1;

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