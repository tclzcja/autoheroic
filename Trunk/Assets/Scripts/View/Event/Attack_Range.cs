using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Range : Base
    {
        private const float Timespan = 0.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            float time = Timespan;

            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            Cub.Attack_Result AR = (Attack_Result)_Data[2];

            C0.transform.LookAt(new Vector3(C1.transform.position.x, 0, C1.transform.position.z));

            C0.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Attack_Range");            
            C0.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Attack_Range");

            C0.BroadcastMessage("Idle", Timespan + 0.5F, SendMessageOptions.DontRequireReceiver);

            GameObject B = UnityEngine.Object.Instantiate(Library.Get_Bullet()) as GameObject;
            B.transform.position = C0.transform.FindChild("Arms_Right").position;
            switch (AR)
            {
                case Attack_Result.Hit:
                    {
                        Material M = new Material(Library.Get_Material());
                        M.color = new Color(3F, 3F, 3F);
                        B.GetComponent<TrailRenderer>().material = M;
                        iTween.MoveTo(B, iTween.Hash("position", C1.transform.position, "time", Timespan, "easetype", iTween.EaseType.linear));
                        GameObject.Destroy(B, Timespan + 0.5F);
                        C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Gun_Hit));
                        break;
                    }
                case Attack_Result.Crit:
                    {
                        Material M = new Material(Library.Get_Material());
                        M.color = new Color(255F, 0F, 0F);
                        B.GetComponent<TrailRenderer>().material = M;
                        iTween.MoveTo(B, iTween.Hash("position", C1.transform.position, "time", Timespan, "easetype", iTween.EaseType.linear));
                        GameObject.Destroy(B, Timespan + 0.5F);
                        C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Gun_Crit));
                        Cub.View.Damage.Create(99, C0.gameObject);
                        break;
                    }
                case Attack_Result.Miss:
                    {
                        Material M = new Material(Library.Get_Material());
                        M.color = new Color(2F, 2F, 2F);
                        B.GetComponent<TrailRenderer>().material = M;
                        
                        B.transform.LookAt(C1.transform);

                        B.AddComponent<Rigidbody>();                        
                        B.rigidbody.useGravity = true;
                        B.rigidbody.AddRelativeForce(new Vector3(0, 0, 5F), ForceMode.Impulse);

                        C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Gun_Miss));

                        GameObject.Destroy(B, Timespan + 1.5F);

                        time = 1.5f;

                        break;
                    }
            }

            //Cub.View.Indicator.Generate(C0.Stat.Position, C1.Stat.Position);

            Cub.View.NarratorController.DisplayText(Desc, Timespan);

            return time;
        }
    }
}
