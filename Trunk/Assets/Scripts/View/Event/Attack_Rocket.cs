using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Attack_Rocket : Base
    {
        public const float Timespan = 2.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C0 = Runtime.Get_Character((Guid)_Data[0]);
            Vector2 impact = (Vector2)_Data[1];
            Vector3 target = new Vector3(impact.x, -0.35f, impact.y);

            C0.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Attack_Rocket");

            GameObject R = UnityEngine.Object.Instantiate(Library.Get_Rocket(), C0.transform.position, Quaternion.identity) as GameObject;
            R.SendMessage("Pump", target);

            C0.BroadcastMessage("Idle", 1.0F, SendMessageOptions.DontRequireReceiver);

            Cub.View.NarratorController.DisplayText(Desc, 2.0f);

            C0.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Rocket));

            //Cub.View.Indicator.Cross(new Position2((int)impact.x, (int)impact.y));

            return Timespan;
        }
    }
}
