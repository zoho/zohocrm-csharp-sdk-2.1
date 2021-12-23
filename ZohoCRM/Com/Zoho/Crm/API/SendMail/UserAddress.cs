using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SendMail
{

	public class UserAddress : Model
	{
		string userName;
		string type;
		string email;
		long? id;
		bool? default1;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string UserName
		{
			/// <summary>The method to get the userName</summary>
			/// <returns>string representing the userName</returns>
			get
			{
				return  userName;

			}
			/// <summary>The method to set the value to userName</summary>
			/// <param name="userName">string</param>
			set
			{
				 userName=value;

				 keyModified["user_name"] = 1;

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

		public string Email
		{
			/// <summary>The method to get the email</summary>
			/// <returns>string representing the email</returns>
			get
			{
				return  email;

			}
			/// <summary>The method to set the value to email</summary>
			/// <param name="email">string</param>
			set
			{
				 email=value;

				 keyModified["email"] = 1;

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

		public bool? Default
		{
			/// <summary>The method to get the default</summary>
			/// <returns>bool? representing the default1</returns>
			get
			{
				return  default1;

			}
			/// <summary>The method to set the value to default</summary>
			/// <param name="default1">bool?</param>
			set
			{
				 default1=value;

				 keyModified["default"] = 1;

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