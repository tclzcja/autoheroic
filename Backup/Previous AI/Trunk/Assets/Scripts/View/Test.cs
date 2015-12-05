using System;
using UnityEngine;

namespace Cub.View
{
    public class Test : MonoBehaviour
    {
        public void Awake()
        {

            Cub.Tool.Library.Initialization();
            Cub.View.Library.Initialization();

            /*

            Character_Model CM = new Character_Model();
            CM.Position_Body = new Vector3(0, 0, 0);
            CM.Position_Body_Foot_Left = new Vector3(-1, -1, -1);
            CM.Position_Body_Foot_Right = new Vector3(1, 1, 1);
            CM.Position_Body_Hand_Left = new Vector3(0, 0, 0);
            CM.Position_Body_Hand_Right = new Vector3(0, 0, 0);
            CM.Position_Body_Head = new Vector3(0, 0, 0);
            CM.Position_Hand_Left_Equipment_Left = new Vector3(0, 0, 0);
            CM.Position_Hand_Right_Equipment_Right = new Vector3(0, 0, 0);

            CM.Rotation_Body = new Vector3(0, 0, 0);
            CM.Rotation_Body_Foot_Left = new Vector3(0, 0, 0);
            CM.Rotation_Body_Foot_Right = new Vector3(0, 0, 0);
            CM.Rotation_Body_Hand_Left = new Vector3(0, 0, 0);
            CM.Rotation_Body_Hand_Right = new Vector3(0, 0, 0);
            CM.Rotation_Body_Head = new Vector3(0, 0, 0);
            CM.Rotation_Hand_Left_Equipment_Left = new Vector3(0, 0, 0);
            CM.Rotation_Hand_Right_Equipment_Right = new Vector3(0, 0, 0);

            CM.Model_Body = new System.Collections.Generic.List<Cubon>();
            CM.Model_Body.Add(new Cubon(new Color32(255, 0, 0, 255), new System.Collections.Generic.List<Position3>() { new Position3(0, 0, 0), new Position3(1, 0, 0) }));


            CM.Model_Equipment_Left = new System.Collections.Generic.List<Cubon>();
            CM.Model_Equipment_Right = new System.Collections.Generic.List<Cubon>();
            CM.Model_Foot_Left = new System.Collections.Generic.List<Cubon>();
            CM.Model_Foot_Right = new System.Collections.Generic.List<Cubon>();
            CM.Model_Hand_Left = new System.Collections.Generic.List<Cubon>();
            CM.Model_Hand_Right = new System.Collections.Generic.List<Cubon>();
            CM.Model_Head = new System.Collections.Generic.List<Cubon>();

            Cub.Tool.Xml.Serialize(CM, "Data/VVV.xml");
             * 
             * */
        }

        public void Start()
        {
            Tool.Character C1 = new Tool.Character(Class.Knight, 0, 0);

            Runtime.Add_Character(C1, true);

            /*
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Move, "", new System.Collections.Generic.List<object>() { C1.ID, 2, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Move, "", new System.Collections.Generic.List<object>() { C1.ID, 1, 1 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Be_Attacked, "", new System.Collections.Generic.List<object>() { C1.ID, 2 }));
            Runtime.Add_Eventon(new Cub.View.Eventon(Cub.Event.Die, "", new System.Collections.Generic.List<object>() { C1.ID }));

            GameObject.Find("Runtime").SendMessage("Run_Eventon");
            */
        }

        public void Update()
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject.Destroy(GameObject.Find("Character(Clone)"));

                Cub.View.Library.Unlock();
                Cub.View.Library.Initialization();

                this.Start();
            }

            /*
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject Knight = GameObject.Find("Knight");
                Animator A = Knight.GetComponent<Animator>();
                A.SetTrigger("Move");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject Knight = GameObject.Find("Knight");
                Animator A = Knight.GetComponent<Animator>();
                A.SetTrigger("Idle");
            }
             * */
        }
    }
}
