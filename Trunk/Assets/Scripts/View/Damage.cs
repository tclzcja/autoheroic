using System;
using UnityEngine;

namespace Cub.View
{
    public static class Damage
    {
        private const float Timespan = 1F;

        public static void Create(int _Damage, GameObject _Whom)
        {
            Cubon _Cubon = Library.Get_Damage(_Damage);

            GameObject _GO = new GameObject();
            _GO.transform.position = _Whom.transform.position + new Vector3(0, 2, 0);
            _GO.transform.LookAt(Kamera._Camera.transform);
            _GO.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);

            Material _M = new Material(Library.Get_Material());
            _M.color = Color.red;

            foreach (Vector3 V in _Cubon.Position)
            {
                GameObject _Cube = UnityEngine.GameObject.Instantiate(Library.Get_Cube()) as GameObject;
                _Cube.renderer.material = _M;

                _Cube.transform.parent = _GO.transform;
                _Cube.transform.localPosition = V;
                _Cube.transform.localRotation = Quaternion.identity;
                _Cube.transform.localScale = new Vector3(0.9F, 0.9F, 0.9F);

                iTween.ShakePosition(_Cube, new Vector3(0.1F, 0, 0), 0.5F);
            }

            UnityEngine.GameObject.Destroy(_GO, Timespan);
        }
    }
}
