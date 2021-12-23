using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class MassUpdateBodyWrapper : Model
	{
		List<Record> data;
		string cvid;
		List<string> ids;
		MassUpdateTerritory territory;
		bool? overWrite;
		List<Criteria> criteria;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Record> Data
		{
			/// <summary>The method to get the data</summary>
			/// <returns>Instance of List<Record></returns>
			get
			{
				return  data;

			}
			/// <summary>The method to set the value to data</summary>
			/// <param name="data">Instance of List<Record></param>
			set
			{
				 data=value;

				 keyModified["data"] = 1;

			}
		}

		public string Cvid
		{
			/// <summary>The method to get the cvid</summary>
			/// <returns>string representing the cvid</returns>
			get
			{
				return  cvid;

			}
			/// <summary>The method to set the value to cvid</summary>
			/// <param name="cvid">string</param>
			set
			{
				 cvid=value;

				 keyModified["cvid"] = 1;

			}
		}

		public List<string> Ids
		{
			/// <summary>The method to get the ids</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  ids;

			}
			/// <summary>The method to set the value to ids</summary>
			/// <param name="ids">Instance of List<string></param>
			set
			{
				 ids=value;

				 keyModified["ids"] = 1;

			}
		}

		public MassUpdateTerritory Territory
		{
			/// <summary>The method to get the territory</summary>
			/// <returns>Instance of MassUpdateTerritory</returns>
			get
			{
				return  territory;

			}
			/// <summary>The method to set the value to territory</summary>
			/// <param name="territory">Instance of MassUpdateTerritory</param>
			set
			{
				 territory=value;

				 keyModified["territory"] = 1;

			}
		}

		public bool? OverWrite
		{
			/// <summary>The method to get the overWrite</summary>
			/// <returns>bool? representing the overWrite</returns>
			get
			{
				return  overWrite;

			}
			/// <summary>The method to set the value to overWrite</summary>
			/// <param name="overWrite">bool?</param>
			set
			{
				 overWrite=value;

				 keyModified["over_write"] = 1;

			}
		}

		public List<Criteria> Criteria
		{
			/// <summary>The method to get the criteria</summary>
			/// <returns>Instance of List<Criteria></returns>
			get
			{
				return  criteria;

			}
			/// <summary>The method to set the value to criteria</summary>
			/// <param name="criteria">Instance of List<Criteria></param>
			set
			{
				 criteria=value;

				 keyModified["criteria"] = 1;

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