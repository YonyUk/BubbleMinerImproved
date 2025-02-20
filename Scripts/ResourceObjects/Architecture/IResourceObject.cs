using UnityEngine;
using System.Collections;

namespace Architecture.World{
	/// <summary>
	/// Resource object interface.
	/// </summary>
	public interface IResourceObject{
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		ResourceType Type { get; }
		/// <summary>
		/// Gets the units.
		/// </summary>
		/// <value>The units.</value>
		int Units { get; }
		/// <summary>
		/// Gets the name of this resource.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }
		/// <summary>
		/// Gets the description of this resource.
		/// </summary>
		/// <value>The description.</value>
		string Description { get; }
	}
}
