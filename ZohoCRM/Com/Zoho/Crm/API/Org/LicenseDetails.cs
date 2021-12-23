using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Org
{

	public class LicenseDetails : Model
	{
		DateTimeOffset? paidExpiry;
		long? usersLicensePurchased;
		string trialType;
		string trialExpiry;
		bool? paid;
		string paidType;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public DateTimeOffset? PaidExpiry
		{
			/// <summary>The method to get the paidExpiry</summary>
			/// <returns>DateTimeOffset? representing the paidExpiry</returns>
			get
			{
				return  paidExpiry;

			}
			/// <summary>The method to set the value to paidExpiry</summary>
			/// <param name="paidExpiry">DateTimeOffset?</param>
			set
			{
				 paidExpiry=value;

				 keyModified["paid_expiry"] = 1;

			}
		}

		public long? UsersLicensePurchased
		{
			/// <summary>The method to get the usersLicensePurchased</summary>
			/// <returns>long? representing the usersLicensePurchased</returns>
			get
			{
				return  usersLicensePurchased;

			}
			/// <summary>The method to set the value to usersLicensePurchased</summary>
			/// <param name="usersLicensePurchased">long?</param>
			set
			{
				 usersLicensePurchased=value;

				 keyModified["users_license_purchased"] = 1;

			}
		}

		public string TrialType
		{
			/// <summary>The method to get the trialType</summary>
			/// <returns>string representing the trialType</returns>
			get
			{
				return  trialType;

			}
			/// <summary>The method to set the value to trialType</summary>
			/// <param name="trialType">string</param>
			set
			{
				 trialType=value;

				 keyModified["trial_type"] = 1;

			}
		}

		public string TrialExpiry
		{
			/// <summary>The method to get the trialExpiry</summary>
			/// <returns>string representing the trialExpiry</returns>
			get
			{
				return  trialExpiry;

			}
			/// <summary>The method to set the value to trialExpiry</summary>
			/// <param name="trialExpiry">string</param>
			set
			{
				 trialExpiry=value;

				 keyModified["trial_expiry"] = 1;

			}
		}

		public bool? Paid
		{
			/// <summary>The method to get the paid</summary>
			/// <returns>bool? representing the paid</returns>
			get
			{
				return  paid;

			}
			/// <summary>The method to set the value to paid</summary>
			/// <param name="paid">bool?</param>
			set
			{
				 paid=value;

				 keyModified["paid"] = 1;

			}
		}

		public string PaidType
		{
			/// <summary>The method to get the paidType</summary>
			/// <returns>string representing the paidType</returns>
			get
			{
				return  paidType;

			}
			/// <summary>The method to set the value to paidType</summary>
			/// <param name="paidType">string</param>
			set
			{
				 paidType=value;

				 keyModified["paid_type"] = 1;

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