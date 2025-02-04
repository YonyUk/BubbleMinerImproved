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
	public class Agent<T,K>: MonoBehaviour,IAgent<T,K> where T: IAgentPerception where K: IAgentKnowledge<T>{
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
		/// The function to update the knowledge an perception of the agent
		/// </summary>
		/// <value>The updater fucntion.</value>
		public System.Action<GameObject,T,K> UpdaterFunction { get; set; }
		/// <summary>
		/// The function to handle the incoming of an object
		/// </summary>
		/// <value>The on enter object handler.</value>
		public System.Action<GameObject,T,K> OnEnterObjectHandler { get; set; }
		/// <summary>
		/// The function to handle the out of an object
		/// </summary>
		/// <value>The on exit object handler.</value>
		public System.Action<GameObject,T,K> OnExitObjectHandler { get; set; }
		/// <summary>
		/// Gets or sets the objects filter.
		/// </summary>
		/// <value>The objects filter.</value>
		public System.Func<GameObject,bool> ObjectsFilter { get; set; }
		/// <summary>
		/// Function with wich the agent will perceive the world around him
		/// </summary>
		protected virtual void See(){
			foreach(var obj in ObjectsInBounds)
				UpdaterFunction(obj,Perception,Knowledge);
		}
		/// <summary>
		/// Raises the object enter event.
		/// </summary>
		/// <param name="obj">Object.</param>
		protected virtual void OnObjectEnter(GameObject obj){
			OnEnterObjectHandler(obj,Perception,Knowledge);
		}
		/// <summary>
		/// Raises the object exit event.
		/// </summary>
		/// <param name="obj">Object.</param>
		protected virtual void OnObjectExit(GameObject obj){
			OnExitObjectHandler(obj,Perception,Knowledge);
		}
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
		public virtual void AddAction(string action,System.Action actionFunction){
			agentActions[action] = actionFunction;
		}
		/// <summary>
		/// Removes the action.
		/// </summary>
		/// <param name="action">Action.</param>
		public virtual void RemoveAction(string action){
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
		/// <summary>
		/// Gets the objects in bounds detected by the perceptor
		/// </summary>
		/// <value>The objects in bounds.</value>
		protected IEnumerable<GameObject> ObjectsInBounds{
			get{
				foreach( var obj in PerceptionDetector.ObjectsInBounds(ObjectsFilter))
					yield return obj;
			}
		}
		/// <summary>
		/// Init this instance. Call is required for every instance
		/// </summary>
		public virtual void Init(){
			// gets the perceptor component
			PerceptionDetector = GetComponent<IGameObjectPerceptor>();
			// subscribes to perceptor detections with the filter defined in this class
			PerceptionDetector.Subscribe((obj,signal) => {
				if (signal == GameObjectPerceptorSignal.Enter) // if the object is just detected, the OnObjectEnter function is called
					OnObjectEnter(obj);
				else if(signal == GameObjectPerceptorSignal.Exit) // else, the OnObjectExit function is called
					OnObjectExit(obj);
			},(obj) => ObjectsFilter(obj)); // the filter defined in this class is used
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