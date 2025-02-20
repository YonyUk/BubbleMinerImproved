using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;
using Architecture.Perceptors;

public class FishAgentInstructionsComponent: MonoBehaviour, IReactiveAgentInstructionsComponent<FishAgentPerception,FishAgentKnowledge> {

	public IReactiveAgentInstructions<FishAgentPerception,FishAgentKnowledge> Instructions { get; private set; }
	// THE FILTER FUNCTION FOR THE OBJECTS IN THE PERCEPTION OF THIS AGENT, BY DEFAULT ANY OBJECT IS DETECTED
	System.Func<GameObject,bool> filter = (obj) => true;

	Vector3 target { get; set; }

	float Counter = 0f;

	FishAgentAction idleAction{
		get{
			return new FishAgentAction("idle",(percp,knowl) => {
				if (percp.Counter >= 3){
					percp.Counter = 0;
					target = new Vector3(Random.Range(-3,3),0,Random.Range(-3,3));
				} else{
					Vector3 dir = target - transform.position;
					transform.Translate(dir.normalized * Mathf.Min(0.1f,dir.magnitude));
				}
			});
		}
	}

	FishAgentAction exploreAction{
		get{
			return new FishAgentAction("exploring",(percp,knowl) => {
				if (transform.position != target + new Vector3(Mathf.Sin(Counter),0,Mathf.Cos(Counter))){
					Vector3 dir = target + new Vector3(Mathf.Sin(Counter),0,Mathf.Cos(Counter)) - transform.position;
					transform.Translate(dir.normalized * Mathf.Min (0.1f,dir.magnitude));
				} else{
					Counter += Time.deltaTime;
					transform.position = target + new Vector3(Mathf.Sin(Counter),0,Mathf.Cos(Counter));
				}
			});
		}
	}

	// THE HANDLER FOR THE ENTERING EVENT OF AN OBJECT TO THE AGENT PERCEPTION
	void onEnterHandler(GameObject obj, FishAgentPerception percp, FishAgentKnowledge knowl){
		// CODE IS NEEDED 
		//throw new System.NotImplementedException();
	}
	// THE HANDLER FOR THE EXITING EVENT OF AN OBJECT TO THE AGENT PERCEPTION
	void onExitHandler(GameObject obj, FishAgentPerception percp, FishAgentKnowledge knowl){
		// CODE IS NEEDED 
		//throw new System.NotImplementedException();
	}
	// THE HANDLER FOR UPDATE THE PERCEPTION OF ANY OBJECT DETECTED
	void onUpdateHandler(GameObject obj, FishAgentPerception percp, FishAgentKnowledge knowl){
		// CODE IS NEEDED 
		//throw new System.NotImplementedException();
	}
	protected void Awake(){
		Instructions = new FishAgentInstructions(filter,onEnterHandler,onExitHandler,onUpdateHandler);
		// STATES DEFINITIONS
		// Example: Instructions.AddState("idle",<instruction>)//instruction most be an IAgentAction<FishAgentPerception,FishAgentKnowledge>

		Instructions.AddState("idle",idleAction);
		Instructions.AddState("exploring",exploreAction);

		Instructions.TransitionFunction = new FishAgentTransitionFunctionContainer().TransitionFunction;
		// TRANSITIONS DEFINITIONS
		// Example: Instructions.AddTransition(<transition>)// transition most be an IReactiveAgentTransition<FishAgentPerception,FishAgentKnowledge>
	}
}