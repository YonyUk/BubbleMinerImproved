using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
		public void Parse(string className, IEnumerable<IReactiveAgentStateRules> predicates,string perceptionClass,string knowledgeClass, string FilePath){
			throw new System.NotImplementedException();
		}
	}
}