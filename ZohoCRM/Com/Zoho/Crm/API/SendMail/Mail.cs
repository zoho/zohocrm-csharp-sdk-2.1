using Com.Zoho.Crm.API.Attachments;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SendMail
{

	public class Mail : Model
	{
		private UserAddress from;
		private List<UserAddress> to;
		private List<UserAddress> cc;
		private List<UserAddress> bcc;
		private UserAddress replyTo;
		private Template template;
		private int? email;
		private long? id;
		private string inReplyTo;
		private DateTimeOffset? scheduledTime;
		private string subject;
		private string content;
		private string paperType;
		private string viewType;
		private string mailFormat;
		private bool? consentEmail;
		private bool? orgEmail;
		private List<Attachment> attachments;
		private InventoryDetails inventoryDetails;
		private DataSubjectRequest dataSubjectRequest;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public UserAddress From
		{
			/// <summary>The method to get the from</summary>
			/// <returns>Instance of UserAddress</returns>
			get
			{
				return  this.from;

			}
			/// <summary>The method to set the value to from</summary>
			/// <param name="from">Instance of UserAddress</param>
			set
			{
				 this.from=value;

				 this.keyModified["from"] = 1;

			}
		}

		public List<UserAddress> To
		{
			/// <summary>The method to get the to</summary>
			/// <returns>Instance of List<UserAddress></returns>
			get
			{
				return  this.to;

			}
			/// <summary>The method to set the value to to</summary>
			/// <param name="to">Instance of List<UserAddress></param>
			set
			{
				 this.to=value;

				 this.keyModified["to"] = 1;

			}
		}

		public List<UserAddress> Cc
		{
			/// <summary>The method to get the cc</summary>
			/// <returns>Instance of List<UserAddress></returns>
			get
			{
				return  this.cc;

			}
			/// <summary>The method to set the value to cc</summary>
			/// <param name="cc">Instance of List<UserAddress></param>
			set
			{
				 this.cc=value;

				 this.keyModified["cc"] = 1;

			}
		}

		public List<UserAddress> Bcc
		{
			/// <summary>The method to get the bcc</summary>
			/// <returns>Instance of List<UserAddress></returns>
			get
			{
				return  this.bcc;

			}
			/// <summary>The method to set the value to bcc</summary>
			/// <param name="bcc">Instance of List<UserAddress></param>
			set
			{
				 this.bcc=value;

				 this.keyModified["bcc"] = 1;

			}
		}

		public UserAddress ReplyTo
		{
			/// <summary>The method to get the replyTo</summary>
			/// <returns>Instance of UserAddress</returns>
			get
			{
				return  this.replyTo;

			}
			/// <summary>The method to set the value to replyTo</summary>
			/// <param name="replyTo">Instance of UserAddress</param>
			set
			{
				 this.replyTo=value;

				 this.keyModified["reply_to"] = 1;

			}
		}

		public Template Template
		{
			/// <summary>The method to get the template</summary>
			/// <returns>Instance of Template</returns>
			get
			{
				return  this.template;

			}
			/// <summary>The method to set the value to template</summary>
			/// <param name="template">Instance of Template</param>
			set
			{
				 this.template=value;

				 this.keyModified["template"] = 1;

			}
		}

		public int? Email
		{
			/// <summary>The method to get the email</summary>
			/// <returns>int? representing the email</returns>
			get
			{
				return  this.email;

			}
			/// <summary>The method to set the value to email</summary>
			/// <param name="email">int?</param>
			set
			{
				 this.email=value;

				 this.keyModified["email"] = 1;

			}
		}

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		public string InReplyTo
		{
			/// <summary>The method to get the inReplyTo</summary>
			/// <returns>string representing the inReplyTo</returns>
			get
			{
				return  this.inReplyTo;

			}
			/// <summary>The method to set the value to inReplyTo</summary>
			/// <param name="inReplyTo">string</param>
			set
			{
				 this.inReplyTo=value;

				 this.keyModified["in_reply_to"] = 1;

			}
		}

		public DateTimeOffset? ScheduledTime
		{
			/// <summary>The method to get the scheduledTime</summary>
			/// <returns>DateTimeOffset? representing the scheduledTime</returns>
			get
			{
				return  this.scheduledTime;

			}
			/// <summary>The method to set the value to scheduledTime</summary>
			/// <param name="scheduledTime">DateTimeOffset?</param>
			set
			{
				 this.scheduledTime=value;

				 this.keyModified["scheduled_time"] = 1;

			}
		}

		public string Subject
		{
			/// <summary>The method to get the subject</summary>
			/// <returns>string representing the subject</returns>
			get
			{
				return  this.subject;

			}
			/// <summary>The method to set the value to subject</summary>
			/// <param name="subject">string</param>
			set
			{
				 this.subject=value;

				 this.keyModified["subject"] = 1;

			}
		}

		public string Content
		{
			/// <summary>The method to get the content</summary>
			/// <returns>string representing the content</returns>
			get
			{
				return  this.content;

			}
			/// <summary>The method to set the value to content</summary>
			/// <param name="content">string</param>
			set
			{
				 this.content=value;

				 this.keyModified["content"] = 1;

			}
		}

		public string PaperType
		{
			/// <summary>The method to get the paperType</summary>
			/// <returns>string representing the paperType</returns>
			get
			{
				return  this.paperType;

			}
			/// <summary>The method to set the value to paperType</summary>
			/// <param name="paperType">string</param>
			set
			{
				 this.paperType=value;

				 this.keyModified["paper_type"] = 1;

			}
		}

		public string ViewType
		{
			/// <summary>The method to get the viewType</summary>
			/// <returns>string representing the viewType</returns>
			get
			{
				return  this.viewType;

			}
			/// <summary>The method to set the value to viewType</summary>
			/// <param name="viewType">string</param>
			set
			{
				 this.viewType=value;

				 this.keyModified["view_type"] = 1;

			}
		}

		public string MailFormat
		{
			/// <summary>The method to get the mailFormat</summary>
			/// <returns>string representing the mailFormat</returns>
			get
			{
				return  this.mailFormat;

			}
			/// <summary>The method to set the value to mailFormat</summary>
			/// <param name="mailFormat">string</param>
			set
			{
				 this.mailFormat=value;

				 this.keyModified["mail_format"] = 1;

			}
		}

		public bool? ConsentEmail
		{
			/// <summary>The method to get the consentEmail</summary>
			/// <returns>bool? representing the consentEmail</returns>
			get
			{
				return  this.consentEmail;

			}
			/// <summary>The method to set the value to consentEmail</summary>
			/// <param name="consentEmail">bool?</param>
			set
			{
				 this.consentEmail=value;

				 this.keyModified["consent_email"] = 1;

			}
		}

		public bool? OrgEmail
		{
			/// <summary>The method to get the orgEmail</summary>
			/// <returns>bool? representing the orgEmail</returns>
			get
			{
				return  this.orgEmail;

			}
			/// <summary>The method to set the value to orgEmail</summary>
			/// <param name="orgEmail">bool?</param>
			set
			{
				 this.orgEmail=value;

				 this.keyModified["org_email"] = 1;

			}
		}

		public List<Attachment> Attachments
		{
			/// <summary>The method to get the attachments</summary>
			/// <returns>Instance of List<Attachment></returns>
			get
			{
				return  this.attachments;

			}
			/// <summary>The method to set the value to attachments</summary>
			/// <param name="attachments">Instance of List<Attachment></param>
			set
			{
				 this.attachments=value;

				 this.keyModified["attachments"] = 1;

			}
		}

		public InventoryDetails InventoryDetails
		{
			/// <summary>The method to get the inventoryDetails</summary>
			/// <returns>Instance of InventoryDetails</returns>
			get
			{
				return  this.inventoryDetails;

			}
			/// <summary>The method to set the value to inventoryDetails</summary>
			/// <param name="inventoryDetails">Instance of InventoryDetails</param>
			set
			{
				 this.inventoryDetails=value;

				 this.keyModified["inventory_details"] = 1;

			}
		}

		public DataSubjectRequest DataSubjectRequest
		{
			/// <summary>The method to get the dataSubjectRequest</summary>
			/// <returns>Instance of DataSubjectRequest</returns>
			get
			{
				return  this.dataSubjectRequest;

			}
			/// <summary>The method to set the value to dataSubjectRequest</summary>
			/// <param name="dataSubjectRequest">Instance of DataSubjectRequest</param>
			set
			{
				 this.dataSubjectRequest=value;

				 this.keyModified["data_subject_request"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if((( this.keyModified.ContainsKey(key))))
			{
				return  this.keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			 this.keyModified[key] = modification;


		}


	}
}