using System;
using System.Collections;
using System.Collections.Generic;

using System.Globalization;

using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Com.Zoho.Crm.API.Util
{
	/// <summary>
	/// This class converts JSON value to the expected data type.
	/// </summary>
	/// <typeparam name="T">T is CSharp permitted data type</typeparam>
	public class DataTypeConverter<T>
    {
	    delegate T PreConverter(object obj);

	    delegate object PostConverter(T obj);

	    static readonly Dictionary<string, PreConverter> PRE_CONVERTER_MAP = new Dictionary<string, PreConverter>();

	    static readonly Dictionary<string, PostConverter> POST_CONVERTER_MAP = new Dictionary<string, PostConverter>();

		static readonly PreConverter preCommonTypeConverter = (obj) =>
		{
			return (T)Convert.ChangeType(obj, typeof(T));
		};

		static readonly PostConverter postCommonTypeConverter = (obj) =>
		{
			return (T)Convert.ChangeType(obj, typeof(T));
		};

		/// <summary>
		/// This method is to initialize the PreConverter and PostConverter lambda functions.
		/// </summary>
		static void Init()
		{
			if (PRE_CONVERTER_MAP.Count > 0 && POST_CONVERTER_MAP.Count > 0)
			{
				return;
			}

			AddToDictionary(typeof(DateTimeOffset).FullName,
            (obj) =>
			{
				var dateTime = DateTimeOffset.Parse(obj.ToString()).ToLocalTime();

				return (T)Convert.ChangeType(dateTime, typeof(T));
			},
            (obj) =>
			{
				var dateTime = (DateTimeOffset)Convert.ChangeType(obj, typeof(T));

				return dateTime.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
			});

			AddToDictionary(typeof(long).FullName,
			(obj) =>
			{
				var value = long.Parse(obj.ToString());

				return (T)Convert.ChangeType(value, typeof(T));
			},
			(obj) =>
			{
				return obj;
			});

			AddToDictionary(typeof(double).FullName,
			(obj) =>
			{
				var value = double.Parse(obj.ToString());

				return (T)Convert.ChangeType(value, typeof(T));
			},
			(obj) =>
			{
				return obj;
			});

			AddToDictionary(typeof(bool).FullName,
			(obj) =>
			{
				var value = bool.Parse(obj.ToString());

				return (T)Convert.ChangeType(value, typeof(T));
			},
			(obj) =>
			{
				return obj;
			});

			AddToDictionary(typeof(object).FullName,
			(obj) =>
			{
				return (T)PreConvertObjectData(obj);
			},
			(obj) =>
            {
				return PostConvertObjectData(obj);

			});

			AddToDictionary(typeof(string).FullName,
			(obj) =>
			{
				return (T)Convert.ChangeType(Convert.ToString(obj), typeof(T));
			},
			(obj) =>
			{
				return Convert.ToString(obj);
			});

			AddToDictionary(typeof(DateTime).FullName,
			(obj) =>
			{
				var dateTime = DateTime.Parse(obj.ToString());

				return (T)Convert.ChangeType(dateTime, typeof(T));
			},
			(obj) =>
			{
				var dateTime = (DateTime)Convert.ChangeType(obj, typeof(T));

				return dateTime.ToString("yyyy-MM-dd");
			});
		}

		public static object PreConvertObjectData(object obj)
		{
			if (obj is JToken token)
			{
				var tokenType = token.Type;

				if (tokenType is JTokenType.Null)
				{
					return null;
				}
			}

			if (obj is JArray jsonArray)
			{
				var values = new List<object>();

				if(jsonArray.Count > 0)
				{
					foreach (object response in jsonArray)
					{
						values.Add(PreConvertObjectData(response));
					}
				}

				return values;
			}
			else if (obj is JObject jsonObject)
			{
				var mapInstance = new Dictionary<object, object>();

				if (jsonObject.Count > 0)
				{
                    foreach (var memberName in jsonObject)
					{
						object jsonValue = memberName.Value;

						mapInstance.Add(memberName.Key, PreConvertObjectData(jsonValue));
					}
				}

				return mapInstance;
			}
			else if (obj.GetType().Name.Equals("Object", StringComparison.OrdinalIgnoreCase))
			{
				return obj;
			}
			else
			{
				if(obj is JToken || obj is JValue)
                {
					var keyData = (JToken)obj;

					var tokenType = keyData.Type;

					if (tokenType is JTokenType.Null)
					{
						return null;
					}
					else
                    {
						var type = Converter.GetType(tokenType);

						if (type.Equals(Constants.CSHARP_INT_NAME))
						{
							var number = (long)keyData;

							if (!(number >= Int32.MinValue && number <= Int32.MaxValue))
							{
								type = Constants.CSHARP_LONG_NAME;
							}
						}

						var t = Type.GetType(Constants.DATATYPECONVERTER.Replace(Constants._TYPE, type));

						var method = t.GetMethod("PreConvert");

						var data = (T)method.Invoke(null, new object[] { obj, type });

						return data;
					}
				}

				return obj;
			}
		}

		public static object PostConvertObjectData(object obj)
		{
			if(obj == null)
            {
				return JValue.CreateNull();
			}

			if(obj is IList iList)
			{
				var list = new JArray();

				foreach (var value in iList)
				{
					list.Add(PostConvertObjectData(value));
				}

				return list;
			}
			else if (obj is IDictionary requestObject)
			{
				var value = new JObject();

				if (requestObject.Count > 0)
				{
					foreach (var key in requestObject.Keys)
					{
						var keyValue = requestObject[key];

						value.Add((string)key, JToken.FromObject(PostConvertObjectData(keyValue)));
					}
				}

				return value;
			}
			else if (obj.GetType().Name.Equals("Object", StringComparison.OrdinalIgnoreCase))
			{
				return obj;
			}
			else
			{
				var t = Type.GetType(Constants.DATATYPECONVERTER.Replace(Constants._TYPE, obj.GetType().FullName));

				var method = t.GetMethod("PostConvert");

				return method.Invoke(null, new object[] { obj, obj.GetType().FullName });
			}
		}

		static void AddToDictionary(string name, PreConverter preConverter, PostConverter postConverter)
        {
			PRE_CONVERTER_MAP[name] = preConverter;

			POST_CONVERTER_MAP[name] = postConverter;
		}

		static PreConverter GetPreConverter(string type)
        {
			if(PRE_CONVERTER_MAP.ContainsKey(type))
            {
				return PRE_CONVERTER_MAP[type];
			}

			return preCommonTypeConverter;
		}

		static PostConverter GetPostConverter(string type)
		{
			if (POST_CONVERTER_MAP.ContainsKey(type))
			{
				return POST_CONVERTER_MAP[type];
			}

			return postCommonTypeConverter;
		}

		/// <summary>
		/// This method is to convert JSON value to expected data value.
		/// </summary>
		/// <param name="obj">A object containing the JSON value.</param>
		/// <param name="type">A string containing the expected method return type.</param>
		/// <returns>A T containing the expected data value.</returns>
		public static T PreConvert(object obj, string type)
        {
	        Init();

			return GetPreConverter(type)(obj);
	    }

		/// <summary>
		/// This method to convert CSharp data to JSON data value.
		/// </summary>
		/// <param name="obj">A T containing the CSharp data value.</param>
		/// <param name="type">A string containing the expected method return type.</param>
		/// <returns>A object containing the expected data value.</returns>
		public static object PostConvert(T obj, string type)
		{
			Init();

			return GetPostConverter(type)(obj);
		}
	}
}
