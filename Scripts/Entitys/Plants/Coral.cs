using UnityEngine;
using System.Collections;
using World.Entitys;

/// <summary>
/// Coral.
/// </summary>
public class Coral : BasePlant {
	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <value>The type.</value>
	public override Plants Type {
		get{ return Plants.Coral; }
	}
	// Use this for initialization
	void Start () {
		Init ();
	}
}
