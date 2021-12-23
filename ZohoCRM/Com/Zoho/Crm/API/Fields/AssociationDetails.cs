using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class AssociationDetails : Model
	{
		LookupField lookupField;
		LookupField relatedField;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public LookupField LookupField
		{
			/// <summary>The method to get the lookupField</summary>
			/// <returns>Instance of LookupField</returns>
			get
			{
				return  lookupField;

			}
			/// <summary>The method to set the value to lookupField</summary>
			/// <param name="lookupField">Instance of LookupField</param>
			set
			{
				 lookupField=value;

				 keyModified["lookup_field"] = 1;

			}
		}

		public LookupField RelatedField
		{
			/// <summary>The method to get the relatedField</summary>
			/// <returns>Instance of LookupField</returns>
			get
			{
				return  relatedField;

			}
			/// <summary>The method to set the value to relatedField</summary>
			/// <param name="relatedField">Instance of LookupField</param>
			set
			{
				 relatedField=value;

				 keyModified["related_field"] = 1;

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