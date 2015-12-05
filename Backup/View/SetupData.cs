using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub;

namespace Cub.View
{

    public class SetupData
    {

        public Cub.Terrain[,] TerrainMap;
        public List<CharController> Characters = new List<CharController>();


        // Use this for initialization
        void Start()
        {
            //TestFill();
        }

        // Update is called once per frame
        void Update()
        {

        }

		public void AddCharacter(string name, System.Guid id, Cub.Class cls, Vector2 where, string team)
        {
            Characters.Add(new CharController(name, id, cls, where,team));
        }

        public void StageData()
        {
            Cub.Terrain[][] map = Cub.Tool.Library.Stage_Terrain;

            TerrainMap = new Cub.Terrain[map.Length, map[0].Length];
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    if (map[y][x] == Cub.Terrain.Desert)
                        TerrainMap[y, x] = Cub.Terrain.Desert;
                    else
                        TerrainMap[y, x] = Cub.Terrain.Grass;
                }
            }

            //		Cub.Class[][] men = Stage_Unit;
            //		for (int y = 0; y < men.Length;y++){
            //			for (int x = 0; x < men[0].Length;x++){
            //				Cub.Class man = Stage_Unit[y][x];
            //			switch (men[y][x]){
            //			case Cub.Class.Archer:
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

        //void TestFill()
        //{
        //    TerrainMap = new Cub.Terrain[10, 10];
        //    for (int y = 0; y < TerrainMap.GetLength(0); y++)
        //    {
        //        for (int x = 0; x < TerrainMap.GetLength(1); x++)
        //        {
        //            if (Random.Range(0, 2) == 1)
        //                TerrainMap[y, x] = Cub.Terrain.Desert;
        //            else
        //                TerrainMap[y, x] = Cub.Terrain.Grass;
        //        }
        //    }


        //    Characters.Add(new CharController("Jim", System.Guid.NewGuid(), Cub.Class.Knight, new Vector2(0, 0)));
        //    Characters.Add(new CharController("John", System.Guid.NewGuid(), Cub.Class.Knight, new Vector2(9, 9)));
        //}
    }
}