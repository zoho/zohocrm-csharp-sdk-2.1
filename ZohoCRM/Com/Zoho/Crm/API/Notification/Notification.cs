using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Notification
{

	public class Notification : Model
	{
		DateTimeOffset? channelExpiry;
		string resourceUri;
		string resourceId;
		string notifyUrl;
		string resourceName;
		long? channelId;
		List<string> events;
		string token;
		bool? notifyOnRelatedAction;
		Dictionary<string, object> fields;
		bool? deleteevents;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public DateTimeOffset? ChannelExpiry
		{
			/// <summary>The method to get the channelExpiry</summary>
			/// <returns>DateTimeOffset? representing the channelExpiry</returns>
			get
			{
				return  channelExpiry;

			}
			/// <summary>The method to set the value to channelExpiry</summary>
			/// <param name="channelExpiry">DateTimeOffset?</param>
			set
			{
				 channelExpiry=value;

				 keyModified["channel_expiry"] = 1;

			}
		}

		public string ResourceUri
		{
			/// <summary>The method to get the resourceUri</summary>
			/// <returns>string representing the resourceUri</returns>
			get
			{
				return  resourceUri;

			}
			/// <summary>The method to set the value to resourceUri</summary>
			/// <param name="resourceUri">string</param>
			set
			{
				 resourceUri=value;

				 keyModified["resource_uri"] = 1;

			}
		}

		public string ResourceId
		{
			/// <summary>The method to get the resourceId</summary>
			/// <returns>string representing the resourceId</returns>
			get
			{
				return  resourceId;

			}
			/// <summary>The method to set the value to resourceId</summary>
			/// <param name="resourceId">string</param>
			set
			{
				 resourceId=value;

				 keyModified["resource_id"] = 1;

			}
		}

		public string NotifyUrl
		{
			/// <summary>The method to get the notifyUrl</summary>
			/// <returns>string representing the notifyUrl</returns>
			get
			{
				return  notifyUrl;

			}
			/// <summary>The method to set the value to notifyUrl</summary>
			/// <param name="notifyUrl">string</param>
			set
			{
				 notifyUrl=value;

				 keyModified["notify_url"] = 1;

			}
		}

		public string ResourceName
		{
			/// <summary>The method to get the resourceName</summary>
			/// <returns>string representing the resourceName</returns>
			get
			{
				return  resourceName;

			}
			/// <summary>The method to set the value to resourceName</summary>
			/// <param name="resourceName">string</param>
			set
			{
				 resourceName=value;

				 keyModified["resource_name"] = 1;

			}
		}

		public long? ChannelId
		{
			/// <summary>The method to get the channelId</summary>
			/// <returns>long? representing the channelId</returns>
			get
			{
				return  channelId;

			}
			/// <summary>The method to set the value to channelId</summary>
			/// <param name="channelId">long?</param>
			set
			{
				 channelId=value;

				 keyModified["channel_id"] = 1;

			}
		}

		public List<string> Events
		{
			/// <summary>The method to get the events</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  events;

			}
			/// <summary>The method to set the value to events</summary>
			/// <param name="events">Instance of List<string></param>
			set
			{
				 events=value;

				 keyModified["events"] = 1;

			}
		}

		public string Token
		{
			/// <summary>The method to get the token</summary>
			/// <returns>string representing the token</returns>
			get
			{
				return  token;

			}
			/// <summary>The method to set the value to token</summary>
			/// <param name="token">string</param>
			set
			{
				 token=value;

				 keyModified["token"] = 1;

			}
		}

		public bool? NotifyOnRelatedAction
		{
			/// <summary>The method to get the notifyOnRelatedAction</summary>
			/// <returns>bool? representing the notifyOnRelatedAction</returns>
			get
			{
				return  notifyOnRelatedAction;

			}
			/// <summary>The method to set the value to notifyOnRelatedAction</summary>
			/// <param name="notifyOnRelatedAction">bool?</param>
			set
			{
				 notifyOnRelatedAction=value;

				 keyModified["notify_on_related_action"] = 1;

			}
		}

		public Dictionary<string, object> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Dictionary representing the fields<String,Object></returns>
			get
			{
				return  fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Dictionary<string,object></param>
			set
			{
				 fields=value;

				 keyModified["fields"] = 1;

			}
		}

		public bool? Deleteevents
		{
			/// <summary>The method to get the deleteevents</summary>
			/// <returns>bool? representing the deleteevents</returns>
			get
			{
				return  deleteevents;

			}
			/// <summary>The method to set the value to deleteevents</summary>
			/// <param name="deleteevents">bool?</param>
			set
			{
				 deleteevents=value;

				 keyModified["_delete_events"] = 1;

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