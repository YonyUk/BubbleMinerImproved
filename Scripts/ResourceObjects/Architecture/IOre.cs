using UnityEngine;
using System.Collections;

namespace Architecture.World{
	/// <summary>
	/// Ore interface.
	/// </summary>
	public interface IOre: IResourceObject{
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		OreType OreType { get; }
		/// <summary>
		/// Gets the durability.
		/// </summary>
		/// <value>The durability.</value>
		int Durability { get; }
		/// <summary>
		/// Gets the hardness.
		/// </summary>
		/// <value>The hardness.</value>
		int Hardness { get; }
	}
}