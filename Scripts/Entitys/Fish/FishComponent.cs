using UnityEngine;
using System.Collections;
using Architecture.Agents;
using Architecture.Perceptors;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish component.
	/// </summary>
	[RequireComponent(typeof(FishAgentComponent))]
	[RequireComponent(typeof(GameObjectPerceptorComponent))]
	[RequireComponent(typeof(FishInstructionsComponent))]
	public class FishComponent : MonoBehaviour {

		/// <summary>
		/// Gets or sets the agent.
		/// </summary>
		/// <value>The agent.</value>
		IReactiveAgent<FishAgentPerception,FishAgentKnowledge> agent { get; set; }
		/// <summary>
		/// Gets or sets the instructions.
		/// </summary>
		/// <value>The instructions.</value>
		IReactiveAgentInstructions<FishAgentPerception,FishAgentKnowledge> instructions { get; set; }
		// Use this for initialization
		void Start() {
			// Gets the agent component
			agent = GetComponent<IReactiveAgent<FishAgentPerception,FishAgentKnowledge>>();
			// starts the agent component
			agent.Init();
			// Gets the instructions
			instructions = GetComponent<IReactiveAgentInstructionsComponent<FishAgentPerception,FishAgentKnowledge>>().Instructions;

			agent.ObjectsFilter = instructions.Filter;
			agent.OnEnterObjectHandler = instructions.OnEnterObjectHandler;
			foreach(var action in instructions.Actions){
				agent.AddAction(action.Name,action.Action);
			}
			foreach(var transition in instructions.Transitions){
				agent.AddTransition(transition.From,transition.TransitionFunction);
			}
			foreach(var state in instructions.States){
				agent[state] = instructions[state];
			}
		}
	}
}