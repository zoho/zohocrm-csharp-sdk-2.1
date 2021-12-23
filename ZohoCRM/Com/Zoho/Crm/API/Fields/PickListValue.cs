using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class PickListValue : Model
	{
		string displayValue;
		int? probability;
		long? forecastCategory;
		string actualValue;
		long? id;
		string forecastType;
		int? sequenceNumber;
		string expectedDataType;
		List<Maps> maps;
		string sysRefName;
		string type;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string DisplayValue
		{
			/// <summary>The method to get the displayValue</summary>
			/// <returns>string representing the displayValue</returns>
			get
			{
				return  displayValue;

			}
			/// <summary>The method to set the value to displayValue</summary>
			/// <param name="displayValue">string</param>
			set
			{
				 displayValue=value;

				 keyModified["display_value"] = 1;

			}
		}

		public int? Probability
		{
			/// <summary>The method to get the probability</summary>
			/// <returns>int? representing the probability</returns>
			get
			{
				return  probability;

			}
			/// <summary>The method to set the value to probability</summary>
			/// <param name="probability">int?</param>
			set
			{
				 probability=value;

				 keyModified["probability"] = 1;

			}
		}

		public long? ForecastCategory
		{
			/// <summary>The method to get the forecastCategory</summary>
			/// <returns>long? representing the forecastCategory</returns>
			get
			{
				return  forecastCategory;

			}
			/// <summary>The method to set the value to forecastCategory</summary>
			/// <param name="forecastCategory">long?</param>
			set
			{
				 forecastCategory=value;

				 keyModified["forecast_category"] = 1;

			}
		}

		public string ActualValue
		{
			/// <summary>The method to get the actualValue</summary>
			/// <returns>string representing the actualValue</returns>
			get
			{
				return  actualValue;

			}
			/// <summary>The method to set the value to actualValue</summary>
			/// <param name="actualValue">string</param>
			set
			{
				 actualValue=value;

				 keyModified["actual_value"] = 1;

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

		public string ForecastType
		{
			/// <summary>The method to get the forecastType</summary>
			/// <returns>string representing the forecastType</returns>
			get
			{
				return  forecastType;

			}
			/// <summary>The method to set the value to forecastType</summary>
			/// <param name="forecastType">string</param>
			set
			{
				 forecastType=value;

				 keyModified["forecast_type"] = 1;

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

		public string ExpectedDataType
		{
			/// <summary>The method to get the expectedDataType</summary>
			/// <returns>string representing the expectedDataType</returns>
			get
			{
				return  expectedDataType;

			}
			/// <summary>The method to set the value to expectedDataType</summary>
			/// <param name="expectedDataType">string</param>
			set
			{
				 expectedDataType=value;

				 keyModified["expected_data_type"] = 1;

			}
		}

		public List<Maps> Maps
		{
			/// <summary>The method to get the maps</summary>
			/// <returns>Instance of List<Maps></returns>
			get
			{
				return  maps;

			}
			/// <summary>The method to set the value to maps</summary>
			/// <param name="maps">Instance of List<Maps></param>
			set
			{
				 maps=value;

				 keyModified["maps"] = 1;

			}
		}

		public string SysRefName
		{
			/// <summary>The method to get the sysRefName</summary>
			/// <returns>string representing the sysRefName</returns>
			get
			{
				return  sysRefName;

			}
			/// <summary>The method to set the value to sysRefName</summary>
			/// <param name="sysRefName">string</param>
			set
			{
				 sysRefName=value;

				 keyModified["sys_ref_name"] = 1;

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

				 keyModified["type"] = 1;

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