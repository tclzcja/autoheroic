using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Die : Base
    {
        private const float Timespan = 1.5F;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);

            /*
            C.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Die");
            C.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Die");
             * */

            Cube[] CL = C.GetComponentsInChildren<Cube>();

            foreach (Cube CO in CL)
            {
                int Flag = UnityEngine.Random.Range(0, 3);

                if (Flag == 1)
                {
                    CO.Fall();
                }
                else
                {
                    GameObject.Destroy(CO.gameObject);
                }
            }

            C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Die));

            Runtime.Remove_Character((Guid)_Data[0]);

            Cub.View.NarratorController.DisplayText(Desc, Timespan);

            GameObject.Destroy(C.gameObject, Timespan);

            Cub.View.Runtime.GSCStatic.SetHealth(C.Stat.ID, 0, 4);
            //bool teamOne = false;
            //if (C.Stat.Team != Cub.View.Runtime.GSCStatic.TeamOne)
            //    teamOne = true;
            //Cub.View.Runtime.GSCStatic.SetScore(teamOne, C.Stat.Value);

            return Timespan;
        }
    }
}
