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
        public static GameplayScreenController GSCStatic = null;

        public void Awake()
        {
            Dictionary_Character = new Dictionary<Guid, Cub.View.Character>();
            Queue_Eventon = new Queue<Eventon>();
            Physics.IgnoreLayerCollision(2, 2);
        }

        public static void AddGSC(GameplayScreenController gsc)
        {
            GSCStatic = gsc;
        }

        public static Cub.View.Character Add_Character(Cub.Model.Character _Character)
        {
            GameObject GO = GameObject.Instantiate(Cub.View.Library.Get_Character(), _Character.Stat.Position.ToVector3(), Quaternion.identity) as GameObject;

            Cub.View.Character C = GO.GetComponent<Cub.View.Character>();

            C.Initialize_Stat(_Character.ID, _Character.Name, _Character.Info.MHP, _Character.Stat.HP, _Character.Stat.Position, _Character.Stat.Team, _Character.Info.Head, _Character.Info.Body, _Character.Info.Arms, _Character.Info.Legs, _Character.Info.Value);
            C.Initialize_Part();

            Dictionary_Character[_Character.ID] = C;

            return C;
        }

        public static Cub.View.Character Remove_Character(Guid _ID)
        {
            Cub.View.Character r = null;
            if (Dictionary_Character.ContainsKey(_ID))
                r = Dictionary_Character[_ID];
            Dictionary_Character.Remove(_ID);
            return r;
        }

        public static Cub.View.Character Get_Character(Guid _ID)
        {
            return Dictionary_Character[_ID];
        }

        public static Cub.View.Character Get_Character(Position2 _Position)
        {
            foreach (Cub.View.Character C in Runtime.Dictionary_Character.Values)
            {
                if (C.Stat.Position == _Position)
                {
                    return C;
                }
            }

            return null;
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

                float Delay = Cub.View.Library.Get_Event(E.Type).Process(E.Data, E.Description);
                GSC.SetCurrentCharacter(E);
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
