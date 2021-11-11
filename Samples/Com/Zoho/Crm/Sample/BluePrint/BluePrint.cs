using System;

using System.Collections.Generic;

using System.Reflection;

using Com.Zoho.Crm.API.BluePrint;

using Com.Zoho.Crm.API.Fields;

using Com.Zoho.Crm.API.Layouts;

using Com.Zoho.Crm.API.Users;

using Com.Zoho.Crm.API.Util;

using Newtonsoft.Json;

namespace Com.Zoho.Crm.Sample.BluePrint
{
	public class BluePrint
    {
		/// <summary>
		/// This method is used to get a single record's Blueprint details with ID and print the response.
		/// </summary>
		/// <param name="moduleAPIName">The API Name of the record's module</param>
		/// <param name="recordId">The ID of the record to get Blueprint</param>
		public static void GetBlueprint (string moduleAPIName, long recordId)
        {
			//example
			//string moduleAPIName = "Leads";
			//long recordId = 3477002;

			//Get instance of BluePrintOperations Class that takes recordId and moduleAPIName as parameter
			BluePrintOperations bluePrintOperations = new BluePrintOperations (recordId, moduleAPIName);

			//Call GetBlueprint method
			APIResponse<API.BluePrint.ResponseHandler> response = bluePrintOperations.GetBlueprint();

			if (response != null)
            {
				//Get the status code from response
				Console.WriteLine("Status Code: " + response.StatusCode);

				if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
				{
					Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

					return;
				}

				//Check if expected response is received
				if (response.IsExpected)
                {
                    //Get object from response
                    API.BluePrint.ResponseHandler responseHandler = response.Object;

					if (responseHandler is API.BluePrint.ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        API.BluePrint.ResponseWrapper responseWrapper = (API.BluePrint.ResponseWrapper) responseHandler;

						//Get the obtained BluePrint instance
						API.BluePrint.BluePrint bluePrint = responseWrapper.Blueprint;

						//Get the ProcessInfo instance of the obtained BluePrint
						ProcessInfo processInfo = bluePrint.ProcessInfo;

						//Check if ProcessInfo is not null
						if (processInfo != null)
                        {
							//Get the Field ID of the ProcessInfo
							Console.WriteLine("ProcessInfo Field-ID: " + processInfo.FieldId);

							Escalation escalation = processInfo.Escalation;

							if (escalation != null)
							{
								Console.WriteLine("ProcessInfo Escalation Days : " + escalation.Days);

								Console.WriteLine("ProcessInfo Escalation Status : " + escalation.Status);
							}

							//Get the isContinuous of the ProcessInfo
							Console.WriteLine("ProcessInfo isContinuous: " + processInfo.IsContinuous);

							//Get the API Name of the ProcessInfo
							Console.WriteLine("ProcessInfo API Name: " + processInfo.APIName);

							//Get the Continuous of the ProcessInfo
							Console.WriteLine("ProcessInfo Continuous: " + processInfo.Continuous);

							//Get the FieldLabel of the ProcessInfo
							Console.WriteLine("ProcessInfo FieldLabel: " + processInfo.FieldLabel);

							//Get the Name of the ProcessInfo
							Console.WriteLine("ProcessInfo Name: " + processInfo.Name);

							//Get the ColumnName of the ProcessInfo
							Console.WriteLine("ProcessInfo ColumnName: " + processInfo.ColumnName);

							//Get the FieldValue of the ProcessInfo
							Console.WriteLine("ProcessInfo FieldValue: " + processInfo.FieldValue);

							//Get the ID of the ProcessInfo
							Console.WriteLine("ProcessInfo ID: " + processInfo.Id);

							//Get the FieldName of the ProcessInfo
							Console.WriteLine("ProcessInfo FieldName: " + processInfo.FieldName);
						}

						//Get the list of transitions from BluePrint instance
						List<Transition> transitions = bluePrint.Transitions;

						foreach (Transition transition in transitions)
                        {
							List<NextTransition> nextTransitions = transition.NextTransitions;

							foreach (NextTransition nextTransition in nextTransitions)
                            {
								//Get the ID of the NextTransition
								Console.WriteLine("NextTransition ID: " + nextTransition.Id);

								//Get the criteria_matched of the NextTransition
								Console.WriteLine("NextTransition criteria_matched: " + nextTransition.CriteriaMatched);

								//Get the Name of the NextTransition
								Console.WriteLine("NextTransition Name: " + nextTransition.Name);

								//Get the type of the NextTransition
								Console.WriteLine("NextTransition type: " + nextTransition.Type);
							}

							//Get the parent of the Transition
							Transition parentTransition = transition.ParentTransition;

							if (parentTransition!=null)
							{
								Console.WriteLine("Parenttransition ID: " + parentTransition.Id);
							}

							Com.Zoho.Crm.API.Record.Record data = transition.Data;

							if(data != null)
							{
								//Get the ID of each record
								Console.WriteLine("Record ID: " + data.Id);

								//Get the createdBy User instance of each record
								User createdBy = data.CreatedBy;

								if (createdBy != null)
								{
									//Get the ID of the createdBy User
									Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

									//Get the name of the createdBy User
									Console.WriteLine("Record Created By User-Name: " + createdBy.Name);
								}

								//Check if the created time is not null
								if (data.CreatedTime != null)
								{
									//Get the created time of each record
									Console.WriteLine("Record Created Time: " + data.CreatedTime.ToString());
								}

								//Check if the modified time is not null
								if (data.ModifiedTime != null)
								{
									//Get the modified time of each record
									Console.WriteLine("Record Modified Time: " + data.ModifiedTime.ToString());
								}

								//Get the modifiedBy User instance of each record
								User modifiedBy = data.ModifiedBy;

								//Check if modifiedByUser is not null
								if (modifiedBy != null)
								{
									//Get the ID of the modifiedBy User
									Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

									//Get the name of the modifiedBy User
									Console.WriteLine("Record Modified By user-Name: " + modifiedBy.Name);
								}

								//Get all entries from the keyValues map
								foreach (KeyValuePair<string, object> entry in data.GetKeyValues())
								{
									//Get each value in the map
									Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
								}
							}

							//Get the NextFieldValue of the Transition
							Console.WriteLine("Transition NextFieldValue: " + transition.NextFieldValue);

							//Get the Name of each Transition
							Console.WriteLine("Transition Name: " + transition.Name);

							//Get the CriteriaMatched of the Transition
							Console.WriteLine("Transition CriteriaMatched: " + transition.CriteriaMatched.ToString());

							//Get the ID of the Transition
							Console.WriteLine("Transition ID: " + transition.Id);

							Console.WriteLine("Transition Fields: ");

							List<API.BluePrint.Field> fields = transition.Fields;

							foreach (API.BluePrint.Field field in fields)
                            {
								//Get the SystemMandatory of each Field
								Console.WriteLine("Field is SystemMandatory: " + field.SystemMandatory);

								//Get the private of each Field
                        		Console.WriteLine("Field is Private" + field.Private);

								//Get the webhook of each Field
								Console.WriteLine("Transition Fields Webhook: " + field.Webhook);

								//Get the JsonType of each Field
								Console.WriteLine("Transition Fields JsonType: " + field.JsonType);

								//Get the DisplayLabel of each Field
								Console.WriteLine("Transition Fields DisplayLabel: " + field.DisplayLabel);

								//Get the UiType of each Field
                        		Console.WriteLine("Field UiType: " + field.UiType);

								//Get the ValidationRule of each Field
								Console.WriteLine("Transition Fields ValidationRule: " + field.ValidationRule);

								if(field.DisplayType != null)
								{
									//Get the DisplayType of each Field
                            		Console.WriteLine("Field DisplayType: " + field.DisplayType.Value);
								}

								//Get the Object obtained Crypt instance
								Crypt crypt = field.Crypt;

								//Check if crypt is not null
								if (crypt != null)
								{
									//Get the Mode of the Crypt
									Console.WriteLine("Field Crypt Mode: " + crypt.Mode);

									//Get the Column of the Crypt
									Console.WriteLine("Field Crypt Column: " + crypt.Column);

									List<string> encFldIds = crypt.Encfldids;

									if (encFldIds != null)
									{
										Console.WriteLine("EncFldIds : ");

										foreach (string encFldId in encFldIds)
										{
											Console.WriteLine(encFldId);
										}
									}

									//Get the Notify of the Crypt
									Console.WriteLine("Field Crypt Notify: " + crypt.Notify);


									//Get the Table of the Crypt
									Console.WriteLine("Field Crypt Table: " + crypt.Table);

									//Get the Status of the Crypt
									Console.WriteLine("Field Crypt Status: " + crypt.Status);
								}

								//Get the FieldLabel of each Field
								Console.WriteLine("Field FieldLabel: " + field.FieldLabel);

								//Get the Tooltip of each Field
								ToolTip toolTip = field.Tooltip;

								if (toolTip != null)
                                {
									//Get the Tooltip Name
									Console.WriteLine("Transition Fields Tooltip Name: " + toolTip.Name);

									//Get the Tooltip Value
									Console.WriteLine("Transition Fields Tooltip Value: " + toolTip.Value);
								}

								//Get the CreatedSource of each Field
								Console.WriteLine("Transition Fields CreatedSource: " + field.CreatedSource);

								Layout layout = field.Layouts;

								if (layout != null)
								{
									//Get the ID of the Layout
									Console.WriteLine("Transition Fields Layout ID: " + layout.Id);

									//Get the name of the Layout
									Console.WriteLine("Transition Fields Layout Name: " + layout.Name);
								}

								if(field.FieldReadOnly != null)
								{
									//Get the FieldReadOnly of each Field
									Console.WriteLine("Transition Fields FieldReadOnly: " + field.FieldReadOnly.ToString());
								}

								//Get the Content of each Field
								Console.WriteLine("Transition Fields Content: " + field.Content);

								if(field.ReadOnly != null)
								{
									//Get the ReadOnly of each Field
									Console.WriteLine("Transition Fields ReadOnly: " + field.ReadOnly.ToString());
								}

								//Get the AssociationDetails of each Field
								Console.WriteLine("Transition Fields AssociationDetails: " + field.AssociationDetails);

								if(field.QuickSequenceNumber != null)
								{
									//Get the QuickSequenceNumber of each Field
									Console.WriteLine("Transition Fields QuickSequenceNumber: " + field.QuickSequenceNumber.ToString());
								}

								//Get the MultiModuleLookup of each Field
								MultiModuleLookup multiModuleLookup = field.MultiModuleLookup;

								if(multiModuleLookup != null)
								{
									API.Fields.Module module = multiModuleLookup.Module;

									if(module != null)
									{
										//Get the APIName of MultiModuleLookup Module
										Console.WriteLine("Field MultiModuleLookup Module APIName: " + module.APIName);

										//Get the Id of MultiModuleLookup Module
										Console.WriteLine("Field MultiModuleLookup Module Id: " + module.Id);
									}

									//Get the APIName of MultiModuleLookup
									Console.WriteLine("Field MultiModuleLookup Name: " + multiModuleLookup.Name);

									//Get the Id of MultiModuleLookup
									Console.WriteLine("Field MultiModuleLookup Id: " + multiModuleLookup.Id);
								}

								//Get the Object obtained Currency instance
								Currency currency = field.Currency;

								//Check if currency is not null
								if(currency != null)
								{
									//Get the RoundingOption of the Currency
									Console.WriteLine("Field Currency RoundingOption: " + currency.RoundingOption);

									if(currency.Precision != null)
									{
										//Get the Precision of the Currency
										Console.WriteLine("Field Currency Precision: " + currency.Precision);
									}
								}

								//Get the ID of each Field
								Console.WriteLine("Transition Fields ID: " + field.Id);

								if(field.CustomField != null)
								{
									//Get the CustomField of each Field
									Console.WriteLine("Transition Fields CustomField: " + field.CustomField.ToString());
								}

                                //Get the Object obtained Module instance
                                API.Fields.Module lookup = field.Lookup;

								//Check if lookup is not null
								if(lookup != null)
								{
									//Get the Object obtained Layout instance
									layout = lookup.Layout;

									//Check if layout is not null
									if(layout != null)
									{
										//Get the ID of the Layout
										Console.WriteLine("Field Lookup Layout ID: " + layout.Id);

										//Get the Name of the Layout
										Console.WriteLine("Field Lookup Layout Name: " + layout.Name);
									}

									//Get the DisplayLabel of the Module
									Console.WriteLine("Field Lookup DisplayLabel: " + lookup.DisplayLabel);

									//Get the APIName of the Module
									Console.WriteLine("Field Lookup APIName: " + lookup.APIName);

									//Get the Module of the Module
									Console.WriteLine("Field Lookup Module: " + lookup.Module_1);

									if(lookup.Id != null)
									{
										//Get the ID of the Module
										Console.WriteLine("Field Lookup ID: " + lookup.Id);
									}

									//Get the ModuleName of the Module
                                	Console.WriteLine("Field Lookup ModuleName: " + lookup.ModuleName);
								}

								//Get the Filterable of each Field
                        		Console.WriteLine("Field Filterable: " + field.Filterable);

								if(field.ConvertMapping != null)
								{
									//Get the ConvertMapping of each Field
									Console.WriteLine("Transition Fields ConvertMapping: ");

									//Get the details map
									foreach (KeyValuePair<string, object> entry in field.ConvertMapping)
									{
										//Get each value in the map
										Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
									}
								}

								if(field.Visible != null)
								{
									//Get the Visible of each Field
									Console.WriteLine("Transition Fields Visible: " + field.Visible.ToString());
								}

								List<API.Profiles.Profile> profiles = field.Profiles;

								if(profiles != null)
								{
									foreach(API.Profiles.Profile profile in profiles)
									{
										//Get the PermissionType of each Profile
										Console.WriteLine("Field Profile PermissionType: " + profile.PermissionType);

										//Get the Name of each Profile
										Console.WriteLine("Field Profile Name: " + profile.Name);

										//Get the Id of each Profile
										Console.WriteLine("Field Profile Id: " + profile.Id);
									}
								}

								if(field.Length != null)
								{
									//Get the Length of each Field
									Console.WriteLine("Transition Fields Length: " + field.Length.ToString());
								}

								//Get the ColumnName of each Field
								Console.WriteLine("Transition Fields ColumnName: " + field.ColumnName);

								//Get the Type of each Field
                        		Console.WriteLine("Field Type: " + field.Type);

								ViewType viewType = field.ViewType;

								if(viewType != null)
								{
									//Get the View of the ViewType
									Console.WriteLine("Field View: " + viewType.View);

									//Get the Edit of the ViewType
									Console.WriteLine("Field Edit: " + viewType.Edit);

									//Get the Create of the ViewType
									Console.WriteLine("Field Create: " + viewType.Create);

									//Get the View of the ViewType
									Console.WriteLine("Field QuickCreate: " + viewType.QuickCreate);
								}

								//Get the PickListValuesSortedLexically of each Field
                        		Console.WriteLine("Field PickListValuesSortedLexically: " + field.PickListValuesSortedLexically);

								//Get the Sortable of each Field
                        		Console.WriteLine("Field Sortable: " + field.Sortable);

								//Get the TransitionSequence of each Field
								Console.WriteLine("Transition Fields TransitionSequence: " + field.TransitionSequence.ToString());

								External external = field.External;

								if(external != null)
								{
									//Get the Show of External
									Console.WriteLine("Field External Show: " + external.Show);

									//Get the Type of External
									Console.WriteLine("Field External Type: " + external.Type);

									//Get the AllowMultipleConfig of External
									Console.WriteLine("Field External AllowMultipleConfig: " + external.AllowMultipleConfig);
								}

								//Get the APIName of each Field
								Console.WriteLine("Transition Fields APIName: " + field.APIName);

								//Get the Object obtained Unique instance
								Unique unique = field.Unique;

								//Check if unique is not null
								if(unique != null)
								{
									//Get the Casesensitive of the Unique
									Console.WriteLine("Transition Field Unique Casesensitive : " + unique.Casesensitive);
								}

								if(field.HistoryTracking != null)
								{
									//Get the HistoryTracking of each Field
									Console.WriteLine("Field HistoryTracking: " + field.HistoryTracking);
								}

								//Get the DataType of each Field
								Console.WriteLine("Transition Fields DataType: " + field.DataType);

								//Get the Object obtained Formula instance
								Formula formula = field.Formula;

								//Check if formula is not null
								if(formula != null)
								{
									//Get the ReturnType of the Formula
									Console.WriteLine("Transition Field Formula ReturnType : " + formula.ReturnType);

									if(formula.Expression != null)
									{
										//Get the Expression of the Formula
										Console.WriteLine("Transition Field Formula Expression : " + formula.Expression);
									}
								}

								//Get the DecimalPlace of each Field
								Console.WriteLine("Transition Fields DecimalPlace: " + field.DecimalPlace);

								//Get all entries from the MultiSelectLookup instance
								MultiSelectLookup multiSelectLookup = field.Multiselectlookup;

								if (multiSelectLookup != null)
                                {
									//Get the DisplayValue of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup DisplayLabel: " + multiSelectLookup.DisplayLabel);

									//Get the LinkingModule of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup LinkingModule: " + multiSelectLookup.LinkingModule);

									//Get the LookupAPIname of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup LookupAPIname: " + multiSelectLookup.LookupApiname);

									//Get the APIName of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup APIName: " + multiSelectLookup.APIName);

									//Get the ConnectedlookupAPIname of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup ConnectedlookupAPIname: " + multiSelectLookup.ConnectedlookupApiname);

									//Get the ConnectedModule of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup ConnectedModule: " + multiSelectLookup.ConnectedModule);

									//Get the ID of the MultiSelectLookup
									Console.WriteLine("Transition Fields MultiSelectLookup ID: " + multiSelectLookup.Id);
								}

								List<PickListValue> pickListValues = field.PickListValues;

								if (pickListValues != null)
                                {
									foreach (PickListValue pickListValue in pickListValues)
                                    {
										PrintPickListValue(pickListValue);
									}
								}

								//Get the AutoNumber of each Field
								AutoNumber autoNumber = field.AutoNumber;

								if (autoNumber != null)
                                {
									//Get the Prefix of the AutoNumber
									Console.WriteLine("Transition Fields AutoNumber Prefix: " + autoNumber.Prefix);

									//Get the Suffix of the AutoNumber
									Console.WriteLine("Transition Fields AutoNumber Suffix: " + autoNumber.Suffix);

									if(autoNumber.StartNumber != null)
									{
										//Get the StartNumber of the AutoNumber
										Console.WriteLine("Transition Fields AutoNumber StartNumber: " + autoNumber.StartNumber.ToString());
									}
								}

								//Get the PersonalityName of each Field
								Console.WriteLine("Transition Fields PersonalityName: " + field.PersonalityName);

								if(field.Mandatory != null)
								{
									//Get the Mandatory of each Field
									Console.WriteLine("Transition Fields Mandatory: " + field.Mandatory.ToString());
								}
							}

							//Get the Type of each Transition
							Console.WriteLine("Transition Type: " + transition.Type);

							//Get the CriteriaMessage of each Transition
							Console.WriteLine("Transition CriteriaMessage: " + transition.CriteriaMessage);

							//Get the PercentPartialSave of each Transition
                    		Console.WriteLine("Transition PercentPartialSave: " + transition.PercentPartialSave);

							//Get the ExecutionTime of each Transition
							Console.WriteLine("Transition ExecutionTime: " + transition.ExecutionTime);
						}
					}
					//Check if the request returned an exception
					else if (responseHandler is API.BluePrint.APIException)
                    {
                        //Get the received APIException instance
                        API.BluePrint.APIException exception = (API.BluePrint.APIException) responseHandler;

						//Get the Status
						Console.WriteLine("Status: " + exception.Status.Value);

						//Get the Code
						Console.WriteLine("Code: " + exception.Code.Value);

						Console.WriteLine("Details: ");

						//Get the details map
						foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
							//Get each value in the map
							Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
						}

						//Get the Message
						Console.WriteLine("Message: " + exception.Message.Value);
					}
				}
				else
				{ //If response is not as expected

					//Get model object from response
					Model responseObject = response.Model;

					//Get the response object's class
					Type type = responseObject.GetType();

					//Get all declared fields of the response class
					Console.WriteLine("Type is: {0}", type.Name);

					PropertyInfo[] props = type.GetProperties();

					Console.WriteLine("Properties (N = {0}):", props.Length);

					foreach (var prop in props)
					{
						if (prop.GetIndexParameters().Length == 0)
						{
							Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
						}
						else
						{
							Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
						}
					}
				}
			}
		}

