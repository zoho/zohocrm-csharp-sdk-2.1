using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.CustomViews
{

	public class Translation : Model
	{
		string publicViews;
		string otherUsersViews;
		string sharedWithMe;
		string createdByMe;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string PublicViews
		{
			/// <summary>The method to get the publicViews</summary>
			/// <returns>string representing the publicViews</returns>
			get
			{
				return  publicViews;

			}
			/// <summary>The method to set the value to publicViews</summary>
			/// <param name="publicViews">string</param>
			set
			{
				 publicViews=value;

				 keyModified["public_views"] = 1;

			}
		}

		public string OtherUsersViews
		{
			/// <summary>The method to get the otherUsersViews</summary>
			/// <returns>string representing the otherUsersViews</returns>
			get
			{
				return  otherUsersViews;

			}
			/// <summary>The method to set the value to otherUsersViews</summary>
			/// <param name="otherUsersViews">string</param>
			set
			{
				 otherUsersViews=value;

				 keyModified["other_users_views"] = 1;

			}
		}

		public string SharedWithMe
		{
			/// <summary>The method to get the sharedWithMe</summary>
			/// <returns>string representing the sharedWithMe</returns>
			get
			{
				return  sharedWithMe;

			}
			/// <summary>The method to set the value to sharedWithMe</summary>
			/// <param name="sharedWithMe">string</param>
			set
			{
				 sharedWithMe=value;

				 keyModified["shared_with_me"] = 1;

			}
		}

		public string CreatedByMe
		{
			/// <summary>The method to get the createdByMe</summary>
			/// <returns>string representing the createdByMe</returns>
			get
			{
				return  createdByMe;

			}
			/// <summary>The method to set the value to createdByMe</summary>
			/// <param name="createdByMe">string</param>
			set
			{
				 createdByMe=value;

				 keyModified["created_by_me"] = 1;

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