using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// Reactive agent instructions component interface.
	/// </summary>
	public interface IReactiveAgentInstructionsComponent<T,K> where T: IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// Gets the instructions.
		/// </summary>
		/// <value>The instructions.</value>
		IReactiveAgentInstructions<T,K> Instructions { get; }
	}
}