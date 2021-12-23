using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class Record : Model
	{
		protected Dictionary<string, object> keyValues=new Dictionary<string, object>();
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				if((( GetKeyValue("id")) != (null)))
				{
					return (long?) GetKeyValue("id");

				}
					return null;


			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 AddKeyValue("id", value);

			}
		}

		public User CreatedBy
		{
			/// <summary>The method to get the createdBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				if((( GetKeyValue("Created_By")) != (null)))
				{
					return (User) GetKeyValue("Created_By");

				}
					return null;


			}
			/// <summary>The method to set the value to createdBy</summary>
			/// <param name="createdBy">Instance of User</param>
			set
			{
				 AddKeyValue("Created_By", value);

			}
		}

		public DateTimeOffset? CreatedTime
		{
			/// <summary>The method to get the createdTime</summary>
			/// <returns>DateTimeOffset? representing the createdTime</returns>
			get
			{
				if((( GetKeyValue("Created_Time")) != (null)))
				{
					return (DateTimeOffset?) GetKeyValue("Created_Time");

				}
					return null;


			}
			/// <summary>The method to set the value to createdTime</summary>
			/// <param name="createdTime">DateTimeOffset?</param>
			set
			{
				 AddKeyValue("Created_Time", value);

			}
		}

		public User ModifiedBy
		{
			/// <summary>The method to get the modifiedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				if((( GetKeyValue("Modified_By")) != (null)))
				{
					return (User) GetKeyValue("Modified_By");

				}
					return null;


			}
			/// <summary>The method to set the value to modifiedBy</summary>
			/// <param name="modifiedBy">Instance of User</param>
			set
			{
				 AddKeyValue("Modified_By", value);

			}
		}

		public DateTimeOffset? ModifiedTime
		{
			/// <summary>The method to get the modifiedTime</summary>
			/// <returns>DateTimeOffset? representing the modifiedTime</returns>
			get
			{
				if((( GetKeyValue("Modified_Time")) != (null)))
				{
					return (DateTimeOffset?) GetKeyValue("Modified_Time");

				}
					return null;


			}
			/// <summary>The method to set the value to modifiedTime</summary>
			/// <param name="modifiedTime">DateTimeOffset?</param>
			set
			{
				 AddKeyValue("Modified_Time", value);

			}
		}

		public List<Tag> Tag
		{
			/// <summary>The method to get the tag</summary>
			/// <returns>Instance of List<Tag></returns>
			get
			{
				if((( GetKeyValue("Tag")) != (null)))
				{
					return (List<Tag>) GetKeyValue("Tag");

				}
					return null;


			}
			/// <summary>The method to set the value to tag</summary>
			/// <param name="tag">Instance of List<Tag></param>
			set
			{
				 AddKeyValue("Tag", value);

			}
		}

		/// <summary>The method to add field value</summary>
		/// <typeparam name="T">T containing the specified type</typeparam>
		/// <param name="field">Instance of Field<T></param>
		/// <param name="value">T</param>
		public void AddFieldValue <T>(Field<T> field, T value)
		{
			 AddKeyValue(field.APIName, value);


		}

		/// <summary>The method to add key value</summary>
		/// <param name="apiName">string</param>
		/// <param name="value">object</param>
		public void AddKeyValue(string apiName, object value)
		{
			 keyValues[apiName] = value;

			 keyModified[apiName] = 1;


		}

		/// <summary>The method to get key value</summary>
		/// <param name="apiName">string</param>
		/// <returns>object representing the keyValue</returns>
		public object GetKeyValue(string apiName)
		{
			if((( keyValues.ContainsKey(apiName))))
			{
				return  keyValues[apiName];

			}
			return null;


		}

		/// <summary>The method to get key values</summary>
		/// <returns>Dictionary representing the keyValues<String,Object></returns>
		public Dictionary<string, object> GetKeyValues()
		{
			return  keyValues;


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