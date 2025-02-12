using UnityEngine;
using System.Collections;
using Architecture.Agents;
using World.Entitys.Agents;

namespace World.Entitys.Components{
	/// <summary>
	/// Fish agent instructions component.
	/// </summary>
	public class FishAgentInstructionsComponent : MonoBehaviour, IReactiveAgentInstructionsComponent<FishAgentSphericPerception,FishAgentKnowledge> {
		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		/// <value>The target.</value>
		Vector3 target { get; set; }

		public float Timing;
		public float minX;
		public float minZ;
		public float maxX;
		public float maxZ;
		public float Speed;

		/// <summary>
		/// Gets or sets the instructions.
		/// </summary>
		/// <value>The instructions.</value>
		public IReactiveAgentInstructions<FishAgentSphericPerception,FishAgentKnowledge> Instructions { get; private set; }
		/// <summary>
		/// The filter.
		/// </summary>
		System.Func<GameObject,bool> filter = (obj) => obj.tag == "plant";
		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected void Awake(){
			Instructions = new FishAgentInstructions(filter,
			(obj,per,knw) => {
				if (obj.tag =="plant")
					per.AddPlant(obj);
			},(obj,per,knw) => {
				if (obj.tag == "plant")
					per.RemovePlant(obj);
			},(obj,per,kwn) => {});

			/// <summary>
			/// The idle action.
			/// </summary>
			FishAction idleAction = new FishAction("idle",(per,knw) => {
				if (per.Counter >= Timing){
					target = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
					per.Counter = 0;
				}
				else{
					Vector3 dir = target - transform.position;
					transform.Translate(dir.normalized*Speed);
				}
			});

			FishAction aroundAction = new FishAction("around",(per,knw) => {
				Vector3 _target = new Vector3(Mathf.Sin(per.Counter),0,Mathf.Cos(per.Counter)) + target;
				Vector3 dir = _target - transform.position;
				transform.Translate(dir.normalized*Speed);
			});

			FishTransition idle_around = new FishTransition("idle",(per,knw) => {

				foreach(var plant in per.Plants){
					if (plant.transform.position.magnitude > 2)
						return "around";
				}
				return "idle";
			});

			Instructions.AddState("idle",idleAction);
			Instructions.AddState("around",aroundAction);

			Instructions.AddTransition(idle_around);
		}
	}
}