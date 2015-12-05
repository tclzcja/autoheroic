using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Kamera : MonoBehaviour
    {
        private static bool _Lock;

        private static GameObject _Lock_Target;

        public static GameObject _Camera { get; set; }

        public void Start()
        {
            Kamera._Camera = GameObject.Find("Main Camera");

            //Kamera._Camera.transform.rotation = Quaternion.Euler(30, 225, 0);
        }

        public void Update()
        {
            if (!_Lock)
            {
                Kamera._Camera.transform.LookAt(new Vector3(4F, -0.5F, 2.5F));
            }
            else
            {
                Kamera._Camera.transform.LookAt(_Lock_Target.transform);
            }
        }

        public static void Shake()
        {
            iTween.ShakePosition(_Camera, new Vector3(0.3F, 0.3F, 0.3F), 0.2F);
        }

        public static void Unlock()
        {
            _Lock = false;
            iTween.MoveTo(_Camera, iTween.Hash("path", new Vector3[] { new Vector3(-2, 8, -6), new Vector3(10, 8, -6) }, "time", 180F, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
        }

        public static void Lock(GameObject _Target)
        {
            _Lock = true;
            _Lock_Target = _Target;
        }
    }
}
