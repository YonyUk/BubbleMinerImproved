using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// ReactiveAgentTransitionContainer base class.
	/// </summary>
	public class ReactiveAgentTransitionContainer<T,K>: IReactiveAgentTransitionContainer<T,K> where T: IReactiveAgentPerception where K: IAgentKnowledge{
		/// <summary>
		/// The states_predicates.
		/// </summary>
		protected Dictionary<string,System.Func<T,K,bool>[][]> states_predicates = new Dictionary<string, System.Func<T, K, bool>[][]>();
		/// <summary>
		/// The states_predicates_declaration.
		/// </summary>
		protected Dictionary<string,string[][]> states_predicates_declaration = new Dictionary<string, string[][]>();

		public System.Func<T,K,string> TransitionFunction{ get{ return transitionFunction; }}

		protected virtual string transitionFunction(T perception,K knowledge){
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
						else if (!states_predicates_declaration[state][i][j].StartsWith("!") && !states_predicates[state][i][j](perception,knowledge)){
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
	}
}