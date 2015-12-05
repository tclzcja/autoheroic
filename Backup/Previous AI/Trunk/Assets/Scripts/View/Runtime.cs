using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Runtime : MonoBehaviour
    {
        private static Dictionary<Guid, Cub.View.Character> Dictionary_Character { get; set; }

        private static Queue<Eventon> Queue_Eventon { get; set; }

        public GameplayScreenController GSC = null;

        public void Awake()
        {
            Dictionary_Character = new Dictionary<Guid, Cub.View.Character>();            
            Queue_Eventon = new Queue<Eventon>();
        }

        public static Character Add_Character(Cub.Tool.Character _Character, bool teamOne)
        {
            GameObject GO = GameObject.Instantiate(Cub.View.Library.Get_Character(), _Character.Stat.Position.ToVector3(), Quaternion.identity) as GameObject;
            Cub.View.Character C = GO.GetComponent<Cub.View.Character>();
            C.Initialize_Stat(_Character.ID, _Character.Info.Class, _Character.Info.MHP, _Character.Stat.HP, _Character.Stat.Position, teamOne);
            C.Initialize_Model();
            Dictionary_Character[_Character.ID] = C;
            return C;
        }

        public static void Remove_Character(Guid _ID)
        {
            Dictionary_Character.Remove(_ID);
        }

        public static Cub.View.Character Get_Character(Guid _ID)
        {
            return Dictionary_Character[_ID];
        }

        public static void Add_Eventon(Eventon _Eventon)
        {
            Queue_Eventon.Enqueue(_Eventon);
        }

        public static void Add_Eventon(List<Eventon> _Eventon)
        {
            foreach (Eventon E in _Eventon)
            {
                Queue_Eventon.Enqueue(E);
            }
        }

        public void Run_Eventon()
        {
            if (Queue_Eventon.Count > 0)
            {
                Eventon E = Queue_Eventon.Dequeue();

				//Debug.Log("Eventon Start -- " + Queue_Eventon.Count.ToString() + " Eventons Queued");

                float Delay = Cub.View.Library.Get_Event_Processor(E.Type).Process(E.Data,E.Description);

                Invoke("Run_Eventon", Delay);
            }
            else
            {
                GSC.EndGame();
                Debug.Log("The game is over");
                //No more Invoke
            }
        }

    }
}
