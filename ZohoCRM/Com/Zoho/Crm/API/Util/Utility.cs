using System;

using System.Collections.Generic;

using System.IO;

using System.Text;

using Com.Zoho.API.Exception;

using Com.Zoho.Crm.API.Fields;

using Com.Zoho.Crm.API.Logger;

using Com.Zoho.Crm.API.Modules;

using Com.Zoho.Crm.API.RelatedLists;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;

using static Com.Zoho.Crm.API.Modules.ModulesOperations;

using APIException = Com.Zoho.Crm.API.RelatedLists.APIException;

using Module = Com.Zoho.Crm.API.Modules.Module;

using ResponseHandler = Com.Zoho.Crm.API.RelatedLists.ResponseHandler;

using ResponseWrapper = Com.Zoho.Crm.API.RelatedLists.ResponseWrapper;

namespace Com.Zoho.Crm.API.Util
{
    /// <summary>
    /// This class handles module field details.
    /// </summary>
    public class Utility
    {
        static object LOCK = new object();

        static Dictionary<string, string> apiTypeVsDataType = new Dictionary<string, string>();

        static Dictionary<string, string> apiTypeVsStructureName = new Dictionary<string, string>();

        static JObject JSONDETAILS = Initializer.jsonDetails;

        static bool newFile = false;

        static bool getModifiedModules = false;

        static bool forceRefresh = false;

        static string moduleAPIName;

        static JObject apiSupportedModule = new JObject();

        public static void AssertNotNull(object value, string errorCode, string errorMessage)
        {
            if(value == null)
            {
                throw new SDKException(errorCode, errorMessage);
            }
        }

        static void FileExistsFlow(string moduleAPIName, String recordFieldDetailsPath, string lastModifiedTime)
        {
            var recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

            if (Initializer.GetInitializer().SDKConfig.AutoRefreshFields && !newFile && !getModifiedModules && (String.IsNullOrEmpty((string)recordFieldDetailsJson[Constants.FIELDS_LAST_MODIFIED_TIME]) || forceRefresh || (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Convert.ToInt64(recordFieldDetailsJson[Constants.FIELDS_LAST_MODIFIED_TIME])) > 3600000))
            {
                getModifiedModules = true;

                lastModifiedTime = (!forceRefresh && recordFieldDetailsJson.ContainsKey(Constants.FIELDS_LAST_MODIFIED_TIME)) ? (string)recordFieldDetailsJson.GetValue(Constants.FIELDS_LAST_MODIFIED_TIME) : null;

                ModifyFields(recordFieldDetailsPath, lastModifiedTime);

                getModifiedModules = false;
            }
            else if (!Initializer.GetInitializer().SDKConfig.AutoRefreshFields && forceRefresh && !getModifiedModules)
            {
                getModifiedModules = true;

                ModifyFields(recordFieldDetailsPath, lastModifiedTime);

                getModifiedModules = false;
            }

            recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

            if (moduleAPIName == null || recordFieldDetailsJson.ContainsKey(moduleAPIName.ToLower()))
            {
                return;
            }
            else
            {
                FillDataType();

                recordFieldDetailsJson[moduleAPIName.ToLower()] = new JObject();

                using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                {
                    var serializer = new JsonSerializer();

                    serializer.Serialize(sw, recordFieldDetailsJson);

                    sw.Flush();

                    sw.Close();
                }

                var fieldDetails = (JObject)GetFieldsDetails(moduleAPIName);

                recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

                recordFieldDetailsJson[moduleAPIName.ToLower()] = fieldDetails;

                using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                {
                    var serializer = new JsonSerializer();

                    serializer.Serialize(sw, recordFieldDetailsJson);

                    sw.Flush();

                    sw.Close();
                }
            }
        }

        static string VerifyModuleAPIName(string moduleName)
        {
            if (moduleName != null && Constants.DEFAULT_MODULENAME_VS_APINAME.ContainsKey(moduleName.ToLower()))
            {
                return Constants.DEFAULT_MODULENAME_VS_APINAME[moduleName.ToLower()];
            }

            var recordFieldDetailsPath = GetFileName();

            if (System.IO.File.Exists(recordFieldDetailsPath))
            {
                var fieldsJSON = Initializer.GetJSON(recordFieldDetailsPath);

                if (fieldsJSON.ContainsKey(Constants.SDK_MODULE_METADATA) && ((JObject)fieldsJSON.GetValue(Constants.SDK_MODULE_METADATA)).ContainsKey(moduleName.ToLower()))
                {
                    var moduleMetaData = ((JObject)fieldsJSON.GetValue(Constants.SDK_MODULE_METADATA));

                    return (string)((JObject)moduleMetaData.GetValue(moduleName.ToLower())).GetValue(Constants.API_NAME);
                }
            }

            return moduleName;
        }

