using UnityEngine;
using System.Collections;

namespace Cub.View.Event
{

    public class BeHealedEvent : EventParent
    {

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        //This wants {string Character.UniqueName, int DamageTaken}
        public override void Begin(string desc, System.Collections.Generic.List<object> data)
        {
            base.Begin(desc, data);
            int dam = (int)data[1];
            ActiveChar.DoAnimation(Cub.Animation.Character_Be_Healed);
        }
    }
}