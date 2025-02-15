using UnityEngine;
using System.Collections;
using System.IO;

public class ReactiveAgentComponentGenerator : MonoBehaviour {

	public string className;
	public string codeFolder;

	// The classname for every file needed
	string perception_class{ get{ return className + "AgentPerception"; }}
	string knowledge_class{ get{ return className + "AgentKnowledge"; }}
	string action_class{ get{ return className + "AgentAction"; }}
	string transition_class{ get{ return className + "AgentTransition"; }}
	string instructions_class{ get{ return className + "AgentInstructions"; }}
	string instructions_component_class{ get{ return className + "AgentInstructionsComponent"; }}
	string agent_component_class{ get{ return className + "AgentComponent"; }}
	string component_class{ get{ return className + "Component"; }}

	// The content for every file needed
	string perception_content_code{ get{ return perception_class + ": GameObjectPerceptorComponent,IAgentPerception{\n// THE PERCEPTION'S CLASS IMPLEMENTATION FOR THIS AGENT\n\tprotected override void Awake(){\n\t\t//DON'T DELETE THIS CODE\n\t\tbase.Awake();\n\t}\n}"; }}
	string knowledge_content_code{ get{ return knowledge_class + ": IAgentKnowledge{\n// THE KNOWLEDGE'S CLASS IMPLEMENTATION FOR THIS AGENT\n}"; }}
	string action_content_code{
		get{
			return action_class + ": IAgentAction<" + perception_class + "," + knowledge_class + "> {\n\n\tpublic string Name { get; private set; }\n\tpublic System.Action<" +
				perception_class + "," + knowledge_class + "> Action { get; private set; }\n\tpublic " + action_class + "(string name,System.Action<" + perception_class + "," + knowledge_class + "> action){\n" +
				"\t\tName = name;\n\t\tAction = action;\n\t}\n}";
		}
	}
	string transition_content_code{
		get{
			return transition_class + ": IReactiveAgentTransition<" + perception_class +"," + knowledge_class + "> {\n\n\t" +
				"public System.Func<" + perception_class + "," + knowledge_class + ",string> TransitionFunction { get; private set; }\n\tpublic string From { get; private set; }\n\t" +
				"public " + transition_class + "(string from, System.Func<" + perception_class + "," + knowledge_class + ",string> transition){\n\t\tFrom = from;\n\t\tTransitionFunction = transition;\n\t}\n}";
		}
	}
	string instructions_content_code{
		get{
			return instructions_class + ": IReactiveAgentInstructions<" + perception_class + "," + knowledge_class + "> {\n\n" +
				"\tLinkedList<IReactiveAgentTransition<" + perception_class + "," + knowledge_class + ">> transitions = new LinkedList<IReactiveAgentTransition<" + perception_class + "," + knowledge_class +">>();\n" +
				"\tDictionary<string,IAgentAction<" + perception_class + "," + knowledge_class + ">> states = new Dictionary<string, IAgentAction<"+ perception_class + "," + knowledge_class + ">>();\n" +
				"\tpublic System.Func<GameObject,bool> Filter { get; protected set; }\n" +
				"\tpublic System.Action<GameObject," + perception_class + "," + knowledge_class + "> UpdaterFunction { get; protected set;}\n" +
				"\tpublic System.Action<GameObject," + perception_class + "," + knowledge_class + "> OnEnterObjectHandler { get; protected set; }\n" +
				"\tpublic System.Action<GameObject," + perception_class + "," + knowledge_class + "> OnExitObjectHandler { get; protected set; }\n\n" +
				"\tpublic " + instructions_class + "(System.Func<GameObject,bool> filter, System.Action<GameObject," + perception_class + "," + knowledge_class + "> onEnter,System.Action<GameObject," + perception_class + "," + knowledge_class + "> onExit, System.Action<GameObject," + perception_class + "," + knowledge_class + "> onUpdate){\n" +
				"\t\tFilter = filter;\n\t\tOnEnterObjectHandler = onEnter;\n\t\tOnExitObjectHandler = onExit;\n\t\tUpdaterFunction = onUpdate;\n" +
				"\t}\n" +
				"\tpublic string this[string state]{\n\t\tget{\n\t\t\tif (!states.ContainsKey(state))\n\t\t\t\tthrow new System.ArgumentOutOfRangeException(\"state\",string.Format (\"The state {0} there's not exists\",state));\n\t\t\treturn states[state].Name;\n\t\t}\n\t}\n" +
				"\tpublic IEnumerable<IReactiveAgentTransition<" + perception_class + "," + knowledge_class + ">> Transitions{\n\t\tget{\n\t\t\tforeach(var transition in transitions)\n\t\t\t\tyield return transition;\n\t\t\tyield break;\n\t\t}\n\t}\n" +
				"\tpublic IEnumerable<IAgentAction<" + perception_class + "," + knowledge_class + ">> Actions{\n\t\tget{\n\t\t\treturn states.Values;\n\t\t}\n\t}\n" +
				"\tpublic IEnumerable<string> States{\n\t\tget{\n\t\t\treturn states.Keys;\n\t\t}\n\t}\n" +
				"\tpublic void AddTransition(IReactiveAgentTransition<" + perception_class + "," + knowledge_class + "> transition){\n\t\ttransitions.AddLast(transition);\n\t}\n" +
				"\tpublic void AddState(string state,IAgentAction<" + perception_class + "," + knowledge_class + "> action){\n\t\tstates[state] = action;\n\t}\n}";
		}
	}
	string instructions_component_content_code{
		get{
			return instructions_component_class + ": MonoBehaviour, IReactiveAgentInstructionsComponent<" + perception_class + "," + knowledge_class + "> {\n\n" +
				"\tpublic IReactiveAgentInstructions<" + perception_class + "," + knowledge_class + "> Instructions { get; private set; }\n" +
				"\t// THE FILTER FUNCTION FOR THE OBJECTS IN THE PERCEPTION OF THIS AGENT, BY DEFAULT ANY OBJECT IS DETECTED\n" +
				"\tSystem.Func<GameObject,bool> filter = (obj) => true;\n" +
				"\t// THE HANDLER FOR THE ENTERING EVENT OF AN OBJECT TO THE AGENT PERCEPTION\n" +
					"\tvoid onEnterHandler(GameObject obj, " + perception_class + " percp, " + knowledge_class + " knowl){\n\t\t// CODE IS NEEDED \n\t\tthrow new System.NotImplementedException();\n\t}\n" +
				"\t// THE HANDLER FOR THE EXITING EVENT OF AN OBJECT TO THE AGENT PERCEPTION\n" +
					"\tvoid onExitHandler(GameObject obj, " + perception_class + " percp, " + knowledge_class + " knowl){\n\t\t// CODE IS NEEDED \n\t\tthrow new System.NotImplementedException();\n\t}\n" +
				"\t// THE HANDLER FOR UPDATE THE PERCEPTION OF ANY OBJECT DETECTED\n" +
					"\tvoid onUpdateHandler(GameObject obj, " + perception_class + " percp, " + knowledge_class + " knowl){\n\t\t// CODE IS NEEDED \n\t\tthrow new System.NotImplementedException();\n\t}\n" +
				"\tprotected void Awake(){\n\t\tInstructions = new " + instructions_class + "(filter,onEnterHandler,onExitHandler,onUpdateHandler);\n" +
				"\t\t// STATES DEFINITIONS\n\t\t// Example: Instructions.AddState(\"idle\",<instruction>)//instruction most be an IAgentAction<" + perception_class + "," + knowledge_class + ">" +
				"\n\n\t\t// TRANSITIONS DEFINITIONS\n\t\t// Example: Instructions.AddTransition(<transition>)// transition most be an IReactiveAgentTransition<" + perception_class + "," + knowledge_class + ">" +
				"\n\t}\n}";
		}
	}
	string agent_component_content_code{
		get{
			return agent_component_class + ": ReactiveAgent<" + perception_class + "," + knowledge_class + ">{\n" +
				"\tprotected override void Awake(){\n\t\t// DON'T DELETE THIS CODE, IT'S NEEDED FOR THE AGENT'S INTERNAL WORKING\n\t\tbase.Awake();\n" +
				"\t\t// THIS INITIALIZE THE KNOWLEDGE FOR THIS AGENT, YOU'RE FREE OF MODIFY THE KNOWLEDGE'S CLASS DEFINITION, THE SAME FOR THE PERCEPTION'S CLASS DEFINITION\n\t\tKnowledge = new " + knowledge_class + "();\n" +
				"\t}\n}";
		}
	}
	string component_content_code{
		get{
			return component_class + ": MonoBehaviour{\n\n" +
				"\tprotected IReactiveAgent<" + perception_class + "," + knowledge_class + "> agent { get; set; }\n" +
				"\tprotected IReactiveAgentInstructions<" + perception_class + "," + knowledge_class + "> instructions { get; set; }\n" +
				"\tvoid Start(){\n\t\t// AGENT'S CONFIGURATION WITH THE GIVEN INSTRUCTIONS\n\t\t// SETTING THE PERCEPTION CONFIGURATION\n" +
				"\t\tagent = GetComponent<IReactiveAgent<" + perception_class + "," + knowledge_class + ">>();\n" +
				"\t\tinstructions = GetComponent<IReactiveAgentInstructionsComponent<" + perception_class + "," + knowledge_class + ">>().Instructions;\n" +
				"\t\tagent.ObjectsFilter = instructions.Filter;\n" +
				"\t\tagent.OnEnterObjectHandler = instructions.OnEnterObjectHandler;\n" +
				"\t\tagent.OnExitObjectHandler = instructions.OnExitObjectHandler;\n" +
				"\t\tagent.UpdaterFunction = instructions.UpdaterFunction;\n\n\t\t// LOADING THE INSTRUCTIONS\n\n\t\t// LOADING THE ACTIONS\n" +
				"\t\tforeach(var action in instructions.Actions){\n\t\t\tagent.AddAction(action.Name,action.Action);\n\t\t}\n" +
				"\t\t// LOADING THE STATES\n" +
				"\t\tforeach(var state in instructions.States){\n\t\t\tagent[state] = instructions[state];\n\t\t}\n" +
				"\t\t// LOADING THE TRANSITIONS\n" +
				"\t\tforeach(var transition in instructions.Transitions){\n\t\t\tagent.AddTransition(transition.From,transition.TransitionFunction);\n\t\t}\n" +
				"\t}\n}";
		}
	}
	string headers_code = "using UnityEngine;\nusing System.Collections;\nusing System.Collections.Generic;\nusing Architecture.Agents;\nusing Architecture.Perceptors;\n\n";

