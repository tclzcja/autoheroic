using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AM
{
    [Serializable]
    public class Character : MonoBehaviour
    {
        public string Info_Name { get; set; }
        public bool Info_Gender { get; set; }

        public int Stats_HP { get; set; }
        public List<Vector2> Stats_Sight { get; set; }
        public Vector2 Stats_Position { get; set; }

        /*
        public int Attributes_Level { get; set; }
        public int Attributes_Strength { get; set; } //Stablity
        public int Attributes_Dexterity { get; set; } //Moving Speed
        public int Attributes_Perception { get; set; } //Sense Range
        public int Attributes_Constitution { get; set; } //Health Point
        public int Attributes_Intelligence { get; set; } //Tactic Slot
        public int Attributes_Willpower { get; set; } //Success Rate
         * */

        public List<AM.Actions.Action> List_Action { get; set; }
        public List<AM.Equipments.Equipment> List_Equipment { get; set; }
        public List<AM.Tactic> List_Tactic { get; set; }

        public delegate void Handler(GameObject GO);
        public event Handler Event_Outputing; //Casting Spell - Affecting others
        public event Handler Event_Inputing; //Suffering from Spell - Being affected by others

        public void Input(GameObject GO)
        {
            Event_Inputing(GO);
        }

        public void Output(GameObject GO)
        {
            Event_Outputing(GO);
        }

        /*
        public void MakeAction()
        {
            foreach (AM.Tactic T in this.List_Tactic)
            {
                List<AM.Character> LCO = new List<Character>();

                switch (T.Target)
                {
                    case Type.TTarget.Self:
                        {
                            LCO.Add(this);
                            break;
                        }
                    case Type.TTarget.Enemy:
                        {
                            foreach (Vector2 V2 in this.Stats_Sight)
                            {
                                if (Field.Units[(int)V2.x][(int)V2.y] != null && V2 != this.Stats_Position)
                                {
                                    LCO.Add(Field.Units[(int)V2.x][(int)V2.y].GetComponent<AM.Character>());
                                }
                            }
                            break;
                        }
                }



            }

        }
         * */

        //Try to use Input and Output in Actions.Action

        //All actions are gameObjects. gameObjects register events

        public void Pump(string _Name, bool _Gender, List<AM.Actions.Action> _LAO, List<AM.Equipments.Equipment> _LEO, List<Tactic> _LTO)
        {
            this.Info_Name = _Name;
            this.Info_Gender = _Gender;

            /*
            this.Attributes_Level = 1;

            this.Attributes_Constitution = 10;
            this.Attributes_Dexterity = 10;
            this.Attributes_Intelligence = 10;
            this.Attributes_Perception = 10;
            this.Attributes_Strength = 10;
            this.Attributes_Willpower = 10;
            */

            this.List_Action = _LAO;
            this.List_Equipment = _LEO;
            this.List_Tactic = _LTO;

        }

        private void Start()
        {
            Wut();
        }

        private void Wut()
        {
            Core.Determine(this);
            Invoke("Wut", 2.0F);
        }
    }
}