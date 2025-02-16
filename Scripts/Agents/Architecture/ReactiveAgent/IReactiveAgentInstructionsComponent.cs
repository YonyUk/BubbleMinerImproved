using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// I reactive agent instructions component.
	/// </summary>
	public interface IReactiveAgentInstructionsComponent<T,K> where T: IAgentPerception where K: IAgentKnowledge {
		/// <summary>
		/// Gets the instructions.
		/// </summary>
		/// <value>The instructions.</value>
		IReactiveAgentInstructions<T,K> Instructions { get; }
	}
}