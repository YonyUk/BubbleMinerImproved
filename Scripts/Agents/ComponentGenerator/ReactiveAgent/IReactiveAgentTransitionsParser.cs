using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Controllers.Architecture;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// Reactive agent transitions parser interface.
	/// </summary>
	public interface IReactiveAgentTransitionsParser{
		/// <summary>
		/// Parse the specified predicates with the perceptionClass and knowledgeClass given, and generates the code in the given FilePath.
		/// </summary>
		/// <param name="predicates">Predicates.</param>
		/// <param name="perceptionClass">Perception class.</param>
		/// <param name="knowledgeClass">Knowledge class.</param>
		/// <param name="FilePath">File path.</param>
		void Parse<T>(string className, IEnumerable<T> predicates,string FilePath,IGameController controller) where T: IReactiveAgentStateRules;
	}
}