		/// <summary>
		/// This method is used to update a single record's Blueprint details with ID and print the response.
		/// </summary>
		/// <param name="moduleAPIName">The API Name of the record's module</param>
		/// <param name="recordId">The ID of the record to Get Blueprint</param>
		/// <param name="transitionId">The ID of the Blueprint transition Id</param>
		public static void UpdateBlueprint(string moduleAPIName, long recordId, long transitionId)
		{
			//ID of the BluePrint to be updated
			//string moduleAPIName = "Leads";
			//long recordId = 34702;
			//long transitionId = 34796;

			//Get instance of BluePrintOperations Class that takes recordId and moduleAPIName as parameter
			BluePrintOperations bluePrintOperations = new BluePrintOperations(recordId, moduleAPIName);

			//Get instance of BodyWrapper Class that will contain the request body
			API.BluePrint.BodyWrapper bodyWrapper = new API.BluePrint.BodyWrapper();

			//List of BluePrint instances
			List<API.BluePrint.BluePrint> bluePrintList = new List<API.BluePrint.BluePrint>();

			//Get instance of BluePrint Class
			API.BluePrint.BluePrint bluePrint = new API.BluePrint.BluePrint();

			//Set transition_id to the BluePrint instance
			bluePrint.TransitionId = transitionId;

			//Get instance of Record Class
			Com.Zoho.Crm.API.Record.Record data = new Com.Zoho.Crm.API.Record.Record();

			Dictionary<string, object> lookup = new Dictionary<string, object>();

			lookup.Add("Phone", "8940372937");

			lookup.Add("id", "8940372937");

			//data.AddKeyValue("Lookup_2", lookup);

			data.AddKeyValue("Phone", "8940372937");

			data.AddKeyValue("Notes", "Updated via blueprint");

			Dictionary<string, object> attachments = new Dictionary<string, object>();

			List<string> fileIds = new List<string>();

			fileIds.Add("blojtd2d13b5f044e4041a3315e0793fb21ef");

			attachments.Add("$file_id", fileIds);

			data.AddKeyValue("Attachments", attachments);

			List<Dictionary<string, object>> checkLists = new List<Dictionary<string, object>>();

			Dictionary<string, object> checkListItem = new Dictionary<string, object>();

			checkListItem.Add("list 1", true);

			checkLists.Add(checkListItem);

			checkListItem = new Dictionary<string, object>();

			checkListItem.Add("list 2", true);

			checkLists.Add(checkListItem);

			checkListItem = new Dictionary<string, object>();

			checkListItem.Add("list 3", true);

			checkLists.Add(checkListItem);

			data.AddKeyValue("CheckLists", checkLists);

			//Set data to the BluePrint instance
			bluePrint.Data = data;

			//Add BluePrint instance to the list
			bluePrintList.Add(bluePrint);

			//Set the list to bluePrint in BodyWrapper instance
			bodyWrapper.Blueprint = bluePrintList;

			//Call UpdateBlueprint method that takes BodyWrapper instance
			APIResponse<API.BluePrint.ActionResponse> response = bluePrintOperations.UpdateBlueprint(bodyWrapper);

			if (response != null)
			{
				//Get the status code from response
				Console.WriteLine("Status Code: " + response.StatusCode);

				//Check if expected response is received
				if (response.IsExpected)
				{
					//Get object from response
					API.BluePrint.ActionResponse actionResponse = response.Object;

					//Check if the request is successful
					if (actionResponse is API.BluePrint.SuccessResponse)
					{
						//Get the received SuccessResponse instance
						API.BluePrint.SuccessResponse successResponse = (API.BluePrint.SuccessResponse)actionResponse;

						//Get the Status
						Console.WriteLine("Status: " + successResponse.Status.Value);

						//Get the Code
						Console.WriteLine("Code: " + successResponse.Code.Value);

						Console.WriteLine("Details: ");

						if (successResponse.Details != null)
						{
							//Get the details map
							foreach (KeyValuePair<string, object> entry in successResponse.Details)
							{
								//Get each value in the map
								Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
							}
						}

						//Get the Message
						Console.WriteLine("Message: " + successResponse.Message.Value);
					}
					//Check if the request returned an exception
					else if (actionResponse is API.BluePrint.APIException)
					{
						//Get the received APIException instance
						API.BluePrint.APIException exception = (API.BluePrint.APIException)actionResponse;

						//Get the Status
						Console.WriteLine("Status: " + exception.Status.Value);

						//Get the Code
						Console.WriteLine("Code: " + exception.Code.Value);

						Console.WriteLine("Details: ");

						//Get the details map
						foreach (KeyValuePair<string, object> entry in exception.Details)
						{
							//Get each value in the map
							Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
						}

						//Get the Message
						Console.WriteLine("Message: " + exception.Message.Value);
					}
				}
				else
				{ //If response is not as expected

					//Get model object from response
					Model responseObject = response.Model;

					//Get the response object's class
					Type type = responseObject.GetType();

					//Get all declared fields of the response class
					Console.WriteLine("Type is: {0}", type.Name);

					PropertyInfo[] props = type.GetProperties();

					Console.WriteLine("Properties (N = {0}):", props.Length);

					foreach (var prop in props)
					{
						if (prop.GetIndexParameters().Length == 0)
						{
							Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
						}
						else
						{
							Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
						}
					}
				}
			}
		}

