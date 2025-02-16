using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// Reactive agent transition parser interface.
	/// </summary>
	public interface IReactiveAgentTransitionContainer<T,K> where T: IReactiveAgentPerception where K: IAgentKnowledge{
		/// <summary>
		/// Gets the transitions.
		/// </summary>
		/// <value>The transitions.</value>
		IEnumerable<System.Func<T,K,string>> Transitions { get; }
	}
}