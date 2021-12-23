using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Pipeline
{

	public class Pipeline : Model
	{
		long? from;
		long? to;
		Pipeline parent;
		bool? childAvailable;
		string displayValue;
		bool? default1;
		List<PickListValue> maps;
		string actualValue;
		long? id;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public long? From
		{
			/// <summary>The method to get the from</summary>
			/// <returns>long? representing the from</returns>
			get
			{
				return  from;

			}
			/// <summary>The method to set the value to from</summary>
			/// <param name="from">long?</param>
			set
			{
				 from=value;

				 keyModified["from"] = 1;

			}
		}

		public long? To
		{
			/// <summary>The method to get the to</summary>
			/// <returns>long? representing the to</returns>
			get
			{
				return  to;

			}
			/// <summary>The method to set the value to to</summary>
			/// <param name="to">long?</param>
			set
			{
				 to=value;

				 keyModified["to"] = 1;

			}
		}

		public Pipeline Parent
		{
			/// <summary>The method to get the parent</summary>
			/// <returns>Instance of Pipeline</returns>
			get
			{
				return  parent;

			}
			/// <summary>The method to set the value to parent</summary>
			/// <param name="parent">Instance of Pipeline</param>
			set
			{
				 parent=value;

				 keyModified["parent"] = 1;

			}
		}

		public bool? ChildAvailable
		{
			/// <summary>The method to get the childAvailable</summary>
			/// <returns>bool? representing the childAvailable</returns>
			get
			{
				return  childAvailable;

			}
			/// <summary>The method to set the value to childAvailable</summary>
			/// <param name="childAvailable">bool?</param>
			set
			{
				 childAvailable=value;

				 keyModified["child_available"] = 1;

			}
		}

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

		public bool? Default
		{
			/// <summary>The method to get the default</summary>
			/// <returns>bool? representing the default1</returns>
			get
			{
				return  default1;

			}
			/// <summary>The method to set the value to default</summary>
			/// <param name="default1">bool?</param>
			set
			{
				 default1=value;

				 keyModified["default"] = 1;

			}
		}

		public List<PickListValue> Maps
		{
			/// <summary>The method to get the maps</summary>
			/// <returns>Instance of List<PickListValue></returns>
			get
			{
				return  maps;

			}
			/// <summary>The method to set the value to maps</summary>
			/// <param name="maps">Instance of List<PickListValue></param>
			set
			{
				 maps=value;

				 keyModified["maps"] = 1;

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