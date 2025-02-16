using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Agents.Generators{
	/// <summary>
	/// State conditions.
	/// </summary>
	[System.Serializable]
	public class StateConditions: ScriptableObject,IReactiveAgentStateRules{
		[SerializeField]
		public string State;
		[SerializeField]
		public List<List<string>> Conditions;
		public string StateName { get{ return State; }}
		public IEnumerable<IEnumerable<string>> Rules{
			get{
				foreach(var enumerable in Conditions)
					yield return inerEnum(enumerable);
				yield break;
			}
		}
		IEnumerable<string> inerEnum(List<string> list){
			foreach(var item in list)
				yield return item;
			yield break;
		}
	}
}
