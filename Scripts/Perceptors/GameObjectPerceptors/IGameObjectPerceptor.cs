using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture{
	/// <summary>
	/// I game object perceptor.
	/// </summary>
	public interface IGameObjectPerceptor{
		/// <summary>
		/// Gets the objects in bounds.
		/// </summary>
		/// <value>The objects in bounds.</value>
		IEnumerable<GameObject> ObjectsInBounds(System.Func<GameObject,bool> filter);
		/// <summary>
		/// Subscribes to detection.
		/// </summary>
		/// <param name="handler">Handler.</param>
		void SubscribeToDetection(System.Action<GameObject> handler,System.Func<GameObject,bool> filter);
		/// <summary>
		/// Delete the handler subscribed to this detector.
		/// </summary>
		/// <param name="handler">Handler.</param>
		void UnSubscribe(System.Action<GameObject> handler);
	}
}