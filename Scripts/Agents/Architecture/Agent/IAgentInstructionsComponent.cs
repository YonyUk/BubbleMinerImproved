using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// I agent instructions component.
	/// </summary>
	public interface IAgentInstructionsComponent<T,K> where T: IAgentPerception where K: IAgentKnowledge {
		/// <summary>
		/// Gets the instructions.
		/// </summary>
		/// <value>The instructions.</value>
		IAgentInstructions<T,K> Instructions { get; }
	}
}