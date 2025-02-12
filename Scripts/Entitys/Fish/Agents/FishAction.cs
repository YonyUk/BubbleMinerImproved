using UnityEngine;
using System.Collections;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish action.
	/// </summary>
	public class FishAction : IAgentAction<FishAgentSphericPerception,FishAgentKnowledge> {
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the action function.
		/// </summary>
		/// <value>The action.</value>
		public System.Action<FishAgentSphericPerception,FishAgentKnowledge> Action { get; private set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="World.Entitys.Agents.FishAction"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="action">Action.</param>
		public FishAction(string name,System.Action<FishAgentSphericPerception,FishAgentKnowledge> action){
			Name = name;
			Action = action;
		}
	}
}