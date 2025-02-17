using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

public class FishAgentComponent: ReactiveAgent<FishAgentPerception,FishAgentKnowledge>{
	protected override void Awake(){
		// DON'T DELETE THIS CODE, IT'S NEEDED FOR THE AGENT'S INTERNAL WORKING
		base.Awake();
		// THIS INITIALIZE THE KNOWLEDGE FOR THIS AGENT, YOU'RE FREE OF MODIFY THE KNOWLEDGE'S CLASS DEFINITION, THE SAME FOR THE PERCEPTION'S CLASS DEFINITION
		Knowledge = new FishAgentKnowledge();
	}
}