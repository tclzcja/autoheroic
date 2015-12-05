using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Move : Base
    {
        private const float Timespan = 1.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);
            int X = (int)_Data[1];
            int Z = (int)_Data[2];

            C.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Move");
            C.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Move");
            C.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Move");
            C.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Move");
            C.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Move");
            C.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Move");

            C.BroadcastMessage("Idle", Timespan, SendMessageOptions.DontRequireReceiver);

            C.transform.LookAt(new Vector3(X, 0, Z));

            iTween.MoveTo(C.gameObject, iTween.Hash("position", new Vector3(X, 0, Z), "time", Timespan, "easetype", iTween.EaseType.linear));

            switch (C.Stat.Legs)
            {
                case Part_Legs.Hover:
                    {
                        C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Move_Hover));
                        break;
                    }
                case Part_Legs.Humanoid:
                    {
                        C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Move_Humanoid));
                        break;
                    }
                case Part_Legs.Tread:
                    {
                        C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Move_Tread));
                        break;
                    }
            }

            C.Stat.Position = new Position2(X, Z);

            Cub.View.NarratorController.DisplayText(Desc, Timespan);

            return Timespan;
        }
    }
}
