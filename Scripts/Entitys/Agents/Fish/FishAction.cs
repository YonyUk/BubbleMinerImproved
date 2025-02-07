using UnityEngine;
using System.Collections;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish action abstraction.
	/// </summary>
	public class FishAction: IAgentAction<FishAgentPerception,FishAgentKnowledge>{
		/// <summary>
		/// Gets the name of this action.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the action function.
		/// </summary>
		/// <value>The action.</value>
		public System.Action<FishAgentPerception,FishAgentKnowledge> Action { get; private set; }

		public FishAction(string name, System.Action<FishAgentPerception,FishAgentKnowledge> action){
			Name = name;
			Action = action;
		}
	}
}
