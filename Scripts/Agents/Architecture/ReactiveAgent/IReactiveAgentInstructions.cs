using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents{
	/// <summary>
	/// IAgent instructions.
	/// </summary>
	public interface IReactiveAgentInstructions<T,K>:IAgentInstructions<T,K> where T: IAgentPerception where K: IAgentKnowledge{
		/// <summary>
		/// Gets the transition function.
		/// </summary>
		/// <value>The transition function.</value>
		System.Func<T,K,string> TransitionFunction { get; }
	}
}