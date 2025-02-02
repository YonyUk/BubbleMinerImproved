using UnityEngine;
using System.Collections;
using World.Generators;

public class GameController : MonoBehaviour {
	public GameObject[] plantsPrefabs;
	public int InitialsCellsAlive;
	public int Rows;
	public int Cols;
	public int ObjectTypes;
	public int Iterations;
	public float Probability;
	public float CellSize;
	static GameController _instance { get; set; }
	static Generator MapGenerator { get; set; }

	public static GameController Controller{
		get{
			if (_instance == null)
				_instance = FindObjectOfType<GameController>();
			if(_instance == null){
				GameObject go = new GameObject("GameController");
				_instance = go.AddComponent<GameController>();
			}
			return _instance;
		}
	}
	void Start () {
		MapGenerator = new Generator (Rows, Cols, ObjectTypes, Iterations, Probability);
		MapGenerator.InitialCells = InitialsCellsAlive;
		MapGenerator.Generate ();
		SetPlants ();
	}
	void SetPlants(){
		Vector3 offset = new Vector3 (MapGenerator.Map.GetLength (0), 0, MapGenerator.Map.GetLength (1)) * CellSize * 0.5f;
		foreach (Vector2 coord in MapGenerator.Cells) {
			Vector3 position = new Vector3(coord.x,0,coord.y)*CellSize - offset;
			Instantiate(plantsPrefabs[0],position,Quaternion.Euler(new Vector3(0,Random.Range(0,360),0)));
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
