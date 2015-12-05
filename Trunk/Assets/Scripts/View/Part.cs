using System;
using UnityEngine;

namespace Cub.View
{
    public class Part : MonoBehaviour
    {
        public void Idle(float _After)
        {
            Invoke("_Idle", _After);
        }

        private void _Idle()
        {
            this.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}