using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.AssignmentRules
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		List<AssignmentRule> assignmentRules;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<AssignmentRule> AssignmentRules
		{
			/// <summary>The method to get the assignmentRules</summary>
			/// <returns>Instance of List<AssignmentRule></returns>
			get
			{
				return  assignmentRules;

			}
			/// <summary>The method to set the value to assignmentRules</summary>
			/// <param name="assignmentRules">Instance of List<AssignmentRule></param>
			set
			{
				 assignmentRules=value;

				 keyModified["assignment_rules"] = 1;

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