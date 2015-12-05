using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Scene
{
    public class Test : MonoBehaviour
    {
        public void Start()
        {
            Cub.View.Library.Unlock();

            Cub.View.Library.Initialization();

            Cub.Model.Character C0 = new Model.Character();
            C0.Name = "fdsa";
            C0.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            C0.Info = new Model.Character_Info();
            C0.Info.Head = Part_Head.Hunter;
            C0.Info.Body = Part_Body.Light;
            C0.Info.Arms = Part_Arms.Sniper_Rifle;
            C0.Info.Legs = Part_Legs.Hover;
            C0.Stat = new Model.Character_Stat();
            C0.Stat.Position = new Position2(0, 0);
            C0.Stat.Team = new Model.Team();

            Cub.View.Runtime.Add_Character(C0);

            Cub.Model.Character C1 = new Model.Character();
            C1.Name = "fdsa";
            C1.ID = new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1);
            C1.Info = new Model.Character_Info();
            C1.Info.Head = Part_Head.Protector;
            C1.Info.Body = Part_Body.Light;
            C1.Info.Arms = Part_Arms.Sword;
            C1.Info.Legs = Part_Legs.Tread;
            C1.Stat = new Model.Character_Stat();
            C1.Stat.Position = new Position2(0, 3);
            C1.Stat.Team = new Model.Team();

            Cub.View.Runtime.Add_Character(C1);

            Cub.View.Alphabet.Create("ABCDEFGHIJKLMNOPQRSTUVWXYZ", new Vector3(0, 0, 2), new Vector3(0, 0, 0), new Vector3(0.1F, 0.1F, 0.1F), Cub.View.Library.Get_Material());

            /*
            List<Cub.View.Cubon> C = new List<View.Cubon>();
            C.Add(new View.Cubon(Colour.Black, new List<Position3>() { new Position3(0, 0, 0) }));

            Cub.Tool.Xml.Serialize(C, "Data/View_Part_Legs_Right_Hover.xml");*/
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Range, "", false, new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), Attack_Result.Crit, 0 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Rocket, "", new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Vector2(0, 3), 0, 0 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Attacked, "", new List<object>() { new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), 1 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Healed, "", new List<object>() { new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1) }));
                GameObject.Find("Runtime").SendMessage("Run_Eventon", SendMessageOptions.DontRequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Range, "", false, new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), Attack_Result.Hit, 0 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Rocket, "", new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Vector2(0, 3), 0, 0 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Attacked, "", new List<object>() { new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), 1 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Healed, "", new List<object>() { new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1) }));
                GameObject.Find("Runtime").SendMessage("Run_Eventon", SendMessageOptions.DontRequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Range, "", false, new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), Attack_Result.Miss, 0 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Attack_Rocket, "", new List<object>() { new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1), new Vector2(0, 3), 0, 0 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Attacked, "", new List<object>() { new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1), 1 }));
                //Cub.View.Runtime.Add_Eventon(new View.Eventon(Event.Be_Healed, "", new List<object>() { new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1) }));
                GameObject.Find("Runtime").SendMessage("Run_Eventon", SendMessageOptions.DontRequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Character"))
                {
                    DestroyImmediate(GO);
                }
                Cub.View.Library.Unlock();
                Cub.View.Library.Initialization();
                Start();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Cub.View.Damage.Create(8, Cub.View.Runtime.Get_Character(new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)).gameObject);
            }
        }
    }
}
