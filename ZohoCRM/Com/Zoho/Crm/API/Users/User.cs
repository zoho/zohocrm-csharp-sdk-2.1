using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Roles;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Users
{

	public class User : Record.Record , Model
	{

		public string Country
		{
			/// <summary>The method to get the country</summary>
			/// <returns>string representing the country</returns>
			get
			{
				if((( GetKeyValue("country")) != (null)))
				{
					return (string) GetKeyValue("country");

				}
					return null;


			}
			/// <summary>The method to set the value to country</summary>
			/// <param name="country">string</param>
			set
			{
				 AddKeyValue("country", value);

			}
		}

		public CustomizeInfo CustomizeInfo
		{
			/// <summary>The method to get the customizeInfo</summary>
			/// <returns>Instance of CustomizeInfo</returns>
			get
			{
				if((( GetKeyValue("customize_info")) != (null)))
				{
					return (CustomizeInfo) GetKeyValue("customize_info");

				}
					return null;


			}
			/// <summary>The method to set the value to customizeInfo</summary>
			/// <param name="customizeInfo">Instance of CustomizeInfo</param>
			set
			{
				 AddKeyValue("customize_info", value);

			}
		}

		public Role Role
		{
			/// <summary>The method to get the role</summary>
			/// <returns>Instance of Role</returns>
			get
			{
				if((( GetKeyValue("role")) != (null)))
				{
					return (Role) GetKeyValue("role");

				}
					return null;


			}
			/// <summary>The method to set the value to role</summary>
			/// <param name="role">Instance of Role</param>
			set
			{
				 AddKeyValue("role", value);

			}
		}

		public string Signature
		{
			/// <summary>The method to get the signature</summary>
			/// <returns>string representing the signature</returns>
			get
			{
				if((( GetKeyValue("signature")) != (null)))
				{
					return (string) GetKeyValue("signature");

				}
					return null;


			}
			/// <summary>The method to set the value to signature</summary>
			/// <param name="signature">string</param>
			set
			{
				 AddKeyValue("signature", value);

			}
		}

		public string SortOrderPreference
		{
			/// <summary>The method to get the sortOrderPreference</summary>
			/// <returns>string representing the sortOrderPreference</returns>
			get
			{
				if((( GetKeyValue("sort_order_preference")) != (null)))
				{
					return (string) GetKeyValue("sort_order_preference");

				}
					return null;


			}
			/// <summary>The method to set the value to sortOrderPreference</summary>
			/// <param name="sortOrderPreference">string</param>
			set
			{
				 AddKeyValue("sort_order_preference", value);

			}
		}

		public string City
		{
			/// <summary>The method to get the city</summary>
			/// <returns>string representing the city</returns>
			get
			{
				if((( GetKeyValue("city")) != (null)))
				{
					return (string) GetKeyValue("city");

				}
					return null;


			}
			/// <summary>The method to set the value to city</summary>
			/// <param name="city">string</param>
			set
			{
				 AddKeyValue("city", value);

			}
		}

		public string NameFormat
		{
			/// <summary>The method to get the nameFormat</summary>
			/// <returns>string representing the nameFormat</returns>
			get
			{
				if((( GetKeyValue("name_format")) != (null)))
				{
					return (string) GetKeyValue("name_format");

				}
					return null;


			}
			/// <summary>The method to set the value to nameFormat</summary>
			/// <param name="nameFormat">string</param>
			set
			{
				 AddKeyValue("name_format", value);

			}
		}

		public bool? PersonalAccount
		{
			/// <summary>The method to get the personalAccount</summary>
			/// <returns>bool? representing the personalAccount</returns>
			get
			{
				if((( GetKeyValue("personal_account")) != (null)))
				{
					return (bool?) GetKeyValue("personal_account");

				}
					return null;


			}
			/// <summary>The method to set the value to personalAccount</summary>
			/// <param name="personalAccount">bool?</param>
			set
			{
				 AddKeyValue("personal_account", value);

			}
		}

		public string DefaultTabGroup
		{
			/// <summary>The method to get the defaultTabGroup</summary>
			/// <returns>string representing the defaultTabGroup</returns>
			get
			{
				if((( GetKeyValue("default_tab_group")) != (null)))
				{
					return (string) GetKeyValue("default_tab_group");

				}
					return null;


			}
			/// <summary>The method to set the value to defaultTabGroup</summary>
			/// <param name="defaultTabGroup">string</param>
			set
			{
				 AddKeyValue("default_tab_group", value);

			}
		}

		public string Language
		{
			/// <summary>The method to get the language</summary>
			/// <returns>string representing the language</returns>
			get
			{
				if((( GetKeyValue("language")) != (null)))
				{
					return (string) GetKeyValue("language");

				}
					return null;


			}
			/// <summary>The method to set the value to language</summary>
			/// <param name="language">string</param>
			set
			{
				 AddKeyValue("language", value);

			}
		}

		public string Locale
		{
			/// <summary>The method to get the locale</summary>
			/// <returns>string representing the locale</returns>
			get
			{
				if((( GetKeyValue("locale")) != (null)))
				{
					return (string) GetKeyValue("locale");

				}
					return null;


			}
			/// <summary>The method to set the value to locale</summary>
			/// <param name="locale">string</param>
			set
			{
				 AddKeyValue("locale", value);

			}
		}

		public bool? Microsoft
		{
			/// <summary>The method to get the microsoft</summary>
			/// <returns>bool? representing the microsoft</returns>
			get
			{
				if((( GetKeyValue("microsoft")) != (null)))
				{
					return (bool?) GetKeyValue("microsoft");

				}
					return null;


			}
			/// <summary>The method to set the value to microsoft</summary>
			/// <param name="microsoft">bool?</param>
			set
			{
				 AddKeyValue("microsoft", value);

			}
		}

		public bool? Isonline
		{
			/// <summary>The method to get the isonline</summary>
			/// <returns>bool? representing the isonline</returns>
			get
			{
				if((( GetKeyValue("Isonline")) != (null)))
				{
					return (bool?) GetKeyValue("Isonline");

				}
					return null;


			}
			/// <summary>The method to set the value to isonline</summary>
			/// <param name="isonline">bool?</param>
			set
			{
				 AddKeyValue("Isonline", value);

			}
		}

		public string Street
		{
			/// <summary>The method to get the street</summary>
			/// <returns>string representing the street</returns>
			get
			{
				if((( GetKeyValue("street")) != (null)))
				{
					return (string) GetKeyValue("street");

				}
					return null;


			}
			/// <summary>The method to set the value to street</summary>
			/// <param name="street">string</param>
			set
			{
				 AddKeyValue("street", value);

			}
		}

		public string Currency
		{
			/// <summary>The method to get the currency</summary>
			/// <returns>string representing the currency</returns>
			get
			{
				if((( GetKeyValue("Currency")) != (null)))
				{
					return (string) GetKeyValue("Currency");

				}
					return null;


			}
			/// <summary>The method to set the value to currency</summary>
			/// <param name="currency">string</param>
			set
			{
				 AddKeyValue("Currency", value);

			}
		}

		public string Alias
		{
			/// <summary>The method to get the alias</summary>
			/// <returns>string representing the alias</returns>
			get
			{
				if((( GetKeyValue("alias")) != (null)))
				{
					return (string) GetKeyValue("alias");

				}
					return null;


			}
			/// <summary>The method to set the value to alias</summary>
			/// <param name="alias">string</param>
			set
			{
				 AddKeyValue("alias", value);

			}
		}

		public Theme Theme
		{
			/// <summary>The method to get the theme</summary>
			/// <returns>Instance of Theme</returns>
			get
			{
				if((( GetKeyValue("theme")) != (null)))
				{
					return (Theme) GetKeyValue("theme");

				}
					return null;


			}
			/// <summary>The method to set the value to theme</summary>
			/// <param name="theme">Instance of Theme</param>
			set
			{
				 AddKeyValue("theme", value);

			}
		}

		public string State
		{
			/// <summary>The method to get the state</summary>
			/// <returns>string representing the state</returns>
			get
			{
				if((( GetKeyValue("state")) != (null)))
				{
					return (string) GetKeyValue("state");

				}
					return null;


			}
			/// <summary>The method to set the value to state</summary>
			/// <param name="state">string</param>
			set
			{
				 AddKeyValue("state", value);

			}
		}

		public string Fax
		{
			/// <summary>The method to get the fax</summary>
			/// <returns>string representing the fax</returns>
			get
			{
				if((( GetKeyValue("fax")) != (null)))
				{
					return (string) GetKeyValue("fax");

				}
					return null;


			}
			/// <summary>The method to set the value to fax</summary>
			/// <param name="fax">string</param>
			set
			{
				 AddKeyValue("fax", value);

			}
		}

		public string CountryLocale
		{
			/// <summary>The method to get the countryLocale</summary>
			/// <returns>string representing the countryLocale</returns>
			get
			{
				if((( GetKeyValue("country_locale")) != (null)))
				{
					return (string) GetKeyValue("country_locale");

				}
					return null;


			}
			/// <summary>The method to set the value to countryLocale</summary>
			/// <param name="countryLocale">string</param>
			set
			{
				 AddKeyValue("country_locale", value);

			}
		}

		public bool? Sandboxdeveloper
		{
			/// <summary>The method to get the sandboxdeveloper</summary>
			/// <returns>bool? representing the sandboxdeveloper</returns>
			get
			{
				if((( GetKeyValue("sandboxDeveloper")) != (null)))
				{
					return (bool?) GetKeyValue("sandboxDeveloper");

				}
					return null;


			}
			/// <summary>The method to set the value to sandboxdeveloper</summary>
			/// <param name="sandboxdeveloper">bool?</param>
			set
			{
				 AddKeyValue("sandboxDeveloper", value);

			}
		}

		public string FirstName
		{
			/// <summary>The method to get the firstName</summary>
			/// <returns>string representing the firstName</returns>
			get
			{
				if((( GetKeyValue("first_name")) != (null)))
				{
					return (string) GetKeyValue("first_name");

				}
					return null;


			}
			/// <summary>The method to set the value to firstName</summary>
			/// <param name="firstName">string</param>
			set
			{
				 AddKeyValue("first_name", value);

			}
		}

		public string Email
		{
			/// <summary>The method to get the email</summary>
			/// <returns>string representing the email</returns>
			get
			{
				if((( GetKeyValue("email")) != (null)))
				{
					return (string) GetKeyValue("email");

				}
					return null;


			}
			/// <summary>The method to set the value to email</summary>
			/// <param name="email">string</param>
			set
			{
				 AddKeyValue("email", value);

			}
		}

		public User ReportingTo
		{
			/// <summary>The method to get the reportingTo</summary>
			/// <returns>Instance of User</returns>
			get
			{
				if((( GetKeyValue("Reporting_To")) != (null)))
				{
					return (User) GetKeyValue("Reporting_To");

				}
					return null;


			}
			/// <summary>The method to set the value to reportingTo</summary>
			/// <param name="reportingTo">Instance of User</param>
			set
			{
				 AddKeyValue("Reporting_To", value);

			}
		}

		public string DecimalSeparator
		{
			/// <summary>The method to get the decimalSeparator</summary>
			/// <returns>string representing the decimalSeparator</returns>
			get
			{
				if((( GetKeyValue("decimal_separator")) != (null)))
				{
					return (string) GetKeyValue("decimal_separator");

				}
					return null;


			}
			/// <summary>The method to set the value to decimalSeparator</summary>
			/// <param name="decimalSeparator">string</param>
			set
			{
				 AddKeyValue("decimal_separator", value);

			}
		}

		public string Zip
		{
			/// <summary>The method to get the zip</summary>
			/// <returns>string representing the zip</returns>
			get
			{
				if((( GetKeyValue("zip")) != (null)))
				{
					return (string) GetKeyValue("zip");

				}
					return null;


			}
			/// <summary>The method to set the value to zip</summary>
			/// <param name="zip">string</param>
			set
			{
				 AddKeyValue("zip", value);

			}
		}

		public string Website
		{
			/// <summary>The method to get the website</summary>
			/// <returns>string representing the website</returns>
			get
			{
				if((( GetKeyValue("website")) != (null)))
				{
					return (string) GetKeyValue("website");

				}
					return null;


			}
			/// <summary>The method to set the value to website</summary>
			/// <param name="website">string</param>
			set
			{
				 AddKeyValue("website", value);

			}
		}

		public string TimeFormat
		{
			/// <summary>The method to get the timeFormat</summary>
			/// <returns>string representing the timeFormat</returns>
			get
			{
				if((( GetKeyValue("time_format")) != (null)))
				{
					return (string) GetKeyValue("time_format");

				}
					return null;


			}
			/// <summary>The method to set the value to timeFormat</summary>
			/// <param name="timeFormat">string</param>
			set
			{
				 AddKeyValue("time_format", value);

			}
		}

		public long? Offset
		{
			/// <summary>The method to get the offset</summary>
			/// <returns>long? representing the offset</returns>
			get
			{
				if((( GetKeyValue("offset")) != (null)))
				{
					return (long?) GetKeyValue("offset");

				}
					return null;


			}
			/// <summary>The method to set the value to offset</summary>
			/// <param name="offset">long?</param>
			set
			{
				 AddKeyValue("offset", value);

			}
		}

		public Profile Profile
		{
			/// <summary>The method to get the profile</summary>
			/// <returns>Instance of Profile</returns>
			get
			{
				if((( GetKeyValue("profile")) != (null)))
				{
					return (Profile) GetKeyValue("profile");

				}
					return null;


			}
			/// <summary>The method to set the value to profile</summary>
			/// <param name="profile">Instance of Profile</param>
			set
			{
				 AddKeyValue("profile", value);

			}
		}

		public string Mobile
		{
			/// <summary>The method to get the mobile</summary>
			/// <returns>string representing the mobile</returns>
			get
			{
				if((( GetKeyValue("mobile")) != (null)))
				{
					return (string) GetKeyValue("mobile");

				}
					return null;


			}
			/// <summary>The method to set the value to mobile</summary>
			/// <param name="mobile">string</param>
			set
			{
				 AddKeyValue("mobile", value);

			}
		}

		public string LastName
		{
			/// <summary>The method to get the lastName</summary>
			/// <returns>string representing the lastName</returns>
			get
			{
				if((( GetKeyValue("last_name")) != (null)))
				{
					return (string) GetKeyValue("last_name");

				}
					return null;


			}
			/// <summary>The method to set the value to lastName</summary>
			/// <param name="lastName">string</param>
			set
			{
				 AddKeyValue("last_name", value);

			}
		}

		public string TimeZone
		{
			/// <summary>The method to get the timeZone</summary>
			/// <returns>string representing the timeZone</returns>
			get
			{
				if((( GetKeyValue("time_zone")) != (null)))
				{
					return (string) GetKeyValue("time_zone");

				}
					return null;


			}
			/// <summary>The method to set the value to timeZone</summary>
			/// <param name="timeZone">string</param>
			set
			{
				 AddKeyValue("time_zone", value);

			}
		}

		public string Zuid
		{
			/// <summary>The method to get the zuid</summary>
			/// <returns>string representing the zuid</returns>
			get
			{
				if((( GetKeyValue("zuid")) != (null)))
				{
					return (string) GetKeyValue("zuid");

				}
					return null;


			}
			/// <summary>The method to set the value to zuid</summary>
			/// <param name="zuid">string</param>
			set
			{
				 AddKeyValue("zuid", value);

			}
		}

		public bool? Confirm
		{
			/// <summary>The method to get the confirm</summary>
			/// <returns>bool? representing the confirm</returns>
			get
			{
				if((( GetKeyValue("confirm")) != (null)))
				{
					return (bool?) GetKeyValue("confirm");

				}
					return null;


			}
			/// <summary>The method to set the value to confirm</summary>
			/// <param name="confirm">bool?</param>
			set
			{
				 AddKeyValue("confirm", value);

			}
		}

		public string FullName
		{
			/// <summary>The method to get the fullName</summary>
			/// <returns>string representing the fullName</returns>
			get
			{
				if((( GetKeyValue("full_name")) != (null)))
				{
					return (string) GetKeyValue("full_name");

				}
					return null;


			}
			/// <summary>The method to set the value to fullName</summary>
			/// <param name="fullName">string</param>
			set
			{
				 AddKeyValue("full_name", value);

			}
		}

		public List<Territory> Territories
		{
			/// <summary>The method to get the territories</summary>
			/// <returns>Instance of List<Territory></returns>
			get
			{
				if((( GetKeyValue("territories")) != (null)))
				{
					return (List<Territory>) GetKeyValue("territories");

				}
					return null;


			}
			/// <summary>The method to set the value to territories</summary>
			/// <param name="territories">Instance of List<Territory></param>
			set
			{
				 AddKeyValue("territories", value);

			}
		}

		public string Phone
		{
			/// <summary>The method to get the phone</summary>
			/// <returns>string representing the phone</returns>
			get
			{
				if((( GetKeyValue("phone")) != (null)))
				{
					return (string) GetKeyValue("phone");

				}
					return null;


			}
			/// <summary>The method to set the value to phone</summary>
			/// <param name="phone">string</param>
			set
			{
				 AddKeyValue("phone", value);

			}
		}

		public string Dob
		{
			/// <summary>The method to get the dob</summary>
			/// <returns>string representing the dob</returns>
			get
			{
				if((( GetKeyValue("dob")) != (null)))
				{
					return (string) GetKeyValue("dob");

				}
					return null;


			}
			/// <summary>The method to set the value to dob</summary>
			/// <param name="dob">string</param>
			set
			{
				 AddKeyValue("dob", value);

			}
		}

		public string DateFormat
		{
			/// <summary>The method to get the dateFormat</summary>
			/// <returns>string representing the dateFormat</returns>
			get
			{
				if((( GetKeyValue("date_format")) != (null)))
				{
					return (string) GetKeyValue("date_format");

				}
					return null;


			}
			/// <summary>The method to set the value to dateFormat</summary>
			/// <param name="dateFormat">string</param>
			set
			{
				 AddKeyValue("date_format", value);

			}
		}

		public string Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>string representing the status</returns>
			get
			{
				if((( GetKeyValue("status")) != (null)))
				{
					return (string) GetKeyValue("status");

				}
					return null;


			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">string</param>
			set
			{
				 AddKeyValue("status", value);

			}
		}

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				if((( GetKeyValue("name")) != (null)))
				{
					return (string) GetKeyValue("name");

				}
					return null;


			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 AddKeyValue("name", value);

			}
		}

		public string Category
		{
			/// <summary>The method to get the category</summary>
			/// <returns>string representing the category</returns>
			get
			{
				if((( GetKeyValue("category")) != (null)))
				{
					return (string) GetKeyValue("category");

				}
					return null;


			}
			/// <summary>The method to set the value to category</summary>
			/// <param name="category">string</param>
			set
			{
				 AddKeyValue("category", value);

			}
		}


	}
}