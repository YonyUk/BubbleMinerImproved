using UnityEngine;
using System.Collections;

namespace Architecture.World{
	/// <summary>
	/// I resource object generator.
	/// </summary>
	public interface IResourceObjectGenerator{
		/// <summary>
		/// Gets the resource types that can generate.
		/// </summary>
		/// <value>The resources.</value>
		ResourceType[] Resources { get; }
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }
		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		string Description { get; }
		/// <summary>
		/// Gets the units that can generate by resource.
		/// </summary>
		/// <returns>The units.</returns>
		/// <param name="resource">Resource.</param>
		int ResourceUnits(ResourceType resource);
		/// <summary>
		/// Gets the rate of generation by resource.
		/// </summary>
		/// <returns>The rate.</returns>
		/// <param name="resource">Resource.</param>
		float ResourceRate(ResourceType resource);
		/// <summary>
		/// Collect the specified resource.
		/// </summary>
		/// <param name="resource">Resource.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		T Collect<T>(ResourceType resource) where T: IResourceObject;
	}
}