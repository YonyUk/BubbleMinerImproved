using UnityEngine;
using System.Collections;

namespace Agents{
	/// <summary>
	/// Reactive agent interface.
	/// </summary>
	public interface IReactiveAgent<T,K> where T: IAgentPerception where K: IAgentKnowledge<T> {
		/// <summary>
		/// Adds the transition.
		/// </summary>
		/// <param name="">.</param>
		void AddTransition(string state,System.Func<T,K,string> transition);
		/// <summary>
		/// Removes the transitions.
		/// </summary>
		/// <param name="state">State.</param>
		void RemoveTransition(string state);
		/// <summary>
		/// Replaces the transition.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="transition">Transition.</param>
		void ReplaceTransition(string state, System.Func<T,K,string> transition);
		/// <summary>
		/// Gets the state of the current.
		/// </summary>
		/// <value>The state of the current.</value>
		string CurrentState { get; }
	}
}