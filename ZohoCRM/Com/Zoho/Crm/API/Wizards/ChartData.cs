using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Wizards
{

	public class ChartData : Model
	{
		List<Node> nodes;
		List<Connection> connections;
		int? canvasWidth;
		int? canvasHeight;
		Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Node> Nodes
		{
			/// <summary>The method to get the nodes</summary>
			/// <returns>Instance of List<Node></returns>
			get
			{
				return  nodes;

			}
			/// <summary>The method to set the value to nodes</summary>
			/// <param name="nodes">Instance of List<Node></param>
			set
			{
				 nodes=value;

				 keyModified["nodes"] = 1;

			}
		}

		public List<Connection> Connections
		{
			/// <summary>The method to get the connections</summary>
			/// <returns>Instance of List<Connection></returns>
			get
			{
				return  connections;

			}
			/// <summary>The method to set the value to connections</summary>
			/// <param name="connections">Instance of List<Connection></param>
			set
			{
				 connections=value;

				 keyModified["connections"] = 1;

			}
		}

		public int? CanvasWidth
		{
			/// <summary>The method to get the canvasWidth</summary>
			/// <returns>int? representing the canvasWidth</returns>
			get
			{
				return  canvasWidth;

			}
			/// <summary>The method to set the value to canvasWidth</summary>
			/// <param name="canvasWidth">int?</param>
			set
			{
				 canvasWidth=value;

				 keyModified["canvas_width"] = 1;

			}
		}

		public int? CanvasHeight
		{
			/// <summary>The method to get the canvasHeight</summary>
			/// <returns>int? representing the canvasHeight</returns>
			get
			{
				return  canvasHeight;

			}
			/// <summary>The method to set the value to canvasHeight</summary>
			/// <param name="canvasHeight">int?</param>
			set
			{
				 canvasHeight=value;

				 keyModified["canvas_height"] = 1;

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