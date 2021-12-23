using Com.Zoho.Crm.API.CustomViews;
using Com.Zoho.Crm.API.Fields;
using Com.Zoho.Crm.API.Layouts;
using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.BluePrint
{

	public class Field : Model
	{
		bool? systemMandatory;
		bool? webhook;
		Private private1;
		Layout layouts;
		List<Profile> profiles;
		int? sequenceNumber;
		string content;
		string columnName;
		string type;
		int? transitionSequence;
		string personalityName;
		string message;
		bool? mandatory;
		Criteria criteria;
		RelatedDetails relatedDetails;
		string jsonType;
		Crypt crypt;
		string fieldLabel;
		ToolTip tooltip;
		string createdSource;
		bool? fieldReadOnly;
		string displayLabel;
		int? uiType;
		bool? readOnly;
		AssociationDetails associationDetails;
		int? quickSequenceNumber;
		bool? businesscardSupported;
		MultiModuleLookup multiModuleLookup;
		Currency currency;
		long? id;
		bool? customField;
		Module lookup;
		bool? filterable;
		bool? visible;
		bool? pickListValuesSortedLexically;
		int? length;
		ViewType viewType;
		Module subform;
		string apiName;
		bool? sortable;
		Unique unique;
		string dataType;
		Formula formula;
		int? decimalPlace;
		bool? massUpdate;
		bool? blueprintSupported;
		MultiSelectLookup multiselectlookup;
		MultiUserLookup multiuserlookup;
		List<PickListValue> pickListValues;
		AutoNumber autoNumber;
		string defaultValue;
		Dictionary<string, object> validationRule;
		Dictionary<string, object> convertMapping;
		External external;
		HistoryTracking historyTracking;
		Choice<int?> displayType;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? SystemMandatory
		{
			/// <summary>The method to get the systemMandatory</summary>
			/// <returns>bool? representing the systemMandatory</returns>
			get
			{
				return  systemMandatory;

			}
			/// <summary>The method to set the value to systemMandatory</summary>
			/// <param name="systemMandatory">bool?</param>
			set
			{
				 systemMandatory=value;

				 keyModified["system_mandatory"] = 1;

			}
		}

		public bool? Webhook
		{
			/// <summary>The method to get the webhook</summary>
			/// <returns>bool? representing the webhook</returns>
			get
			{
				return  webhook;

			}
			/// <summary>The method to set the value to webhook</summary>
			/// <param name="webhook">bool?</param>
			set
			{
				 webhook=value;

				 keyModified["webhook"] = 1;

			}
		}

		public Private Private
		{
			/// <summary>The method to get the private</summary>
			/// <returns>Instance of Private</returns>
			get
			{
				return  private1;

			}
			/// <summary>The method to set the value to private</summary>
			/// <param name="private1">Instance of Private</param>
			set
			{
				 private1=value;

				 keyModified["private"] = 1;

			}
		}

		public Layout Layouts
		{
			/// <summary>The method to get the layouts</summary>
			/// <returns>Instance of Layout</returns>
			get
			{
				return  layouts;

			}
			/// <summary>The method to set the value to layouts</summary>
			/// <param name="layouts">Instance of Layout</param>
			set
			{
				 layouts=value;

				 keyModified["layouts"] = 1;

			}
		}

		public List<Profile> Profiles
		{
			/// <summary>The method to get the profiles</summary>
			/// <returns>Instance of List<Profile></returns>
			get
			{
				return  profiles;

			}
			/// <summary>The method to set the value to profiles</summary>
			/// <param name="profiles">Instance of List<Profile></param>
			set
			{
				 profiles=value;

				 keyModified["profiles"] = 1;

			}
		}

		public int? SequenceNumber
		{
			/// <summary>The method to get the sequenceNumber</summary>
			/// <returns>int? representing the sequenceNumber</returns>
			get
			{
				return  sequenceNumber;

			}
			/// <summary>The method to set the value to sequenceNumber</summary>
			/// <param name="sequenceNumber">int?</param>
			set
			{
				 sequenceNumber=value;

				 keyModified["sequence_number"] = 1;

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

		public string ColumnName
		{
			/// <summary>The method to get the columnName</summary>
			/// <returns>string representing the columnName</returns>
			get
			{
				return  columnName;

			}
			/// <summary>The method to set the value to columnName</summary>
			/// <param name="columnName">string</param>
			set
			{
				 columnName=value;

				 keyModified["column_name"] = 1;

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

				 keyModified["_type"] = 1;

			}
		}

		public int? TransitionSequence
		{
			/// <summary>The method to get the transitionSequence</summary>
			/// <returns>int? representing the transitionSequence</returns>
			get
			{
				return  transitionSequence;

			}
			/// <summary>The method to set the value to transitionSequence</summary>
			/// <param name="transitionSequence">int?</param>
			set
			{
				 transitionSequence=value;

				 keyModified["transition_sequence"] = 1;

			}
		}

		public string PersonalityName
		{
			/// <summary>The method to get the personalityName</summary>
			/// <returns>string representing the personalityName</returns>
			get
			{
				return  personalityName;

			}
			/// <summary>The method to set the value to personalityName</summary>
			/// <param name="personalityName">string</param>
			set
			{
				 personalityName=value;

				 keyModified["personality_name"] = 1;

			}
		}

		public string Message
		{
			/// <summary>The method to get the message</summary>
			/// <returns>string representing the message</returns>
			get
			{
				return  message;

			}
			/// <summary>The method to set the value to message</summary>
			/// <param name="message">string</param>
			set
			{
				 message=value;

				 keyModified["message"] = 1;

			}
		}

		public bool? Mandatory
		{
			/// <summary>The method to get the mandatory</summary>
			/// <returns>bool? representing the mandatory</returns>
			get
			{
				return  mandatory;

			}
			/// <summary>The method to set the value to mandatory</summary>
			/// <param name="mandatory">bool?</param>
			set
			{
				 mandatory=value;

				 keyModified["mandatory"] = 1;

			}
		}

		public Criteria Criteria
		{
			/// <summary>The method to get the criteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  criteria;

			}
			/// <summary>The method to set the value to criteria</summary>
			/// <param name="criteria">Instance of Criteria</param>
			set
			{
				 criteria=value;

				 keyModified["criteria"] = 1;

			}
		}

		public RelatedDetails RelatedDetails
		{
			/// <summary>The method to get the relatedDetails</summary>
			/// <returns>Instance of RelatedDetails</returns>
			get
			{
				return  relatedDetails;

			}
			/// <summary>The method to set the value to relatedDetails</summary>
			/// <param name="relatedDetails">Instance of RelatedDetails</param>
			set
			{
				 relatedDetails=value;

				 keyModified["related_details"] = 1;

			}
		}

		public string JsonType
		{
			/// <summary>The method to get the jsonType</summary>
			/// <returns>string representing the jsonType</returns>
			get
			{
				return  jsonType;

			}
			/// <summary>The method to set the value to jsonType</summary>
			/// <param name="jsonType">string</param>
			set
			{
				 jsonType=value;

				 keyModified["json_type"] = 1;

			}
		}

		public Crypt Crypt
		{
			/// <summary>The method to get the crypt</summary>
			/// <returns>Instance of Crypt</returns>
			get
			{
				return  crypt;

			}
			/// <summary>The method to set the value to crypt</summary>
			/// <param name="crypt">Instance of Crypt</param>
			set
			{
				 crypt=value;

				 keyModified["crypt"] = 1;

			}
		}

		public string FieldLabel
		{
			/// <summary>The method to get the fieldLabel</summary>
			/// <returns>string representing the fieldLabel</returns>
			get
			{
				return  fieldLabel;

			}
			/// <summary>The method to set the value to fieldLabel</summary>
			/// <param name="fieldLabel">string</param>
			set
			{
				 fieldLabel=value;

				 keyModified["field_label"] = 1;

			}
		}

		public ToolTip Tooltip
		{
			/// <summary>The method to get the tooltip</summary>
			/// <returns>Instance of ToolTip</returns>
			get
			{
				return  tooltip;

			}
			/// <summary>The method to set the value to tooltip</summary>
			/// <param name="tooltip">Instance of ToolTip</param>
			set
			{
				 tooltip=value;

				 keyModified["tooltip"] = 1;

			}
		}

		public string CreatedSource
		{
			/// <summary>The method to get the createdSource</summary>
			/// <returns>string representing the createdSource</returns>
			get
			{
				return  createdSource;

			}
			/// <summary>The method to set the value to createdSource</summary>
			/// <param name="createdSource">string</param>
			set
			{
				 createdSource=value;

				 keyModified["created_source"] = 1;

			}
		}

		public bool? FieldReadOnly
		{
			/// <summary>The method to get the fieldReadOnly</summary>
			/// <returns>bool? representing the fieldReadOnly</returns>
			get
			{
				return  fieldReadOnly;

			}
			/// <summary>The method to set the value to fieldReadOnly</summary>
			/// <param name="fieldReadOnly">bool?</param>
			set
			{
				 fieldReadOnly=value;

				 keyModified["field_read_only"] = 1;

			}
		}

		public string DisplayLabel
		{
			/// <summary>The method to get the displayLabel</summary>
			/// <returns>string representing the displayLabel</returns>
			get
			{
				return  displayLabel;

			}
			/// <summary>The method to set the value to displayLabel</summary>
			/// <param name="displayLabel">string</param>
			set
			{
				 displayLabel=value;

				 keyModified["display_label"] = 1;

			}
		}

		public int? UiType
		{
			/// <summary>The method to get the uiType</summary>
			/// <returns>int? representing the uiType</returns>
			get
			{
				return  uiType;

			}
			/// <summary>The method to set the value to uiType</summary>
			/// <param name="uiType">int?</param>
			set
			{
				 uiType=value;

				 keyModified["ui_type"] = 1;

			}
		}

		public bool? ReadOnly
		{
			/// <summary>The method to get the readOnly</summary>
			/// <returns>bool? representing the readOnly</returns>
			get
			{
				return  readOnly;

			}
			/// <summary>The method to set the value to readOnly</summary>
			/// <param name="readOnly">bool?</param>
			set
			{
				 readOnly=value;

				 keyModified["read_only"] = 1;

			}
		}

		public AssociationDetails AssociationDetails
		{
			/// <summary>The method to get the associationDetails</summary>
			/// <returns>Instance of AssociationDetails</returns>
			get
			{
				return  associationDetails;

			}
			/// <summary>The method to set the value to associationDetails</summary>
			/// <param name="associationDetails">Instance of AssociationDetails</param>
			set
			{
				 associationDetails=value;

				 keyModified["association_details"] = 1;

			}
		}

		public int? QuickSequenceNumber
		{
			/// <summary>The method to get the quickSequenceNumber</summary>
			/// <returns>int? representing the quickSequenceNumber</returns>
			get
			{
				return  quickSequenceNumber;

			}
			/// <summary>The method to set the value to quickSequenceNumber</summary>
			/// <param name="quickSequenceNumber">int?</param>
			set
			{
				 quickSequenceNumber=value;

				 keyModified["quick_sequence_number"] = 1;

			}
		}

		public bool? BusinesscardSupported
		{
			/// <summary>The method to get the businesscardSupported</summary>
			/// <returns>bool? representing the businesscardSupported</returns>
			get
			{
				return  businesscardSupported;

			}
			/// <summary>The method to set the value to businesscardSupported</summary>
			/// <param name="businesscardSupported">bool?</param>
			set
			{
				 businesscardSupported=value;

				 keyModified["businesscard_supported"] = 1;

			}
		}

		public MultiModuleLookup MultiModuleLookup
		{
			/// <summary>The method to get the multiModuleLookup</summary>
			/// <returns>Instance of MultiModuleLookup</returns>
			get
			{
				return  multiModuleLookup;

			}
			/// <summary>The method to set the value to multiModuleLookup</summary>
			/// <param name="multiModuleLookup">Instance of MultiModuleLookup</param>
			set
			{
				 multiModuleLookup=value;

				 keyModified["multi_module_lookup"] = 1;

			}
		}

		public Currency Currency
		{
			/// <summary>The method to get the currency</summary>
			/// <returns>Instance of Currency</returns>
			get
			{
				return  currency;

			}
			/// <summary>The method to set the value to currency</summary>
			/// <param name="currency">Instance of Currency</param>
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

		public bool? CustomField
		{
			/// <summary>The method to get the customField</summary>
			/// <returns>bool? representing the customField</returns>
			get
			{
				return  customField;

			}
			/// <summary>The method to set the value to customField</summary>
			/// <param name="customField">bool?</param>
			set
			{
				 customField=value;

				 keyModified["custom_field"] = 1;

			}
		}

		public Module Lookup
		{
			/// <summary>The method to get the lookup</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  lookup;

			}
			/// <summary>The method to set the value to lookup</summary>
			/// <param name="lookup">Instance of Module</param>
			set
			{
				 lookup=value;

				 keyModified["lookup"] = 1;

			}
		}

		public bool? Filterable
		{
			/// <summary>The method to get the filterable</summary>
			/// <returns>bool? representing the filterable</returns>
			get
			{
				return  filterable;

			}
			/// <summary>The method to set the value to filterable</summary>
			/// <param name="filterable">bool?</param>
			set
			{
				 filterable=value;

				 keyModified["filterable"] = 1;

			}
		}

		public bool? Visible
		{
			/// <summary>The method to get the visible</summary>
			/// <returns>bool? representing the visible</returns>
			get
			{
				return  visible;

			}
			/// <summary>The method to set the value to visible</summary>
			/// <param name="visible">bool?</param>
			set
			{
				 visible=value;

				 keyModified["visible"] = 1;

			}
		}

		public bool? PickListValuesSortedLexically
		{
			/// <summary>The method to get the pickListValuesSortedLexically</summary>
			/// <returns>bool? representing the pickListValuesSortedLexically</returns>
			get
			{
				return  pickListValuesSortedLexically;

			}
			/// <summary>The method to set the value to pickListValuesSortedLexically</summary>
			/// <param name="pickListValuesSortedLexically">bool?</param>
			set
			{
				 pickListValuesSortedLexically=value;

				 keyModified["pick_list_values_sorted_lexically"] = 1;

			}
		}

		public int? Length
		{
			/// <summary>The method to get the length</summary>
			/// <returns>int? representing the length</returns>
			get
			{
				return  length;

			}
			/// <summary>The method to set the value to length</summary>
			/// <param name="length">int?</param>
			set
			{
				 length=value;

				 keyModified["length"] = 1;

			}
		}

		public ViewType ViewType
		{
			/// <summary>The method to get the viewType</summary>
			/// <returns>Instance of ViewType</returns>
			get
			{
				return  viewType;

			}
			/// <summary>The method to set the value to viewType</summary>
			/// <param name="viewType">Instance of ViewType</param>
			set
			{
				 viewType=value;

				 keyModified["view_type"] = 1;

			}
		}

		public Module Subform
		{
			/// <summary>The method to get the subform</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  subform;

			}
			/// <summary>The method to set the value to subform</summary>
			/// <param name="subform">Instance of Module</param>
			set
			{
				 subform=value;

				 keyModified["subform"] = 1;

			}
		}

		public string APIName
		{
			/// <summary>The method to get the aPIName</summary>
			/// <returns>string representing the apiName</returns>
			get
			{
				return  apiName;

			}
			/// <summary>The method to set the value to aPIName</summary>
			/// <param name="apiName">string</param>
			set
			{
				 apiName=value;

				 keyModified["api_name"] = 1;

			}
		}

		public bool? Sortable
		{
			/// <summary>The method to get the sortable</summary>
			/// <returns>bool? representing the sortable</returns>
			get
			{
				return  sortable;

			}
			/// <summary>The method to set the value to sortable</summary>
			/// <param name="sortable">bool?</param>
			set
			{
				 sortable=value;

				 keyModified["sortable"] = 1;

			}
		}

		public Unique Unique
		{
			/// <summary>The method to get the unique</summary>
			/// <returns>Instance of Unique</returns>
			get
			{
				return  unique;

			}
			/// <summary>The method to set the value to unique</summary>
			/// <param name="unique">Instance of Unique</param>
			set
			{
				 unique=value;

				 keyModified["unique"] = 1;

			}
		}

		public string DataType
		{
			/// <summary>The method to get the dataType</summary>
			/// <returns>string representing the dataType</returns>
			get
			{
				return  dataType;

			}
			/// <summary>The method to set the value to dataType</summary>
			/// <param name="dataType">string</param>
			set
			{
				 dataType=value;

				 keyModified["data_type"] = 1;

			}
		}

		public Formula Formula
		{
			/// <summary>The method to get the formula</summary>
			/// <returns>Instance of Formula</returns>
			get
			{
				return  formula;

			}
			/// <summary>The method to set the value to formula</summary>
			/// <param name="formula">Instance of Formula</param>
			set
			{
				 formula=value;

				 keyModified["formula"] = 1;

			}
		}

		public int? DecimalPlace
		{
			/// <summary>The method to get the decimalPlace</summary>
			/// <returns>int? representing the decimalPlace</returns>
			get
			{
				return  decimalPlace;

			}
			/// <summary>The method to set the value to decimalPlace</summary>
			/// <param name="decimalPlace">int?</param>
			set
			{
				 decimalPlace=value;

				 keyModified["decimal_place"] = 1;

			}
		}

		public bool? MassUpdate
		{
			/// <summary>The method to get the massUpdate</summary>
			/// <returns>bool? representing the massUpdate</returns>
			get
			{
				return  massUpdate;

			}
			/// <summary>The method to set the value to massUpdate</summary>
			/// <param name="massUpdate">bool?</param>
			set
			{
				 massUpdate=value;

				 keyModified["mass_update"] = 1;

			}
		}

		public bool? BlueprintSupported
		{
			/// <summary>The method to get the blueprintSupported</summary>
			/// <returns>bool? representing the blueprintSupported</returns>
			get
			{
				return  blueprintSupported;

			}
			/// <summary>The method to set the value to blueprintSupported</summary>
			/// <param name="blueprintSupported">bool?</param>
			set
			{
				 blueprintSupported=value;

				 keyModified["blueprint_supported"] = 1;

			}
		}

		public MultiSelectLookup Multiselectlookup
		{
			/// <summary>The method to get the multiselectlookup</summary>
			/// <returns>Instance of MultiSelectLookup</returns>
			get
			{
				return  multiselectlookup;

			}
			/// <summary>The method to set the value to multiselectlookup</summary>
			/// <param name="multiselectlookup">Instance of MultiSelectLookup</param>
			set
			{
				 multiselectlookup=value;

				 keyModified["multiselectlookup"] = 1;

			}
		}

		public MultiUserLookup Multiuserlookup
		{
			/// <summary>The method to get the multiuserlookup</summary>
			/// <returns>Instance of MultiUserLookup</returns>
			get
			{
				return  multiuserlookup;

			}
			/// <summary>The method to set the value to multiuserlookup</summary>
			/// <param name="multiuserlookup">Instance of MultiUserLookup</param>
			set
			{
				 multiuserlookup=value;

				 keyModified["multiuserlookup"] = 1;

			}
		}

		public List<PickListValue> PickListValues
		{
			/// <summary>The method to get the pickListValues</summary>
			/// <returns>Instance of List<PickListValue></returns>
			get
			{
				return  pickListValues;

			}
			/// <summary>The method to set the value to pickListValues</summary>
			/// <param name="pickListValues">Instance of List<PickListValue></param>
			set
			{
				 pickListValues=value;

				 keyModified["pick_list_values"] = 1;

			}
		}

		public AutoNumber AutoNumber
		{
			/// <summary>The method to get the autoNumber</summary>
			/// <returns>Instance of AutoNumber</returns>
			get
			{
				return  autoNumber;

			}
			/// <summary>The method to set the value to autoNumber</summary>
			/// <param name="autoNumber">Instance of AutoNumber</param>
			set
			{
				 autoNumber=value;

				 keyModified["auto_number"] = 1;

			}
		}

		public string DefaultValue
		{
			/// <summary>The method to get the defaultValue</summary>
			/// <returns>string representing the defaultValue</returns>
			get
			{
				return  defaultValue;

			}
			/// <summary>The method to set the value to defaultValue</summary>
			/// <param name="defaultValue">string</param>
			set
			{
				 defaultValue=value;

				 keyModified["default_value"] = 1;

			}
		}

		public Dictionary<string, object> ValidationRule
		{
			/// <summary>The method to get the validationRule</summary>
			/// <returns>Dictionary representing the validationRule<String,Object></returns>
			get
			{
				return  validationRule;

			}
			/// <summary>The method to set the value to validationRule</summary>
			/// <param name="validationRule">Dictionary<string,object></param>
			set
			{
				 validationRule=value;

				 keyModified["validation_rule"] = 1;

			}
		}

		public Dictionary<string, object> ConvertMapping
		{
			/// <summary>The method to get the convertMapping</summary>
			/// <returns>Dictionary representing the convertMapping<String,Object></returns>
			get
			{
				return  convertMapping;

			}
			/// <summary>The method to set the value to convertMapping</summary>
			/// <param name="convertMapping">Dictionary<string,object></param>
			set
			{
				 convertMapping=value;

				 keyModified["convert_mapping"] = 1;

			}
		}

		public External External
		{
			/// <summary>The method to get the external</summary>
			/// <returns>Instance of External</returns>
			get
			{
				return  external;

			}
			/// <summary>The method to set the value to external</summary>
			/// <param name="external">Instance of External</param>
			set
			{
				 external=value;

				 keyModified["external"] = 1;

			}
		}

		public HistoryTracking HistoryTracking
		{
			/// <summary>The method to get the historyTracking</summary>
			/// <returns>Instance of HistoryTracking</returns>
			get
			{
				return  historyTracking;

			}
			/// <summary>The method to set the value to historyTracking</summary>
			/// <param name="historyTracking">Instance of HistoryTracking</param>
			set
			{
				 historyTracking=value;

				 keyModified["history_tracking"] = 1;

			}
		}

		public Choice<int?> DisplayType
		{
			/// <summary>The method to get the displayType</summary>
			/// <returns>Instance of Choice<Integer></returns>
			get
			{
				return  displayType;

			}
			/// <summary>The method to set the value to displayType</summary>
			/// <param name="displayType">Instance of Choice<int?></param>
			set
			{
				 displayType=value;

				 keyModified["display_type"] = 1;

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