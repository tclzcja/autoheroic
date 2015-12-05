using UnityEngine;
using System.Collections;

public class AttackEvent : EventParent {



	// Use this for initialization
	void Start () {
		Initialize();
	}

	//This wants {string Character.UniqueName}
	public override void Begin (System.Collections.Generic.List<string> data)
	{
		base.Begin(data);
		ActiveChar.DoAnimation(Cub.Type.Anim.Attack);
		//Timer = TimerMax;
	}

}
