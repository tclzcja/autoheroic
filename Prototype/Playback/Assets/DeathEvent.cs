using UnityEngine;
using System.Collections;

public class DeathEvent : EventParent {

	// Use this for initialization
	void Start () {
		Initialize();
	}
	
	//This wants {string Character.UniqueName}
	public override void Begin (System.Collections.Generic.List<string> data)
	{
		base.Begin(data);
		ClassController who = Manager.CharacterReference[data[0]];
		//who.DoAnimation(AM.Actions.Attack);
		who.DeathSpray.transform.position = who.transform.position;
		who.DeathSpray.particleSystem.Emit(50);
		Manager.RemoveCharacter(who);
	}
}
