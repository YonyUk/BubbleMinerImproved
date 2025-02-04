using UnityEngine;
using System.Collections;
using Architecture;
using Agents;
using World.Entitys;

/// <summary>
/// Fish component.
/// </summary>
[RequireComponent(typeof(FishAgentComponent))]
public class FishComponent : MonoBehaviour {
	public float minX;
	public float minZ;
	public float maxX;
	public float maxZ;
	public float TimeDelay;
	
	float counter { get; set; }
	Vector3 target { get; set; }

	string[] states = new[]{"idle"};
	string[] actions = new[]{"move random"};

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
		// sets the updater function
		agent.UpdaterFunction = (obj,perception,knowledge) => {
			if (obj.tag == "plant")
				perception.UpdatePerception<IPlant>(obj.GetInstanceID(),obj.GetComponent<IPlant>());
		};
		// sets the discover function
		agent.OnEnterObjectHandler = (obj,perception,knowledge) => {
			if (obj.tag == "plant"){
				perception.AddPerception<IPlant>(obj.GetInstanceID(),obj.GetComponent<IPlant>());
				Debug.Log ("Plant Entered");
			}
		};
		// sets the loser function
		agent.OnExitObjectHandler = (obj,perception,knowledge) => {
			if (obj.tag == "plant")
				perception.RemovePerception<IPlant>(obj.GetInstanceID());
		};
		// sets the filter
		agent.ObjectsFilter = (obj) => obj.tag == "plant";

		// sets the idle action
		agent.AddAction(actions[0],() => {
			if (counter >= TimeDelay){
				counter = 0;
				target = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
			}
			else{
				Vector3 direction = target - transform.position;
				transform.Translate(direction.normalized * 0.2f);
			}
		});

		// sets the state with it's action
		agent[states[0]] = actions[0];
		counter = TimeDelay;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
	}
}