        static void SetHandlerAPIPath(string moduleAPIName, CommonAPIHandler handlerInstance)
        {
            if(handlerInstance == null)
            {
                return;
            }

            var apiPath = handlerInstance.APIPath;

            if(apiPath.ToLower().Contains(moduleAPIName.ToLower()))
            {
                var apiPathSplit = apiPath.Split('/');

                for(var i = 0; i < apiPathSplit.Length; i++)
                {
                    if(apiPathSplit[i].Equals(moduleAPIName, StringComparison.OrdinalIgnoreCase))
                    {
                        apiPathSplit[i] = moduleAPIName;
                    }
                    else if(Constants.DEFAULT_MODULENAME_VS_APINAME.ContainsKey(apiPathSplit[i].ToLower()))
                    {
                        apiPathSplit[i] = Constants.DEFAULT_MODULENAME_VS_APINAME[apiPathSplit[i].ToLower()];
                    }
                }

                apiPath = string.Join("/", apiPathSplit);

                handlerInstance.APIPath = apiPath;
            }
        }

        public static void GetFields(string moduleAPIName, CommonAPIHandler handlerInstance)
        {
            Utility.moduleAPIName = moduleAPIName;

            GetFieldsInfo(moduleAPIName, handlerInstance);
        }

        /// <summary>
        /// This method to fetch field details of the current module for the current user and store the result in a JSON file.
        /// </summary>
        /// <param name="moduleAPIName">A String containing the CRM module API name.</param>

