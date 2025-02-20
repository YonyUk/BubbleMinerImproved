using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

public class FishAgentPerception: GameObjectPerceptorComponent,IReactiveAgentPerception{
	// THE PERCEPTION'S CLASS IMPLEMENTATION FOR THIS AGENT
	public string CurrentState { get; set; }
	public float Counter { get; set; }
	public float Timer { get; private set; }

	void Update(){
		Counter += Time.deltaTime;
		Timer += Time.deltaTime;
	}
}