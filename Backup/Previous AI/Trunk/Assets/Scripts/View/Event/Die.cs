using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Die : Base
    {
        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.Character C = Runtime.Get_Character((Guid)_Data[0]);

            Rigidbody[] RL = C.gameObject.transform.GetComponentsInChildren<Rigidbody>(true);
            BoxCollider[] BL = C.gameObject.transform.GetComponentsInChildren<BoxCollider>(true);

            foreach (Rigidbody R in RL)
            {
                R.useGravity = true;
                R.AddForce(new Vector3(UnityEngine.Random.Range(-2, 2), 0, UnityEngine.Random.Range(-2, 2)), ForceMode.Impulse);
            }

            foreach (BoxCollider B in BL)
            {
                B.enabled = true;
            }
            C.PlaySound(Cub.View.Library.Get_Sound("Scream"));
            Runtime.Remove_Character((Guid)_Data[0]);

            GameObject.Destroy(C.gameObject, 3.0F);

            return 1.5F;
        }
    }
}
