using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Be_Attacked : Base
    {
        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);
            int A = (int)_Data[1] * 5;

            Rigidbody[] RL = C.gameObject.transform.GetComponentsInChildren<Rigidbody>(true);

            while (A > 0)
            {
                GameObject GO = RL[UnityEngine.Random.Range(0, RL.Length)].gameObject;

                GO.rigidbody.useGravity = true;
                GO.rigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-5F, 5F), 0, UnityEngine.Random.Range(-5F, 5F)), ForceMode.Impulse);
                GO.GetComponent<BoxCollider>().enabled = true;
                
                //GOL[I].GetComponent<TrailRenderer>().enabled = true;

                A--;
            }

            return 1.5F;
        }
    }
}
