using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents{
	/// <summary>
	/// Agent instructions interaface.
	/// </summary>
	public interface IAgentInstructions<T,K> where T: IAgentPerception where K: IAgentKnowledge<T>{
		/// <summary>
		/// Gets the actions.
		/// </summary>
		/// <value>The actions.</value>
		IEnumerable<IAgentAction<T,K>> Actions { get; }
		/// <summary>
		/// Gets the states.
		/// </summary>
		/// <value>The states.</value>
		IEnumerable<string> States { get; }
		/// <summary>
		/// Gets the filter for the perception.
		/// </summary>
		/// <value>The filter.</value>
		System.Func<GameObject,bool> Filter { get; }
		/// <summary>
		/// Gets the on enter object handler.
		/// </summary>
		/// <value>The on enter object handler.</value>
		System.Action<GameObject,T,K> OnEnterObjectHandler { get; }
		/// <summary>
		/// Gets the on exit object handler.
		/// </summary>
		/// <value>The on exit object handler.</value>
		System.Action<GameObject,T,K> OnExitObjectHandler { get; }
		/// <summary>
		/// Adds the state with the corresponding action
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="action">Action.</param>
		void AddState(string state,IAgentAction<T,K> action);
		/// <summary>
		/// Gets the action for the specified state.
		/// </summary>
		/// <param name="state">State.</param>
		string this[string state] { get; }
	}
}