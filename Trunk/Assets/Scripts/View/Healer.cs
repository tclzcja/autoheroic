using System;
using UnityEngine;

namespace Cub.View
{
    public class Healer : MonoBehaviour
    {
        public const float Timespan = 1F;
        private GameObject G { get; set; }

        public void Pump(GameObject GO)
        {
            this.G = GO;
            Invoke("_Pump", UnityEngine.Random.Range(0.0F, Cub.View.Event.Attack_Heal.Timespan));
        }

        private void _Pump()
        {
            iTween.MoveTo(this.gameObject, iTween.Hash("position", G.transform.position, "time", Timespan, "easetype", iTween.EaseType.linear));
            iTween.RotateBy(this.gameObject, iTween.Hash("amount", new Vector3(1, 1, 1), "time", Timespan, "easetype", iTween.EaseType.linear));
            Destroy(this.gameObject, Timespan);
        }
    }
}
