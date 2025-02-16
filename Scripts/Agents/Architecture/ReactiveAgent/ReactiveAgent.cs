using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents{
	/// <summary>
	/// Reactive agent abstract class.
	/// 
	/// Init method must be clled at Start method
	/// </summary>
	public class ReactiveAgent<T,K>:Agent<T,K>,IReactiveAgent<T,K> where T: IReactiveAgentPerception where K: IAgentKnowledge{
		/// <summary>
		/// Gets or sets the transitions.
		/// </summary>
		/// <value>The transitions.</value>
		protected Dictionary<string,System.Func<T,K,string>> transitions { get; set; }
		/// <summary>
		/// Gets or sets the actions_by_state.
		/// </summary>
		/// <value>The actions_by_state.</value>
		protected Dictionary<string,string> actions_by_state { get; set; }
		/// <summary>
		/// Gets the state of the current.
		/// </summary>
		/// <value>The state of the current.</value>
		public string CurrentState{ get{ return Perception.CurrentState; }}
		/// <summary>
		/// Adds the transition.
		/// </summary>
		/// <param name="">.</param>
		/// <param name="state">State.</param>
		/// <param name="transition">Transition.</param>
		public void AddTransition(string state,System.Func<T,K,string> transition){
			transitions[state] = transition;
		}
		/// <summary>
		/// Removes the transitions.
		/// </summary>
		/// <param name="state">State.</param>
		public void RemoveTransition(string state){
			if (transitions.ContainsKey(state))
				transitions.Remove(state);
			else
				throw new System.ArgumentOutOfRangeException(state,"There's not an state named " + state);
		}
		/// <summary>
		/// Replaces the transition.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="transition">Transition.</param>
		public void ReplaceTransition(string state,System.Func<T,K,string> transition){
			if (transitions.ContainsKey(state))
				transitions[state] = transition;
			else
				throw new System.ArgumentOutOfRangeException(state,"There's not an state named " + state);
		}
		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update(){
			if (Perception.CurrentState == null) return;
			if (transitions.ContainsKey(Perception.CurrentState))
				Perception.CurrentState = transitions[Perception.CurrentState](Perception,Knowledge);
			if (!actions_by_state.ContainsKey(Perception.CurrentState))
				throw new System.ArgumentOutOfRangeException(Perception.CurrentState,"There's not an action defined for " + Perception.CurrentState);
			if (actions_by_state[Perception.CurrentState] == null)
				throw new System.NullReferenceException("actions_by_state[" + Perception.CurrentState + "]");
			Execute(actions_by_state[Perception.CurrentState]);
		}
		/// <summary>
		/// Gets or sets the <see cref="Agents.ReactiveAgent`2"/> with the specified state.
		/// </summary>
		/// <param name="state">State.</param>
		public virtual string this[string state] {
			get{
				if (actions_by_state.ContainsKey(state))
					return actions_by_state[state];
				return null;
			}
			set{
				if (agentActions.ContainsKey(value))
					actions_by_state[state] = value;
				else
					throw new System.ArgumentOutOfRangeException(value,"There's not a definition for the given action");
				if (Perception.CurrentState == null)
					Perception.CurrentState = state;
			}
		}
		/// <summary>
		/// Init this instance. Call is required for every instance
		/// </summary>
		protected override void Awake(){
			base.Awake();
			actions_by_state = new Dictionary<string, string>();
			transitions = new Dictionary<string, System.Func<T, K, string>>();
		}
	}
}