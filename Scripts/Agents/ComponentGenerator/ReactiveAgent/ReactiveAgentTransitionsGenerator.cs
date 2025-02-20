using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// Reactive agent transitions generator.
	/// </summary>
	public class ReactiveAgentTransitionsGenerator: EditorWindow{
		/// <summary>
		/// The name of the class.
		/// </summary>
		public string className;
		/// <summary>
		/// Gets or sets the states_conditions.
		/// </summary>
		/// <value>The states_conditions.</value>
		List<StateConditions> states_conditions = new List<StateConditions>();
		/// <summary>
		/// The scroll position.
		/// </summary>
		Vector2 scrollPosition;

		[MenuItem("NPC Maker/New/Agent/Reactive Agent/Transitions File")]
		public static void ShowWindow(){
			var window = GetWindow<ReactiveAgentTransitionsGenerator>("Reactive Agent Transitions Generator");
			window.maxSize = new Vector2(600,600);
		}
		void OnGUI(){
			className = EditorGUILayout.TextField("Class name",className);
			GUILayout.Label("Rules");

			// Init an scrollable area to see the rules
			scrollPosition = GUILayout.BeginScrollView(scrollPosition);
			// The rule transition definition
			for(int i = 0; i < states_conditions.Count; i++){
				GUILayout.BeginVertical("Box");
				states_conditions[i].State = EditorGUILayout.TextField("State name",states_conditions[i].State,GUILayout.MaxWidth(400));
				ShowRule(i);
				GUILayout.EndVertical();
			}
			GUILayout.EndScrollView();
			// Adds an state transition rule
			if (GUILayout.Button("Add state rule transition",GUILayout.MaxWidth(200))){
				states_conditions.Add(CreateInstance<StateConditions>());
			}
			if (GUILayout.Button("Generate Rules",GUILayout.MaxWidth(100))){
				GenerateRules();
			}
        }
		void ShowRule(int index){
			// If aren't rules, create a list of it's
			if (states_conditions[index].Conditions == null)
				states_conditions[index].Conditions = new List<List<string>>();
			GUILayout.Label(string.Format("Rule {0}", index + 1));
			for(int i = 0; i < states_conditions[index].Conditions.Count; i++){
				GUILayout.BeginVertical("Box");
				// Show the rules conditions
				ShowRuleConditions(index,i);
				GUILayout.EndVertical();
			}
			// Adds a conditions rules
			if (GUILayout.Button ("Add conditions rule",GUILayout.MaxWidth(200))){
				states_conditions[index].Conditions.Add(new List<string>());
				EditorUtility.SetDirty(states_conditions[index]);
            }
		}
		void ShowRuleConditions(int rule_index,int condition_index){
			GUILayout.Label (string.Format("Conditions set {0}",condition_index + 1));
			for(int i = 0; i < states_conditions[rule_index].Conditions[condition_index].Count; i++){
				GUILayout.BeginVertical("Box");
				states_conditions[rule_index].Conditions[condition_index][i] = EditorGUILayout.TextField(string.Format("Condition {0}",i + 1),states_conditions[rule_index].Conditions[condition_index][i],GUILayout.MaxWidth(400));
				GUILayout.EndVertical();
			}
			// Adds a conditions
			if (GUILayout.Button("Add condition",GUILayout.MaxWidth(200))){
				states_conditions[rule_index].Conditions[condition_index].Add("");
			}
		}
		void GenerateRules(){
			new ReactiveAgentTransitionsParser().Parse(className,states_conditions,"nada",GameController.Controller);
			AssetDatabase.Refresh();
		}
	}
}