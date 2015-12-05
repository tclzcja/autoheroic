using System;
using UnityEngine;

namespace Cub.View
{
    public class Cube : MonoBehaviour
    {
        public CubeType CubeType = CubeType.Black;
        bool Dying = false;

        void OnCollisionEnter(Collision collision)
        {
            if (!Dying && collision.gameObject.CompareTag("Terrain"))
            {
                if (UnityEngine.Random.Range(0, 2) == 1)
                    Destroy(this.gameObject);
                else
                {
                    float time = UnityEngine.Random.Range(4.0f, 12.0f);
                    iTween.ScaleTo(this.gameObject, Vector3.zero, time);
                    Destroy(this.gameObject, time);
                    Dying = true;
                }
            }
        }

        public void SetMaterial(CubeType ct, bool TeamOne)
        {
            CubeType = ct;
            renderer.material = Cub.View.Library.Get_Cube(CubeType,TeamOne);
        }
    }
}
