using System;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Net;

using System.Reflection;

using System.Text;

using Com.Zoho.API.Exception;

using Com.Zoho.Crm.API.Logger;

using Com.Zoho.Crm.API.Record;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;

namespace Com.Zoho.Crm.API.Util
{
    /// <summary>
    /// This class processes the API response object to the POJO object and POJO object to a JSON object.
    /// </summary>
    public class JSONConverter : Converter
    {
        public JSONConverter(CommonAPIHandler commonAPIHandler) : base(commonAPIHandler) { }

        Dictionary<string, List<object>> uniqueValuesMap = new Dictionary<string, List<object>>();

        public override void AppendToRequest(HttpWebRequest requestBase, object requestObject)
        {
            var dataString = requestObject.ToString();

            var data = Encoding.UTF8.GetBytes(dataString);

            var dataLength = data.Length;

            requestBase.ContentLength = dataLength;

            using (var writer = requestBase.GetRequestStream())
            {
                writer.Write(data, 0, dataLength);
            }
        }

        public override object FormRequest(object requestInstance, string pack, int? instanceNumber, JObject memberDetail)
        {
            var classDetail = (JObject)Initializer.jsonDetails.GetValue(pack); // JSONdetails of class

            if (classDetail.ContainsKey(Constants.INTERFACE) && (bool)classDetail.GetValue(Constants.INTERFACE))
            {
                var classes = (JArray)classDetail[Constants.CLASSES];

                var requestObjectClassName = requestInstance.GetType().FullName;

                foreach (object className in classes)
                {
                    if (Convert.ToString(className).Equals(requestObjectClassName, StringComparison.OrdinalIgnoreCase))
                    {
                        classDetail = (JObject)Initializer.jsonDetails.GetValue(requestObjectClassName);

                        break;
                    }
                }
            }

            if (requestInstance is Record.Record)
            {
                var moduleAPIName = commonAPIHandler.ModuleAPIName;

                var requestJSON = IsRecordRequest(requestInstance, classDetail, instanceNumber, memberDetail);

                commonAPIHandler.ModuleAPIName = moduleAPIName;

                return requestJSON;
            }
            else
            {
                return IsNotRecordRequest(requestInstance, classDetail, instanceNumber, memberDetail);
            }
        }

        JObject IsNotRecordRequest(object requestInstance, JObject classDetail, int? instanceNumber, JObject classMemberDetail)
        {
            var lookUp = false;

            var skipMandatory = false;

            string classMemberName = null;

            if (classMemberDetail != null)
            {
                lookUp = classMemberDetail.ContainsKey(Constants.LOOKUP) && (bool)classMemberDetail[Constants.LOOKUP];

                skipMandatory = classMemberDetail.ContainsKey(Constants.SKIP_MANDATORY) && (bool)classMemberDetail[Constants.SKIP_MANDATORY];

                var name = classMemberDetail.ContainsKey(Constants.NAME) ? classMemberDetail[Constants.NAME].ToString() : "";

                classMemberName = BuildName(name);
            }

            var requestJSON = new JObject();

            var requiredKeys = new Dictionary<string, bool>();

            var primaryKeys = new Dictionary<string, bool>();

            var requiredInUpdateKeys = new Dictionary<string, bool>();

            foreach (var data in classDetail) // all members
            {
                var memberName = data.Key;

                var memberDetail = (JObject)data.Value;

                object modification = null;

                if ((memberDetail.ContainsKey(Constants.READ_ONLY) && (bool)memberDetail.GetValue(Constants.READ_ONLY)) || !memberDetail.ContainsKey(Constants.NAME))
                {
                    continue;
                }

                var keyName = (string)memberDetail[Constants.NAME];

                try
                {
                    var isKeyModified = requestInstance.GetType().GetMethod(Constants.IS_KEY_MODIFIED);

                    var param = new object[1];

                    param[0] = memberDetail.GetValue(Constants.NAME).ToString();

                    modification = isKeyModified.Invoke(requestInstance, param);
                }
                catch (Exception ex)
                {
                    throw new SDKException(Constants.EXCEPTION_IS_KEY_MODIFIED, ex);
                }

                var required = memberDetail.ContainsKey(Constants.REQUIRED) && (bool)memberDetail[Constants.REQUIRED];

                if (memberDetail.ContainsKey(Constants.REQUIRED) && required)
                {
                    requiredKeys.Add(keyName, true);
                }

                var requiredInUpdate = memberDetail.ContainsKey(Constants.REQUIRED_IN_UPDATE) && (bool)memberDetail[Constants.REQUIRED_IN_UPDATE];

                if (requiredInUpdate)
                {
                    requiredInUpdateKeys.Add(keyName, true);
                }

                var primary = memberDetail.ContainsKey(Constants.PRIMARY) && (bool)memberDetail[Constants.PRIMARY];

                if (memberDetail.ContainsKey(Constants.PRIMARY) && primary && (!memberDetail.ContainsKey(Constants.REQUIRED_IN_UPDATE) || (bool)memberDetail[Constants.REQUIRED_IN_UPDATE]))
                {
                    primaryKeys.Add(keyName, true);
                }

                if (modification == null || (int)modification == 0) continue;
                
                var field = GetPrivateFieldInfo(requestInstance.GetType(), memberName);

                var fieldValue = field.GetValue(requestInstance);

                if (ValueChecker(requestInstance.GetType().FullName, memberName, memberDetail, fieldValue, uniqueValuesMap, instanceNumber) == true)// set if not null
                {
                    if (fieldValue != null)
                    {
                        requiredKeys.Remove(keyName);

                        primaryKeys.Remove(keyName);

                        requiredInUpdateKeys.Remove(keyName);
                    }

                    if (requestInstance is FileDetails)
                    {
                        if(fieldValue == null || (fieldValue is string && fieldValue.ToString().ToLower().Equals("null")))
                        {
                            requestJSON.Add(keyName.ToLower(), JValue.CreateNull());
                        }
                        else
                        {
                            requestJSON.Add(keyName.ToLower(), JToken.FromObject(fieldValue));
                        }
                    }
                    else
                    {
                        var recordData = SetData(memberDetail, fieldValue);

                        requestJSON.Add((string)memberDetail.GetValue(Constants.NAME) ?? throw new InvalidOperationException(), recordData != null ? JToken.FromObject(recordData) : JValue.CreateNull());
                    }
                }
            }

            if (skipMandatory || CheckException(classMemberName, requestInstance, instanceNumber, lookUp, requiredKeys, primaryKeys, requiredInUpdateKeys))
            {
                return requestJSON;
            }

            return requestJSON;
        }

