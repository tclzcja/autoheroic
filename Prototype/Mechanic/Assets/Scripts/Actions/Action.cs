using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AM.Actions
{
    public abstract class Action : MonoBehaviour
    {
        public abstract string Name { get; }

        protected List<GameObject> Target_List { get; set; }

        public abstract void Start();

        public abstract void Update();

        public void Pump(List<GameObject> _TL)
        {
            this.Target_List = _TL;
        }
    }
}