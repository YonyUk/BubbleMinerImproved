using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture;

namespace Agents{
	/// <summary>
	/// Agent.
	/// 
	/// The abstraction for an agent architecture entity
	/// 
	/// InitMethod must be called at Start method
	/// 
	/// The See function most handle all the incoming and outcoming objects in his perception 
	/// </summary>
	public abstract class Agent<T,K>: MonoBehaviour,IAgent where T: IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// Gets or sets the perception detector.
		/// </summary>
		/// <value>The perception detector.</value>
		protected IGameObjectPerceptor PerceptionDetector { get; set; }
		/// <summary>
		/// Gets or sets the perception.
		/// </summary>
		/// <value>The perception.</value>
		protected T Perception { get; set; }
		/// <summary>
		/// Gets or sets the knowledge data structure.
		/// </summary>
		/// <value>The knowledge.</value>
		protected K Knowledge { get; set; }
		/// <summary>
		/// Gets or sets the agent actions.
		/// </summary>
		/// <value>The agent actions.</value>
		protected Dictionary<string,System.Action> agentActions { get; set; }
		/// <summary>
		/// Function with wich the agent will perceive the world around him
		/// </summary>
		protected abstract void See();
		/// <summary>
		/// Raises the object enter event.
		/// </summary>
		/// <param name="obj">Object.</param>
		protected abstract void OnObjectEnter(GameObject obj);
		/// <summary>
		/// Raises the object exit event.
		/// </summary>
		/// <param name="obj">Object.</param>
		protected abstract void OnObjectExit(GameObject obj);
		/// <summary>
		/// Function to filt the objects for the perceptor
		/// </summary>
		/// <returns><c>true</c>, if filter was objectsed, <c>false</c> otherwise.</returns>
		/// <param name="obj">Object.</param>
		protected abstract bool ObjectsFilter(GameObject obj);
		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="action">Action.</param>
		protected virtual void Execute(string action){
			if (agentActions.ContainsKey(action))
				agentActions[action]();
		}
		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="actionFunction">Action function.</param>
		public void AddAction(string action,System.Action actionFunction){
			agentActions[action] = actionFunction;
		}
		/// <summary>
		/// Removes the action.
		/// </summary>
		/// <param name="action">Action.</param>
		public void RemoveAction(string action){
			if (agentActions.ContainsKey(action))
				agentActions.Remove(action);
		}
		/// <summary>
		/// Gets the agent actions.
		/// </summary>
		/// <value>The agent actions.</value>
		/// <returns>The set of actions for this agent</returns>
		protected IEnumerable<string> AgentActions {
			get{
				foreach(var action in agentActions.Keys)
					yield return action;
				yield break;
			}
		}
		protected IEnumerable<GameObject> ObjectsInBounds{
			get{
				foreach( var obj in PerceptionDetector.ObjectsInBounds(ObjectsFilter))
					yield return obj;
			}
		}
		/// <summary>
		/// Init this instance. Call is required for every instance
		/// </summary>
		protected virtual void Start(){
			PerceptionDetector = GetComponent<IGameObjectPerceptor>();
			agentActions = new Dictionary<string, System.Action>();
		}
		/// <summary>
		/// Fixeds the update.
		/// </summary>
		protected virtual void FixedUpdate(){
			See ();
		}
	}
}