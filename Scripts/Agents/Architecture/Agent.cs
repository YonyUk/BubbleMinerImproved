using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	[RequireComponent(typeof(SphereCollider))]
	public abstract class Agent<T,K>: MonoBehaviour,IAgent where T: IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// Gets or sets the perception detector.
		/// </summary>
		/// <value>The perception detector.</value>
		protected SphereCollider PerceptionDetector { get; set; }
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
		/// Gets or sets the objects in bounds.
		/// </summary>
		/// <value>The objects in bounds.</value>
		LinkedList<GameObject> objectsInBounds { get; set; }
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
		/// <summary>
		/// Gets the objects in bounds.
		/// </summary>
		/// <value>The objects in bounds.</value>
		protected IEnumerable<GameObject> ObjectsInBounds {
			get{
				foreach( var obj in objectsInBounds)
					yield return obj;
				yield break;
			}
		}
		/// <summary>
		/// Init this instance. Call is required for every instance
		/// </summary>
		protected virtual void Init(){
			objectsInBounds = new LinkedList<GameObject> ();
			PerceptionDetector = GetComponent<SphereCollider> ();
			agentActions = new Dictionary<string, System.Action>();
			PerceptionDetector.isTrigger = true;
		}
		/// <summary>
		/// Fixeds the update.
		/// </summary>
		protected virtual void FixedUpdate(){
			See ();
		}
		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="obj">Object.</param>
		protected virtual void OnTriggerEnter(Collider obj){
			objectsInBounds.AddLast (obj.gameObject);
			OnObjectEnter(obj.gameObject);
		}
		/// <summary>
		/// Raises the trigger exit event.
		/// </summary>
		/// <param name="obj">Object.</param>
		protected virtual void OnTriggerExit(Collider obj){
			objectsInBounds.Remove (obj.gameObject);
			OnObjectExit(obj.gameObject);
		}
	}
}