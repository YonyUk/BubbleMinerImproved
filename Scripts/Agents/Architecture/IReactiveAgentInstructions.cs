using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents{
	/// <summary>
	/// IAgent instructions.
	/// </summary>
	public interface IReactiveAgentInstructions<T,K>:IAgentInstructions<T,K> where T: IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// Gets the transitions.
		/// </summary>
		/// <value>The transitions.</value>
		IEnumerable<IReactiveAgentTransition<T,K>> Transitions { get; }
		/// <summary>
		/// Adds the transition.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="transition">Transition.</param>
		void AddTransition(IReactiveAgentTransition<T,K> transition);
	}
}