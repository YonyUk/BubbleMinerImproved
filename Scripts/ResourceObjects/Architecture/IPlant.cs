using UnityEngine;
using System.Collections;

namespace Architecture.World{
	/// <summary>
	/// Plant interface.
	/// </summary>
	public interface IPlant: IResourceObjectGenerator{
		/// <summary>
		/// Gets the type of the plant.
		/// </summary>
		/// <value>The type of the plant.</value>
		PlantType PlantType { get; }
		/// <summary>
		/// Gets a value indicating whether this instance is food.
		/// </summary>
		/// <value><c>true</c> if this instance is food; otherwise, <c>false</c>.</value>
		bool IsFood { get; }
		/// <summary>
		/// Gets the calories units.
		/// </summary>
		/// <value>The calories.</value>
		int Calories { get; }
	}
}