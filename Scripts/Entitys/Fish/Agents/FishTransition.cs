using UnityEngine;
using System.Collections;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish transition.
	/// </summary>
	public class FishTransition : IReactiveAgentTransition<FishAgentSphericPerception,FishAgentKnowledge> {
		/// <summary>
		/// Gets the transition.
		/// </summary>
		/// <value>The transition.</value>
		public System.Func<FishAgentSphericPerception,FishAgentKnowledge,string> TransitionFunction { get; private set; }
		/// <summary>
		/// Gets the initial state for this transition.
		/// </summary>
		/// <value>From.</value>
		public string From { get; private set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="World.Entitys.Agents.FishTransition"/> class.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="transition">Transition.</param>
		public FishTransition(string from, System.Func<FishAgentSphericPerception,FishAgentKnowledge,string> transition){
			From = from;
			TransitionFunction = transition;
		}
	}
}