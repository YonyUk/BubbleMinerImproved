using UnityEngine;
using System.Collections;

namespace Agents{
	/// <summary>
	/// IAgent.
	/// 
	/// Interface to envolves all  agents definitions
	/// </summary>
	public interface IAgent{
		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="actionFunction">Action function.</param>
		void AddAction(string action, System.Action actionFunction);
		/// <summary>
		/// Removes the action.
		/// </summary>
		/// <param name="action">Action.</param>
		void RemoveAction(string action);
	}

}
