using UnityEngine;
using System.Collections;

namespace World.Entitys{
	/// <summary>
	/// Plants.
	/// </summary>
	public enum Plants{
		Coral
	}
	/// <summary>
	/// Plant interface.
	/// </summary>
	public interface IPlant{
		Plants Type { get; }
	}
	/// <summary>
	/// Base plant class.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public abstract class BasePlant:MonoBehaviour,IPlant{
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>The type.</value>
		public abstract Plants Type { get; }
		/// <summary>
		/// Gets or sets the _collider.
		/// </summary>
		/// <value>The _collider.</value>
		Collider _collider { get; set; }
		/// <summary>
		/// Init this instance.
		/// </summary>
		protected void Init(){
			_collider = GetComponent<Collider>();
			_collider.isTrigger = true;
		}
	}

}