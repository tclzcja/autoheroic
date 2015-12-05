using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AM.Actions
{
    public class Attack : AM.Actions.Action
    {
        public override string Name { get { return "Attack"; } }

        public override void Start()
        {
            Destroy(this.Target_List[1]);
            Destroy(this.gameObject, 1.0F);
        }
    
        public override void Update()
        {
    
        }
    }
}