using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

[RequireComponent(typeof(FishAgentPerception))]
[RequireComponent(typeof(FishAgentInstructionsComponent))]
[RequireComponent(typeof(FishAgentComponent))]
public class FishComponent: MonoBehaviour{

	protected IReactiveAgent<FishAgentPerception,FishAgentKnowledge> agent { get; set; }
	protected IReactiveAgentInstructions<FishAgentPerception,FishAgentKnowledge> instructions { get; set; }
	void Start(){
		// AGENT'S CONFIGURATION WITH THE GIVEN INSTRUCTIONS
		// SETTING THE PERCEPTION CONFIGURATION
		agent = GetComponent<IReactiveAgent<FishAgentPerception,FishAgentKnowledge>>();
		instructions = GetComponent<IReactiveAgentInstructionsComponent<FishAgentPerception,FishAgentKnowledge>>().Instructions;
		agent.ObjectsFilter = instructions.Filter;
		agent.OnEnterObjectHandler = instructions.OnEnterObjectHandler;
		agent.OnExitObjectHandler = instructions.OnExitObjectHandler;
		agent.UpdaterFunction = instructions.UpdaterFunction;

		// LOADING THE INSTRUCTIONS

		// LOADING THE ACTIONS
		foreach(var action in instructions.Actions){
			agent.AddAction(action.Name,action.Action);
		}
		// LOADING THE STATES
		foreach(var state in instructions.States){
			agent[state] = instructions[state];
		}
		// LOADING THE TRANSITION
		agent.TransitionFunction = instructions.TransitionFunction;
	}
}