        public static void GetFieldsInfo(string moduleAPIName, CommonAPIHandler handlerInstance)
        {
            lock (LOCK)
            {
                string recordFieldDetailsPath = null;

                string lastModifiedTime = null;

                try
                {
                    if (moduleAPIName != null && SearchJSONDetails(moduleAPIName) != null)
                    {
                        return;
                    }

                    var resourcesPath = Initializer.GetInitializer().ResourcePath + Path.DirectorySeparatorChar + Constants.FIELD_DETAILS_DIRECTORY;

                    if (!Directory.Exists(resourcesPath))
                    {
                        Directory.CreateDirectory(resourcesPath);
                    }

                    moduleAPIName = VerifyModuleAPIName(moduleAPIName);

                    SetHandlerAPIPath(moduleAPIName, handlerInstance);

                    if (handlerInstance != null && handlerInstance.ModuleAPIName == null && !Constants.SKIP_MODULES.Contains(moduleAPIName.ToLower()))
                    {
                        return;
                    }

                    recordFieldDetailsPath = GetFileName();

                    if (System.IO.File.Exists(recordFieldDetailsPath))
                    {
                        FileExistsFlow(moduleAPIName, recordFieldDetailsPath, lastModifiedTime);
                    }
                    else if(Initializer.GetInitializer().SDKConfig.AutoRefreshFields)
                    {
                        newFile = true;

                        FillDataType();

                        apiSupportedModule = apiSupportedModule.Count > 0 ? apiSupportedModule : GetModules(null);

                        var recordFieldDetailsJson = System.IO.File.Exists(recordFieldDetailsPath) ? Initializer.GetJSON(recordFieldDetailsPath) : new JObject();

                        recordFieldDetailsJson[Constants.FIELDS_LAST_MODIFIED_TIME] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                        foreach(var moduleData in apiSupportedModule)
                        {
                            var moduleName = moduleData.Key;

                            var metaData = (JObject)moduleData.Value;

                            if (!recordFieldDetailsJson.ContainsKey(moduleName.ToLower()))
                            {
                                recordFieldDetailsJson[moduleName.ToLower()] = new JObject();

                                using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                                {
                                    var serializer = new JsonSerializer();

                                    serializer.Serialize(sw, recordFieldDetailsJson);

                                    sw.Flush();

                                    sw.Close();
                                }

							    var fieldDetails = (JObject)GetFieldsDetails((string)metaData.GetValue(Constants.API_NAME));

							    recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

							    recordFieldDetailsJson[moduleName.ToLower()] = fieldDetails;

                                using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                                {
                                    var serializer = new JsonSerializer();

                                    serializer.Serialize(sw, recordFieldDetailsJson);

                                    sw.Flush();

                                    sw.Close();
                                }
                            }
                        }

                        newFile = false;
                    }
                    else if(forceRefresh && !getModifiedModules)
                    {
                        //New file - and force refresh by User
                        getModifiedModules = true;

                        var recordFieldDetailsJson = new JObject();

                        using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                        {
                            var serializer = new JsonSerializer();

                            serializer.Serialize(sw, recordFieldDetailsJson);

                            sw.Flush();

                            sw.Close();// file created with only dummy
                        }

                        ModifyFields(recordFieldDetailsPath, lastModifiedTime);

                        getModifiedModules = false;
                    }
                    else
                    {
                        FillDataType();

                        var recordFieldDetailsJson = new JObject();

                        recordFieldDetailsJson[moduleAPIName.ToLower()] = new JObject();

                        using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                        {
                            var serializer = new JsonSerializer();

                            serializer.Serialize(sw, recordFieldDetailsJson);

                            sw.Flush();

                            sw.Close();// file created with only dummy
                        }

                        var fieldsDetails = (JObject)GetFieldsDetails(moduleAPIName);

                        recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

                        recordFieldDetailsJson[moduleAPIName.ToLower()] = fieldsDetails;

                        using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                        {
                            var serializer = new JsonSerializer();

                            serializer.Serialize(sw, recordFieldDetailsJson);

                            sw.Flush();

                            sw.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    if (recordFieldDetailsPath == null || !System.IO.File.Exists(recordFieldDetailsPath)) throw e is SDKException sdkException
                        ? sdkException : new SDKException(Constants.EXCEPTION, e);
                    try
                    {
                        var recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

                        if(recordFieldDetailsJson.ContainsKey(moduleAPIName.ToLower()))
                        {
                            recordFieldDetailsJson.Remove(moduleAPIName.ToLower());
                        }

                        if(newFile)
                        {
                            if(recordFieldDetailsJson.GetValue(Constants.FIELDS_LAST_MODIFIED_TIME) != null)
                            {
                                recordFieldDetailsJson.Remove(Constants.FIELDS_LAST_MODIFIED_TIME);
                            }

                            newFile = false;
                        }

                        if(getModifiedModules || forceRefresh)
                        {
                            getModifiedModules = false;

                            forceRefresh = false;

                            if(lastModifiedTime != null)
                            {
                                recordFieldDetailsJson[Constants.FIELDS_LAST_MODIFIED_TIME] = lastModifiedTime;
                            }
                        }

                        using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                        {
                            var serializer = new JsonSerializer();

                            serializer.Serialize(sw, recordFieldDetailsJson);

                            sw.Flush();

                            sw.Close();
                        }
                    }
                    catch (IOException ex)
                    {
                        throw new SDKException(Constants.EXCEPTION, ex);
                    }

                    throw (e is SDKException exception) ? exception : new SDKException(Constants.EXCEPTION, e);
                }
            }
        }

        static void ModifyFields(string recordFieldDetailsPath, string modifiedTime)
	    {
		    var modifiedModules = GetModules(modifiedTime);

		    var recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

		    recordFieldDetailsJson[Constants.FIELDS_LAST_MODIFIED_TIME] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

			using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
			{
				var serializer = new JsonSerializer();

				serializer.Serialize(sw, recordFieldDetailsJson);

				sw.Flush();

				sw.Close();
			}

			if (modifiedModules.Count > 0)
		    {
			    foreach(var moduleMetaData in modifiedModules)
			    {
				    if(recordFieldDetailsJson.ContainsKey(moduleMetaData.Key.ToLower()))
				    {
					    DeleteFields(recordFieldDetailsJson, moduleMetaData.Key.ToLower());
				    }
			    }

				using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
				{
					var serializer = new JsonSerializer();

					serializer.Serialize(sw, recordFieldDetailsJson);

					sw.Flush();

					sw.Close();
				}

				foreach (var moduleMetaData in modifiedModules)
			    {
                    var moduleData = (JObject)moduleMetaData.Value;

				    GetFieldsInfo((string)moduleData.GetValue(Constants.API_NAME), null);
			    }
		    }
	    }

        public static void DeleteFields(JObject recordFieldDetailsJson, string module)
        {
            lock (LOCK)
            {
                var subformModules = new List<string>();

                var fieldsJSON = (JObject)recordFieldDetailsJson[module.ToLower()];

                foreach (var fieldDetails in fieldsJSON)
                {
                    var fieldDetail = (JObject)fieldDetails.Value;

                    if (fieldDetail.ContainsKey(Constants.SUBFORM) && (bool)fieldDetail[Constants.SUBFORM] && recordFieldDetailsJson.ContainsKey((string)fieldDetail[Constants.MODULE]))
                    {
                        subformModules.Add((string)fieldDetail[Constants.MODULE]);
                    }
                }

                recordFieldDetailsJson.Remove(module.ToLower());

                if (subformModules.Count > 0)
                {
                    foreach (var subformModule in subformModules)
                    {
                        DeleteFields(recordFieldDetailsJson, subformModule);
                    }
                }
            }
        }

        static string GetFileName()
	    {
            var resourcesPath = Initializer.GetInitializer().ResourcePath + Path.DirectorySeparatorChar + Constants.FIELD_DETAILS_DIRECTORY;

            var fileName = Initializer.GetInitializer().User.Email;

			fileName = fileName.Substring(0, fileName.IndexOf("@")) + Initializer.GetInitializer().Environment.GetUrl();

			var input = Encoding.UTF8.GetBytes(fileName);

			var str = Convert.ToBase64String(input);

			return resourcesPath + Path.DirectorySeparatorChar + str + ".json";
	    }

        public static void GetRelatedLists(string relatedModuleName, string moduleAPIName, CommonAPIHandler commonAPIHandler)
	    {
            lock (LOCK)
            {
                try
                {
                    var isNewData = false;

                    var key = (moduleAPIName + Constants.UNDERSCORE + Constants.RELATED_LISTS).ToLower();

                    var resourcesPath = Initializer.GetInitializer().ResourcePath + Path.DirectorySeparatorChar + Constants.FIELD_DETAILS_DIRECTORY;

                    if (!Directory.Exists(resourcesPath))
                    {
                        Directory.CreateDirectory(resourcesPath);
                    }

                    var recordFieldDetailsPath = GetFileName();

                    if (!System.IO.File.Exists(recordFieldDetailsPath) || (System.IO.File.Exists(recordFieldDetailsPath) && (!Initializer.GetJSON(recordFieldDetailsPath).ContainsKey(key) || (Initializer.GetJSON(recordFieldDetailsPath).GetValue(key) == null || ((JArray)Initializer.GetJSON(recordFieldDetailsPath).GetValue(key)).Count <= 0))))
                    {
                        isNewData = true;

                        moduleAPIName = VerifyModuleAPIName(moduleAPIName);

                        var relatedListValues = GetRelatedListDetails(moduleAPIName);

                        var recordFieldDetailsJSON1 = System.IO.File.Exists(recordFieldDetailsPath) ? Initializer.GetJSON(recordFieldDetailsPath) : new JObject();

                        recordFieldDetailsJSON1[key] = relatedListValues;

                        using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                        {
                            var serializer = new JsonSerializer();

                            serializer.Serialize(sw, recordFieldDetailsJSON1);

                            sw.Flush();

                            sw.Close();
                        }
                    }

                    var recordFieldDetailsJSON = Initializer.GetJSON(recordFieldDetailsPath);

                    var modulerelatedList = recordFieldDetailsJSON.ContainsKey(key) ? (JArray)recordFieldDetailsJSON.GetValue(key) : new JArray();

                    if (!CheckRelatedListExists(relatedModuleName, modulerelatedList, commonAPIHandler) && !isNewData)
                    {
                        recordFieldDetailsJSON.Remove(key);

                        using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
                        {
                            var serializer = new JsonSerializer();

                            serializer.Serialize(sw, recordFieldDetailsJSON);

                            sw.Flush();

                            sw.Close();
                        }

                        GetRelatedLists(relatedModuleName, moduleAPIName, commonAPIHandler);
                    }
                }
                catch (SDKException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw new SDKException(Constants.EXCEPTION, e);
                }
            }
	    }

        static bool CheckRelatedListExists(string relatedModuleName, JArray modulerelatedListJA, CommonAPIHandler commonAPIHandler)
	    {
		    foreach (JObject relatedListJO in modulerelatedListJA)
		    {
			    if(relatedListJO.ContainsKey(Constants.API_NAME) && relatedListJO.GetValue(Constants.API_NAME) != null && relatedListJO.GetValue(Constants.API_NAME).ToString().Equals(relatedModuleName, StringComparison.OrdinalIgnoreCase))
			    {
                    if(relatedListJO[Constants.HREF].Equals(Constants.NULL_VALUE))
                    {
                        throw new SDKException(Constants.UNSUPPORTED_IN_API, commonAPIHandler.HttpMethod + " " + commonAPIHandler.APIPath + Constants.UNSUPPORTED_IN_API_MESSAGE);
                    }

                    if (relatedListJO.ContainsKey(Constants.MODULE) && !string.IsNullOrEmpty(relatedListJO[Constants.MODULE].ToString()))
                    {
                        commonAPIHandler.ModuleAPIName = (string)relatedListJO[Constants.MODULE];

                        GetFieldsInfo((string)relatedListJO[Constants.MODULE], commonAPIHandler);
                    }

				    return true;
			    }
		    }

		    return false;
	    }

        static JArray GetRelatedListDetails(string moduleAPIName)
	    {
		    var relatedListsOperations = new RelatedListsOperations(moduleAPIName);

		    var response = relatedListsOperations.GetRelatedLists();

		    var relatedListJA = new JArray();

		    if(response != null)
		    {
			    if(response.StatusCode == Constants.NO_CONTENT_STATUS_CODE)
			    {
				    return relatedListJA;
			    }

			    if(response.IsExpected)
			    {
					var responseHandler = response.Object;

				    if(responseHandler is ResponseWrapper responseWrapper)
				    {
                        var relatedLists = (List<RelatedList>) responseWrapper.RelatedLists;

					    foreach(var relatedList in relatedLists)
					    {
						    var relatedListDetail = new JObject
                            {
                                { Constants.API_NAME, relatedList.APIName },
                                { Constants.MODULE, relatedList.Module },
                                { Constants.NAME, relatedList.Name },
                                { Constants.HREF, relatedList.Href }
                            };

                            relatedListJA.Add(relatedListDetail);
					    }
				    }
				    else if(responseHandler is APIException exception)
				    {
                        var errorResponse = new JObject
                        {
                            { Constants.CODE, exception.Code.Value },
                            { Constants.STATUS, exception.Status.Value },
                            { Constants.MESSAGE, exception.Message.Value }
                        };

                        throw new SDKException(Constants.API_EXCEPTION, errorResponse);
				    }
			    }
			    else
			    {
				    var errorResponse = new JObject { { Constants.CODE, response.StatusCode } };

                    throw new SDKException(Constants.API_EXCEPTION, errorResponse);
			    }
		    }

		    return relatedListJA;
	    }

	    ///<summary>
	    ///This method to get module field data from Zoho CRM.
	    ///</summary>
	    ///<param name="moduleAPIName">A String containing the CRM module API name.</param>
	    ///<returns>A Object representing the Zoho CRM module field details.</returns>
	    public static object GetFieldsDetails(string moduleAPIName)
	    {
		    var fieldsDetails = new JObject();

		    var fieldOperation = new FieldsOperations(moduleAPIName);

		    var response = fieldOperation.GetFields(new ParameterMap());

		    if(response != null)
		    {
			    if(response.StatusCode == Constants.NO_CONTENT_STATUS_CODE)
			    {
				    return fieldsDetails;
			    }

			    //Check if expected response is received
			    if(response.IsExpected)
			    {
					var responseHandler = response.Object;

				    if (responseHandler is Com.Zoho.Crm.API.Fields.ResponseWrapper responseWrapper)
				    {
                        var fields = (List<Field>) responseWrapper.Fields;

					    foreach (var field in fields)
					    {
							var keyName = field.APIName;

						    if (Constants.KEYS_TO_SKIP.Contains(keyName))
						    {
							    continue;
						    }

						    var fieldDetail = new JObject();

						    SetDataType(fieldDetail, field, moduleAPIName);

						    fieldsDetails[field.APIName] = fieldDetail;
					    }

					    if(Constants.INVENTORY_MODULES.Contains(moduleAPIName.ToLower()))
					    {
						    var fieldDetail = new JObject
                            {
                                { Constants.NAME, Constants.LINE_TAX },
                                { Constants.TYPE, Constants.LIST_NAMESPACE },
                                { Constants.STRUCTURE_NAME, Constants.LINE_TAX_NAMESPACE },
                                { Constants.LOOKUP, true }
                            };

                            fieldsDetails.Add(Constants.LINE_TAX, fieldDetail);
					    }

                        if (Constants.NOTES.Equals(moduleAPIName, StringComparison.OrdinalIgnoreCase))
                        {
                            var fieldDetail = new JObject
                            {
                                { Constants.NAME, Constants.ATTACHMENTS },
                                { Constants.TYPE, Constants.LIST_NAMESPACE },
                                { Constants.STRUCTURE_NAME, Constants.ATTACHMENTS_NAMESPACE }
                            };

                            fieldsDetails.Add(Constants.ATTACHMENTS, fieldDetail);
                        }
				    }
				    else if(responseHandler is Fields.APIException exception)
				    {
                        var errorResponse = new JObject
                        {
                            { Constants.CODE, exception.Code.Value },
                            { Constants.STATUS, exception.Status.Value },
                            { Constants.MESSAGE, exception.Message.Value }
                        };

                        var exception1 = new SDKException(Constants.API_EXCEPTION, errorResponse);

                        if(Utility.moduleAPIName.Equals(moduleAPIName, StringComparison.OrdinalIgnoreCase))
                        {
                            throw exception1;
                        }

                        SDKLogger.LogError(JsonConvert.SerializeObject(exception1));
                    }
			    }
			    else
			    {
				    var errorResponse = new JObject { { Constants.CODE, response.StatusCode } };

                    throw new SDKException(Constants.API_EXCEPTION, errorResponse);
			    }
		    }

		    return fieldsDetails;
	    }

        public static JObject SearchJSONDetails(string key)
        {
            key = Constants.PACKAGE_NAMESPACE + ".Record." + key;

            if(JSONDETAILS == null)
            {
                JSONDETAILS = Initializer.jsonDetails;
            }

            foreach (var member in JSONDETAILS)
            {
                var keyInJSON = member.Key;

                if(keyInJSON.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    var returnJSON = new JObject
                    {
                        { Constants.MODULEPACKAGENAME, keyInJSON },
                        { Constants.MODULEDETAILS, (JObject)member.Value }
                    };

                    return returnJSON;
                }
            }

            return null;
        }

        public static bool VerifyPhotoSupport(string moduleAPIName)
        {
            lock(LOCK)
            {
                try
                {
                    moduleAPIName = VerifyModuleAPIName(moduleAPIName);

                    if (Constants.PHOTO_SUPPORTED_MODULES.Contains(moduleAPIName.ToLower()))
                    {
                        return true;
                    }

                    var modules = GetModuleNames();

                    if(modules.ContainsKey(moduleAPIName.ToLower()))
                    {
                        var moduleMetaData = (JObject)modules.GetValue(moduleAPIName.ToLower());

                        if(moduleMetaData.ContainsKey(Constants.GENERATED_TYPE) && !((string)moduleMetaData.GetValue(Constants.GENERATED_TYPE)).Equals(Constants.GENERATED_TYPE_CUSTOM))
                        {
                            throw new SDKException(Constants.UPLOAD_PHOTO_UNSUPPORTED_ERROR, Constants.UPLOAD_PHOTO_UNSUPPORTED_MESSAGE + moduleAPIName);
                        }
                    }
                }
                catch (SDKException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    var exception = new SDKException(Constants.EXCEPTION, e);

                    throw exception;
                }

                return true;
            }
        }

        static JObject GetModuleNames()
        {
            var moduleData = new JObject();

            var resourcesPath = Initializer.GetInitializer().ResourcePath + Path.DirectorySeparatorChar + Constants.FIELD_DETAILS_DIRECTORY;

            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }

            var recordFieldDetailsPath = GetFileName();

            if (!System.IO.File.Exists(recordFieldDetailsPath) || (System.IO.File.Exists(recordFieldDetailsPath) && (!Initializer.GetJSON(recordFieldDetailsPath).ContainsKey(Constants.SDK_MODULE_METADATA) || (Initializer.GetJSON(recordFieldDetailsPath).GetValue(Constants.SDK_MODULE_METADATA) == null || ((JObject)Initializer.GetJSON(recordFieldDetailsPath).GetValue(Constants.SDK_MODULE_METADATA)).Count <= 0))))
            {
                moduleData = GetModules(null);

                WriteModuleMetaData(recordFieldDetailsPath, moduleData);

                return moduleData;
            }

            var recordFieldDetailsJson = Initializer.GetJSON(recordFieldDetailsPath);

            return (JObject)recordFieldDetailsJson.GetValue(Constants.SDK_MODULE_METADATA);
        }

        static void WriteModuleMetaData(string recordFieldDetailsPath, JObject moduleData)
        {
            var recordFieldDetailsJSON = System.IO.File.Exists(recordFieldDetailsPath) ? Initializer.GetJSON(recordFieldDetailsPath) : new JObject();

            recordFieldDetailsJSON[Constants.SDK_MODULE_METADATA] = moduleData;

            using (var sw = System.IO.File.CreateText(recordFieldDetailsPath))
            {
                var serializer = new JsonSerializer();

                serializer.Serialize(sw, recordFieldDetailsJSON);

                sw.Flush();

                sw.Close();
            }
        }

        static JObject GetModules(string header)
	    {
            var apiNames = new JObject();

		    var headerMap = new HeaderMap();

		    if(header != null)
		    {
				var headerValue = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(header));

                var targetTime = TimeZoneInfo.ConvertTime(headerValue, TimeZoneInfo.Local);

                headerMap.Add(GetModulesHeader.IF_MODIFIED_SINCE, targetTime);
		    }

		    var response = new ModulesOperations().GetModules(headerMap);

		    if(response != null)
		    {
                if (new List<int>() { Constants.NO_CONTENT_STATUS_CODE, Constants.NOT_MODIFIED_STATUS_CODE }.Contains(response.StatusCode))
                {
                    return apiNames;
                }

                // Check if expected response is received
                if (response.IsExpected)
                {
                    var responseObject = response.Object;

                    if (responseObject is Com.Zoho.Crm.API.Modules.ResponseWrapper wrapper)
                    {
                        var modules = wrapper.Modules;

                        foreach (var module in modules)
                        {
                            if (module.APISupported != null && (bool)module.APISupported)
                            {
                                var moduleDetails = new JObject
                                {
                                    { Constants.API_NAME, module.APIName },
                                    { Constants.GENERATED_TYPE, module.GeneratedType.Value }
                                };

                                apiNames.Add(module.APIName.ToLower(), moduleDetails);
                            }
                        }
                    }
                    else if (responseObject is Com.Zoho.Crm.API.Modules.APIException exception)
                    {
                        var errorResponse = new JObject
                        {
                            { Constants.CODE, exception.Code.Value },
                            { Constants.STATUS, exception.Status.Value },
                            { Constants.MESSAGE, exception.Message.Value }
                        };

                        throw new SDKException(Constants.API_EXCEPTION, errorResponse);
                    }
                }
		    }

            if(header == null)
            {
                try
                {
                    var resourcesPath = Initializer.GetInitializer().ResourcePath + Path.DirectorySeparatorChar + Constants.FIELD_DETAILS_DIRECTORY;

                    if (!Directory.Exists(resourcesPath))
                    {
                        Directory.CreateDirectory(resourcesPath);
                    }

                    WriteModuleMetaData(GetFileName(), apiNames);
                }
                catch (IOException ex)
                {
                    throw new SDKException(Constants.EXCEPTION, ex);
                }
            }

		    return apiNames;
	    }

