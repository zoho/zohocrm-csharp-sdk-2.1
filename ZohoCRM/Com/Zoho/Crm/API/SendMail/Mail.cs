using Com.Zoho.Crm.API.Attachments;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SendMail
{

	public class Mail : Model
	{
		UserAddress from;
		List<UserAddress> to;
		List<UserAddress> cc;
		List<UserAddress> bcc;
		UserAddress replyTo;
		Template template;
		int? email;
		long? id;
		string inReplyTo;
		DateTimeOffset? scheduledTime;
		string subject;
		string content;
		string paperType;
		string viewType;
		string mailFormat;
		bool? consentEmail;
		bool? orgEmail;
		List<Attachment> attachments;
		InventoryDetails inventoryDetails;
		DataSubjectRequest dataSubjectRequest;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public UserAddress From
		{
			/// <summary>The method to get the from</summary>
			/// <returns>Instance of UserAddress</returns>
			get
			{
				return  from;

			}
			/// <summary>The method to set the value to from</summary>
			/// <param name="from">Instance of UserAddress</param>
			set
			{
				 from=value;

				 keyModified["from"] = 1;

			}
		}

		public List<UserAddress> To
		{
			/// <summary>The method to get the to</summary>
			/// <returns>Instance of List<UserAddress></returns>
			get
			{
				return  to;

			}
			/// <summary>The method to set the value to to</summary>
			/// <param name="to">Instance of List<UserAddress></param>
			set
			{
				 to=value;

				 keyModified["to"] = 1;

			}
		}

		public List<UserAddress> Cc
		{
			/// <summary>The method to get the cc</summary>
			/// <returns>Instance of List<UserAddress></returns>
			get
			{
				return  cc;

			}
			/// <summary>The method to set the value to cc</summary>
			/// <param name="cc">Instance of List<UserAddress></param>
			set
			{
				 cc=value;

				 keyModified["cc"] = 1;

			}
		}

		public List<UserAddress> Bcc
		{
			/// <summary>The method to get the bcc</summary>
			/// <returns>Instance of List<UserAddress></returns>
			get
			{
				return  bcc;

			}
			/// <summary>The method to set the value to bcc</summary>
			/// <param name="bcc">Instance of List<UserAddress></param>
			set
			{
				 bcc=value;

				 keyModified["bcc"] = 1;

			}
		}

		public UserAddress ReplyTo
		{
			/// <summary>The method to get the replyTo</summary>
			/// <returns>Instance of UserAddress</returns>
			get
			{
				return  replyTo;

			}
			/// <summary>The method to set the value to replyTo</summary>
			/// <param name="replyTo">Instance of UserAddress</param>
			set
			{
				 replyTo=value;

				 keyModified["reply_to"] = 1;

			}
		}

		public Template Template
		{
			/// <summary>The method to get the template</summary>
			/// <returns>Instance of Template</returns>
			get
			{
				return  template;

			}
			/// <summary>The method to set the value to template</summary>
			/// <param name="template">Instance of Template</param>
			set
			{
				 template=value;

				 keyModified["template"] = 1;

			}
		}

		public int? Email
		{
			/// <summary>The method to get the email</summary>
			/// <returns>int? representing the email</returns>
			get
			{
				return  email;

			}
			/// <summary>The method to set the value to email</summary>
			/// <param name="email">int?</param>
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

		public string InReplyTo
		{
			/// <summary>The method to get the inReplyTo</summary>
			/// <returns>string representing the inReplyTo</returns>
			get
			{
				return  inReplyTo;

			}
			/// <summary>The method to set the value to inReplyTo</summary>
			/// <param name="inReplyTo">string</param>
			set
			{
				 inReplyTo=value;

				 keyModified["in_reply_to"] = 1;

			}
		}

		public DateTimeOffset? ScheduledTime
		{
			/// <summary>The method to get the scheduledTime</summary>
			/// <returns>DateTimeOffset? representing the scheduledTime</returns>
			get
			{
				return  scheduledTime;

			}
			/// <summary>The method to set the value to scheduledTime</summary>
			/// <param name="scheduledTime">DateTimeOffset?</param>
			set
			{
				 scheduledTime=value;

				 keyModified["scheduled_time"] = 1;

			}
		}

		public string Subject
		{
			/// <summary>The method to get the subject</summary>
			/// <returns>string representing the subject</returns>
			get
			{
				return  subject;

			}
			/// <summary>The method to set the value to subject</summary>
			/// <param name="subject">string</param>
			set
			{
				 subject=value;

				 keyModified["subject"] = 1;

			}
		}

		public string Content
		{
			/// <summary>The method to get the content</summary>
			/// <returns>string representing the content</returns>
			get
			{
				return  content;

			}
			/// <summary>The method to set the value to content</summary>
			/// <param name="content">string</param>
			set
			{
				 content=value;

				 keyModified["content"] = 1;

			}
		}

		public string PaperType
		{
			/// <summary>The method to get the paperType</summary>
			/// <returns>string representing the paperType</returns>
			get
			{
				return  paperType;

			}
			/// <summary>The method to set the value to paperType</summary>
			/// <param name="paperType">string</param>
			set
			{
				 paperType=value;

				 keyModified["paper_type"] = 1;

			}
		}

		public string ViewType
		{
			/// <summary>The method to get the viewType</summary>
			/// <returns>string representing the viewType</returns>
			get
			{
				return  viewType;

			}
			/// <summary>The method to set the value to viewType</summary>
			/// <param name="viewType">string</param>
			set
			{
				 viewType=value;

				 keyModified["view_type"] = 1;

			}
		}

		public string MailFormat
		{
			/// <summary>The method to get the mailFormat</summary>
			/// <returns>string representing the mailFormat</returns>
			get
			{
				return  mailFormat;

			}
			/// <summary>The method to set the value to mailFormat</summary>
			/// <param name="mailFormat">string</param>
			set
			{
				 mailFormat=value;

				 keyModified["mail_format"] = 1;

			}
		}

		public bool? ConsentEmail
		{
			/// <summary>The method to get the consentEmail</summary>
			/// <returns>bool? representing the consentEmail</returns>
			get
			{
				return  consentEmail;

			}
			/// <summary>The method to set the value to consentEmail</summary>
			/// <param name="consentEmail">bool?</param>
			set
			{
				 consentEmail=value;

				 keyModified["consent_email"] = 1;

			}
		}

		public bool? OrgEmail
		{
			/// <summary>The method to get the orgEmail</summary>
			/// <returns>bool? representing the orgEmail</returns>
			get
			{
				return  orgEmail;

			}
			/// <summary>The method to set the value to orgEmail</summary>
			/// <param name="orgEmail">bool?</param>
			set
			{
				 orgEmail=value;

				 keyModified["org_email"] = 1;

			}
		}

		public List<Attachment> Attachments
		{
			/// <summary>The method to get the attachments</summary>
			/// <returns>Instance of List<Attachment></returns>
			get
			{
				return  attachments;

			}
			/// <summary>The method to set the value to attachments</summary>
			/// <param name="attachments">Instance of List<Attachment></param>
			set
			{
				 attachments=value;

				 keyModified["attachments"] = 1;

			}
		}

		public InventoryDetails InventoryDetails
		{
			/// <summary>The method to get the inventoryDetails</summary>
			/// <returns>Instance of InventoryDetails</returns>
			get
			{
				return  inventoryDetails;

			}
			/// <summary>The method to set the value to inventoryDetails</summary>
			/// <param name="inventoryDetails">Instance of InventoryDetails</param>
			set
			{
				 inventoryDetails=value;

				 keyModified["inventory_details"] = 1;

			}
		}

		public DataSubjectRequest DataSubjectRequest
		{
			/// <summary>The method to get the dataSubjectRequest</summary>
			/// <returns>Instance of DataSubjectRequest</returns>
			get
			{
				return  dataSubjectRequest;

			}
			/// <summary>The method to set the value to dataSubjectRequest</summary>
			/// <param name="dataSubjectRequest">Instance of DataSubjectRequest</param>
			set
			{
				 dataSubjectRequest=value;

				 keyModified["data_subject_request"] = 1;

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