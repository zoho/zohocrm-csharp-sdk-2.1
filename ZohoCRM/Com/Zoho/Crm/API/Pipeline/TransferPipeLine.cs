using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Pipeline
{

	public class TransferPipeLine : Model
	{
		private Pipeline pipeline;
		private List<Stage> stages;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Pipeline Pipeline
		{
			/// <summary>The method to get the pipeline</summary>
			/// <returns>Instance of Pipeline</returns>
			get
			{
				return  this.pipeline;

			}
			/// <summary>The method to set the value to pipeline</summary>
			/// <param name="pipeline">Instance of Pipeline</param>
			set
			{
				 this.pipeline=value;

				 this.keyModified["pipeline"] = 1;

			}
		}

		public List<Stage> Stages
		{
			/// <summary>The method to get the stages</summary>
			/// <returns>Instance of List<Stage></returns>
			get
			{
				return  this.stages;

			}
			/// <summary>The method to set the value to stages</summary>
			/// <param name="stages">Instance of List<Stage></param>
			set
			{
				 this.stages=value;

				 this.keyModified["stages"] = 1;

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