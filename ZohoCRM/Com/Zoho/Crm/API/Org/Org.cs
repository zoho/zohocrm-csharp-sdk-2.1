using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Org
{

	public class Org : Model
	{
		string country;
		HierarchyPreference hierarchyPreferences;
		string photoId;
		string city;
		string description;
		bool? mcStatus;
		bool? gappsEnabled;
		string domainName;
		bool? translationEnabled;
		string street;
		string alias;
		string currency;
		long? id;
		string state;
		string fax;
		string employeeCount;
		string zip;
		string website;
		string currencySymbol;
		string mobile;
		string currencyLocale;
		string primaryZuid;
		string ziaPortalId;
		string timeZone;
		string zgid;
		string countryCode;
		LicenseDetails licenseDetails;
		string phone;
		string companyName;
		bool? privacySettings;
		string primaryEmail;
		bool? hipaaComplianceEnabled;
		string isoCode;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Country
		{
			/// <summary>The method to get the country</summary>
			/// <returns>string representing the country</returns>
			get
			{
				return  country;

			}
			/// <summary>The method to set the value to country</summary>
			/// <param name="country">string</param>
			set
			{
				 country=value;

				 keyModified["country"] = 1;

			}
		}

		public HierarchyPreference HierarchyPreferences
		{
			/// <summary>The method to get the hierarchyPreferences</summary>
			/// <returns>Instance of HierarchyPreference</returns>
			get
			{
				return  hierarchyPreferences;

			}
			/// <summary>The method to set the value to hierarchyPreferences</summary>
			/// <param name="hierarchyPreferences">Instance of HierarchyPreference</param>
			set
			{
				 hierarchyPreferences=value;

				 keyModified["hierarchy_preferences"] = 1;

			}
		}

		public string PhotoId
		{
			/// <summary>The method to get the photoId</summary>
			/// <returns>string representing the photoId</returns>
			get
			{
				return  photoId;

			}
			/// <summary>The method to set the value to photoId</summary>
			/// <param name="photoId">string</param>
			set
			{
				 photoId=value;

				 keyModified["photo_id"] = 1;

			}
		}

		public string City
		{
			/// <summary>The method to get the city</summary>
			/// <returns>string representing the city</returns>
			get
			{
				return  city;

			}
			/// <summary>The method to set the value to city</summary>
			/// <param name="city">string</param>
			set
			{
				 city=value;

				 keyModified["city"] = 1;

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

		public bool? McStatus
		{
			/// <summary>The method to get the mcStatus</summary>
			/// <returns>bool? representing the mcStatus</returns>
			get
			{
				return  mcStatus;

			}
			/// <summary>The method to set the value to mcStatus</summary>
			/// <param name="mcStatus">bool?</param>
			set
			{
				 mcStatus=value;

				 keyModified["mc_status"] = 1;

			}
		}

		public bool? GappsEnabled
		{
			/// <summary>The method to get the gappsEnabled</summary>
			/// <returns>bool? representing the gappsEnabled</returns>
			get
			{
				return  gappsEnabled;

			}
			/// <summary>The method to set the value to gappsEnabled</summary>
			/// <param name="gappsEnabled">bool?</param>
			set
			{
				 gappsEnabled=value;

				 keyModified["gapps_enabled"] = 1;

			}
		}

		public string DomainName
		{
			/// <summary>The method to get the domainName</summary>
			/// <returns>string representing the domainName</returns>
			get
			{
				return  domainName;

			}
			/// <summary>The method to set the value to domainName</summary>
			/// <param name="domainName">string</param>
			set
			{
				 domainName=value;

				 keyModified["domain_name"] = 1;

			}
		}

		public bool? TranslationEnabled
		{
			/// <summary>The method to get the translationEnabled</summary>
			/// <returns>bool? representing the translationEnabled</returns>
			get
			{
				return  translationEnabled;

			}
			/// <summary>The method to set the value to translationEnabled</summary>
			/// <param name="translationEnabled">bool?</param>
			set
			{
				 translationEnabled=value;

				 keyModified["translation_enabled"] = 1;

			}
		}

		public string Street
		{
			/// <summary>The method to get the street</summary>
			/// <returns>string representing the street</returns>
			get
			{
				return  street;

			}
			/// <summary>The method to set the value to street</summary>
			/// <param name="street">string</param>
			set
			{
				 street=value;

				 keyModified["street"] = 1;

			}
		}

		public string Alias
		{
			/// <summary>The method to get the alias</summary>
			/// <returns>string representing the alias</returns>
			get
			{
				return  alias;

			}
			/// <summary>The method to set the value to alias</summary>
			/// <param name="alias">string</param>
			set
			{
				 alias=value;

				 keyModified["alias"] = 1;

			}
		}

		public string Currency
		{
			/// <summary>The method to get the currency</summary>
			/// <returns>string representing the currency</returns>
			get
			{
				return  currency;

			}
			/// <summary>The method to set the value to currency</summary>
			/// <param name="currency">string</param>
			set
			{
				 currency=value;

				 keyModified["currency"] = 1;

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

		public string State
		{
			/// <summary>The method to get the state</summary>
			/// <returns>string representing the state</returns>
			get
			{
				return  state;

			}
			/// <summary>The method to set the value to state</summary>
			/// <param name="state">string</param>
			set
			{
				 state=value;

				 keyModified["state"] = 1;

			}
		}

		public string Fax
		{
			/// <summary>The method to get the fax</summary>
			/// <returns>string representing the fax</returns>
			get
			{
				return  fax;

			}
			/// <summary>The method to set the value to fax</summary>
			/// <param name="fax">string</param>
			set
			{
				 fax=value;

				 keyModified["fax"] = 1;

			}
		}

		public string EmployeeCount
		{
			/// <summary>The method to get the employeeCount</summary>
			/// <returns>string representing the employeeCount</returns>
			get
			{
				return  employeeCount;

			}
			/// <summary>The method to set the value to employeeCount</summary>
			/// <param name="employeeCount">string</param>
			set
			{
				 employeeCount=value;

				 keyModified["employee_count"] = 1;

			}
		}

		public string Zip
		{
			/// <summary>The method to get the zip</summary>
			/// <returns>string representing the zip</returns>
			get
			{
				return  zip;

			}
			/// <summary>The method to set the value to zip</summary>
			/// <param name="zip">string</param>
			set
			{
				 zip=value;

				 keyModified["zip"] = 1;

			}
		}

		public string Website
		{
			/// <summary>The method to get the website</summary>
			/// <returns>string representing the website</returns>
			get
			{
				return  website;

			}
			/// <summary>The method to set the value to website</summary>
			/// <param name="website">string</param>
			set
			{
				 website=value;

				 keyModified["website"] = 1;

			}
		}

		public string CurrencySymbol
		{
			/// <summary>The method to get the currencySymbol</summary>
			/// <returns>string representing the currencySymbol</returns>
			get
			{
				return  currencySymbol;

			}
			/// <summary>The method to set the value to currencySymbol</summary>
			/// <param name="currencySymbol">string</param>
			set
			{
				 currencySymbol=value;

				 keyModified["currency_symbol"] = 1;

			}
		}

		public string Mobile
		{
			/// <summary>The method to get the mobile</summary>
			/// <returns>string representing the mobile</returns>
			get
			{
				return  mobile;

			}
			/// <summary>The method to set the value to mobile</summary>
			/// <param name="mobile">string</param>
			set
			{
				 mobile=value;

				 keyModified["mobile"] = 1;

			}
		}

		public string CurrencyLocale
		{
			/// <summary>The method to get the currencyLocale</summary>
			/// <returns>string representing the currencyLocale</returns>
			get
			{
				return  currencyLocale;

			}
			/// <summary>The method to set the value to currencyLocale</summary>
			/// <param name="currencyLocale">string</param>
			set
			{
				 currencyLocale=value;

				 keyModified["currency_locale"] = 1;

			}
		}

		public string PrimaryZuid
		{
			/// <summary>The method to get the primaryZuid</summary>
			/// <returns>string representing the primaryZuid</returns>
			get
			{
				return  primaryZuid;

			}
			/// <summary>The method to set the value to primaryZuid</summary>
			/// <param name="primaryZuid">string</param>
			set
			{
				 primaryZuid=value;

				 keyModified["primary_zuid"] = 1;

			}
		}

		public string ZiaPortalId
		{
			/// <summary>The method to get the ziaPortalId</summary>
			/// <returns>string representing the ziaPortalId</returns>
			get
			{
				return  ziaPortalId;

			}
			/// <summary>The method to set the value to ziaPortalId</summary>
			/// <param name="ziaPortalId">string</param>
			set
			{
				 ziaPortalId=value;

				 keyModified["zia_portal_id"] = 1;

			}
		}

		public string TimeZone
		{
			/// <summary>The method to get the timeZone</summary>
			/// <returns>string representing the timeZone</returns>
			get
			{
				return  timeZone;

			}
			/// <summary>The method to set the value to timeZone</summary>
			/// <param name="timeZone">string</param>
			set
			{
				 timeZone=value;

				 keyModified["time_zone"] = 1;

			}
		}

		public string Zgid
		{
			/// <summary>The method to get the zgid</summary>
			/// <returns>string representing the zgid</returns>
			get
			{
				return  zgid;

			}
			/// <summary>The method to set the value to zgid</summary>
			/// <param name="zgid">string</param>
			set
			{
				 zgid=value;

				 keyModified["zgid"] = 1;

			}
		}

		public string CountryCode
		{
			/// <summary>The method to get the countryCode</summary>
			/// <returns>string representing the countryCode</returns>
			get
			{
				return  countryCode;

			}
			/// <summary>The method to set the value to countryCode</summary>
			/// <param name="countryCode">string</param>
			set
			{
				 countryCode=value;

				 keyModified["country_code"] = 1;

			}
		}

		public LicenseDetails LicenseDetails
		{
			/// <summary>The method to get the licenseDetails</summary>
			/// <returns>Instance of LicenseDetails</returns>
			get
			{
				return  licenseDetails;

			}
			/// <summary>The method to set the value to licenseDetails</summary>
			/// <param name="licenseDetails">Instance of LicenseDetails</param>
			set
			{
				 licenseDetails=value;

				 keyModified["license_details"] = 1;

			}
		}

		public string Phone
		{
			/// <summary>The method to get the phone</summary>
			/// <returns>string representing the phone</returns>
			get
			{
				return  phone;

			}
			/// <summary>The method to set the value to phone</summary>
			/// <param name="phone">string</param>
			set
			{
				 phone=value;

				 keyModified["phone"] = 1;

			}
		}

		public string CompanyName
		{
			/// <summary>The method to get the companyName</summary>
			/// <returns>string representing the companyName</returns>
			get
			{
				return  companyName;

			}
			/// <summary>The method to set the value to companyName</summary>
			/// <param name="companyName">string</param>
			set
			{
				 companyName=value;

				 keyModified["company_name"] = 1;

			}
		}

		public bool? PrivacySettings
		{
			/// <summary>The method to get the privacySettings</summary>
			/// <returns>bool? representing the privacySettings</returns>
			get
			{
				return  privacySettings;

			}
			/// <summary>The method to set the value to privacySettings</summary>
			/// <param name="privacySettings">bool?</param>
			set
			{
				 privacySettings=value;

				 keyModified["privacy_settings"] = 1;

			}
		}

		public string PrimaryEmail
		{
			/// <summary>The method to get the primaryEmail</summary>
			/// <returns>string representing the primaryEmail</returns>
			get
			{
				return  primaryEmail;

			}
			/// <summary>The method to set the value to primaryEmail</summary>
			/// <param name="primaryEmail">string</param>
			set
			{
				 primaryEmail=value;

				 keyModified["primary_email"] = 1;

			}
		}

		public bool? HipaaComplianceEnabled
		{
			/// <summary>The method to get the hipaaComplianceEnabled</summary>
			/// <returns>bool? representing the hipaaComplianceEnabled</returns>
			get
			{
				return  hipaaComplianceEnabled;

			}
			/// <summary>The method to set the value to hipaaComplianceEnabled</summary>
			/// <param name="hipaaComplianceEnabled">bool?</param>
			set
			{
				 hipaaComplianceEnabled=value;

				 keyModified["hipaa_compliance_enabled"] = 1;

			}
		}

		public string IsoCode
		{
			/// <summary>The method to get the isoCode</summary>
			/// <returns>string representing the isoCode</returns>
			get
			{
				return  isoCode;

			}
			/// <summary>The method to set the value to isoCode</summary>
			/// <param name="isoCode">string</param>
			set
			{
				 isoCode=value;

				 keyModified["iso_code"] = 1;

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