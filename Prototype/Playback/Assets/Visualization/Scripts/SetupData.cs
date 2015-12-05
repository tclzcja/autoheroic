using UnityEngine;
using System.Collections;
using AM;
using System.Collections.Generic;
using Cub;

public class SetupData : MonoBehaviour {

	public Cub.Type.Terrain[,] TerrainMap;
	public List<CharController> Characters = new List<CharController>();


	// Use this for initialization
	void Start () {
		//TestFill();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddCharacter(string name, string unique, Cub.Type.Class cls, Vector2 where){
		Characters.Add(new CharController(name,unique, cls,where));
	}

	public void StageData(){
		Cub.Type.Terrain[][] map = Library.Stage_Terrain;

		TerrainMap = new Cub.Type.Terrain[map.Length,map[0].Length];
		for (int y = 0; y < map.Length;y++){
			for (int x = 0; x < map[0].Length;x++){
				if (map[y][x] == Cub.Type.Terrain.Desert)
					TerrainMap[y,x] = Cub.Type.Terrain.Desert;
				else
					TerrainMap[y,x] = Cub.Type.Terrain.Grass;
			}
		}

//		Cub.Type.Class[][] men = Stage_Unit;
//		for (int y = 0; y < men.Length;y++){
//			for (int x = 0; x < men[0].Length;x++){
//				Cub.Type.Class man = Stage_Unit[y][x];
//			switch (men[y][x]){
//			case Cub.Type.Class.Archer:
//				{
//				Characters.Add(new CharController(man,"Jim1", CharClass.Knight,new Vector2(0,0)));
//				break;
//				}
//			}
//		}
//
//		Characters.Add(new CharController("Jim","Jim1", CharClass.Knight,new Vector2(0,0)));
//		Characters.Add(new CharController("John", "John2", CharClass.Knight,new Vector2(9,9)));
	}

	void TestFill(){
		TerrainMap = new Cub.Type.Terrain[10,10];
		for (int y = 0; y < TerrainMap.GetLength(0);y++){
			for (int x = 0; x < TerrainMap.GetLength(1);x++){
				if (Random.Range(0,2) == 1)
					TerrainMap[y,x] = Cub.Type.Terrain.Desert;
				else
					TerrainMap[y,x] = Cub.Type.Terrain.Grass;
			}
		}


		Characters.Add(new CharController("Jim","Jim1", Cub.Type.Class.Knight,new Vector2(0,0)));
		Characters.Add(new CharController("John", "John2", Cub.Type.Class.Knight,new Vector2(9,9)));
	}
}
