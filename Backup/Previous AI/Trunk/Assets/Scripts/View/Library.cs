using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public static class Library
    {
        private static bool Trigger = true;

        private static GameObject Prefab_Cube { get; set; }
        private static GameObject Prefab_Bullet { get; set; }
        private static GameObject Prefab_Character { get; set; }

        private static Dictionary<Cub.Class, Cub.View.Character_Model> Dictionary_Character_Model { get; set; }
        private static Dictionary<Cub.Event, Cub.View.Event.Base> Dictionary_Event { get; set; }
        private static Dictionary<CubeType, Material> Dictionary_Cube { get; set; }
        private static Dictionary<string, AudioClip> Dictionary_Sound { get; set; }

		private static Dictionary<Cub.Terrain,GameObject> Dictionary_Terrain = new Dictionary<Terrain, GameObject>();

        public static void Initialization()
        {
            if (Trigger)
            {
                Prefab_Cube = Resources.Load<GameObject>("Prefabs/Cube");
                Prefab_Cube.renderer.material = Resources.Load<Material>("Materials/Transparent");

                Prefab_Character = Resources.Load<GameObject>("Prefabs/Character");

                Prefab_Bullet = Resources.Load<GameObject>("Prefabs/Bullet");

				Dictionary_Terrain.Add(Cub.Terrain.Desert, Resources.Load<GameObject>("Prefabs/Terrains/Desert"));
				Dictionary_Terrain.Add(Cub.Terrain.Grass, Resources.Load<GameObject>("Prefabs/Terrains/Grass"));

                Dictionary_Character_Model = new Dictionary<Class, Character_Model>();
                Dictionary_Character_Model[Class.Knight] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Knight.xml") as Cub.View.Character_Model;
                Dictionary_Character_Model[Class.Jerk] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Jerk.xml") as Cub.View.Character_Model;
                Dictionary_Character_Model[Class.Medic] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Medic.xml") as Cub.View.Character_Model;
                Dictionary_Character_Model[Class.Rocket] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Rocket.xml") as Cub.View.Character_Model;
                Dictionary_Character_Model[Class.Sniper] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Sniper.xml") as Cub.View.Character_Model;
                Dictionary_Character_Model[Class.Soldier] = Cub.Tool.Xml.Deserialize(typeof(Cub.View.Character_Model), "Data/Character_Model_Soldier.xml") as Cub.View.Character_Model;

                Dictionary_Event = new Dictionary<Cub.Event, Event.Base>();
                Dictionary_Event[Cub.Event.Attack_Heal] = new Cub.View.Event.Attack_Heal();
                Dictionary_Event[Cub.Event.Attack_Melee] = new Cub.View.Event.Attack_Melee();
                Dictionary_Event[Cub.Event.Attack_Range] = new Cub.View.Event.Attack_Range();
                Dictionary_Event[Cub.Event.Attack_Rocket] = new Cub.View.Event.Attack_Rocket();
                Dictionary_Event[Cub.Event.Attack_Snipe] = new Cub.View.Event.Attack_Snipe();
                Dictionary_Event[Cub.Event.Be_Attacked] = new Cub.View.Event.Be_Attacked();
                Dictionary_Event[Cub.Event.Be_Healed] = new Cub.View.Event.Be_Healed();
                Dictionary_Event[Cub.Event.Die] = new Cub.View.Event.Die();
                Dictionary_Event[Cub.Event.Idle] = new Cub.View.Event.Idle();
                Dictionary_Event[Cub.Event.Move] = new Cub.View.Event.Move();
                Dictionary_Event[Cub.Event.Win] = new Cub.View.Event.Win();
                Dictionary_Event[Cub.Event.TimeOut] = new Cub.View.Event.TimeOut();

                Dictionary_Cube = new Dictionary<CubeType, Material>();
                Dictionary_Cube.Add(CubeType.Black, (Material)Resources.Load<Material>("Prefabs/Cubes/Black"));
                Dictionary_Cube.Add(CubeType.White, (Material)Resources.Load<Material>("Prefabs/Cubes/White"));
                Dictionary_Cube.Add(CubeType.TeamColorOneA, (Material)Resources.Load<Material>("Prefabs/Cubes/Trans_Red"));
                Dictionary_Cube.Add(CubeType.TeamColorTwoA, (Material)Resources.Load<Material>("Prefabs/Cubes/Trans_Orange"));
                Dictionary_Cube.Add(CubeType.TeamColorOneB, (Material)Resources.Load<Material>("Prefabs/Cubes/Trans_Green"));
                Dictionary_Cube.Add(CubeType.TeamColorTwoB, (Material)Resources.Load<Material>("Prefabs/Cubes/Trans_Blue"));

                Dictionary_Sound = new Dictionary<string, AudioClip>();
                Dictionary_Sound.Add("Scream",(AudioClip)Resources.Load<AudioClip>("Sounds/Scream"));
                
                Trigger = false;
            }
        }

        public static void Unlock()
        {
            Trigger = true;
        }

        
		public static Cub.View.Character_Model Get_Character_Model(Cub.Class _C)
		{
			return Dictionary_Character_Model[_C];
		}

        public static Cub.View.Event.Base Get_Event_Processor(Cub.Event _E)
        {
			if (!Dictionary_Event.ContainsKey(_E))
				Debug.Log(_E.ToString());
            return Dictionary_Event[_E];
        }

        public static GameObject Get_Cube()
        {
            return Prefab_Cube;
        }

		public static GameObject Get_Character()
		{
			return Prefab_Character;
		}

		public static GameObject Get_Terrain(Cub.Terrain t)
		{
			if (Dictionary_Terrain.ContainsKey(t))
				return Dictionary_Terrain[t];
			return null;
		}

        public static GameObject Get_Bullet()
        {
            return Prefab_Bullet;
        }

        public static Material Get_Cube(CubeType ct, bool TeamOne)
        {
            if (ct == CubeType.TeamColorOne)
            {
                if (TeamOne)
                    ct = CubeType.TeamColorOneA;
                else
                    ct = CubeType.TeamColorOneB;
            }
            if (ct == CubeType.TeamColorTwo)
            {
                if (TeamOne)
                    ct = CubeType.TeamColorTwoA;
                else
                    ct = CubeType.TeamColorTwoB;
            }
            if (Dictionary_Cube.ContainsKey(ct))
                return Dictionary_Cube[ct];
            return null;
        }

        public static AudioClip Get_Sound(string name)
        {
            if (Dictionary_Sound.ContainsKey(name))
                return Dictionary_Sound[name];
            return null;
        }
    }
}
