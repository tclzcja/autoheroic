using UnityEngine;
using System.Collections;

public class TakeDamageEvent : EventParent {

	// Use this for initialization
	void Start () {
	
	}
	
	//This wants {string Character.UniqueName, int DamageTaken}
	public override void Begin (System.Collections.Generic.List<string> data)
	{
		base.Begin(data);
		ClassController who = Manager.CharacterReference[data[0]];
		int dam = int.Parse(data[1]);
		who.DoAnimation(Cub.Type.Anim.Attack);
	}
}
