using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// Reactive agent state rules interface.
	/// </summary>
	public interface IReactiveAgentStateRules{
		/// <summary>
		/// Gets the state.
		/// </summary>
		/// <value>The state.</value>
		string StateName { get; }
		/// <summary>
		/// Gets the rules.
		/// </summary>
		/// <value>The rules.</value>
		IEnumerable<IEnumerable<string>> Rules { get; }
	}
}