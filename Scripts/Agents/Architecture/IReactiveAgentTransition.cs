using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// Reactive agent transition interface.
	/// </summary>
	public interface IReactiveAgentTransition<T,K> where T:IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// Gets the initial state for this transition.
		/// </summary>
		/// <value>From.</value>
		string From { get; }
		/// <summary>
		/// Gets the transition function.
		/// </summary>
		/// <value>The transition function.</value>
		System.Func<T,K,string> TransitionFunction { get; }
	}
}