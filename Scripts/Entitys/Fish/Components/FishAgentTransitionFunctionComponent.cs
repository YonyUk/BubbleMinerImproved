using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents.Generators;

public class FishAgentTransitionFunctionContainer: IReactiveAgentTransitionContainer<FishAgentPerception,FishAgentKnowledge>{

	// PREDICATES FOR THE STATES TRANSITIONS
	Dictionary<string,System.Func<FishAgentPerception,FishAgentKnowledge,bool>[][]> states_predicates = new Dictionary<string, System.Func<FishAgentPerception, FishAgentKnowledge, bool>[][]>();
	// PREDICATES DECLARATIONS
	Dictionary<string,string[][]> states_predicates_declaration = new Dictionary<string, string[][]>();
	/// <summary>
	/// Gets the transitions.
	/// </summary>
	/// <value>The transitions.</value>
	public System.Func<FishAgentPerception,FishAgentKnowledge,string> TransitionFunction{ get{ return transitionFunction; }}

	public FishAgentTransitionFunctionContainer(){
		states_predicates_declaration["idle"] = new string[][]{
			new[]{"CounterLessThan10","!PlantsSeen"},
			new[]{"CounterGreatherThan20"}
		};
		states_predicates_declaration["exploring"] = new string[][]{
			new[]{"CounterIn10And20","PlantsSeen"}
		};
		states_predicates["idle"] = new System.Func<FishAgentPerception, FishAgentKnowledge, bool>[][]{
			new System.Func<FishAgentPerception, FishAgentKnowledge, bool>[]{CounterLessThan10,PlantsSeen},
			new System.Func<FishAgentPerception, FishAgentKnowledge, bool>[]{CounterGreatherThan20} 
		};
		states_predicates["exploring"] = new System.Func<FishAgentPerception, FishAgentKnowledge, bool>[][]{
			new System.Func<FishAgentPerception, FishAgentKnowledge, bool>[]{CounterIn10And20,PlantsSeen}
		};
	}
	// THE TRANSITION FUNCTION
	string transitionFunction(FishAgentPerception perception, FishAgentKnowledge knowledge){
		// FOR EACH STATE, CHECKS IT'S PREDICATES 
		foreach(var state in states_predicates.Keys){
			// FOR EACH SET OF CONDITIONS, CHECKS IT'S PREDICATES
			for(int i = 0; i < states_predicates[state].Length; i++){
				bool success = true;
				// CHECKS ALL CONDITIONS
				for(int j = 0; j < states_predicates[state][i].Length; j++){
					// IF THE FUNCTION IS NEGATED AND IT RETURNS TRUE, THE CONDITION FAIL
					if (states_predicates_declaration[state][i][j].StartsWith("!") && states_predicates[state][i][j](perception,knowledge)){
						success = false;
						break;
					}
					else if (!states_predicates[state][i][j](perception,knowledge)){
						success = false;
						break;
					}
				}
				if (success)
					return state;
			}
		}
		// RETURNS THE CURRENT STATE IF NONE PREDICATE IS SUCCESS
		return perception.CurrentState;
	}
	bool CounterLessThan10(FishAgentPerception perception, FishAgentKnowledge knowledge){
		return perception.Timer < 15;
	}
	bool PlantsSeen(FishAgentPerception perception, FishAgentKnowledge knowledge){
		return true;
	}
	bool CounterGreatherThan20(FishAgentPerception perception, FishAgentKnowledge knowledge){
		return perception.Timer > 30;
	}
	bool CounterIn10And20(FishAgentPerception perception, FishAgentKnowledge knowledge){
		return perception.Timer > 15 && perception.Timer < 30;
	}
}