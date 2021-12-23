using System;

using System.Collections;

using System.Collections.Generic;

using System.IO;

using System.Net;

using System.Reflection;

using System.Text;

using Com.Zoho.API.Exception;

using Newtonsoft.Json.Linq;

namespace Com.Zoho.Crm.API.Util
{
    /// <summary>
    /// This class is to process the upload file and stream.
    /// </summary>
    public class FormDataConverter : Converter
    {
        public FormDataConverter(CommonAPIHandler commonAPIHandler) : base(commonAPIHandler) {}

        Dictionary<string, List<object>> uniqueValuesMap = new Dictionary<string, List<object>>();

        public override object FormRequest(object requestInstance, string pack, int? instanceNumber, JObject classMemberDetail)
        {
            var classDetail = (JObject)Initializer.jsonDetails.GetValue(pack); // JSONdetails of class

            var request = new Dictionary<string, object>();

            foreach (var data in classDetail) // all members
            {
                object modification = null;

                var memberName = data.Key;

                MethodInfo method = null;

                var memberDetail = (JObject)classDetail.GetValue(memberName);

                if ((memberDetail.ContainsKey(Constants.READ_ONLY) && (bool)memberDetail.GetValue(Constants.READ_ONLY)) || !memberDetail.ContainsKey(Constants.NAME))
                {
                    continue;
                }

                try
                {
                    method = requestInstance.GetType().GetMethod(Constants.IS_KEY_MODIFIED);

                    var param = new object[1];

                    param[0] = memberDetail.GetValue(Constants.NAME).ToString();

                    modification = method.Invoke(requestInstance, param);
                }
                catch (Exception ex)
                {
                    throw new SDKException(Constants.EXCEPTION_IS_KEY_MODIFIED , ex);
                }

                // check required
                if ((modification == null || (int)modification == 0) && memberDetail.ContainsKey(Constants.REQUIRED) && (bool)memberDetail.GetValue(Constants.REQUIRED))
                {
                    throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.MANDATORY_KEY_ERROR + memberName);
                }

                var field = GetPrivateFieldInfo(requestInstance.GetType(), memberName);

                if (field != null)
                {
                    var fieldValue = field.GetValue(requestInstance);// value of the member

                    if (modification != null && (int)modification != 0 && fieldValue != null && ValueChecker(requestInstance.GetType().FullName, memberName, memberDetail, fieldValue, uniqueValuesMap, instanceNumber) == true)
                    {
                        var keyName = (string)memberDetail.GetValue(Constants.NAME);

                        var type = (string)memberDetail.GetValue(Constants.TYPE);

                        if (type.Equals(Constants.LIST_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                        {
                            request.Add(keyName, SetJSONArray(fieldValue, memberDetail));
                        }
                        else if (type.Equals(Constants.MAP_NAMESPACE, StringComparison.OrdinalIgnoreCase))
                        {
                            request.Add(keyName, SetJSONObject(fieldValue, memberDetail));
                        }
                        else if (memberDetail.ContainsKey(Constants.STRUCTURE_NAME))
                        {
                            var fieldData = FormRequest(fieldValue, (string)memberDetail.GetValue(Constants.STRUCTURE_NAME), 1, memberDetail);

                            request.Add(keyName, fieldData != null ? JToken.FromObject(fieldData) : JValue.CreateNull());
                        }
                        else
                        {
                            request.Add(keyName, fieldValue);
                        }
                    }
                }
            }

            return request;
        }

        public override void AppendToRequest(HttpWebRequest requestBase, object requestObject)
        {
            var boundary = "----FILEBOUNDARY----";

            requestBase.ContentType = "multipart/form-data; boundary=" + boundary;

            Stream fileDataStream = new MemoryStream();

            var boundarybytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");

            var endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--");

            var headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: application/octet-stream\r\n\r\n";

            if (requestObject is IDictionary)
            {
                AddFileBody(requestObject, fileDataStream, boundarybytes, endBoundaryBytes, headerTemplate);
            }

            fileDataStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);

            requestBase.ContentLength = fileDataStream.Length;

            using (var requestStream = requestBase.GetRequestStream())
            {
                fileDataStream.Position = 0;

                var tempBuffer = new byte[fileDataStream.Length];

                fileDataStream.Read(tempBuffer, 0, tempBuffer.Length);

                fileDataStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }
        }

