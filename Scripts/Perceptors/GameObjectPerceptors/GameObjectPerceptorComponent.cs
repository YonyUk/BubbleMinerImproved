using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture{
	/// <summary>
	/// Game object perceptor.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class GameObjectPerceptorComponent : MonoBehaviour,IGameObjectPerceptor {
		/// <summary>
		/// Gets or sets the objects in bounds.
		/// </summary>
		/// <value>The objects in bounds by each filter.</value>
		protected Dictionary<System.Func<GameObject,bool>,LinkedList<GameObject>> objectsInBoundsByFilter { get; set; } 
		/// <summary>
		/// Gets or sets the handlers.
		/// </summary>
		/// <value>The handlers.</value>
		protected Dictionary<System.Action<GameObject>,System.Func<GameObject,bool>> handlers { get; set; }
		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected virtual void Awake(){
			handlers = new Dictionary<System.Action<GameObject>, System.Func<GameObject, bool>>();
			objectsInBoundsByFilter = new Dictionary<System.Func<GameObject, bool>, LinkedList<GameObject>>();
		}
		/// <summary>
		/// Gets the objects in bounds.
		/// </summary>
		/// <value>The objects in bounds.</value>
		public virtual IEnumerable<GameObject> ObjectsInBounds(System.Func<GameObject,bool> filter){
			if (objectsInBoundsByFilter.ContainsKey(filter)){
				foreach(var obj in objectsInBoundsByFilter[filter])
					yield return obj;
			}
			yield break;
		}
		/// <summary>
		/// Subscribes to detection.
		/// </summary>
		/// <param name="handler">Handler.</param>
		public virtual void SubscribeToDetection(System.Action<GameObject> handler,System.Func<GameObject,bool> filter){
			handlers[handler] = filter;
		}
		/// <summary>
		/// Delete the handler subscribed to this detector.
		/// </summary>
		/// <param name="handler">Handler.</param>
		public virtual void UnSubscribe(System.Action<GameObject> handler){
			handlers.Remove(handler);
		}
		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="other">Other.</param>
		protected virtual void OnTriggerEnter(Collider other){
			foreach(var handler in handlers.Keys){
				if (handlers[handler](other.gameObject)){
					handler(other.gameObject);
					if (!objectsInBoundsByFilter.ContainsKey(handlers[handler])){
						objectsInBoundsByFilter[handlers[handler]] = new LinkedList<GameObject>();
					}
					objectsInBoundsByFilter[handlers[handler]].AddLast(other.gameObject);
				}
			}
		}
		/// <summary>
		/// Raises the trigger exit event.
		/// </summary>
		/// <param name="other">Other.</param>
		protected virtual void OnTriggerExit(Collider other){
			LinkedList<System.Func<GameObject,bool>> filter_to_delete = new LinkedList<System.Func<GameObject, bool>>();
			foreach(var filter in objectsInBoundsByFilter.Keys){
				if (objectsInBoundsByFilter[filter].Contains(other.gameObject)){
					objectsInBoundsByFilter[filter].Remove(other.gameObject);
					if (objectsInBoundsByFilter[filter].Count == 0)
						filter_to_delete.AddLast(filter);
				}
			}
			foreach(var filter in filter_to_delete)
				objectsInBoundsByFilter.Remove(filter);
		}
	}
}