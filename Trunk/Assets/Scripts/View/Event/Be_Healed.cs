using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Be_Healed : Base
    {
        private const float Timespan = 1.0F;
        private const int Healing_Number = 50;
        private const float Healing_Timespan = 1.0F;

        public override float Process(List<object> _Data, string Desc)
        {
            Vector3 Healing_Initial_Size = new Vector3(3, 3, 3);

            Material M = Cub.View.Library.Get_Material();
            M.color = new Color(0.6F, 1F, 0F);

            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);

            List<Vector3> CL_Head = new List<Vector3>();
            foreach (Cubon CB in C.Part.Head)
            {
                CL_Head.AddRange(CB.Position);
            }
            List<Vector3> CL_Body = new List<Vector3>();
            foreach (Cubon CB in C.Part.Body)
            {
                CL_Body.AddRange(CB.Position);
            }
            List<Vector3> CL_Arms_Left = new List<Vector3>();
            foreach (Cubon CB in C.Part.Arms_Left)
            {
                CL_Arms_Left.AddRange(CB.Position);
            }
            List<Vector3> CL_Arms_Right = new List<Vector3>();
            foreach (Cubon CB in C.Part.Arms_Right)
            {
                CL_Arms_Right.AddRange(CB.Position);
            }
            List<Vector3> CL_Legs_Left = new List<Vector3>();
            foreach (Cubon CB in C.Part.Legs_Left)
            {
                CL_Legs_Left.AddRange(CB.Position);
            }
            List<Vector3> CL_Legs_Right = new List<Vector3>();
            foreach (Cubon CB in C.Part.Legs_Right)
            {
                CL_Legs_Right.AddRange(CB.Position);
            }

            foreach (Transform T in C.gameObject.transform.FindChild("Head").GetComponentsInChildren<Transform>())
            {
                if (CL_Head.Contains(T.localPosition))
                {
                    CL_Head.Remove(T.localPosition);
                }
            }

            foreach (Transform T in C.gameObject.transform.FindChild("Body").GetComponentsInChildren<Transform>())
            {
                if (CL_Body.Contains(T.localPosition))
                {
                    CL_Body.Remove(T.localPosition);
                }
            }

            foreach (Transform T in C.gameObject.transform.FindChild("Arms_Left").GetComponentsInChildren<Transform>())
            {
                if (CL_Arms_Left.Contains(T.localPosition))
                {
                    CL_Arms_Left.Remove(T.localPosition);
                }
            }

            foreach (Transform T in C.gameObject.transform.FindChild("Arms_Right").GetComponentsInChildren<Transform>())
            {
                if (CL_Arms_Right.Contains(T.localPosition))
                {
                    CL_Arms_Right.Remove(T.localPosition);
                }
            }

            foreach (Transform T in C.gameObject.transform.FindChild("Legs_Left").GetComponentsInChildren<Transform>())
            {
                if (CL_Legs_Left.Contains(T.localPosition))
                {
                    CL_Legs_Left.Remove(T.localPosition);
                }
            }

            foreach (Transform T in C.gameObject.transform.FindChild("Legs_Right").GetComponentsInChildren<Transform>())
            {
                if (CL_Legs_Right.Contains(T.localPosition))
                {
                    CL_Legs_Right.Remove(T.localPosition);
                }
            }

            int Count = Healing_Number;

            bool Knock = false;

            while (Count > 0 && Knock == false)
            {
                Knock = true;

                if (CL_Head.Count > 0 && Knock)
                {
                    GameObject G = GameObject.Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = C.transform.FindChild("Head");
                    G.transform.localPosition = CL_Head[0];
                    G.transform.localScale = Healing_Initial_Size;
                    iTween.ScaleTo(G, new Vector3(1, 1, 1), Healing_Timespan + UnityEngine.Random.Range(-0.3F, 0.3F));
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = M;

                    CL_Head.Remove(CL_Head[0]);

                    Count--;
                    Knock = false;
                }

                if (CL_Body.Count > 0 && Knock)
                {
                    GameObject G = GameObject.Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = C.transform.FindChild("Body");
                    G.transform.localPosition = CL_Body[0];
                    G.transform.localScale = Healing_Initial_Size;
                    iTween.ScaleTo(G, new Vector3(1, 1, 1), Healing_Timespan + UnityEngine.Random.Range(-0.3F, 0.3F));

                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = M;

                    CL_Body.Remove(CL_Body[0]);

                    Count--;
                    Knock = false;
                }

                if (CL_Arms_Left.Count > 0 && Knock)
                {
                    GameObject G = GameObject.Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = C.transform.FindChild("Arms_Left");
                    G.transform.localPosition = CL_Arms_Left[0];
                    G.transform.localScale = Healing_Initial_Size;
                    iTween.ScaleTo(G, new Vector3(1, 1, 1), Healing_Timespan + UnityEngine.Random.Range(-0.3F, 0.3F));

                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = M;

                    CL_Arms_Left.Remove(CL_Arms_Left[0]);

                    Count--;
                    Knock = false;
                }

                if (CL_Arms_Right.Count > 0 && Knock)
                {
                    GameObject G = GameObject.Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = C.transform.FindChild("Arms_Right");
                    G.transform.localPosition = CL_Arms_Right[0];
                    G.transform.localScale = Healing_Initial_Size;
                    iTween.ScaleTo(G, new Vector3(1, 1, 1), Healing_Timespan + UnityEngine.Random.Range(-0.3F, 0.3F));

                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = M;

                    CL_Arms_Right.Remove(CL_Arms_Right[0]);

                    Count--;
                    Knock = false;
                }

                if (CL_Legs_Left.Count > 0 && Knock)
                {
                    GameObject G = GameObject.Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = C.transform.FindChild("Legs_Left");
                    G.transform.localPosition = CL_Legs_Left[0];
                    G.transform.localScale = Healing_Initial_Size;
                    iTween.ScaleTo(G, new Vector3(1, 1, 1), Healing_Timespan + UnityEngine.Random.Range(-0.3F, 0.3F));

                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = M;

                    CL_Legs_Left.Remove(CL_Legs_Left[0]);

                    Count--;
                    Knock = false;
                }

                if (CL_Legs_Right.Count > 0 && Knock)
                {
                    GameObject G = GameObject.Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = C.transform.FindChild("Legs_Right");
                    G.transform.localPosition = CL_Legs_Right[0];
                    G.transform.localScale = Healing_Initial_Size;
                    iTween.ScaleTo(G, new Vector3(1, 1, 1), Healing_Timespan + UnityEngine.Random.Range(-0.3F, 0.3F));

                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = M;

                    CL_Legs_Right.Remove(CL_Legs_Right[0]);

                    Count--;
                    Knock = false;
                }
            }

            C.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Be_Healed");
            C.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Be_Healed");
            C.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Be_Healed");
            C.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Be_Healed");
            C.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Be_Healed");
            C.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Be_Healed");

            C.BroadcastMessage("Idle", Timespan, SendMessageOptions.DontRequireReceiver);

            int hp = (int)_Data[2];
            int mhp = (int)_Data[3];

            Cub.View.Runtime.GSCStatic.SetHealth(C.Stat.ID, hp, mhp);

            return Timespan;
        }
    }
}
