using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish agent instructions.
	/// </summary>
	public class FishAgentInstructions: IReactiveAgentInstructions<FishAgentPerception,FishAgentKnowledge>{
		/// <summary>
		/// Gets the filter for the perception.
		/// </summary>
		/// <value>The filter.</value>
		public System.Func<GameObject,bool> Filter { get; private set; }
		/// <summary>
		/// Gets or sets the states.
		/// </summary>
		/// <value>The states.</value>
		Dictionary<string,IAgentAction<FishAgentPerception,FishAgentKnowledge>> states { get; set; }
		/// <summary>
		/// Gets or sets the transitions.
		/// </summary>
		/// <value>The transitions.</value>
		LinkedList<IReactiveAgentTransition<FishAgentPerception,FishAgentKnowledge>> transitions { get; set; }
		/// <summary>
		/// Gets or sets the on enter object handler.
		/// </summary>
		/// <value>The on enter object handler.</value>
		public System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> OnEnterObjectHandler { get; private set; }
		/// <summary>
		/// Gets the on exit object handler.
		/// </summary>
		/// <value>The on exit object handler.</value>
		public System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> OnExitObjectHandler { get; private set; }

		public FishAgentInstructions(System.Func<GameObject,bool> filter,System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> action_enter,System.Action<GameObject,FishAgentPerception,FishAgentKnowledge> action_exit){
			if (filter == null)
				throw new System.ArgumentNullException("filter");
			if (action_enter == null)
				throw new System.ArgumentNullException("action_enter");
			if (action_exit == null)
				throw new System.ArgumentNullException("action_exit");
			states = new Dictionary<string, IAgentAction<FishAgentPerception, FishAgentKnowledge>>();
			transitions = new LinkedList<IReactiveAgentTransition<FishAgentPerception, FishAgentKnowledge>>();
			Filter = filter;
			OnEnterObjectHandler = action_enter;
			OnExitObjectHandler = action_exit;
		}
		public string this[string state]{
			get{
				if (!states.ContainsKey(state))
					throw new System.ArgumentOutOfRangeException("state",state + " state does not exists");
				return states[state].Name;
			}
		}
		public IEnumerable<string> States{
			get{
				return states.Keys;
			}
		}
		/// <summary>
		/// Gets the transitions.
		/// </summary>
		/// <value>The transitions.</value>
		public IEnumerable<IReactiveAgentTransition<FishAgentPerception,FishAgentKnowledge>> Transitions {
			get{
				foreach(var transition in transitions)
					yield return transition;
				yield break;
			}
		}
		/// <summary>
		/// Gets the actions.
		/// </summary>
		/// <value>The actions.</value>
		public IEnumerable<IAgentAction<FishAgentPerception,FishAgentKnowledge>> Actions {
			get{
				foreach(var action in states.Values)
					yield return action;
				yield break;
			}
		}
		/// <summary>
		/// Adds the transition.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="transition">Transition.</param>
		public void AddTransition(IReactiveAgentTransition<FishAgentPerception,FishAgentKnowledge> transition){
			if (!states.ContainsKey(transition.From))
				throw new System.ArgumentOutOfRangeException("transition.From","The initial state for the given transition does not exists");
			transitions.AddLast(transition);
		}
		/// <summary>
		/// Adds the state with the corresponding action
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="action">Action.</param>
		public void AddState(string state,IAgentAction<FishAgentPerception,FishAgentKnowledge> action){
			states[state] = action;
		}
	}
}