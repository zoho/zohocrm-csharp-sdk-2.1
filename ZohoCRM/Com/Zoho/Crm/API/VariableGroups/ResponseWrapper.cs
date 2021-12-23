using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.VariableGroups
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		List<VariableGroup> variableGroups;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<VariableGroup> VariableGroups
		{
			/// <summary>The method to get the variableGroups</summary>
			/// <returns>Instance of List<VariableGroup></returns>
			get
			{
				return  variableGroups;

			}
			/// <summary>The method to set the value to variableGroups</summary>
			/// <param name="variableGroups">Instance of List<VariableGroup></param>
			set
			{
				 variableGroups=value;

				 keyModified["variable_groups"] = 1;

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