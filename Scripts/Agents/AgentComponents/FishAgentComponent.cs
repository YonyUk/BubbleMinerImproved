using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Agents;
using World.Entitys;

/// <summary>
/// Fish agent component.
/// </summary>
public class FishAgentComponent : ReactiveAgent<FishAgentPerception,FishAgentKnowledge> {
	public override void Init() {
		base.Init();
		Perception = new FishAgentPerception();
		Knowledge = new FishAgentKnowledge();
		UpdaterFunction = (obj,perception,knowledge) => {};
		OnEnterObjectHandler = (obj,perception,knowledge) => {};
		OnExitObjectHandler = (obj,perception,knowledge) => {};
	}
}
