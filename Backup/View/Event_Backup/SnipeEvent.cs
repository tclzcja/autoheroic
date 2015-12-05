using UnityEngine;
using System.Collections;

namespace Cub.View.Event
{
    public class SnipeEvent : EventParent
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
			//string projType = data[2];
			string projType = "Sniper Shot";
			//Debug.Log("Fire Projectile: " + projType);
			if (Manager.ProjectileReference.ContainsKey(projType)){
				Proj = Manager.ProjectileReference[projType];
				Proj.transform.position = ActiveChar.transform.position;
				Proj.transform.LookAt(Target.transform);
			}
			ActiveChar.DoAnimation(Cub.Animation.Character_Attack_Range);
			//Timer = TimerMax;
		}
		
		public override void Continue ()
		{
			base.Continue ();
			if (Proj != null){
				Vector3 where = Proj.transform.position;
				float speed = 0.1f;
				Vector3 dir = (Target.transform.position - where).normalized;
				where += new Vector3(dir.x * speed, dir.y * speed, dir.z * speed);
				Proj.transform.position = where;
			}
		}
		
		public override void End ()
		{
			if (Proj != null)
				Proj.transform.position = new Vector3(9999,9999,9999);
			Proj = null;
			base.End ();
		}
		
		public override bool StillRunning ()
		{
			if (Proj != null){
				if (Vector3.Distance(Proj.transform.position,Target.transform.position) <= 0.1f)
					return false;
				else
					return true;
			}
			else
				return base.StillRunning ();
		}
    }
}