	// Use this for initialization
	void Start () {
		GenerateCode();
	}
	void GenerateCode(){


		if(!Directory.Exists(Path.Combine(Application.dataPath,codeFolder))){
			Directory.CreateDirectory(Path.Combine(Application.dataPath,codeFolder));
		}

		string agent_perception_code = headers_code + "public class " + perception_content_code;
		string agent_knowledge_code = headers_code + "public class " + knowledge_content_code;
		string agent_action_code = headers_code + "public class " + action_content_code;
		string agent_transition_code = headers_code + "public class " + transition_content_code;
		string agent_instructions_code = headers_code + "public class " + instructions_content_code;
		string agent_instructions_component_code = headers_code + "public class " + instructions_component_content_code;
		string agent_component_code = headers_code + "public class " + agent_component_content_code;
		string component_code = headers_code + "[RequireComponent(typeof(" +
			perception_class +"))]\n[RequireComponent(typeof(" + instructions_component_class +
			"))]\n[RequireComponent(typeof(" + agent_component_class + 
			"))]\npublic class " + component_content_code;

		string PerceptionFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),perception_class + ".cs");
		string KnowledgeFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),knowledge_class + ".cs");
		string ActionFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),action_class + ".cs");
		string TransitionFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),transition_class + ".cs");
		string InstructionsFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),instructions_class + ".cs");
		string InstructionsComponentFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),instructions_component_class + ".cs");
		string AgentComponentFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),agent_component_class + ".cs");
		string ComponentFilePath = Path.Combine(Path.Combine(Application.dataPath,codeFolder),component_class + ".cs");

		File.WriteAllText(PerceptionFilePath,agent_perception_code);
		File.WriteAllText(KnowledgeFilePath,agent_knowledge_code);
		File.WriteAllText(ActionFilePath,agent_action_code);
		File.WriteAllText(TransitionFilePath,agent_transition_code);
		File.WriteAllText(InstructionsFilePath,agent_instructions_code);
		File.WriteAllText(InstructionsComponentFilePath,agent_instructions_component_code);
		File.WriteAllText(AgentComponentFilePath,agent_component_code);
		File.WriteAllText(ComponentFilePath,component_code);
	}
}
