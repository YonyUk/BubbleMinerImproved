using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// Reactive agent interface.
	/// </summary>
	public interface IReactiveAgent<T,K>:IAgent<T,K> where T: IAgentPerception where K: IAgentKnowledge{
		/// <summary>
		/// Gets or sets the transition function.
		/// </summary>
		/// <value>The transition function.</value>
		System.Func<T,K,string> TransitionFunction { get; set; }
		/// <summary>
		/// Gets the state of the current.
		/// </summary>
		/// <value>The state of the current.</value>
		string CurrentState { get; }
		/// <summary>
		/// The indexer for set or get the corresponding action to a given state.
		/// </summary>
		/// <param name="state">State.</param>
		string this[string state] { get; set; }
	}
}