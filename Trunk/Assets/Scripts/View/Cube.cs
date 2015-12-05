using System;
using UnityEngine;

namespace Cub.View
{
    public class Cube : MonoBehaviour
    {
        private const float Timespan = 2.0F;
        private bool falling = false;

        public void Fall()
        {
            if (falling)
                return;
            falling = true;
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.AddComponent<BoxCollider>();

            this.transform.parent = null;

            this.collider.material = Library.Get_Physic_Material();

            this.rigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-2F, 2F), 2F, UnityEngine.Random.Range(-2F, 2F)), ForceMode.Impulse);

            Destroy(this.gameObject, Timespan);
        }

        public void OnCollisionEnter(Collision collision)
        {
            iTween.ScaleTo(this.gameObject, Vector3.zero, Timespan);
        }
    }
}
