using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Pipeline
{

	public class TransferAndDeleteWrapper : Model
	{
		List<TransferPipeLine> transferPipeline;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<TransferPipeLine> TransferPipeline
		{
			/// <summary>The method to get the transferPipeline</summary>
			/// <returns>Instance of List<TransferPipeLine></returns>
			get
			{
				return  transferPipeline;

			}
			/// <summary>The method to set the value to transferPipeline</summary>
			/// <param name="transferPipeline">Instance of List<TransferPipeLine></param>
			set
			{
				 transferPipeline=value;

				 keyModified["transfer_pipeline"] = 1;

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