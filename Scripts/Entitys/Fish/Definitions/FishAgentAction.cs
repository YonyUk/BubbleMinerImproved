using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

public class FishAgentAction: IAgentAction<FishAgentPerception,FishAgentKnowledge> {

	public string Name { get; private set; }
	public System.Action<FishAgentPerception,FishAgentKnowledge> Action { get; private set; }
	public FishAgentAction(string name,System.Action<FishAgentPerception,FishAgentKnowledge> action){
		Name = name;
		Action = action;
	}
}