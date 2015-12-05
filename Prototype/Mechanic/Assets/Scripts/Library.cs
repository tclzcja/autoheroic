using System;
using UnityEngine;

namespace AM
{
    public class Library : MonoBehaviour
    {
        public static GameObject Prefab_Action_Explore { get; private set; }
        public static GameObject Prefab_Action_Attack { get; private set; }

        public static GameObject Prefab_Terrain_Grass { get; private set; }
        public static GameObject Prefab_Terrain_Desert { get; private set; }

        public static GameObject Prefab_Character_Fat { get; private set; }
        public static GameObject Prefab_Character_Normal { get; private set; }

        private void Awake()
        {
            Prefab_Action_Explore = Resources.Load<GameObject>("Prefabs/Actions/Explore");
            Prefab_Action_Attack = Resources.Load<GameObject>("Prefabs/Actions/Attack");

            Prefab_Terrain_Desert = Resources.Load<GameObject>("Prefabs/Terrains/Desert");
            Prefab_Terrain_Grass = Resources.Load<GameObject>("Prefabs/Terrains/Grass");

            Prefab_Character_Fat = Resources.Load<GameObject>("Prefabs/Characters/Fat");
            Prefab_Character_Normal = Resources.Load<GameObject>("Prefabs/Characters/Normal");
        }
    }
}
