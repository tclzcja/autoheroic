using UnityEngine;
using System.Collections;

namespace Cub.View.Event
{
    public class DeathEvent : EventParent
    {

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        //This wants {string Character.UniqueName}
        public override void Begin(string desc, System.Collections.Generic.List<object> data)
        {
            base.Begin(desc, data);
//			ClassController who = Manager.CharacterReference[(System.Guid)data[0]];
            //who.DoAnimation(AM.Actions.Attack);
            ActiveChar.DeathSpray.transform.position = ActiveChar.transform.position;
            ActiveChar.DeathSpray.particleSystem.Emit(50);
            Manager.RemoveCharacter(ActiveChar);
        }
    }
}