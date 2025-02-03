using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Agents{
	/// <summary>
	/// I agent perception.
	/// </summary>
	public interface IAgentPerception{
		/// <summary>
		/// Adds the perception.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <param name="default_value">Default_value.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void AddPerception<T>(int Id,T default_value);
		/// <summary>
		/// Removes the perception.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void RemovePerception<T>(int Id);
		/// <summary>
		/// Reset this instance.
		/// </summary>
		void Reset();
		/// <summary>
		/// Updates the perception.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <param name="value">Value.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		void UpdatePerception<T>(int Id,T value);
	}
}