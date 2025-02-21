using UnityEngine;
using System.Collections;
using Architecture.World;

namespace World.Entitys{
	/// <summary>
	/// Sea weed.
	/// </summary>
	public class SeaWeed : MonoBehaviour,IResourceObjectGenerator {

		public string Name{
			get{ return "Sea Weed"; }
		}
		public string Description{
			get{
				throw new System.NotImplementedException();
			}
		}
		public ResourceType[] Resources{
			get{
				throw new System.NotImplementedException();
			}
		}
		public float ResourceRate(ResourceType resource){
			throw new System.NotImplementedException();
		}
		public T Collect<T>(ResourceType resource) where T: IResourceObject{
			throw new System.NotImplementedException();
		}
		public int ResourceUnits(ResourceType resource){
			throw new System.NotImplementedException();
		}
	}
}
