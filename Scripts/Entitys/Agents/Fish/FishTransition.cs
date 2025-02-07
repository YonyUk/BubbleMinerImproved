using UnityEngine;
using System.Collections;
using Architecture.Agents;

namespace World.Entitys.Agents{
	public class FishTransition: IReactiveAgentTransition<FishAgentPerception,FishAgentKnowledge>{
		/// <summary>
		/// Gets the initial state for this transition.
		/// </summary>
		/// <value>From.</value>
		public string From { get; private set; }
		/// <summary>
		/// Gets the transition function.
		/// </summary>
		/// <value>The transition function.</value>
		public System.Func<FishAgentPerception,FishAgentKnowledge,string> TransitionFunction { get; private set; }

		public FishTransition(string state, System.Func<FishAgentPerception,FishAgentKnowledge,string> function){
			From = state;
			TransitionFunction = function;
		}
	}
}