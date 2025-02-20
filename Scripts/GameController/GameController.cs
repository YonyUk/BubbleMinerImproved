using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using World.Generators;
using Controllers.Architecture;
using Architecture.Agents.Generators;

public class GameController : MonoBehaviour,IGameController {
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
	public string GetFilePath(string filename){
		Queue<string> paths = new Queue<string>();
		paths.Enqueue(Application.dataPath);
		while (paths.Count > 0){
			try{
				string root = paths.Dequeue();
				foreach(var file in Directory.GetFiles(root))
					if (Path.GetFileName(file).Equals(filename,System.StringComparison.OrdinalIgnoreCase))
						return Path.Combine(root,filename);
				foreach(var directory in Directory.GetDirectories(root))
                    paths.Enqueue(directory);
			}catch(System.UnauthorizedAccessException){
				throw new System.UnauthorizedAccessException();
			}catch(DirectoryNotFoundException){
				throw new DirectoryNotFoundException();
			}
		}
		throw new FileNotFoundException("No se puede encontrar el archivo",filename);
	}
}
