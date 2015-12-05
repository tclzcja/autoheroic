using UnityEngine;
using System.Collections;

namespace Cub.View.Event
{
    public class GameOverEvent : EventParent
    {
		
		// Use this for initialization
		void Start () {
			Initialize();
		}
		
		//This wants {string Character.UniqueName}
        public override void Begin(string desc, System.Collections.Generic.List<object> data)
		{
			base.Begin(desc, data);
            foreach (ClassController c in Manager.CharacterReference.Values)
                c.DoAnimation(Cub.Animation.Character_Win);
		}

        public override void Continue()
        {
            base.Continue();
            
        }

		public override bool StillRunning ()
		{
            return true;
		}
    }
}
