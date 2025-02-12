using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish agent spheric perception.
	/// </summary>
	[RequireComponent(typeof(SphereCollider))]
	public class FishAgentSphericPerception : GameObjectPerceptorComponent,IAgentPerception {
		/// <summary>
		/// Gets the counter.
		/// </summary>
		/// <value>The counter.</value>
		public float Counter { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="World.Entitys.Agents.FishAgentSphericPerception"/> plant detected.
		/// </summary>
		/// <value><c>true</c> if plant detected; otherwise, <c>false</c>.</value>
		public LinkedList<GameObject> Plants { get; private set; }
		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected override void Awake(){
			base.Awake();
			Counter = 0;
			Plants = new LinkedList<GameObject>();
		}
		public void AddPlant(GameObject obj){
			Plants.AddLast(obj);
		}
		public void RemovePlant(GameObject obj){
			Plants.Remove(obj);
		}
		public int PlantsSeen{
			get{ return Plants.Count; }
		}
		/// <summary>
		/// Update this instance.
		/// </summary>
		void Update(){
			Counter += Time.deltaTime;
		}
	}
}