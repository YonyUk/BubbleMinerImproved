using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Perceptors{
	/// <summary>
	/// Game object perceptor.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class GameObjectPerceptorComponent : MonoBehaviour,IGameObjectPerceptor {
		/// <summary>
		/// Gets or sets the _collider.
		/// </summary>
		/// <value>The _collider.</value>
		protected Collider _collider { get; set; }
		/// <summary>
		/// Gets or sets the objects in bounds.
		/// </summary>
		/// <value>The objects in bounds by each filter.</value>
		protected Dictionary<System.Func<GameObject,bool>,LinkedList<GameObject>> objectsInBoundsByFilter  = new Dictionary<System.Func<GameObject, bool>, LinkedList<GameObject>>(); 
		/// <summary>
		/// Gets or sets the handlers.
		/// </summary>
		/// <value>The handlers.</value>
		protected Dictionary<System.Action<GameObject,GameObjectPerceptorSignal>,System.Func<GameObject,bool>> handlers = new Dictionary<System.Action<GameObject,GameObjectPerceptorSignal>, System.Func<GameObject, bool>>();
		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected virtual void Start(){
			_collider = GetComponent<Collider>();
			_collider.isTrigger = true;
		}
		/// <summary>
		/// Gets the objects in bounds.
		/// </summary>
		/// <param name="filter">filter: the filter who triggered it's detection.</param>
		/// <value>The objects in bounds.</value>
		public virtual IEnumerable<GameObject> ObjectsInBounds(System.Func<GameObject,bool> filter){
			if (filter == null)
				throw new System.ArgumentNullException("filter");
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
		/// <param name="filter">Filter.</param>
		/// <remarks>If handler may takes too long time to execute, consider use a background call to a corutine, other ways the working of this component will be afected</remarks>
		public virtual void Subscribe(System.Action<GameObject,GameObjectPerceptorSignal> handler,System.Func<GameObject,bool> filter){
			if (handler == null)
				throw new System.ArgumentNullException("handler");
			if (filter == null)
				throw new System.ArgumentNullException("filter");
			handlers[handler] = filter;
		}
		/// <summary>
		/// Delete the handler subscribed to this detector.
		/// </summary>
		/// <param name="handler">Handler.</param>
		public virtual void UnSubscribe(System.Action<GameObject,GameObjectPerceptorSignal> handler){
			if (handler == null)
				throw new System.ArgumentNullException("handler");
			if (handlers[handler] == null)
				throw new System.NullReferenceException("handlers[" + handler + "]");
			objectsInBoundsByFilter.Remove(handlers[handler]);
			handlers.Remove(handler);
		}
		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="other">Other.</param>
		protected virtual void OnTriggerEnter(Collider other){
			foreach(var handler in handlers.Keys){
				if (handler == null)
					throw new System.NullReferenceException("handler");
				if (handlers[handler] == null)
					throw new System.NullReferenceException("handlers[" + handler + "]");
				if (handlers[handler](other.gameObject)){

					handler(other.gameObject,GameObjectPerceptorSignal.Enter);
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

			foreach(var handler in handlers.Keys){
				if (handler == null)
					throw new System.NullReferenceException("handler");
				if (handlers[handler] == null)
					throw new System.NullReferenceException("handlers[" + handler + "]");
				if (handlers[handler](other.gameObject)){
					handler(other.gameObject,GameObjectPerceptorSignal.Exit);
					filter_to_delete.AddLast(handlers[handler]);
				}
			}
			foreach(var filter in filter_to_delete)
				objectsInBoundsByFilter.Remove(filter);
		}
	}
}