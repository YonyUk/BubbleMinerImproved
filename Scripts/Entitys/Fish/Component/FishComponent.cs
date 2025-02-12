using UnityEngine;
using System.Collections;
using World.Entitys.Agents;
using Architecture.Agents;

namespace World.Entitys.Components{
	/// <summary>
	/// Fish component.
	/// </summary>
	[RequireComponent(typeof(FishAgentComponent))]
	[RequireComponent(typeof(FishAgentSphericPerception))]
	[RequireComponent(typeof(FishAgentInstructionsComponent))]
	public class FishComponent : MonoBehaviour {
		/// <summary>
		/// Gets or sets the agent.
		/// </summary>
		/// <value>The agent.</value>
		protected FishAgentComponent agent { get; set; }
		protected IReactiveAgentInstructions<FishAgentSphericPerception,FishAgentKnowledge> instructions { get; set; }
		// Use this for initialization
		void Start () {
			agent = GetComponent<FishAgentComponent>();
			instructions = GetComponent<FishAgentInstructionsComponent>().Instructions;

			agent.ObjectsFilter = instructions.Filter;
			agent.OnEnterObjectHandler = instructions.OnEnterObjectHandler;
			agent.OnExitObjectHandler = instructions.OnExitObjectHandler;
			agent.UpdaterFunction = instructions.UpdaterFunction;

			foreach(var action in instructions.Actions){
				agent.AddAction(action.Name,action.Action);
			}
			foreach(var state in instructions.States){
				agent[state] = instructions[state];
			}
			foreach(var transition in instructions.Transitions){
				agent.AddTransition(transition.From,transition.TransitionFunction);
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}