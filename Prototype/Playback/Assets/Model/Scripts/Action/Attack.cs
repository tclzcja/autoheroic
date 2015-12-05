using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Action
{
    public class Attack : Cub.Action.Base
    {
        public override string Name { get { return "Attack"; } }

        public override float Time_Casting { get { return 0F; } }
        public override float Time_Cooldown { get { return 0F; } }

		public override void Body(EventController con)
        {
			con.QueueEvent(new AM.GameEvent(Cub.Type.GEventType.Attack,new List<string>{Target[0].UName}));
            Debug.Log("Attack: " + this.Target[0].Class + ">" + this.Target[1].Class);
            bool kill = this.Target[1].Damage(2);
			if (kill){
				con.QueueEvent(new AM.GameEvent(Cub.Type.GEventType.Die,new List<string>{Target[1].UName}));
			}
        }
    }
}
