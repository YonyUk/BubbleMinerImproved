using UnityEngine;
using System.Collections;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish agent component.
	/// </summary>
	public class FishAgentComponent : ReactiveAgent<FishAgentSphericPerception,FishAgentKnowledge>{

		protected override void Awake(){
			base.Awake();
			Perception = GetComponent<FishAgentSphericPerception>();
			Knowledge = new FishAgentKnowledge();
		}
	}
}
