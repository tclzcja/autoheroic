using System;
using UnityEngine;

namespace Cub.View
{
    public class Warhead : MonoBehaviour
    {
        public void Start()
        {
            iTween.RotateAdd(this.gameObject.transform.FindChild("Head").gameObject, iTween.Hash("amount", new Vector3(0, 0, 7200), "time", 10.0F, "easetype", iTween.EaseType.linear));
            Destroy(this.gameObject, Cub.View.Event.Attack_Snipe.Timespan);
        }
    }
}
