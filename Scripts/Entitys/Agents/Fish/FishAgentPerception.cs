using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Architecture.Agents;

namespace World.Entitys.Agents{
	/// <summary>
	/// Fish agent perception.
	/// </summary>
	public class FishAgentPerception : IAgentPerception {
		/// <summary>
		/// Gets or sets the plants.
		/// </summary>
		/// <value>The plants.</value>
		Dictionary<int,IPlant> plants { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="FishAgentPerception"/> class.
		/// </summary>
		public FishAgentPerception(){
			plants = new Dictionary<int, IPlant>();
		}
		/// <summary>
		/// Adds the perception.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <param name="perception">Perception.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void AddPerception<T>(int Id, T perception){
			if (typeof(T) is IPlant)
				plants[Id] = perception as IPlant;
		}
		/// <summary>
		/// Removes the perception.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void RemovePerception<T>(int Id){
			if (typeof(T) is IPlant)
				plants.Remove(Id);
		}
		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset(){
			plants.Clear();
		}
		/// <summary>
		/// Updates the perception.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <param name="value">Value.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void UpdatePerception<T>(int Id,T value){
			if (typeof(T) is IPlant)
				plants[Id] = value as IPlant;
		}
		/// <summary>
		/// Gets the plants in bounds.
		/// </summary>
		/// <value>The plants in bounds.</value>
		public IEnumerable<IPlant> PlantsInBounds{
			get{
				foreach(IPlant plant in plants.Values)
					yield return plant;
				yield break;
			}
		}
	}
}