        public static void RefreshModules()
        {
            lock (LOCK)
            {
                forceRefresh = true;

                GetFieldsInfo(null, null);

                forceRefresh = false;
            }
        }

        public static JObject GetJSONObject(JObject json, string key)
        {
            foreach(var entry in json)
            {
                var keyInJSON = entry.Key;

                if(keyInJSON.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return (JObject)entry.Value;
                }
            }

            return null;
        }

        static void SetDataType(JObject fieldDetail, Field field, string moduleAPIName)
        {
            var apiType = field.DataType;

            var keyName = field.APIName;

            var module = "";

            if(field.SystemMandatory != null && field.SystemMandatory == true && !(moduleAPIName.Equals(Constants.CALLS, StringComparison.OrdinalIgnoreCase) && keyName.Equals(Constants.CALL_DURATION, StringComparison.OrdinalIgnoreCase)))
            {
                fieldDetail.Add(Constants.REQUIRED, true);
            }

            if (keyName.Equals(Constants.PRICING_DETAILS, StringComparison.OrdinalIgnoreCase) && moduleAPIName.Equals(Constants.PRICE_BOOKS, StringComparison.OrdinalIgnoreCase))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LIST_NAMESPACE);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.PRICINGDETAILS);

                fieldDetail.Add(Constants.SKIP_MANDATORY, true);

