using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Snipe : Base
    {
        public const float Timespan = 0.5F;

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

            GameObject W = UnityEngine.Object.Instantiate(Library.Get_Warhead()) as GameObject;

            W.transform.position = C0.transform.position;

            iTween.MoveTo(W, iTween.Hash("position", C1.transform.position, "time", Timespan, "easetype", iTween.EaseType.linear));

            Cub.View.NarratorController.DisplayText(Desc, 2.0f);

            C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Snipe));

            Cub.View.Indicator.Generate(C0.Stat.Position, C1.Stat.Position);

            return Timespan;
        }
    }
}