		private static void PrintPickListValue(PickListValue pickListValue)
		{
			//Get the DisplayValue of each PickListValues
			Console.WriteLine("Field PickListValue DisplayValue: " + pickListValue.DisplayValue);

			if(pickListValue.SequenceNumber != null)
			{
				//Get the SequenceNumber of each PickListValues
				Console.WriteLine(" Field PickListValue SequenceNumber: " + pickListValue.SequenceNumber);
			}

			//Get the ExpectedDataType of each PickListValues
			Console.WriteLine("Field PickListValue ExpectedDataType: " + pickListValue.ExpectedDataType);

			if(pickListValue.Maps != null)
			{
				foreach(Maps map in pickListValue.Maps)
				{
					Console.WriteLine("Field PickListValue Maps APIName: " + map.APIName);

					//Get the PickListValue of each Maps
					List<PickListValue> pickListValues = map.PickListValues;

					//Check if formula is not null
					if(pickListValues != null)
					{
						foreach(PickListValue pickListValue1 in pickListValues)
						{
							PrintPickListValue(pickListValue1);
						}
					}
				}
			}

			//Get the ActualValue of each PickListValues
			Console.WriteLine("Field PickListValue ActualValue: " + pickListValue.ActualValue);

			//Get the SysRefName of each PickListValues
			Console.WriteLine("Field PickListValue SysRefName: " + pickListValue.SysRefName);

			//Get the Type of each PickListValues
			Console.WriteLine("Field PickListValue Type: " + pickListValue.Type);

			//Get the Id of each PickListValues
			Console.WriteLine("Field PickListValue Id: " + pickListValue.Id);
		}
	}
}