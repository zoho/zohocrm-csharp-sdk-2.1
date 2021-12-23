using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.Record
{

	public class Participants : Record , Model
	{

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

		public string Email
		{
			/// <summary>The method to get the email</summary>
			/// <returns>string representing the email</returns>
			get
			{
				if((( GetKeyValue("Email")) != (null)))
				{
					return (string) GetKeyValue("Email");

				}
					return null;


			}
			/// <summary>The method to set the value to email</summary>
			/// <param name="email">string</param>
			set
			{
				 AddKeyValue("Email", value);

			}
		}

		public bool? Invited
		{
			/// <summary>The method to get the invited</summary>
			/// <returns>bool? representing the invited</returns>
			get
			{
				if((( GetKeyValue("invited")) != (null)))
				{
					return (bool?) GetKeyValue("invited");

				}
					return null;


			}
			/// <summary>The method to set the value to invited</summary>
			/// <param name="invited">bool?</param>
			set
			{
				 AddKeyValue("invited", value);

			}
		}

		public string Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>string representing the type</returns>
			get
			{
				if((( GetKeyValue("type")) != (null)))
				{
					return (string) GetKeyValue("type");

				}
					return null;


			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">string</param>
			set
			{
				 AddKeyValue("type", value);

			}
		}

		public string Participant
		{
			/// <summary>The method to get the participant</summary>
			/// <returns>string representing the participant</returns>
			get
			{
				if((( GetKeyValue("participant")) != (null)))
				{
					return (string) GetKeyValue("participant");

				}
					return null;


			}
			/// <summary>The method to set the value to participant</summary>
			/// <param name="participant">string</param>
			set
			{
				 AddKeyValue("participant", value);

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


	}
}