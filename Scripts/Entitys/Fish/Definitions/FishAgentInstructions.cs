using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

public class FishAgentInstructions: IReactiveAgentInstructions<FishAgentPerception,FishAgentKnowledge> {

	Dictionary<string,IAgentAction<FishAgentPerception,FishAgentKnowledge>> states = new Dictionary<string, IAgentAction<FishAgentPerception,FishAgentKnowledge>>();
	public System.Func<FishAgentPerception,FishAgentKnowledge,string> TransitionFunction { get; protected set; }
	public System.Func<GameObject,bool> Filter { get; protected set; }
	public System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> UpdaterFunction { get; protected set;}
	public System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> OnEnterObjectHandler { get; protected set; }
	public System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> OnExitObjectHandler { get; protected set; }

	public FishAgentInstructions(System.Func<GameObject,bool> filter, System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> onEnter,System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> onExit, System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> onUpdate){
		Filter = filter;
		OnEnterObjectHandler = onEnter;
		OnExitObjectHandler = onExit;
		UpdaterFunction = onUpdate;
	}
	public string this[string state]{
		get{
			if (!states.ContainsKey(state))
				throw new System.ArgumentOutOfRangeException("state",string.Format ("The state {0} there's not exists",state));
			return states[state].Name;
		}
	}
	public IEnumerable<IAgentAction<FishAgentPerception,FishAgentKnowledge>> Actions{
		get{
			return states.Values;
		}
	}
	public IEnumerable<string> States{
		get{
			return states.Keys;
		}
	}
	public void AddState(string state,IAgentAction<FishAgentPerception,FishAgentKnowledge> action){
		states[state] = action;
	}
}