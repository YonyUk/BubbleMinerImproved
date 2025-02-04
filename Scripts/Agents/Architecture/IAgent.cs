using UnityEngine;
using System.Collections;

namespace Agents{
	/// <summary>
	/// IAgent.
	/// 
	/// Interface to envolves all  agents definitions
	/// </summary>
	public interface IAgent<T,K> where T: IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// The function to update the knowledge an perception of the agent.
		/// </summary>
		/// <value>The updater function.</value>
		System.Action<GameObject,T,K> UpdaterFunction { get; set; }
		/// <summary>
		/// The function to handle the incoming of an object
		/// </summary>
		/// <value>The on enter object handler.</value>
		System.Action<GameObject,T,K> OnEnterObjectHandler { get; set; }
		/// <summary>
		/// The function to handle the out of an object
		/// </summary>
		/// <value>The on exit object handler.</value>
		System.Action<GameObject,T,K> OnExitObjectHandler { get; set; }
		/// <summary>
		/// Gets or sets the objects filter.
		/// </summary>
		/// <value>The objects filter.</value>
		System.Func<GameObject,bool> ObjectsFilter { get; set; }
		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="actionFunction">Action function.</param>
		void AddAction(string action, System.Action<T,K> actionFunction);
		/// <summary>
		/// Removes the action.
		/// </summary>
		/// <param name="action">Action.</param>
		void RemoveAction(string action);
		/// <summary>
		/// Init this instance.
		/// </summary>
		void Init();
	}

}
