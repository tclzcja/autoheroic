using System;
using UnityEngine;

namespace Cub.View
{
    public class Explosion : MonoBehaviour
    {
        public void Start()
        {
            Rigidbody[] RL = this.transform.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody RO in RL)
            {
                RO.AddForce(new Vector3(UnityEngine.Random.Range(-0.5F, 0.5F), 5F, UnityEngine.Random.Range(-0.5F, 0.5F)), ForceMode.Impulse);
            }

            audio.PlayOneShot(Cub.View.Library.Get_Sound(Cub.Sound.Explosion));

            Destroy(this.gameObject, 0.5F);
        }
    }
}
