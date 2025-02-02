using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Agents{
	public interface IAgentPerception{
		void AddPerception<T>(int Id,T default_value);
		void RemovePerception<T>(int Id);
		void Reset();
		void UpdatePerception<T>(int Id,T value);
	}
}