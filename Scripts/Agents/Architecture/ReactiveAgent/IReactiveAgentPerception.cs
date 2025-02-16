using UnityEngine;
using System.Collections;

namespace Architecture.Agents{
	/// <summary>
	/// Reactive agent perception interface.
	/// </summary>
	public interface IReactiveAgentPerception: IAgentPerception{
		/// <summary>
		/// Gets or sets the the current state of the agent.
		/// </summary>
		/// <value>The state of the current.</value>
		string CurrentState { get; set; }
	}
}