        bool CheckException(string memberName, object requestInstance, int? instanceNumber, bool lookUp, Dictionary<string, bool> requiredKeys, Dictionary<string, bool> primaryKeys, Dictionary<string, bool> requiredInUpdateKeys)
        {
            if (requiredInUpdateKeys.Any() && commonAPIHandler.CategoryMethod.Equals(Constants.REQUEST_CATEGORY_UPDATE, StringComparison.OrdinalIgnoreCase))
            {
                var error = new JObject
                {
                    { Constants.FIELD, memberName },
                    { Constants.TYPE, requestInstance.GetType().FullName },
                    { Constants.KEYS, string.Join(",", requiredInUpdateKeys.Keys) }
                };

                if (instanceNumber != null)
                {
                    error.Add(Constants.INSTANCE_NUMBER, instanceNumber);
                }

                throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.MANDATORY_KEY_ERROR, error, null);
            }

            if (commonAPIHandler.MandatoryChecker)
            {
                if (commonAPIHandler.CategoryMethod.Equals(Constants.REQUEST_CATEGORY_CREATE, StringComparison.OrdinalIgnoreCase))
                {
                    if (lookUp)
                    {
                        if (primaryKeys.Count > 0)
                        {
                            var error = new JObject
                            {
                                { Constants.FIELD, memberName },
                                { Constants.TYPE, requestInstance.GetType().FullName },
                                { Constants.KEYS, string.Join(",", primaryKeys.Keys) }
                            };

                            if (instanceNumber != null)
                            {
                                error.Add(Constants.INSTANCE_NUMBER, instanceNumber);
                            }

                            throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.PRIMARY_KEY_ERROR, error, null);
                        }
                    }
                    else if (requiredKeys.Count > 0)
                    {
                        var error = new JObject
                        {
                            { Constants.FIELD, memberName },
                            { Constants.TYPE, requestInstance.GetType().FullName },
                            { Constants.KEYS, string.Join(",", requiredKeys.Keys) }
                        };

                        if (instanceNumber != null)
                        {
                            error.Add(Constants.INSTANCE_NUMBER, instanceNumber);
                        }

                        throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.MANDATORY_KEY_ERROR, error, null);
                    }
                }

