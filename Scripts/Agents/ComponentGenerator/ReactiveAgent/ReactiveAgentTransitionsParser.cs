using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Controllers.Architecture;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// Reactive agent transitions parser.
	/// </summary>
	public class ReactiveAgentTransitionsParser: IReactiveAgentTransitionsParser{
		/// <summary>
		/// Parse the specified predicates with the perceptionClass and knowledgeClass given, and generates the code in the
		/// given FilePath.
		/// </summary>
		/// <param name="predicates">Predicates.</param>
		/// <param name="perceptionClass">Perception class.</param>
		/// <param name="knowledgeClass">Knowledge class.</param>
		/// <param name="FilePath">File path.</param>
		public void Parse<T>(string className, IEnumerable<T> predicates, string FilePath, IGameController controller) where T: IReactiveAgentStateRules{
			// LOCATING THE TEMPLATE AND LOADING IT"S CONTENT
			string template_path = controller.GetFilePath("TransitionFunctionCodeTemplate.txt");
			string code_template = File.ReadAllText(template_path);
			// WRITING THE CODE INTO THE TEMPLATE
			var template_functions = make_transitions_predicates(code_template,predicates);
			code_template = build_functions_definitions(template_functions.Key,template_functions.Value);

			// SOLVING THE TYPING
			code_template = code_template.Replace("<PERCEPTION-CLASSNAME>",string.Format("{0}AgentPerception",className));
			code_template = code_template.Replace("<KNOWLEDGE-CLASSNAME>",string.Format("{0}AgentKnowledge",className));
			code_template = code_template.Replace("<CLASSNAME>",className);

			string file_path = Path.Combine(Application.dataPath,string.Format("{0}AgentTransitionFunctionContainer.cs",className));
			File.WriteAllText(file_path,code_template);
		}
		string build_functions_definitions(string code_template, List<string> functions_name){
			string code = "";
			foreach(var function in functions_name)
			{
				code += string.Format ("bool {0}(<PERCEPTION-CLASSNAME> perception, <KNOWLEDGE-CLASSNAME> knowledge)",function);
				code += "{\n\t\t throw new System.NotImplementedException();\n\t}\n\t";
			}
			code = code.Substring(0,code.Length - 1);
			return code_template.Replace("<CONDITIONS-FUNCTIONS>",code);
		}
		KeyValuePair<string,List<string>> make_transitions_predicates<T>(string code_template,IEnumerable<T> predicates) where T: IReactiveAgentStateRules{
			string predicates_definitions = "";
			string predicates_functions_definitions = "";
			string predicates_header = "states_predicates_declaration";
			string predicates_functions_headers = "states_predicates";
			List<string> functions_names = new List<string>();
			
			foreach(var state in predicates){
				predicates_definitions += predicates_header + string.Format("[\"{0}\"]",state.StateName) + " = new string[][]{\n";
				predicates_functions_definitions += predicates_functions_headers + string.Format("[\"{0}\"]",state.StateName) + " = new System.Func<FishAgentPerception, FishAgentKnowledge, bool>[][]{\n";
				foreach(var rules_set in state.Rules){
					predicates_definitions += "\t\t\tnew[]{\n";
					predicates_functions_definitions += "\t\t\tnew System.Func<<PERCEPTION-CLASSNAME>,<KNOWLEDGE-CLASSNAME>,bool>[]{\n";
					foreach(var rule in rules_set){
						predicates_definitions += string.Format("\t\t\t\t\"{0}\",\n",rule);
						if (rule.StartsWith("!") && !functions_names.Contains(rule.Substring(1)))
							functions_names.Add(rule.Substring(1));
						else if (!functions_names.Contains(rule))
							functions_names.Add (rule);
						if (rule.StartsWith("!"))
							predicates_functions_definitions += string.Format("\t\t\t\t{0},\n",rule.Substring(1));
						else
							predicates_functions_definitions += string.Format("\t\t\t\t{0},\n",rule);
						
					}
					predicates_definitions = predicates_definitions.Substring(0,predicates_definitions.Length - 2);
					predicates_definitions += "\n\t\t\t},\n";
					predicates_functions_definitions = predicates_functions_definitions.Substring(0,predicates_functions_definitions.Length - 2);
					predicates_functions_definitions += "\n\t\t\t},\n";
				}
				predicates_definitions = predicates_definitions.Substring(0,predicates_definitions.Length - 2);
				predicates_definitions += "\n\t\t};\n\t\t";
				predicates_functions_definitions = predicates_functions_definitions.Substring(0,predicates_functions_definitions.Length - 2);
				predicates_functions_definitions += "\n\t\t};\n\t\t";
			}
			predicates_definitions = predicates_definitions.Substring(0,predicates_definitions.Length - 2);
			predicates_functions_definitions = predicates_functions_definitions.Substring(0,predicates_functions_definitions.Length - 2);
			
			// WRITING THE CODE FOR RULES
			code_template = code_template.Replace("<STATES-PREDICATES-DECLARATIONS>",predicates_definitions);
			code_template = code_template.Replace("<STATES-PREDICATES-FUNCTIONS>",predicates_functions_definitions);
			return new KeyValuePair<string, List<string>>(code_template,functions_names);
		}
	}
}