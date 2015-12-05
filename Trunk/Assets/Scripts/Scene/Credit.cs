using System;
using UnityEngine;

namespace Cub.Scene
{
    public class Credit : MonoBehaviour
    {
        public void Start()
        {
            Cub.View.Library.Unlock();
            Cub.View.Library.Initialization();

            Cub.Model.Character C0 = new Model.Character();
            C0.Name = "fdsa";
            C0.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            C0.Info = new Model.Character_Info();
            C0.Info.Head = Part_Head.Soldier;
            C0.Info.Body = Part_Body.Medium;
            C0.Info.Arms = Part_Arms.Sniper_Rifle;
            C0.Info.Legs = Part_Legs.Humanoid;
            C0.Stat = new Model.Character_Stat();
            C0.Stat.Position = new Position2(0, 0);
            C0.Stat.Team = new Model.Team();
            C0.Stat.Team.Colour_Primary = new Color32(24, 24, 24, 255);
            C0.Stat.Team.Colour_Secondary = new Color32(48, 214, 48, 255);

            Cub.Model.Character C1 = new Model.Character();
            C1.Name = "fdsa";
            C1.ID = new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1);
            C1.Info = new Model.Character_Info();
            C1.Info.Head = Part_Head.Protector;
            C1.Info.Body = Part_Body.Heavy;
            C1.Info.Arms = Part_Arms.Sword;
            C1.Info.Legs = Part_Legs.Humanoid;
            C1.Stat = new Model.Character_Stat();
            C1.Stat.Position = new Position2(2, 0);
            C1.Stat.Team = new Model.Team();
            C1.Stat.Team.Colour_Primary = new Color32(255, 0, 0, 255);
            C1.Stat.Team.Colour_Secondary = new Color32(200, 0, 0, 255);

            Cub.View.Runtime.Add_Character(C0);
            Cub.View.Runtime.Add_Character(C1);

            Cub.View.Runtime.Get_Character(new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)).gameObject.transform.Rotate(new Vector3(0, 20, 0));
            Cub.View.Runtime.Get_Character(new Guid(2, 4, 1, 1, 5, 1, 1, 7, 3, 1, 1)).gameObject.transform.Rotate(new Vector3(0, -20, 0));

            Sha();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.LoadLevel(0);
        }

        public void Sha()
        {
            foreach (Cub.View.Cube CO in GameObject.Find("Name_Misha").GetComponentsInChildren<Cub.View.Cube>())
            {
                iTween.ShakeScale(CO.gameObject, new Vector3(0.8F, 0.8F, 0.8F), 1.0F);
            }

            foreach (Cub.View.Cube CO in GameObject.Find("Name_CJ").GetComponentsInChildren<Cub.View.Cube>())
            {
                iTween.ShakeScale(CO.gameObject, new Vector3(0.8F, 0.8F, 0.8F), 1.0F);
            }

            Invoke("Sha", 1.5F);
        }
    }
}
