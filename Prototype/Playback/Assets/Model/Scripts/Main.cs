using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub
{
    public class Main
		//: MonoBehaviour
    {
		EventController Parent;
        public static List<Cub.Character> Info_Character { get; set; }
        public static Dictionary<Cub.Character, Vector2> Info_Position { get; set; }
        public static Dictionary<Cub.Character, float> Info_Cooldown { get; set; }
		public static Dictionary<Cub.Character, string> Unique_Names { get; set; }

		public Main (EventController parent)
				{
				Parent = parent;
				}

        public void Awake()
        {
            Cub.Library.Initialization();

            Info_Character = new List<Character>();
            Info_Position = new Dictionary<Character, Vector2>();
            Info_Cooldown = new Dictionary<Character, float>();
			Unique_Names = new Dictionary<Character, string>();

			int n = 0;
            for (int i = 0; i < Cub.Library.Stage_Terrain.Length; i++)
                for (int j = 0; j < Cub.Library.Stage_Terrain[i].Length; j++)
                    if (Cub.Library.Stage_Unit[i][j] != Type.Class.None)
                    {
                        Cub.Character C = new Cub.Character(Cub.Library.Stage_Unit[i][j]);
						C.Name = "John Smith " + n;
                        Info_Character.Add(C);
                        Info_Position.Add(C, new Vector2(i, j));
                        Info_Cooldown.Add(C, 0.0F);
						C.UName = C.Name;
						Unique_Names.Add(C,C.UName);
						n++;
                    }

        }

		public void Start(){
			SetupData SD = new SetupData();
			SD.StageData();
			foreach(Character man in Info_Character){
				Debug.Log("WELCOME:" + man.Name);
				switch (man.Class){
				case Cub.Type.Class.Archer:
				{
					SD.AddCharacter(man.Name,Unique_Names[man],Cub.Type.Class.Archer,Info_Position[man]);
					break;
				}
				case Cub.Type.Class.Knight:
				{
					SD.AddCharacter(man.Name,Unique_Names[man],Cub.Type.Class.Knight,Info_Position[man]);
					break;
				}
				case Cub.Type.Class.Mage:
				{
					SD.AddCharacter(man.Name,Unique_Names[man],Cub.Type.Class.Mage,Info_Position[man]);
					break;
				}
				}
			}
			((EventController)GameObject.Find("Event Manager").GetComponent("EventController")).Setup(SD);
			Go();
		}

        public void Go()
        {
			if (Info_Character.Count <= 1)
			{
				return;
				//Invoke("Go", 0.0F);
			}
            int Index = 0;

            while (Index < Info_Character.Count)
            {
                Cub.Character C = Info_Character[Index];
                Info_Cooldown[C] -= 0.1F;
                if (Info_Cooldown[C] <= 0F)
                {
                    C.Go(Parent);
                }
                Index++;
            }

            
        }

        public static void Cool(Cub.Character C, float T)
        {
            Info_Cooldown[C] += T;
        }

        public static void Dispose(Cub.Character C)
        {
            Info_Character.Remove(C);
            Info_Position.Remove(C);
            Info_Cooldown.Remove(C);

            if (Info_Character.Count == 1)
            {
                Debug.Log(Info_Character[0].Class + " Win");
            }
        }

        public static void Move(Cub.Character C, Vector2 V)
        {
            Info_Position[C] = V;
        }
    }
}
