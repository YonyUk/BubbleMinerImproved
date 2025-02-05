using UnityEngine;
using System.Collections;
using Architecture.Agents;
using Architecture.Perceptors;

namespace Architecture.Components{
	/// <summary>
	/// Fish component.
	/// </summary>
	[RequireComponent(typeof(FishAgentComponent))]
	[RequireComponent(typeof(GameObjectPerceptorComponent))]
	public class FishComponent : MonoBehaviour {
		public float minX;
		public float minZ;
		public float maxX;
		public float maxZ;
		public float TimeDelay;
		
		float counter { get; set; }
		Vector3 target { get; set; }
		bool exploring = false;
		
		string[] states = new[]{"idle","explore"};
		string[] actions = new[]{"move random","move around"};
		
		/// <summary>
		/// Gets or sets the agent.
		/// </summary>
		/// <value>The agent.</value>
		IReactiveAgent<FishAgentPerception,FishAgentKnowledge> agent { get; set; }
		// Use this for initialization
		void Start() {
			// Gets the agent component
			agent = GetComponent<IReactiveAgent<FishAgentPerception,FishAgentKnowledge>>();
			// starts the agent component
			agent.Init();
			// sets the filter
			agent.ObjectsFilter = (obj) => obj.tag == "plant";
			// sets the idle action
			agent.AddAction(actions[0],(perception,knowledge) => {
				if (counter >= TimeDelay){
					counter = 0;
					target = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
				}
				else{
					Vector3 direction = target - transform.position;
					transform.Translate(direction.normalized * 0.2f);
				}
			});
			
			agent.AddAction(actions[1],(perception,knowledge) => {
				Vector3 _target = new Vector3(Mathf.Sin(counter),0,Mathf.Cos(counter)) + target;
				Vector3 direction = _target - transform.position;
				transform.Translate(direction.normalized * 0.2f);
			});
			
			agent.OnEnterObjectHandler = (obj,perception,knowledge) => {
				if (obj.transform.position.magnitude > 3 && !exploring){
					exploring = true;
				}
			};
			
			// sets the state with it's action
			agent[states[0]] = actions[0];
			agent[states[1]] = actions[1];
			
			agent.AddTransition(states[0],(perception,knowledge) => {
				if (exploring)
					return states[1];
				return states[0];
			});
		}
		
		// Update is called once per frame
		void Update () {
			counter += Time.deltaTime;
		}
	}
}