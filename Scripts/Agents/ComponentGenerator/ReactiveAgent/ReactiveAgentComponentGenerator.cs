using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// Reactive agent component generator.
	/// </summary>
	public class ReactiveAgentComponentGenerator : EditorWindow {
		/// <summary>
		/// The name of the class.
		/// </summary>
		public string className;
		/// <summary>
		/// The code folder.
		/// </summary>
		public string codeFolder;

		void GenerateCode(){
			
			if(!Directory.Exists(Path.Combine(Application.dataPath,codeFolder)))
				Directory.CreateDirectory(Path.Combine(Application.dataPath,codeFolder));
			if(!Directory.Exists(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Definitions")))
				Directory.CreateDirectory(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Definitions"));
			if(!Directory.Exists(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Components")))
				Directory.CreateDirectory(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Components"));

			// FILEPATHS EACH TEMPLATE
			string agent_action_template_path = GameController.Controller.GetFilePath("AgentActionCodeTemplate.txt");
			string agent_component_template_path = GameController.Controller.GetFilePath("AgentComponentCodeTemplate.txt");
			string agent_instructions_template_path = GameController.Controller.GetFilePath("AgentInstructionsCodeTemplate.txt");
			string agent_instructions_component_template_path = GameController.Controller.GetFilePath("AgentInstructionsComponentCodeTemplate.txt");
			string agent_knowledge_template_path = GameController.Controller.GetFilePath("AgentKnowledgeCodeTemplate.txt");
			string component_template_path = GameController.Controller.GetFilePath("EntityAgentComponentCodeTemplate.txt");
			string agent_perception_template_path = GameController.Controller.GetFilePath("ReactiveAgentPerceptionCodeTemplate.txt");

			// CODES BY FILE
			string agent_action_code = File.ReadAllText(agent_action_template_path).Replace("<CLASSNAME>",className);
			string agent_component_code = File.ReadAllText(agent_component_template_path).Replace("<CLASSNAME>",className);
			string agent_instructions_code = File.ReadAllText(agent_instructions_template_path).Replace("<CLASSNAME>",className);
			string agent_instructions_component_code = File.ReadAllText(agent_instructions_component_template_path).Replace("<CLASSNAME>",className);
			string agent_knowledge_code = File.ReadAllText(agent_knowledge_template_path).Replace("<CLASSNAME>",className);
			string component_code = File.ReadAllText(component_template_path).Replace("<CLASSNAME>",className);
			string agent_perception_code = File.ReadAllText(agent_perception_template_path).Replace("<CLASSNAME>",className);

			// SETTING THE FILESYSTEM LOCATION
			string agent_action_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Definitions"),string.Format("{0}AgentAction.cs",className));
			string agent_component_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Components"),string.Format("{0}AgentComponent.cs",className));
			string agent_instructions_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Definitions"),string.Format("{0}AgentInstructions.cs",className));
			string agent_instructions_component_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Components"),string.Format("{0}AgentInstructionsComponent.cs",className));
			string agent_knowledge_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Definitions"),string.Format("{0}AgentKnowledge.cs",className));
			string component_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Components"),string.Format("{0}Component.cs",className));
			string agent_perception_file = Path.Combine(Path.Combine(Path.Combine(Application.dataPath,codeFolder),"Definitions"),string.Format("{0}AgentPerception.cs",className));

			File.WriteAllText(agent_action_file,agent_action_code);
			File.WriteAllText(agent_component_file,agent_component_code);
			File.WriteAllText(agent_instructions_file,agent_instructions_code);
			File.WriteAllText(agent_instructions_component_file,agent_instructions_component_code);
			File.WriteAllText(agent_knowledge_file,agent_knowledge_code);
			File.WriteAllText(component_file,component_code);
			File.WriteAllText(agent_perception_file,agent_perception_code);
		}
		/// <summary>
		/// Shows the window.
		/// </summary>
		[MenuItem("NPC Maker/Reactive Agent/Code Template")]
		public static void ShowWindow(){
			GetWindow<ReactiveAgentComponentGenerator>("Reactive Agent Code Generator");
		}
		private void OnGUI(){
			GUILayout.Label("Generate new templates",EditorStyles.boldLabel);
			className = EditorGUILayout.TextField("Templates name",className);
			codeFolder = EditorGUILayout.TextField("Folder",codeFolder);

			if (GUILayout.Button ("Select location ...")){
				string location = EditorUtility.SaveFolderPanel("Select the output folder",Application.dataPath,"");
				if (!string.IsNullOrEmpty(location)){
					if (!location.StartsWith(Application.dataPath))
						Debug.LogWarning("The location must be inside the Assets folder");
					else{
						codeFolder = Path.Combine(location,codeFolder);
					}
				}
			}

			if (GUILayout.Button("Generate")){
				GenerateCode();
				AssetDatabase.Refresh();
			}
		}
	}
}
