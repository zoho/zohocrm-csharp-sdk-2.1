using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Users
{

	public class CustomizeInfo : Model
	{
		bool? notesDesc;
		string showRightPanel;
		string bcView;
		bool? showHome;
		bool? showDetailView;
		string unpinRecentItem;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? NotesDesc
		{
			/// <summary>The method to get the notesDesc</summary>
			/// <returns>bool? representing the notesDesc</returns>
			get
			{
				return  notesDesc;

			}
			/// <summary>The method to set the value to notesDesc</summary>
			/// <param name="notesDesc">bool?</param>
			set
			{
				 notesDesc=value;

				 keyModified["notes_desc"] = 1;

			}
		}

		public string ShowRightPanel
		{
			/// <summary>The method to get the showRightPanel</summary>
			/// <returns>string representing the showRightPanel</returns>
			get
			{
				return  showRightPanel;

			}
			/// <summary>The method to set the value to showRightPanel</summary>
			/// <param name="showRightPanel">string</param>
			set
			{
				 showRightPanel=value;

				 keyModified["show_right_panel"] = 1;

			}
		}

		public string BcView
		{
			/// <summary>The method to get the bcView</summary>
			/// <returns>string representing the bcView</returns>
			get
			{
				return  bcView;

			}
			/// <summary>The method to set the value to bcView</summary>
			/// <param name="bcView">string</param>
			set
			{
				 bcView=value;

				 keyModified["bc_view"] = 1;

			}
		}

		public bool? ShowHome
		{
			/// <summary>The method to get the showHome</summary>
			/// <returns>bool? representing the showHome</returns>
			get
			{
				return  showHome;

			}
			/// <summary>The method to set the value to showHome</summary>
			/// <param name="showHome">bool?</param>
			set
			{
				 showHome=value;

				 keyModified["show_home"] = 1;

			}
		}

		public bool? ShowDetailView
		{
			/// <summary>The method to get the showDetailView</summary>
			/// <returns>bool? representing the showDetailView</returns>
			get
			{
				return  showDetailView;

			}
			/// <summary>The method to set the value to showDetailView</summary>
			/// <param name="showDetailView">bool?</param>
			set
			{
				 showDetailView=value;

				 keyModified["show_detail_view"] = 1;

			}
		}

		public string UnpinRecentItem
		{
			/// <summary>The method to get the unpinRecentItem</summary>
			/// <returns>string representing the unpinRecentItem</returns>
			get
			{
				return  unpinRecentItem;

			}
			/// <summary>The method to set the value to unpinRecentItem</summary>
			/// <param name="unpinRecentItem">string</param>
			set
			{
				 unpinRecentItem=value;

				 keyModified["unpin_recent_item"] = 1;

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