using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using World.Entitys.Agents;

[System.Serializable]
public class StateAction{
	public string State;
	public string Action;
	public StateAction(string s,string a){
		State = s;
		Action = a;
	}
}

public class FishInstructionsComponent : MonoBehaviour,IReactiveAgentInstructionsComponent<FishAgentPerception,FishAgentKnowledge> {
	public float minX;
	public float minZ;
	public float maxX;
	public float maxZ;
	Vector3 target;
	bool exploring_state = false;

	public float TimeDelay;
	float counter;

	public StateAction[] actions;

	Dictionary<string,string> actions_by_state;

	void OnEnter(GameObject obj,FishAgentPerception perc,FishAgentKnowledge know){
		if (obj.transform.position.magnitude > 3 && !exploring_state)
			exploring_state = true;
	}

	public IReactiveAgentInstructions<FishAgentPerception,FishAgentKnowledge> Instructions { get; private set; }
	// Use this for initialization
	void Awake() {

		FishAction random = new FishAction("move random",(perception,knowledge) => {
			if (counter >= TimeDelay){
				counter = 0;
				target = new Vector3(Random.Range(minX,maxX),0,Random.Range(minZ,maxZ));
			}
			else{
				Vector3 direction = target - transform.position;
				transform.Translate(direction.normalized * 0.2f);
			}
		});
		
		FishAction exploring = new FishAction("move around",(perception,knowledge) => {
			Vector3 _target = new Vector3(Mathf.Sin(counter),0,Mathf.Cos(counter)) + target;
			Vector3 direction = _target - transform.position;
			transform.Translate(direction.normalized * 0.2f);
		});
		
		FishTransition idleTransition = new FishTransition("idle",(perc,knw) => {
			if (exploring_state)
				return "explore";
			return "idle";
		});

		Instructions = new FishAgentInstructions((obj) => obj.tag == "plant",OnEnter,(obj,perc,knw) => {});
		Instructions.AddState("idle",random);
		Instructions.AddState("explore",exploring);
		Instructions.AddTransition(idleTransition);
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
	}
}
