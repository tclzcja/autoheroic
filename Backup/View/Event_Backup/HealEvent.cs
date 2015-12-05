using UnityEngine;
using System.Collections;

namespace Cub.View.Event
{
    public class HealEvent : EventParent
    {
		ClassController Target;
		GameObject Proj;
		
		// Use this for initialization
		void Start () {
			Initialize();
		}
		
		//This wants {string Character.UniqueName}
        public override void Begin(string desc, System.Collections.Generic.List<object> data)
		{
			base.Begin(desc, data);
			Target = Manager.CharacterReference[(System.Guid)data[1]];
			ActiveChar.DoAnimation(Cub.Animation.Character_Attack_Heal);
			//Timer = TimerMax;
		}
    }
}
