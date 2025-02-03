using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Agents;
using World.Entitys;

public class FishAgentComponent : ReactiveAgent<FishAgentPerception,FishAgentKnowledge> {

	public float minX;
	public float minZ;
	public float maxX;
	public float maxZ;
	public float TimeDelay;

	Dictionary<string,System.Action<GameObject>> updaters { get; set; }
	Dictionary<string,System.Action<GameObject>> enterers { get; set; }
	Dictionary<string,System.Action<GameObject>> exiters { get; set; }

	float counter { get; set; }
	Vector3 target { get; set; }

	string[] states = new[]{"idle"};
	string[] actions = new[]{"move random"};

	void InitPerceptionsFunctions(){
		updaters = new Dictionary<string, System.Action<GameObject>>();
		enterers = new Dictionary<string, System.Action<GameObject>>();
		exiters = new Dictionary<string, System.Action<GameObject>>();

		updaters["plant"] = (obj) => {
			Perception.UpdatePerception<IPlant>(obj.GetInstanceID(),obj.GetComponent<IPlant>());
		};
		enterers["plant"] = (obj) => {
			Perception.AddPerception<IPlant>(obj.GetInstanceID(),obj.GetComponent<IPlant>());
			Debug.Log("Plant Entered");
		};
		exiters["plant"] = (obj) => {
			Perception.RemovePerception<IPlant>(obj.GetInstanceID());
		};
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
		InitPerceptionsFunctions();
		counter = TimeDelay;
		Perception = new FishAgentPerception();
		Knowledge = new FishAgentKnowledge();
		CurrentState = states[0];

		//definiendo la accion "move random"
		AddAction(actions[0],() => {
			if (counter >= TimeDelay){
				counter = 0;
				target = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
			}
			else{
				Vector3 direction = target - transform.position;
				transform.Translate(direction.normalized * 0.2f);
			}
		});
		// enlazando el estado "idle" a la accion "move random"
		this[states[0]] = actions[0];
	}
	/// <summary>
	/// Function with wich the agent will perceive the world around him
	/// </summary>
	protected override void See(){
		foreach(var obj in ObjectsInBounds)
		{
			if (updaters.ContainsKey(obj.tag))
				updaters[obj.tag](obj);
		}
	}
	protected override bool ObjectsFilter(GameObject obj){
		return obj.tag == "plant";
	}
	/// <summary>
	/// Raises the object just enter event.
	/// </summary>
	/// <param name="obj">Object.</param>
	protected override void OnObjectEnter(GameObject obj){
		if (enterers.ContainsKey(obj.tag))
			enterers[obj.tag](obj);
	}
	/// <summary>
	/// Raises the object just exit event.
	/// </summary>
	/// <param name="obj">Object.</param>
	protected override void OnObjectExit(GameObject obj){
		if (exiters.ContainsKey(obj.tag))
			exiters[obj.tag](obj);
	}
	/// <summary>
	/// Update this instance.
	/// </summary>
	protected override void Update(){
		base.Update();
		counter += Time.deltaTime;
	}
}
