using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish agent instructions.
	/// </summary>
	public class FishAgentInstructions :IReactiveAgentInstructions<FishAgentSphericPerception,FishAgentKnowledge>{
		/// <summary>
		/// The transitions.
		/// </summary>
		LinkedList<IReactiveAgentTransition<FishAgentSphericPerception,FishAgentKnowledge>> transitions = new LinkedList<IReactiveAgentTransition<FishAgentSphericPerception, FishAgentKnowledge>>();
		/// <summary>
		/// The states.
		/// </summary>
		Dictionary<string,IAgentAction<FishAgentSphericPerception,FishAgentKnowledge>> states = new Dictionary<string, IAgentAction<FishAgentSphericPerception, FishAgentKnowledge>>();
		/// <summary>
		/// Gets the filter for the perception.
		/// </summary>
		/// <value>The filter.</value>
		public System.Func<GameObject,bool> Filter { get; protected set; }
		/// <summary>
		/// Gets or sets the updater function.
		/// </summary>
		/// <value>The updater function.</value>
		public System.Action<GameObject,FishAgentSphericPerception,FishAgentKnowledge> UpdaterFunction { get; protected set;}
		/// <summary>
		/// Gets the on enter object handler.
		/// </summary>
		/// <value>The on enter object handler.</value>
		public System.Action<GameObject,FishAgentSphericPerception,FishAgentKnowledge> OnEnterObjectHandler { get; protected set; }
		/// <summary>
		/// Gets the on exit object handler.
		/// </summary>
		/// <value>The on exit object handler.</value>
		public System.Action<GameObject,FishAgentSphericPerception,FishAgentKnowledge> OnExitObjectHandler { get; protected set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="World.Entitys.Agents.FishAgentInstructions"/> class.
		/// </summary>
		/// <param name="filter">Filter.</param>
		/// <param name="onEnter">On enter.</param>
		/// <param name="onExit">On exit.</param>
		public FishAgentInstructions(System.Func<GameObject,bool> filter, System.Action<GameObject,FishAgentSphericPerception,FishAgentKnowledge> onEnter,System.Action<GameObject,FishAgentSphericPerception,FishAgentKnowledge> onExit, System.Action<GameObject,FishAgentSphericPerception,FishAgentKnowledge> onUpdate){
			Filter = filter;
			OnEnterObjectHandler = onEnter;
			OnExitObjectHandler = onExit;
			UpdaterFunction = onUpdate;
		}
		/// <summary>
		/// Gets the action for the specified state.
		/// </summary>
		/// <param name="state">State.</param>
		public string this[string state]{
			get{
				if (!states.ContainsKey(state))
					throw new System.ArgumentOutOfRangeException("state",@"The state {state} there's not exists");
				return states[state].Name;
			}
		}
        /// <summary>
		/// Gets the transitions.
		/// </summary>
		/// <value>The transitions.</value>
		public IEnumerable<IReactiveAgentTransition<FishAgentSphericPerception,FishAgentKnowledge>> Transitions{
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
		public IEnumerable<IAgentAction<FishAgentSphericPerception,FishAgentKnowledge>> Actions{
			get{
				return states.Values;
			}
		}
		/// <summary>
		/// Gets the states.
		/// </summary>
		/// <value>The states.</value>
		public IEnumerable<string> States{
			get{
				return states.Keys;
			}
		}
		/// <summary>
		/// Adds the transition.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="transition">Transition.</param>
		public void AddTransition(IReactiveAgentTransition<FishAgentSphericPerception,FishAgentKnowledge> transition){
			transitions.AddLast(transition);
		}
		/// <summary>
		/// Adds the state with the corresponding action
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="action">Action.</param>
		public void AddState(string state,IAgentAction<FishAgentSphericPerception,FishAgentKnowledge> action){
			states[state] = action;
		}
	}
}