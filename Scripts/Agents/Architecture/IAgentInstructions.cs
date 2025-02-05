using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents{
	/// <summary>
	/// I agent instructions.
	/// </summary>
	public interface IAgentInstructions<T,K> where T: IAgentPerception where K: IAgentKnowledge<T>{

	}
}