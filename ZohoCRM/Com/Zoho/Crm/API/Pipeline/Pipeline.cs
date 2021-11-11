using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Pipeline
{

	public class Pipeline : Model
	{
		private long? from;
		private long? to;
		private Pipeline parent;
		private bool? childAvailable;
		private string displayValue;
		private bool? default1;
		private List<PickListValue> maps;
		private string actualValue;
		private long? id;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public long? From
		{
			/// <summary>The method to get the from</summary>
			/// <returns>long? representing the from</returns>
			get
			{
				return  this.from;

			}
			/// <summary>The method to set the value to from</summary>
			/// <param name="from">long?</param>
			set
			{
				 this.from=value;

				 this.keyModified["from"] = 1;

			}
		}

		public long? To
		{
			/// <summary>The method to get the to</summary>
			/// <returns>long? representing the to</returns>
			get
			{
				return  this.to;

			}
			/// <summary>The method to set the value to to</summary>
			/// <param name="to">long?</param>
			set
			{
				 this.to=value;

				 this.keyModified["to"] = 1;

			}
		}

		public Pipeline Parent
		{
			/// <summary>The method to get the parent</summary>
			/// <returns>Instance of Pipeline</returns>
			get
			{
				return  this.parent;

			}
			/// <summary>The method to set the value to parent</summary>
			/// <param name="parent">Instance of Pipeline</param>
			set
			{
				 this.parent=value;

				 this.keyModified["parent"] = 1;

			}
		}

		public bool? ChildAvailable
		{
			/// <summary>The method to get the childAvailable</summary>
			/// <returns>bool? representing the childAvailable</returns>
			get
			{
				return  this.childAvailable;

			}
			/// <summary>The method to set the value to childAvailable</summary>
			/// <param name="childAvailable">bool?</param>
			set
			{
				 this.childAvailable=value;

				 this.keyModified["child_available"] = 1;

			}
		}

		public string DisplayValue
		{
			/// <summary>The method to get the displayValue</summary>
			/// <returns>string representing the displayValue</returns>
			get
			{
				return  this.displayValue;

			}
			/// <summary>The method to set the value to displayValue</summary>
			/// <param name="displayValue">string</param>
			set
			{
				 this.displayValue=value;

				 this.keyModified["display_value"] = 1;

			}
		}

		public bool? Default
		{
			/// <summary>The method to get the default</summary>
			/// <returns>bool? representing the default1</returns>
			get
			{
				return  this.default1;

			}
			/// <summary>The method to set the value to default</summary>
			/// <param name="default1">bool?</param>
			set
			{
				 this.default1=value;

				 this.keyModified["default"] = 1;

			}
		}

		public List<PickListValue> Maps
		{
			/// <summary>The method to get the maps</summary>
			/// <returns>Instance of List<PickListValue></returns>
			get
			{
				return  this.maps;

			}
			/// <summary>The method to set the value to maps</summary>
			/// <param name="maps">Instance of List<PickListValue></param>
			set
			{
				 this.maps=value;

				 this.keyModified["maps"] = 1;

			}
		}

		public string ActualValue
		{
			/// <summary>The method to get the actualValue</summary>
			/// <returns>string representing the actualValue</returns>
			get
			{
				return  this.actualValue;

			}
			/// <summary>The method to set the value to actualValue</summary>
			/// <param name="actualValue">string</param>
			set
			{
				 this.actualValue=value;

				 this.keyModified["actual_value"] = 1;

			}
		}

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if((( this.keyModified.ContainsKey(key))))
			{
				return  this.keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			 this.keyModified[key] = modification;


		}


	}
}