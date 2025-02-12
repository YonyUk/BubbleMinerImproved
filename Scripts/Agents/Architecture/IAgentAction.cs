using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// Agent action interface.
	/// </summary>
	public interface IAgentAction<T,K> where T: IAgentPerception where K: IAgentKnowledge{
		/// <summary>
		/// Gets the name of this action.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }
		/// <summary>
		/// Gets the action function.
		/// </summary>
		/// <value>The action.</value>
		System.Action<T,K> Action { get; }
	}
}