                if (commonAPIHandler.CategoryMethod.Equals(Constants.REQUEST_CATEGORY_UPDATE, StringComparison.OrdinalIgnoreCase) && primaryKeys.Count > 0)
                {
                    var error = new JObject
                    {
                        { Constants.FIELD, memberName },
                        { Constants.TYPE, requestInstance.GetType().FullName },
                        { Constants.KEYS, string.Join(",", primaryKeys.Keys) }
                    };

                    if (instanceNumber != null)
                    {
                        error.Add(Constants.INSTANCE_NUMBER, instanceNumber);
                    }

                    throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.PRIMARY_KEY_ERROR, error, null);
                }
            }
            else if (lookUp && commonAPIHandler.CategoryMethod.Equals(Constants.REQUEST_CATEGORY_UPDATE, StringComparison.OrdinalIgnoreCase))
            {
                if (primaryKeys.Count > 0)
                {
                    var error = new JObject
                    {
                        { Constants.FIELD, memberName },
                        { Constants.TYPE, requestInstance.GetType().FullName },
                        { Constants.KEYS, string.Join(",", primaryKeys.Keys) }
                    };

                    if(instanceNumber != null)
                    {
                        error.Add(Constants.INSTANCE_NUMBER, instanceNumber);
                    }

                    throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.PRIMARY_KEY_ERROR, error, null);
                }
            }

            return true;
        }

        JObject IsRecordRequest(object recordInstance, JObject classDetail, int? instanceNumber, JObject memberDetail)
        {
            var lookUp = false;

            var skipMandatory = false;

            string classMemberName = null;

            if (memberDetail != null)
            {
                lookUp = memberDetail.ContainsKey(Constants.LOOKUP) ? (bool)memberDetail[Constants.LOOKUP] : false;

                skipMandatory = memberDetail.ContainsKey(Constants.SKIP_MANDATORY) ? (bool)memberDetail[Constants.SKIP_MANDATORY] : false;

                classMemberName = memberDetail.ContainsKey(Constants.NAME) ? memberDetail[Constants.NAME].ToString() : "";
            }

            var requestJSON = new JObject();

            var moduleDetail = new JObject();

            var moduleAPIName = commonAPIHandler.ModuleAPIName;

            if (moduleAPIName != null)// entry
            {
                commonAPIHandler.ModuleAPIName = null;

                var fullDetail = Utility.SearchJSONDetails(moduleAPIName);// to get correct moduleapiname in proper format

                if (fullDetail != null)// from Jsondetails
                {
                    moduleDetail = (JObject)fullDetail[Constants.MODULEDETAILS];
                }
                else// from user spec
                {
                    moduleDetail = GetModuleDetailFromUserSpecJSON(moduleAPIName);
                }
            }
            else// inner case
            {
                moduleDetail = classDetail;

                classDetail = (JObject)Initializer.jsonDetails[Constants.RECORD_NAMESPACE];
            } // class detail must contain record structure at this point

            var cl = recordInstance.GetType();

            var scl = cl.BaseType;

            if (scl.FullName.Equals(Constants.RECORD_NAMESPACE))
            {
                cl = scl;
            }

            var keyValueField = GetPrivateFieldInfo(cl, Constants.KEY_VALUES);

            var keyModifiedField = GetPrivateFieldInfo(cl, Constants.KEY_MODIFIED);

            var keyValues = (Dictionary<string, object>)keyValueField.GetValue(recordInstance);

            var keyModified = (Dictionary<string, int?>)keyModifiedField.GetValue(recordInstance);

            var requiredKeys = new Dictionary<string, bool>();

            var primaryKeys = new Dictionary<string, bool>();

            if (!skipMandatory)
            {
                foreach (var keyDetail in moduleDetail)
                {
                    var keyDetails = (JObject)keyDetail.Value;

                    var name = (string)keyDetails[Constants.NAME];

                    if (keyDetails.ContainsKey(Constants.REQUIRED) && (bool)keyDetails[Constants.REQUIRED])
                    {
                        requiredKeys[name] = true;
                    }

                    if (keyDetails.ContainsKey(Constants.PRIMARY) && (bool)keyDetails[Constants.PRIMARY])
                    {
                        primaryKeys[name] = true;
                    }
                }

                foreach (var keyDetail in classDetail)
                {
                    var keyDetails = (JObject)keyDetail.Value;

                    var name = keyDetail.Key;

                    if (keyDetails.ContainsKey(Constants.REQUIRED) && (bool)keyDetails[Constants.REQUIRED])
                    {
                        requiredKeys[name] = true;
                    }

                    if (keyDetails.ContainsKey(Constants.PRIMARY) && (bool)keyDetails[Constants.PRIMARY])
                    {
                        primaryKeys[name] = true;
                    }
                }
            }

            foreach (var keyDetail in keyModified)
            {
                var keyName = keyDetail.Key;

                if (keyDetail.Value != null && keyDetail.Value != 1)
                {
                    continue;
                }

                var keyDetails = new JObject();

                var keyValue = keyValues.ContainsKey(keyName) ? keyValues[keyName] : null;

                object jsonValue = null;

                var memberName = BuildName(keyName);

                if (moduleDetail.Count > 0 && (moduleDetail.ContainsKey(keyName) || moduleDetail.ContainsKey(memberName)))
                {
                    if (moduleDetail.ContainsKey(keyName))
                    {
                        keyDetails = (JObject)moduleDetail[keyName];// incase of user spec json
                    }
                    else
                    {
                        keyDetails = (JObject)moduleDetail[memberName];// json details
                    }
                }
                else if (classDetail.ContainsKey(memberName))
                {
                    keyDetails = (JObject)classDetail[memberName];
                }

                if (keyDetails.Count > 0)
                {
                    if ((keyDetails.ContainsKey(Constants.READ_ONLY) && (bool)keyDetails[Constants.READ_ONLY]) || !keyDetails.ContainsKey(Constants.NAME))// read only or no keyName
                    {
                        continue;
                    }

                    if (ValueChecker(cl.GetType().FullName, memberName, keyDetails, keyValue, uniqueValuesMap, instanceNumber) == true)
                    {
                        jsonValue = SetData(keyDetails, keyValue);
                    }
                }
                else
                {
                    jsonValue = RedirectorForObjectToJSON(keyValue);
                }

                if (keyValue != null)
                {
                    requiredKeys.Remove(keyName);

                    primaryKeys.Remove(keyName);
                }

                requestJSON.Add(keyName, jsonValue != null ? JToken.FromObject(jsonValue) : JValue.CreateNull());
            }

            if (skipMandatory || CheckException(classMemberName, recordInstance, instanceNumber, lookUp, requiredKeys, primaryKeys, new Dictionary<string, bool>()))
            {
                return requestJSON;
            }

            return requestJSON;
        }

        object SetData(JObject memberDetail, object fieldValue)
        {
            if (fieldValue != null)
            {
                var type = (string)memberDetail[Constants.TYPE];

                if (type.Equals(Constants.LIST_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                {
                    return SetJSONArray(fieldValue, memberDetail);
                }
                else if (type.Equals(Constants.MAP_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                {
                    return SetJSONObject(fieldValue, memberDetail);
                }
                else if (type.Equals(Constants.CHOICE_NAMESPACE) || (memberDetail.ContainsKey(Constants.STRUCTURE_NAME) && ((string)memberDetail[Constants.STRUCTURE_NAME]).Equals(Constants.CHOICE_NAMESPACE)))
                {
                    var t = fieldValue.GetType();

                    var prop = t.GetProperty("Value");

                    return prop.GetValue(fieldValue);
                }
                else if (memberDetail.ContainsKey(Constants.STRUCTURE_NAME) && memberDetail.ContainsKey(Constants.MODULE))
                {
                    return IsRecordRequest(fieldValue, GetModuleDetailFromUserSpecJSON((string)memberDetail[Constants.MODULE]), null, memberDetail);
                }
                else if (memberDetail.ContainsKey(Constants.STRUCTURE_NAME))
                {
                    return FormRequest(fieldValue, (string)memberDetail[Constants.STRUCTURE_NAME], null, memberDetail);
                }
                else
                {
                    var t = Type.GetType(Constants.DATATYPECONVERTER.Replace(Constants._TYPE, type));

                    var method = t.GetMethod(Constants.POST_CONVERT);

                    return method.Invoke(null, new object[] { fieldValue, type });
                }
            }

            return JValue.CreateNull();
        }

        JObject SetJSONObject(object fieldValue, JObject memberDetail)
        {
            var jsonObject = new JObject();

            var requestObject = (IDictionary)fieldValue;

            if (requestObject.Count > 0)
            {
                if (memberDetail == null || (memberDetail != null && !memberDetail.ContainsKey(Constants.KEYS)))
                {
                    foreach (var key in requestObject.Keys)
                    {
                        var data = RedirectorForObjectToJSON(requestObject[key]);

                        jsonObject.Add((string)key, data != null ? JToken.FromObject(data) : JValue.CreateNull());
                    }
                }
                else
                {
                    if (memberDetail.ContainsKey(Constants.KEYS))
                    {
                        var keysDetail = (JArray)memberDetail.GetValue(Constants.KEYS);

                        for (var keyIndex = 0; keyIndex < keysDetail.Count; keyIndex++)
                        {
                            var keyDetail = (JObject)keysDetail[keyIndex];

                            var keyName = (string)keyDetail.GetValue(Constants.NAME);

                            object keyValue = null;

                            if (requestObject.Contains(keyName) && requestObject[keyName] != null)
                            {
                                keyValue = SetData(keyDetail, requestObject[keyName]);

                                jsonObject.Add(keyName, keyValue != null ? JToken.FromObject(keyValue) : JValue.CreateNull());
                            }
                        }
                    }
                }
            }

            return jsonObject;
        }

        JArray SetJSONArray(object fieldValue, JObject memberDetail)
        {
            var jsonArray = new JArray();

            var requestObjects = (IList)fieldValue;

            if (requestObjects.Count > 0)
            {
                if (memberDetail == null || (memberDetail != null && !memberDetail.ContainsKey(Constants.STRUCTURE_NAME)))
                {
                    foreach (var request in requestObjects)
                    {
                        jsonArray.Add(RedirectorForObjectToJSON(request));
                    }
                }
                else
                {
                    var pack = (string)memberDetail.GetValue(Constants.STRUCTURE_NAME);

                    if (pack.Equals(Constants.CHOICE_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var request in requestObjects)
                        {
                            var field = GetPrivateFieldInfo(request.GetType(), "value");

                            jsonArray.Add(field.GetValue(request));
                        }
                    }
                    else if (memberDetail.ContainsKey(Constants.MODULE) && memberDetail[Constants.MODULE] != null)
                    {
                        var instanceCount = 0;

                        foreach (var request in requestObjects)
                        {
                            jsonArray.Add(IsRecordRequest(request, GetModuleDetailFromUserSpecJSON((string)memberDetail[Constants.MODULE]), instanceCount, memberDetail));

                            instanceCount++;
                        }
                    }
                    else
                    {
                        var instanceCount = 0;

                        foreach (var request in requestObjects)
                        {
                            jsonArray.Add(FormRequest(request, pack, instanceCount, memberDetail));

                            instanceCount++;
                        }
                    }
                }
            }

            return jsonArray;
        }

        object RedirectorForObjectToJSON(object request)
        {
            if (request is IList)
            {
                return SetJSONArray(request, null);
            }
            else if (request is IDictionary)
            {
                return SetJSONObject(request, null);
            }
            else
            {
                return request;
            }
        }

        public override object GetWrappedResponse(object response, string pack)
        {
            var responseEntity = ((HttpWebResponse)response);

            var responseString = new StreamReader(responseEntity.GetResponseStream()).ReadToEnd();

            responseEntity.Close();

            if (responseString != null && !string.IsNullOrEmpty(responseString) && !string.IsNullOrWhiteSpace(responseString))
            {
                // convert string to stream
                var byteArray = Encoding.UTF8.GetBytes(responseString);

                var stream = new MemoryStream(byteArray);

                var responseStream = new JsonTextReader(new StreamReader(stream));

                var serializer = new JsonSerializer
                {
                    DateParseHandling = DateParseHandling.None
                };

                var responseJson = serializer.Deserialize<JObject>(responseStream);

                return GetResponse(responseJson, pack);
            }

            return null;
        }

        public override object GetResponse(object response, string packageName)
        {
            var classDetail = (JObject)Initializer.jsonDetails.GetValue(packageName); // JSONdetails of class

            var value = response is JValue jValue ? jValue : null;

            if (response == null || response.ToString().Equals("null") || (response.ToString().Trim()).Length == 0 || (value != null && value.Value == null))
            {
                return null;
            }

            var responseJson = (JObject)response;

            object instance = null;

            if (classDetail.ContainsKey(Constants.INTERFACE) && (bool)classDetail[Constants.INTERFACE])// if interface
            {
                var classes = (JArray)classDetail[Constants.CLASSES];

                instance = FindMatch(classes, responseJson);// find match returns instance(calls getresponse() recursively)
            }
            else
            {
                try
                {
                    // create an instance of that type
                    instance = Activator.CreateInstance(Type.GetType(packageName));
                }
                catch (MissingMethodException) //when there is no public constructor- invoke the private constructor
                {
                    instance = Activator.CreateInstance(Type.GetType(packageName), true);
                }

                if (instance is Record.Record)
                {
                    var moduleAPIName = commonAPIHandler.ModuleAPIName;

                    instance = IsRecordResponse(responseJson, classDetail, packageName);

                    commonAPIHandler.ModuleAPIName = moduleAPIName;
                }
                else
                {
                    instance = NotRecordResponse(instance, responseJson, classDetail);// based on json details data will be assigned
                }
            }

            return instance;
        }

        object NotRecordResponse(object instance, JObject responseJson, JObject classDetail)
        {
            foreach (var member in classDetail)
            {
                var memberName = member.Key;

                var keyDetail = (JObject)classDetail[memberName];// JSONdetails of the member

                var keyName = keyDetail.ContainsKey(Constants.NAME) ? (string)keyDetail[Constants.NAME] : null;// api-name of the member

                if ((keyName != null && !string.IsNullOrEmpty(keyName) && !string.IsNullOrWhiteSpace(keyName)) && responseJson.ContainsKey(keyName) && responseJson[keyName] != null)
                {
                    object keyData = responseJson[keyName];

                    var field = GetPrivateFieldInfo(instance.GetType(), memberName);

                    var memberValue = GetData(keyData, keyDetail, field);

                    field.SetValue(instance, memberValue);
                }
            }

            return instance;
        }

        object IsRecordResponse(JObject responseJSON, JObject classDetail, String pack)
        {
            var recordInstance = Activator.CreateInstance(Type.GetType(pack));

            var moduleAPIName = commonAPIHandler.ModuleAPIName;

            var moduleDetail = new JObject();

            if (moduleAPIName != null)// entry
            {
                commonAPIHandler.ModuleAPIName = null;

                var fullDetail = Utility.SearchJSONDetails(moduleAPIName);// to get correct moduleapiname in proper format

                if (fullDetail != null)// from Jsondetails
                {
                    moduleDetail = (JObject)fullDetail[Constants.MODULEDETAILS];

                    recordInstance = Activator.CreateInstance(Type.GetType((string)fullDetail[Constants.MODULEPACKAGENAME]));
                }
                else// from user spec
                {
                    moduleDetail = GetModuleDetailFromUserSpecJSON(moduleAPIName);
                }
            }

            moduleDetail.Merge(classDetail, new JsonMergeSettings
            {
                // union array values together to avoid duplicates
                MergeArrayHandling = MergeArrayHandling.Union
            });

            var recordDetail = (JObject)Initializer.jsonDetails[Constants.RECORD_NAMESPACE];

            // after above steps, recordDetail must always contain record structure detail,module detail could be any,entry case pack detail is record

            var cl = recordInstance.GetType();

            var scl = cl.BaseType;

            if (scl.FullName.Equals(Constants.RECORD_NAMESPACE))
            {
                cl = scl;
            }

            var field = GetPrivateFieldInfo(cl, Constants.KEY_VALUES);

            var keyValues = new Dictionary<string, object>();

            foreach (var member in responseJSON)
            {
                var keyName = member.Key;

                var memberName = BuildName(keyName);

                var keyDetail = new JObject();

                if (moduleDetail.Count > 0 && (moduleDetail.ContainsKey(keyName) || moduleDetail.ContainsKey(memberName)))
                {
                    if (moduleDetail.ContainsKey(keyName))
                    {
                        keyDetail = (JObject)moduleDetail[keyName];// incase of user spec json
                    }
                    else
                    {
                        keyDetail = (JObject)moduleDetail[memberName];// json details
                    }
                }
                else if (recordDetail.ContainsKey(memberName))
                {
                    keyDetail = (JObject)recordDetail[memberName];
                }

                object keyValue = null;

                object keyData = member.Value;

                if (keyDetail.Count > 0)
                {
                    keyName = (string)keyDetail[Constants.NAME];

                    keyValue = GetData(keyData, keyDetail, field);
                }
                else// if not key detail
                {
                    keyValue = RedirectorForJSONToObject(keyData);
                }

                keyValues.Add(keyName, keyValue);
            }

            field.SetValue(recordInstance, keyValues);

            return recordInstance;
        }

        object GetData(object keyData, JObject memberDetail, FieldInfo field)
        {
            object memberValue = null;

            if (keyData != null)
            {
                if ((keyData is JValue value && value.Value == null) || keyData.ToString() == Constants.NULL_VALUE)
                {
                    return memberValue;
                }

                var type = (string)memberDetail[Constants.TYPE];

                if (type.Equals(Constants.LIST_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                {
                    memberValue = GetCollectionsData((JArray)keyData, memberDetail);
                }
                else if (type.Equals(Constants.MAP_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                {
                    memberValue = GetMapData((JObject)keyData, memberDetail);
                }
                else if (type.Equals(Constants.CHOICE_NAMESPACE) || (memberDetail.ContainsKey(Constants.STRUCTURE_NAME) && ((string)memberDetail[Constants.STRUCTURE_NAME]).Equals(Constants.CHOICE_NAMESPACE)))
                {
                    if(field != null && field.FieldType.FullName.Contains(Constants.CSHARP_NULL_TYPE_NAME))
                    {
                        var t = Type.GetType(CSharpName(field.FieldType));

                        memberValue = Activator.CreateInstance(field.FieldType, ChangeType(((JValue)keyData).Value, t));
                    }
                    else
                    {
                        var valueType = ((JValue)keyData).Value.GetType().FullName;

                        var t = Type.GetType(Constants.CHOICE.Replace(Constants._TYPE, valueType));

                        memberValue = Activator.CreateInstance(t, ((JValue)keyData).Value);
                    }
                }
                else if (memberDetail.ContainsKey(Constants.STRUCTURE_NAME) && memberDetail.ContainsKey(Constants.MODULE))
                {
                    memberValue = IsRecordResponse((JObject)keyData, GetModuleDetailFromUserSpecJSON((string)memberDetail[Constants.MODULE]), (string)memberDetail[Constants.STRUCTURE_NAME]);
                }
                else if (memberDetail.ContainsKey(Constants.STRUCTURE_NAME))
                {
                    memberValue = GetResponse(keyData, (string)memberDetail[Constants.STRUCTURE_NAME]);
                }
                else
                {
                    var t = Type.GetType(Constants.DATATYPECONVERTER.Replace(Constants._TYPE, type));

                    var method = t.GetMethod(Constants.PRE_CONVERT);

                    memberValue = method.Invoke(null, new object[] { keyData, type });
                }
            }

            return memberValue;
        }


        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }


        public static string CSharpName(Type type)
        {
            var sb = new StringBuilder();

            var name = type.Name;

            if (!type.IsGenericType) return name;

            foreach(var genericArgument in type.GenericTypeArguments)
            {
                var sb1 = new StringBuilder();

                sb1.Append(genericArgument.Namespace).Append(".").Append(genericArgument.Name);

                if (sb1.ToString().Equals(Constants.CSHARP_NULL_TYPE_NAME, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var genericArgument1 in genericArgument.GenericTypeArguments)
                    {
                        sb.Append(genericArgument1.Namespace).Append(".").Append(genericArgument1.Name);
                    }
                }
            }

            return sb.ToString();
        }

        object GetMapData(JObject response, JObject memberDetail)
        {
            var mapInstance = new Dictionary<string, object>();

            if (response.Count > 0)
            {
                if (memberDetail == null || (memberDetail != null && !memberDetail.ContainsKey(Constants.KEYS)))
                {
                    foreach (var responseData in response)
                    {
                        mapInstance.Add(responseData.Key, RedirectorForJSONToObject(responseData.Value));
                    }
                }
                else
                {
                    if (memberDetail.ContainsKey(Constants.KEYS))
                    {
                        var keysDetail = (JArray)memberDetail[Constants.KEYS];

                        for (var keyIndex = 0; keyIndex < keysDetail.Count; keyIndex++)
                        {
                            var keyDetail = (JObject)keysDetail[keyIndex];

                            var keyName = (string)keyDetail[Constants.NAME];

                            object keyValue = null;

                            if (response.ContainsKey(keyName) && response[keyName] != null)
                            {
                                keyValue = GetData(response[keyName], keyDetail, null);

                                mapInstance.Add(keyName, keyValue);
                            }
                        }
                    }
                }
            }

            return mapInstance;
        }

        object GetCollectionsData(JArray responses, JObject memberDetail)
        {
            var values = new List<object>();

            if (responses.Count > 0)
            {
                if (memberDetail == null || (memberDetail != null && !memberDetail.ContainsKey(Constants.STRUCTURE_NAME)))
                {
                    foreach (object response in responses)
                    {
                        values.Add(RedirectorForJSONToObject(response));
                    }
                }
                else// need to have structure Name in memberDetail
                {
                    var pack = (string)memberDetail[Constants.STRUCTURE_NAME];

                    if (pack.Equals(Constants.CHOICE_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (object response in responses)
                        {
                            var keyData = (JToken)response;

                            var tokenType = keyData.Type;

                            if (response.GetType().Name.Equals("JValue", StringComparison.OrdinalIgnoreCase))
                            {
                                var type = Type.GetType(Constants.CHOICE.Replace(Constants._TYPE, GetType(tokenType)));

                                values.Add(Activator.CreateInstance(type, ((JValue)response).Value));
                            }
                            else
                            {
                                var type = Type.GetType(Constants.CHOICE.Replace(Constants._TYPE, GetType(tokenType)));

                                values.Add(Activator.CreateInstance(type, ((JValue)response).Value));
                            }
                        }
                    }
                    else if (memberDetail.ContainsKey(Constants.MODULE) && memberDetail[Constants.MODULE] != null)
                    {
                        foreach (object response in responses)
                        {
                            values.Add(IsRecordResponse((JObject)response, GetModuleDetailFromUserSpecJSON((string)memberDetail[Constants.MODULE]), pack));
                        }
                    }
                    else
                    {
                        foreach (Object response in responses)
                        {
                            values.Add(GetResponse(response, pack));
                        }
                    }
                }
            }

            IList list = null;

            if (values is List<Object> list1 && memberDetail != null)
            {
                var listTypeName = memberDetail[Constants.TYPE] + "[" + memberDetail[Constants.STRUCTURE_NAME] + "]";

                var type = list1.Count > 0 ? list1[0].GetType().FullName : null;

                var type1 = type;

                if (type != null && (type.Contains("JValue") || (memberDetail.ContainsKey(Constants.STRUCTURE_NAME) && memberDetail[Constants.STRUCTURE_NAME].ToString().Equals(Constants.CHOICE_NAMESPACE, StringComparison.OrdinalIgnoreCase))))
                {
                    type1 = type;

                    if (type.Contains("JValue"))
                    {
                        var keyData = (JToken)list1[0];

                        var tokenType = keyData.Type;

                        if(memberDetail[Constants.TYPE].Contains("Choice"))
                        {
                            type1 = Constants.CHOICE.Replace(Constants._TYPE, GetType(tokenType));
                        }
                        else
                        {
                            type1 = GetType(tokenType);
                        }
                    }

                    listTypeName = memberDetail[Constants.TYPE] + "[" + type1 + "]";
                }

                var t = Type.GetType(listTypeName);

                if (list == null && t != null && (memberDetail.ContainsKey(Constants.STRUCTURE_NAME) || type1 != null))
                {
                    list = (IList)Activator.CreateInstance(t);
                }

                foreach (var record in list1)
                {
                    var value = record is JValue jValue ? jValue : null;

                    if (record != null && (value != null && value.Value == null) && record.GetType().Name.Equals("JValue"))
                    {
                        var keyData = (JToken)list1[0];

                        var tokenType = keyData.Type;

                        if (record.GetType().Name.Equals("JValue", StringComparison.OrdinalIgnoreCase))
                        {
                            t = Type.GetType(Constants.CHOICE.Replace(Constants._TYPE, GetType(tokenType)));

                            list.Add(Activator.CreateInstance(t, ((JValue)record).Value));
                        }
                        else
                        {
                            t = Type.GetType(Constants.CHOICE.Replace(Constants._TYPE, GetType(tokenType)));

                            list.Add(Activator.CreateInstance(t, ((JValue)record).Value));
                        }
                    }
                    else
                    {
                        if(!memberDetail.ContainsKey(Constants.STRUCTURE_NAME))
                        {
                            var dataTypeConverter = Type.GetType(Constants.DATATYPECONVERTER.Replace(Constants._TYPE, type1));

                            var method = dataTypeConverter.GetMethod(Constants.PRE_CONVERT);

                            list.Add(method.Invoke(null, new Object[] { record, type }));
                        }
                        else
                        {
                            list.Add(record);
                        }
                    }
                }

                return list;
            }

            return values;
        }

        JObject GetModuleDetailFromUserSpecJSON(string module)
        {
            var moduleDetail = new JObject();

            var recordFieldDetailsPath = Initializer.GetInitializer().ResourcePath + Path.DirectorySeparatorChar + Constants.FIELD_DETAILS_DIRECTORY + Path.DirectorySeparatorChar + GetEncodedFileName();

            return Utility.GetJSONObject(Initializer.GetJSON((recordFieldDetailsPath)), module);
        }

        object RedirectorForJSONToObject(object keyData)
        {
            if (keyData is JObject data)
            {
                return GetMapData(data, null);
            }
            else if (keyData is JArray array)
            {
                return GetCollectionsData(array, null);
            }
            else
            {
                return keyData;
            }
        }

        object FindMatch(JArray classes, JObject responseJson)
        {
            var pack = "";

            float ratio = 0;

            foreach (string className in classes)
            {
                var matchRatio = FindRatio(className, responseJson);

                if (matchRatio == 1.0)
                {
                    pack = (string)className;

                    ratio = 1;

                    break;
                }
                else if (matchRatio > ratio)
                {
                    ratio = matchRatio;

                    pack = (string)className;
                }
            }

            return GetResponse(responseJson, pack);
        }

        float FindRatio(string className, JObject responseJson)
        {
            var classDetail = (JObject)Initializer.jsonDetails.GetValue(className); // JSONdetails of class

            float totalPoints = classDetail.Count;

            float matches = 0;

            if (totalPoints == 0)
            {
                return 0;
            }
            else
            {
                foreach (var memberName in classDetail)
                {
                    var memberDetail = (JObject)memberName.Value;

                    var keyName = memberDetail.ContainsKey(Constants.NAME) ? (string)memberDetail[Constants.NAME] : null;

                    if ((keyName != null && !string.IsNullOrEmpty(keyName) && !string.IsNullOrWhiteSpace(keyName)) && responseJson.ContainsKey(keyName) && responseJson[keyName] != null)
                    {// key not empty
                        var keyData = responseJson[keyName];

                        var tokenType = keyData.Type;

                        var structureName = memberDetail.ContainsKey(Constants.STRUCTURE_NAME) ? (string)memberDetail[Constants.STRUCTURE_NAME] : null;

                        var type = GetType(tokenType);

                        if (type.Equals((string)memberDetail[Constants.TYPE]))
                        {
                            matches++;
                        }
                        else if (keyName.Equals(Constants.COUNT, StringComparison.OrdinalIgnoreCase) && type.Equals(Constants.CSHARP_INT_NAME, StringComparison.OrdinalIgnoreCase) && tokenType == JTokenType.Integer)
                        {
                            matches++;
                        }
                        else if (((string)memberDetail[Constants.TYPE]).Equals(Constants.CHOICE_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                        {
                            var values = (JArray)memberDetail[Constants.VALUES];

                            foreach (object value in values)
                            {
                                if (value.Equals(keyData))
                                {
                                    matches++;

                                    break;
                                }
                            }
                        }

                        if ((structureName != null && !string.IsNullOrEmpty(structureName) && !string.IsNullOrWhiteSpace(structureName)) && structureName.Equals((string)memberDetail[Constants.TYPE]))
                        {
                            if (memberDetail.ContainsKey(Constants.VALUES))
                            {
                                var values = (JArray)memberDetail[Constants.VALUES];

                                foreach (object value in values)
                                {
                                    if (value.Equals(keyData))
                                    {
                                        matches++;

                                        break;
                                    }
                                }
                            }
                            else
                            {
                                matches++;
                            }
                        }
                    }
                }
            }
            return matches / totalPoints;
        }

        public static string BuildName(string memberName)
        {
            var name = memberName.ToLower().Split('_').ToList();

            var sdkName = "";

            int index;


            if (name.Count <= 0)
            {
                index = 1;
            }

            sdkName = string.Concat(name[0].Substring(0, 1).ToLower(), name[0].Substring(1));

            index = 1;

            for (var nameIndex = index; nameIndex < name.Count; nameIndex++)
            {
                var firstLetterUppercase = "";

                if (name[nameIndex].Length > 0)
                {
                    firstLetterUppercase = string.Concat(name[nameIndex].Substring(0, 1).ToUpper(), name[nameIndex].Substring(1));
                }

                sdkName = string.Concat(sdkName, firstLetterUppercase);
            }

            return sdkName;
        }

        public static string GetProperMethodName(string methodName)
        {
            var method = "";

            if (!string.IsNullOrEmpty(methodName))
            {
                if (methodName[0].Equals("_"))
                {
                    method = char.ToUpper(methodName[1]) + methodName.Substring(2);
                }
                else if (methodName.Contains("_"))
                {
                    var splits = methodName.Split('_');

                    for (var i = 0; i < splits.Length; i++)
                    {
                        method += char.ToUpper(splits[i][0]) + splits[i].Substring(1);
                    }
                }
                else
                {
                    method += char.ToUpper(methodName[0]) + methodName.Substring(1);
                }
            }
            return method;
        }

        public static object ConvertList(List<object> value, Type type)
        {
            var list = (IList)Activator.CreateInstance(type);

            foreach (var item in value)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
