using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AM
{
    public class Field : MonoBehaviour
    {
        public static AM.Type.TTerrain[][] Terrains { get; set; }
        public static int[][] Heights { get; set; }
        public static GameObject[][] Units { get; set; }

        private void Start()
        {
            Initialize_Terrain();
        }

        private void Initialize_Terrain()
        {
            for (int i = 0; i < Field.Terrains.Length; i++)
                for (int j = 0; j < Field.Terrains[i].Length; j++)
                {
                    switch (Field.Terrains[i][j])
                    {
                        case AM.Type.TTerrain.Grass:
                            {
                                Instantiate(Library.Prefab_Terrain_Grass, new Vector3(i, Field.Heights[i][j] / 10F, j), Quaternion.identity);
                                break;
                            }
                        case AM.Type.TTerrain.Desert:
                            {
                                Instantiate(Library.Prefab_Terrain_Desert, new Vector3(i, Field.Heights[i][j] / 10F, j), Quaternion.identity);
                                break;
                            }
                    }
                }
        }
    }
}
