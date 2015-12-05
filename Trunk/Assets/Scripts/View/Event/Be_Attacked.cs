using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Be_Attacked : Base
    {
        private const float Timespan = 1.5F;
        private const int Falling_Number = 50;

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);
            Attack_Result AR = (Attack_Result)_Data[4];
            int Amount = (int)_Data[1];

            C.transform.FindChild("Head").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Body").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Arms_Left").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Arms_Right").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Legs_Left").GetComponent<Animator>().SetTrigger("Be_Attacked");
            C.transform.FindChild("Legs_Right").GetComponent<Animator>().SetTrigger("Be_Attacked");

            C.BroadcastMessage("Idle", 1.0F, SendMessageOptions.DontRequireReceiver);

            Cube[] CO = C.gameObject.transform.GetComponentsInChildren<Cube>();

            for (int i = 0; i < Falling_Number * Amount; i++)
            {
                CO[UnityEngine.Random.Range(0, CO.Length)].Fall();
            }

            Cub.View.Damage.Create(Amount, C.gameObject);

            //Cub.View.Kamera.Shake();
            switch (AR)
            {
                case Attack_Result.Hit:
                    {
                        C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Scream_Hit));
                        //Debug.Log("FDSA");
                        break;
                    }
                case Attack_Result.Miss:
                    {
                        C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Scream_Crit));
                        //Debug.Log("FDSA");
                        break;
                    }
                case Attack_Result.Crit:
                    {
                        C.PlaySound(Cub.View.Library.Get_Sound(Cub.Sound.Scream_Miss));
                        break;
                    }
            }            

            int hp = (int)_Data[2];
            int mhp = (int)_Data[3];

            Cub.View.Runtime.GSCStatic.SetHealth(C.Stat.ID, hp, mhp);

            return Timespan;
        }
    }
}