        void AddFileBody(object requestObject, Stream fileDataStream, byte[] boundarybytes, byte[] endBoundaryBytes, string headerTemplate)
        {
            var requestObjectMap = (Dictionary<string, object>)requestObject;

            foreach (var requestData in requestObjectMap)
            {
                if (requestData.Value is IList keysDetail)
                {
                    foreach (var fileObject in keysDetail)
                    {
                        if (fileObject is StreamWrapper streamWrapper)
                        {
                            fileDataStream.Write(boundarybytes, 0, boundarybytes.Length);

                            var header = string.Format(headerTemplate, requestData.Key, streamWrapper.Name);

                            var headerbytes = Encoding.UTF8.GetBytes(header);

                            fileDataStream.Write(headerbytes, 0, headerbytes.Length);

                            var buffer = new byte[1024];

                            var bytesRead = 0;

                            while ((bytesRead = streamWrapper.Stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                fileDataStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                else if (requestData.Value is StreamWrapper streamWrapper)
                {
                    fileDataStream.Write(boundarybytes, 0, boundarybytes.Length);

                    var header = string.Format(headerTemplate, requestData.Key, streamWrapper.Name);

                    var headerbytes = Encoding.UTF8.GetBytes(header);

                    fileDataStream.Write(headerbytes, 0, headerbytes.Length);

                    var buffer = new byte[1024];

                    var bytesRead = 0;

                    while ((bytesRead = streamWrapper.Stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        fileDataStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        JObject SetJSONObject(object fieldValue, JObject memberDetail)
        {
            var jsonObject = new JObject();

            var requestObject = (IDictionary)fieldValue;

            if (memberDetail == null)
            {
                foreach (KeyValuePair<object, object> requestObjectDetails in requestObject)
                {
                    var data = RedirectorForObjectToJSON(requestObjectDetails.Value);

                    jsonObject.Add((string)requestObjectDetails.Key, data != null ? JToken.FromObject(data) : JValue.CreateNull());
                }
            }
            else
            {
                var keysDetail = (JArray)memberDetail.GetValue(Constants.KEYS);

                for (var keyIndex = 0; keyIndex < keysDetail.Count; keyIndex++)
                {
                    var keyDetail = (JObject)keysDetail[keyIndex];

                    var keyName = (string)keyDetail.GetValue(Constants.NAME);

                    object keyValue = null;

                    if (requestObject.Contains(keyName) && requestObject[keyName] != null)
                    {
                        if (keyDetail.ContainsKey(Constants.STRUCTURE_NAME))
                        {
                            keyValue = FormRequest(requestObject[keyName], (string)keyDetail.GetValue(Constants.STRUCTURE_NAME), 1, memberDetail);

                            jsonObject.Add(keyName, keyValue != null? JToken.FromObject(keyValue) : JValue.CreateNull());
                        }
                        else
                        {
                            keyValue = requestObject[keyName];

                            var data = RedirectorForObjectToJSON(keyValue);

                            jsonObject.Add(keyName, data != null ? JToken.FromObject(data) : JValue.CreateNull());
                        }
                    }
                }
            }

            return jsonObject;
        }

        IList SetJSONArray(object fieldValue, JObject memberDetail)
        {
            IList listObject = new List<object>();

            var requestObjects = (IList)fieldValue;

            if (memberDetail == null)
            {
                foreach (var request in requestObjects)
                {
                    listObject.Add(RedirectorForObjectToJSON(request));
                }
            }
            else
            {
                if (memberDetail.ContainsKey(Constants.STRUCTURE_NAME))
                {
                    var instanceCount = 0;

                    var pack = (string)memberDetail.GetValue(Constants.STRUCTURE_NAME);

                    foreach (var request in requestObjects)
                    {
                        listObject.Add(FormRequest(request, pack, ++instanceCount, memberDetail));
                    }
                }
                else
                {
                    foreach (var request in requestObjects)
                    {
                        listObject.Add(RedirectorForObjectToJSON(request));
                    }
                }
            }

            return listObject;
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
            return GetResponse(response, pack);
        }

        public override object GetResponse(object response, string pack)
        {
            throw new NotImplementedException();
        }
    }
}
