using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Heal_Harm : Base
    {
        public const float Timespan = 2.0F;

        private const int Amount = 5;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Cub.View.Character C1 = Runtime.Get_Character((Guid)_Data[1]);

            C0.transform.LookAt(new Vector3(C1.transform.position.x, 0, C1.transform.position.z));

            C0.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Attack_Range");
            C0.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Attack_Range");

            C0.BroadcastMessage("Idle", Timespan + 0.5F, SendMessageOptions.DontRequireReceiver);

            for (int i = 0; i < Amount; i++)
            {
                GameObject HO = UnityEngine.Object.Instantiate(Library.Get_Healer(), C0.transform.position, Quaternion.identity) as GameObject;
                HO.SendMessage("Pump", C1.gameObject);
            }

            Cub.View.NarratorController.DisplayText(Desc, Timespan + 0.5F);
            C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Heal));

            return Timespan + Cub.View.Healer.Timespan;
        }
    }
}
