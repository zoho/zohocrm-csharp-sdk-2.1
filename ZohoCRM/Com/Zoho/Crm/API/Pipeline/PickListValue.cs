using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Pipeline
{

	public class PickListValue : Model
	{
		string displayValue;
		bool? delete;
		int? sequenceNumber;
		string actualValue;
		long? id;
		string forecastType;
		ForecastCategory forecastCategory;
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

		public bool? Delete
		{
			/// <summary>The method to get the delete</summary>
			/// <returns>bool? representing the delete</returns>
			get
			{
				return  delete;

			}
			/// <summary>The method to set the value to delete</summary>
			/// <param name="delete">bool?</param>
			set
			{
				 delete=value;

				 keyModified["_delete"] = 1;

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

		public ForecastCategory ForecastCategory
		{
			/// <summary>The method to get the forecastCategory</summary>
			/// <returns>Instance of ForecastCategory</returns>
			get
			{
				return  forecastCategory;

			}
			/// <summary>The method to set the value to forecastCategory</summary>
			/// <param name="forecastCategory">Instance of ForecastCategory</param>
			set
			{
				 forecastCategory=value;

				 keyModified["forecast_category"] = 1;

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