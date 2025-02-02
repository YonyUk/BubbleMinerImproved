using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Agents;

namespace World.Entitys{
	/// <summary>
	/// Fish agent knowledge.
	/// </summary>
	public class FishAgentKnowledge : IAgentKnowledge<FishAgentPerception>{
		/// <summary>
		/// Gets or sets the plants_positions.
		/// </summary>
		/// <value>The plants_positions.</value>
		Dictionary<int,Vector3> plants_positions { get; set; }
		/// <summary>
		/// Gets or sets the plants_types.
		/// </summary>
		/// <value>The plants_types.</value>
		Dictionary<int,Plants> plants_types { get; set; }
		/// <summary>
		/// Gets the position plant.
		/// </summary>
		/// <returns>The position plant.</returns>
		/// <param name="Id">Identifier.</param>
		public Vector3 GetPositionPlant(int Id){
			if (plants_positions.ContainsKey(Id))
				return plants_positions[Id];
			throw new System.ArgumentOutOfRangeException("The given id doesn't exists");
		}
		/// <summary>
		/// Gets the type of the plant.
		/// </summary>
		/// <returns>The plant type.</returns>
		/// <param name="Id">Identifier.</param>
		public Plants GetPlantType(int Id){
			if (plants_types.ContainsKey(Id))
				return plants_types[Id];
			throw new System.ArgumentOutOfRangeException("The given id doesn't exists");
		}
		/// <summary>
		/// Adds the plant.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		/// <param name="position">Position.</param>
		/// <param name="type">Type.</param>
		public void AddPlant(int Id,Vector3 position,Plants type){
			plants_types[Id] = type;
			plants_positions[Id] = position;
		}
		/// <summary>
		/// Removes the plant.
		/// </summary>
		/// <param name="Id">Identifier.</param>
		public void RemovePlant(int Id){
			if (plants_types.ContainsKey(Id))
				plants_types.Remove(Id);
			else
				throw new System.ArgumentOutOfRangeException("The given id doesn't exists");
			if (plants_positions.ContainsKey(Id))
				plants_positions.Remove(Id);
			else
				throw new System.ArgumentOutOfRangeException("The given id doesn't exists");
		}
	}
}