                return;
            }
            else if(keyName.Equals(Constants.PARTICIPANT_API_NAME, StringComparison.OrdinalIgnoreCase) && (moduleAPIName.Equals(Constants.EVENTS, StringComparison.OrdinalIgnoreCase) || moduleAPIName.Equals(Constants.ACTIVITIES, StringComparison.OrdinalIgnoreCase)))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LIST_NAMESPACE);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.PARTICIPANTS);

                fieldDetail.Add(Constants.SKIP_MANDATORY, true);

                return;
            }
            else if (keyName.Equals(Constants.COMMENTS, StringComparison.OrdinalIgnoreCase) && (moduleAPIName.Equals(Constants.SOLUTIONS, StringComparison.OrdinalIgnoreCase) || moduleAPIName.Equals(Constants.CASES, StringComparison.OrdinalIgnoreCase)))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LIST_NAMESPACE);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.COMMENT_NAMESPACE);

                fieldDetail.Add(Constants.LOOKUP, true);

                return;
            }
            else if(keyName.Equals(Constants.LAYOUT, StringComparison.OrdinalIgnoreCase))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LAYOUT_NAMESPACE);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.LAYOUT_NAMESPACE);

                fieldDetail.Add(Constants.LOOKUP, true);

                return;

            }
            else if((keyName.Equals(Constants.TERRITORIES, StringComparison.OrdinalIgnoreCase) || keyName.Equals(Constants.TERRITORY,StringComparison.OrdinalIgnoreCase)) && field.CustomField != null && field.CustomField == false)
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LIST_NAMESPACE);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.TERRITORY_NAMESPACE);

                fieldDetail.Add(Constants.LOOKUP, true);

                return;

            }
            else if (keyName.Equals(Constants.PRODUCT_NAME, StringComparison.OrdinalIgnoreCase) && Constants.INVENTORY_MODULES_ITEMS.Contains(moduleAPIName.ToLower()))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LINEITEM_PRODUCT);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.LINEITEM_PRODUCT);

                fieldDetail.Add(Constants.SKIP_MANDATORY, true);

                return;
            }
            else if (keyName.Equals(Constants.DISCOUNT, StringComparison.OrdinalIgnoreCase) && Constants.INVENTORY_MODULES_ITEMS.Contains(moduleAPIName.ToLower()))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.CSHARP_STRING_NAME);

                return;
            }
            else if (keyName.Equals(Constants.TAX, StringComparison.OrdinalIgnoreCase) && Constants.PRODUCTS.Equals(moduleAPIName, StringComparison.OrdinalIgnoreCase))
            {
                fieldDetail.Add(Constants.NAME, keyName);

                fieldDetail.Add(Constants.TYPE, Constants.LIST_NAMESPACE);

                fieldDetail.Add(Constants.STRUCTURE_NAME, Constants.TAX_NAMESPACE);

                return;
            }
            else if(apiTypeVsDataType.ContainsKey(apiType))
            {
                fieldDetail[Constants.TYPE] = apiTypeVsDataType[apiType];
            }
            else if(apiType.Equals(Constants.FORMULA, StringComparison.OrdinalIgnoreCase))
            {
                if(field.Formula != null)
                {
                    var returnType = field.Formula.ReturnType;

                    if(returnType != null && apiTypeVsDataType.ContainsKey(returnType) && apiTypeVsDataType[returnType] != null)
                    {
                        fieldDetail[Constants.TYPE] = apiTypeVsDataType[returnType];
                    }
                }

                fieldDetail[Constants.READ_ONLY] = true;
            }
            else
            {
                return;
            }

            if(apiType.ToLower().Contains(Constants.LOOKUP))
            {
                fieldDetail[Constants.LOOKUP] = true;
            }

            if (apiType.ToLower().Equals(Constants.CONSENT_LOOKUP, StringComparison.OrdinalIgnoreCase) || apiType.ToLower().Equals(Constants.OWNER_LOOKUP, StringComparison.OrdinalIgnoreCase))
            {
                fieldDetail[Constants.SKIP_MANDATORY] =  true;
            }

            if(apiType.Equals(Constants.MULTI_SELECT_LOOKUP, StringComparison.OrdinalIgnoreCase))
            {
                fieldDetail[Constants.SKIP_MANDATORY] = true;

                if(field.Multiselectlookup != null && field.Multiselectlookup.LinkingModule != null)
                {
                    var linkingModule = field.Multiselectlookup.LinkingModule;

                    fieldDetail[Constants.MODULE] = linkingModule;

                    module = linkingModule;
                }

                fieldDetail[Constants.SUBFORM] = true;
            }

            if (apiType.Equals(Constants.MULTI_USER_LOOKUP, StringComparison.OrdinalIgnoreCase))
            {
                fieldDetail[Constants.SKIP_MANDATORY] = true;

                if (field.Multiuserlookup != null && field.Multiuserlookup.LinkingModule != null)
                {
                    var linkingModule = field.Multiuserlookup.LinkingModule;

                    fieldDetail[Constants.MODULE] = linkingModule;

                    module = linkingModule;
                }

                fieldDetail[Constants.SUBFORM] = true;
            }

            if (apiTypeVsStructureName.ContainsKey(apiType))
            {
                fieldDetail[Constants.STRUCTURE_NAME] = apiTypeVsStructureName[apiType];
            }

            if(apiType.Equals(Constants.PICKLIST, StringComparison.OrdinalIgnoreCase) && (field.PickListValues != null && field.PickListValues.Count > 0))
            {
                fieldDetail.Add(Constants.PICKLIST, true);

                var values = new JArray();

                field.PickListValues.ForEach(plv => values.Add(plv.DisplayValue));

                fieldDetail[Constants.VALUES] = values;
            }

            if(apiType.Equals(Constants.SUBFORM, StringComparison.OrdinalIgnoreCase) && field.Subform != null)
            {
                module = field.Subform.Module_1;

                fieldDetail[Constants.MODULE] = module;

                fieldDetail[Constants.SKIP_MANDATORY] = true;

			    fieldDetail[Constants.SUBFORM] = true;
            }

            if(apiType.Equals(Constants.LOOKUP, StringComparison.OrdinalIgnoreCase) && field.Lookup != null)
            {
                module = field.Lookup.Module_1;

                if(module != null && !module.Equals(Constants.SE_MODULE, StringComparison.OrdinalIgnoreCase))
                {
                    fieldDetail[Constants.MODULE] = module;

                    if(module.Equals(Constants.ACCOUNTS, StringComparison.OrdinalIgnoreCase) && (field.CustomField != null && !(bool)field.CustomField))
                    {
                        fieldDetail[Constants.SKIP_MANDATORY] = true;
                    }
                }
                else
                {
                    module = "";
                }

                fieldDetail[Constants.LOOKUP] = true;
            }

            if (module.Length > 0)
            {
                GetFieldsInfo(module, null);
            }

            fieldDetail[Constants.NAME] = keyName;
        }

        static void FillDataType()
        {
            if (apiTypeVsDataType.Count > 0)
            {
                return;
            }

            var fieldAPINamesString = new string[] { "textarea", "text", "website", "email", "phone", "mediumtext", "profileimage", "autonumber"};

            var fieldAPINamesInteger = new string[] { "integer" };

            var fieldAPINamesBoolean = new string[] { "boolean" };

            var fieldAPINamesLong = new string[] { "long", "bigint" };

            var fieldAPINamesDouble = new string[] { "double", "percent", "lookup", "currency" };

            var fieldAPINamesFieldFile = new string[] { "fileupload" };

            var fieldAPINamesDateTime = new string[] { "datetime", "event_reminder" };

            var fieldAPINamesDate = new string[] { "date" };

            var fieldAPINamesLookup = new string[] { "lookup" };

            var fieldAPINamesPickList = new string[] { "picklist" };

            var fieldAPINamesMultiSelectPickList = new string[] { "multiselectpicklist" };

            var fieldAPINamesSubForm = new string[] { "subform" };

            var fieldAPINamesOwnerLookUp = new string[] { "ownerlookup", "userlookup" };

            var fieldAPINamesMultiUserLookUp = new string[] { "multiuserlookup" };

            var fieldAPINamesMultiModuleLookUp = new string[] { "multimodulelookup" };

            var fieldAPINameTaskRemindAt = new string[] { "ALARM" };

            var fieldAPINameRecurringActivity = new string[] {"RRULE"};

            var fieldAPINameReminder = new string[] {"multireminder"};

            var fieldAPINameConsentLookUp = new string[] { "consent_lookup" };

            var fieldAPINameImageUpload = new string[] { "imageupload" };

            var fieldAPInameMultiSelectLookUp = new string[] { "multiselectlookup" };

            var fieldAPINameLineTax = new string[] {"linetax"};


            foreach (var fieldAPIName in fieldAPINamesString)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.CSHARP_STRING_NAME;
            }

            foreach (var fieldAPIName in fieldAPINamesInteger)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.CSHARP_INT_NAME;
            }

            foreach (var fieldAPIName in fieldAPINamesBoolean)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.CSHARP_BOOLEAN_NAME;
            }

            foreach (var fieldAPIName in fieldAPINamesLong)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.CSHARP_LONG_NAME;
            }

            foreach (var fieldAPIName in fieldAPINamesDouble)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.CSHARP_DOUBLE_NAME;
            }

            foreach (var fieldAPIName in fieldAPINamesDateTime)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.SYSTEM_DATETIME_OFFSET;
            }

            foreach (var fieldAPIName in fieldAPINamesDate)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.SYSTEM_DATETIME;
            }

            foreach (var fieldAPIName in fieldAPINamesLookup)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.RECORD_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.RECORD_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesPickList)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.CHOICE_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesMultiSelectPickList)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.CHOICE_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesSubForm)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.RECORD_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesOwnerLookUp)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.USER_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.USER_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesMultiUserLookUp)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.RECORD_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesMultiModuleLookUp)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.MODULE_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINamesFieldFile)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.FIELD_FILE_NAMESPACE;
            }

            foreach(var fieldAPIName in fieldAPINameTaskRemindAt)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.REMINDAT_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.REMINDAT_NAMESPACE;
            }

            foreach(var fieldAPIName in fieldAPINameRecurringActivity)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.RECURRING_ACTIVITY_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.RECURRING_ACTIVITY_NAMESPACE;
            }

            foreach(var fieldAPIName in fieldAPINameReminder)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.REMINDER_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINameConsentLookUp)
            {
                apiTypeVsDataType.Add(fieldAPIName, Constants.CONSENT_NAMESPACE);

                apiTypeVsStructureName.Add(fieldAPIName, Constants.CONSENT_NAMESPACE);
            }

            foreach (var fieldAPIName in fieldAPINameImageUpload)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.IMAGEUPLOAD_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPInameMultiSelectLookUp)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.RECORD_NAMESPACE;
            }

            foreach (var fieldAPIName in fieldAPINameLineTax)
            {
                apiTypeVsDataType[fieldAPIName] = Constants.LIST_NAMESPACE;

                apiTypeVsStructureName[fieldAPIName] = Constants.LINETAX;
            }
        }
    }
}
