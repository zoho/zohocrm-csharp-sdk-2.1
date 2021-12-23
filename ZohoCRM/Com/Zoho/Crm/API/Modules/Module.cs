using Com.Zoho.Crm.API.CustomViews;
using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using System;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Modules
{

	public class Module : Model
	{
		string name;
		bool? globalSearchSupported;
		bool? kanbanView;
		bool? deletable;
		string description;
		bool? creatable;
		bool? filterStatus;
		bool? inventoryTemplateSupported;
		DateTimeOffset? modifiedTime;
		string pluralLabel;
		bool? presenceSubMenu;
		bool? isblueprintsupported;
		bool? triggersSupported;
		long? id;
		RelatedListProperties relatedListProperties;
		List<string> properties;
		List<string> onDemandProperties;
		int? perPage;
		int? visibility;
		bool? visible;
		bool? convertable;
		bool? editable;
		bool? emailtemplateSupport;
		List<Profile> profiles;
		bool? filterSupported;
		string displayField;
		List<string> searchLayoutFields;
		bool? kanbanViewSupported;
		bool? showAsTab;
		string webLink;
		int? sequenceNumber;
		string singularLabel;
		bool? viewable;
		bool? apiSupported;
		string apiName;
		bool? quickCreate;
		User modifiedBy;
		Choice<string> generatedType;
		bool? feedsRequired;
		bool? scoringSupported;
		bool? webformSupported;
		List<Argument> arguments;
		string moduleName;
		int? businessCardFieldLimit;
		CustomView customView;
		Module parentModule;
		Territory territory;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				return  name;

			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 name=value;

				 keyModified["name"] = 1;

			}
		}

		public bool? GlobalSearchSupported
		{
			/// <summary>The method to get the globalSearchSupported</summary>
			/// <returns>bool? representing the globalSearchSupported</returns>
			get
			{
				return  globalSearchSupported;

			}
			/// <summary>The method to set the value to globalSearchSupported</summary>
			/// <param name="globalSearchSupported">bool?</param>
			set
			{
				 globalSearchSupported=value;

				 keyModified["global_search_supported"] = 1;

			}
		}

		public bool? KanbanView
		{
			/// <summary>The method to get the kanbanView</summary>
			/// <returns>bool? representing the kanbanView</returns>
			get
			{
				return  kanbanView;

			}
			/// <summary>The method to set the value to kanbanView</summary>
			/// <param name="kanbanView">bool?</param>
			set
			{
				 kanbanView=value;

				 keyModified["kanban_view"] = 1;

			}
		}

		public bool? Deletable
		{
			/// <summary>The method to get the deletable</summary>
			/// <returns>bool? representing the deletable</returns>
			get
			{
				return  deletable;

			}
			/// <summary>The method to set the value to deletable</summary>
			/// <param name="deletable">bool?</param>
			set
			{
				 deletable=value;

				 keyModified["deletable"] = 1;

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

		public bool? Creatable
		{
			/// <summary>The method to get the creatable</summary>
			/// <returns>bool? representing the creatable</returns>
			get
			{
				return  creatable;

			}
			/// <summary>The method to set the value to creatable</summary>
			/// <param name="creatable">bool?</param>
			set
			{
				 creatable=value;

				 keyModified["creatable"] = 1;

			}
		}

		public bool? FilterStatus
		{
			/// <summary>The method to get the filterStatus</summary>
			/// <returns>bool? representing the filterStatus</returns>
			get
			{
				return  filterStatus;

			}
			/// <summary>The method to set the value to filterStatus</summary>
			/// <param name="filterStatus">bool?</param>
			set
			{
				 filterStatus=value;

				 keyModified["filter_status"] = 1;

			}
		}

		public bool? InventoryTemplateSupported
		{
			/// <summary>The method to get the inventoryTemplateSupported</summary>
			/// <returns>bool? representing the inventoryTemplateSupported</returns>
			get
			{
				return  inventoryTemplateSupported;

			}
			/// <summary>The method to set the value to inventoryTemplateSupported</summary>
			/// <param name="inventoryTemplateSupported">bool?</param>
			set
			{
				 inventoryTemplateSupported=value;

				 keyModified["inventory_template_supported"] = 1;

			}
		}

		public DateTimeOffset? ModifiedTime
		{
			/// <summary>The method to get the modifiedTime</summary>
			/// <returns>DateTimeOffset? representing the modifiedTime</returns>
			get
			{
				return  modifiedTime;

			}
			/// <summary>The method to set the value to modifiedTime</summary>
			/// <param name="modifiedTime">DateTimeOffset?</param>
			set
			{
				 modifiedTime=value;

				 keyModified["modified_time"] = 1;

			}
		}

		public string PluralLabel
		{
			/// <summary>The method to get the pluralLabel</summary>
			/// <returns>string representing the pluralLabel</returns>
			get
			{
				return  pluralLabel;

			}
			/// <summary>The method to set the value to pluralLabel</summary>
			/// <param name="pluralLabel">string</param>
			set
			{
				 pluralLabel=value;

				 keyModified["plural_label"] = 1;

			}
		}

		public bool? PresenceSubMenu
		{
			/// <summary>The method to get the presenceSubMenu</summary>
			/// <returns>bool? representing the presenceSubMenu</returns>
			get
			{
				return  presenceSubMenu;

			}
			/// <summary>The method to set the value to presenceSubMenu</summary>
			/// <param name="presenceSubMenu">bool?</param>
			set
			{
				 presenceSubMenu=value;

				 keyModified["presence_sub_menu"] = 1;

			}
		}

		public bool? Isblueprintsupported
		{
			/// <summary>The method to get the isblueprintsupported</summary>
			/// <returns>bool? representing the isblueprintsupported</returns>
			get
			{
				return  isblueprintsupported;

			}
			/// <summary>The method to set the value to isblueprintsupported</summary>
			/// <param name="isblueprintsupported">bool?</param>
			set
			{
				 isblueprintsupported=value;

				 keyModified["isBlueprintSupported"] = 1;

			}
		}

		public bool? TriggersSupported
		{
			/// <summary>The method to get the triggersSupported</summary>
			/// <returns>bool? representing the triggersSupported</returns>
			get
			{
				return  triggersSupported;

			}
			/// <summary>The method to set the value to triggersSupported</summary>
			/// <param name="triggersSupported">bool?</param>
			set
			{
				 triggersSupported=value;

				 keyModified["triggers_supported"] = 1;

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

		public RelatedListProperties RelatedListProperties
		{
			/// <summary>The method to get the relatedListProperties</summary>
			/// <returns>Instance of RelatedListProperties</returns>
			get
			{
				return  relatedListProperties;

			}
			/// <summary>The method to set the value to relatedListProperties</summary>
			/// <param name="relatedListProperties">Instance of RelatedListProperties</param>
			set
			{
				 relatedListProperties=value;

				 keyModified["related_list_properties"] = 1;

			}
		}

		public List<string> Properties
		{
			/// <summary>The method to get the properties</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  properties;

			}
			/// <summary>The method to set the value to properties</summary>
			/// <param name="properties">Instance of List<string></param>
			set
			{
				 properties=value;

				 keyModified["$properties"] = 1;

			}
		}

		public List<string> OnDemandProperties
		{
			/// <summary>The method to get the onDemandProperties</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  onDemandProperties;

			}
			/// <summary>The method to set the value to onDemandProperties</summary>
			/// <param name="onDemandProperties">Instance of List<string></param>
			set
			{
				 onDemandProperties=value;

				 keyModified["$on_demand_properties"] = 1;

			}
		}

		public int? PerPage
		{
			/// <summary>The method to get the perPage</summary>
			/// <returns>int? representing the perPage</returns>
			get
			{
				return  perPage;

			}
			/// <summary>The method to set the value to perPage</summary>
			/// <param name="perPage">int?</param>
			set
			{
				 perPage=value;

				 keyModified["per_page"] = 1;

			}
		}

		public int? Visibility
		{
			/// <summary>The method to get the visibility</summary>
			/// <returns>int? representing the visibility</returns>
			get
			{
				return  visibility;

			}
			/// <summary>The method to set the value to visibility</summary>
			/// <param name="visibility">int?</param>
			set
			{
				 visibility=value;

				 keyModified["visibility"] = 1;

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

		public bool? Convertable
		{
			/// <summary>The method to get the convertable</summary>
			/// <returns>bool? representing the convertable</returns>
			get
			{
				return  convertable;

			}
			/// <summary>The method to set the value to convertable</summary>
			/// <param name="convertable">bool?</param>
			set
			{
				 convertable=value;

				 keyModified["convertable"] = 1;

			}
		}

		public bool? Editable
		{
			/// <summary>The method to get the editable</summary>
			/// <returns>bool? representing the editable</returns>
			get
			{
				return  editable;

			}
			/// <summary>The method to set the value to editable</summary>
			/// <param name="editable">bool?</param>
			set
			{
				 editable=value;

				 keyModified["editable"] = 1;

			}
		}

		public bool? EmailtemplateSupport
		{
			/// <summary>The method to get the emailtemplateSupport</summary>
			/// <returns>bool? representing the emailtemplateSupport</returns>
			get
			{
				return  emailtemplateSupport;

			}
			/// <summary>The method to set the value to emailtemplateSupport</summary>
			/// <param name="emailtemplateSupport">bool?</param>
			set
			{
				 emailtemplateSupport=value;

				 keyModified["emailTemplate_support"] = 1;

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

		public bool? FilterSupported
		{
			/// <summary>The method to get the filterSupported</summary>
			/// <returns>bool? representing the filterSupported</returns>
			get
			{
				return  filterSupported;

			}
			/// <summary>The method to set the value to filterSupported</summary>
			/// <param name="filterSupported">bool?</param>
			set
			{
				 filterSupported=value;

				 keyModified["filter_supported"] = 1;

			}
		}

		public string DisplayField
		{
			/// <summary>The method to get the displayField</summary>
			/// <returns>string representing the displayField</returns>
			get
			{
				return  displayField;

			}
			/// <summary>The method to set the value to displayField</summary>
			/// <param name="displayField">string</param>
			set
			{
				 displayField=value;

				 keyModified["display_field"] = 1;

			}
		}

		public List<string> SearchLayoutFields
		{
			/// <summary>The method to get the searchLayoutFields</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  searchLayoutFields;

			}
			/// <summary>The method to set the value to searchLayoutFields</summary>
			/// <param name="searchLayoutFields">Instance of List<string></param>
			set
			{
				 searchLayoutFields=value;

				 keyModified["search_layout_fields"] = 1;

			}
		}

		public bool? KanbanViewSupported
		{
			/// <summary>The method to get the kanbanViewSupported</summary>
			/// <returns>bool? representing the kanbanViewSupported</returns>
			get
			{
				return  kanbanViewSupported;

			}
			/// <summary>The method to set the value to kanbanViewSupported</summary>
			/// <param name="kanbanViewSupported">bool?</param>
			set
			{
				 kanbanViewSupported=value;

				 keyModified["kanban_view_supported"] = 1;

			}
		}

		public bool? ShowAsTab
		{
			/// <summary>The method to get the showAsTab</summary>
			/// <returns>bool? representing the showAsTab</returns>
			get
			{
				return  showAsTab;

			}
			/// <summary>The method to set the value to showAsTab</summary>
			/// <param name="showAsTab">bool?</param>
			set
			{
				 showAsTab=value;

				 keyModified["show_as_tab"] = 1;

			}
		}

		public string WebLink
		{
			/// <summary>The method to get the webLink</summary>
			/// <returns>string representing the webLink</returns>
			get
			{
				return  webLink;

			}
			/// <summary>The method to set the value to webLink</summary>
			/// <param name="webLink">string</param>
			set
			{
				 webLink=value;

				 keyModified["web_link"] = 1;

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

		public string SingularLabel
		{
			/// <summary>The method to get the singularLabel</summary>
			/// <returns>string representing the singularLabel</returns>
			get
			{
				return  singularLabel;

			}
			/// <summary>The method to set the value to singularLabel</summary>
			/// <param name="singularLabel">string</param>
			set
			{
				 singularLabel=value;

				 keyModified["singular_label"] = 1;

			}
		}

		public bool? Viewable
		{
			/// <summary>The method to get the viewable</summary>
			/// <returns>bool? representing the viewable</returns>
			get
			{
				return  viewable;

			}
			/// <summary>The method to set the value to viewable</summary>
			/// <param name="viewable">bool?</param>
			set
			{
				 viewable=value;

				 keyModified["viewable"] = 1;

			}
		}

		public bool? APISupported
		{
			/// <summary>The method to get the aPISupported</summary>
			/// <returns>bool? representing the apiSupported</returns>
			get
			{
				return  apiSupported;

			}
			/// <summary>The method to set the value to aPISupported</summary>
			/// <param name="apiSupported">bool?</param>
			set
			{
				 apiSupported=value;

				 keyModified["api_supported"] = 1;

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

		public bool? QuickCreate
		{
			/// <summary>The method to get the quickCreate</summary>
			/// <returns>bool? representing the quickCreate</returns>
			get
			{
				return  quickCreate;

			}
			/// <summary>The method to set the value to quickCreate</summary>
			/// <param name="quickCreate">bool?</param>
			set
			{
				 quickCreate=value;

				 keyModified["quick_create"] = 1;

			}
		}

		public User ModifiedBy
		{
			/// <summary>The method to get the modifiedBy</summary>
			/// <returns>Instance of User</returns>
			get
			{
				return  modifiedBy;

			}
			/// <summary>The method to set the value to modifiedBy</summary>
			/// <param name="modifiedBy">Instance of User</param>
			set
			{
				 modifiedBy=value;

				 keyModified["modified_by"] = 1;

			}
		}

		public Choice<string> GeneratedType
		{
			/// <summary>The method to get the generatedType</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  generatedType;

			}
			/// <summary>The method to set the value to generatedType</summary>
			/// <param name="generatedType">Instance of Choice<string></param>
			set
			{
				 generatedType=value;

				 keyModified["generated_type"] = 1;

			}
		}

		public bool? FeedsRequired
		{
			/// <summary>The method to get the feedsRequired</summary>
			/// <returns>bool? representing the feedsRequired</returns>
			get
			{
				return  feedsRequired;

			}
			/// <summary>The method to set the value to feedsRequired</summary>
			/// <param name="feedsRequired">bool?</param>
			set
			{
				 feedsRequired=value;

				 keyModified["feeds_required"] = 1;

			}
		}

		public bool? ScoringSupported
		{
			/// <summary>The method to get the scoringSupported</summary>
			/// <returns>bool? representing the scoringSupported</returns>
			get
			{
				return  scoringSupported;

			}
			/// <summary>The method to set the value to scoringSupported</summary>
			/// <param name="scoringSupported">bool?</param>
			set
			{
				 scoringSupported=value;

				 keyModified["scoring_supported"] = 1;

			}
		}

		public bool? WebformSupported
		{
			/// <summary>The method to get the webformSupported</summary>
			/// <returns>bool? representing the webformSupported</returns>
			get
			{
				return  webformSupported;

			}
			/// <summary>The method to set the value to webformSupported</summary>
			/// <param name="webformSupported">bool?</param>
			set
			{
				 webformSupported=value;

				 keyModified["webform_supported"] = 1;

			}
		}

		public List<Argument> Arguments
		{
			/// <summary>The method to get the arguments</summary>
			/// <returns>Instance of List<Argument></returns>
			get
			{
				return  arguments;

			}
			/// <summary>The method to set the value to arguments</summary>
			/// <param name="arguments">Instance of List<Argument></param>
			set
			{
				 arguments=value;

				 keyModified["arguments"] = 1;

			}
		}

		public string ModuleName
		{
			/// <summary>The method to get the moduleName</summary>
			/// <returns>string representing the moduleName</returns>
			get
			{
				return  moduleName;

			}
			/// <summary>The method to set the value to moduleName</summary>
			/// <param name="moduleName">string</param>
			set
			{
				 moduleName=value;

				 keyModified["module_name"] = 1;

			}
		}

		public int? BusinessCardFieldLimit
		{
			/// <summary>The method to get the businessCardFieldLimit</summary>
			/// <returns>int? representing the businessCardFieldLimit</returns>
			get
			{
				return  businessCardFieldLimit;

			}
			/// <summary>The method to set the value to businessCardFieldLimit</summary>
			/// <param name="businessCardFieldLimit">int?</param>
			set
			{
				 businessCardFieldLimit=value;

				 keyModified["business_card_field_limit"] = 1;

			}
		}

		public CustomView CustomView
		{
			/// <summary>The method to get the customView</summary>
			/// <returns>Instance of CustomView</returns>
			get
			{
				return  customView;

			}
			/// <summary>The method to set the value to customView</summary>
			/// <param name="customView">Instance of CustomView</param>
			set
			{
				 customView=value;

				 keyModified["custom_view"] = 1;

			}
		}

		public Module ParentModule
		{
			/// <summary>The method to get the parentModule</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  parentModule;

			}
			/// <summary>The method to set the value to parentModule</summary>
			/// <param name="parentModule">Instance of Module</param>
			set
			{
				 parentModule=value;

				 keyModified["parent_module"] = 1;

			}
		}

		public Territory Territory
		{
			/// <summary>The method to get the territory</summary>
			/// <returns>Instance of Territory</returns>
			get
			{
				return  territory;

			}
			/// <summary>The method to set the value to territory</summary>
			/// <param name="territory">Instance of Territory</param>
			set
			{
				 territory=value;

				 keyModified["territory"] = 1;

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