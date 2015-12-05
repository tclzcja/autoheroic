using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventParent : MonoBehaviour {

	protected EventController Manager;
	public float TimerMax;
	protected float Timer;
	protected ClassController ActiveChar = null;

	// Use this for initialization
	void Start () {
	
	}

	protected virtual void Initialize(){
		Manager = (EventController)GetComponent("EventController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	virtual public void Begin(List<string> data){
		Timer = TimerMax;
		if (data.Count == 0) Debug.Log("0 Length Data");
		//Debug.Log(Manager.CharacterReference.ContainsKey(data[0]));
		if (Manager.CharacterReference.ContainsKey(data[0])){
			ActiveChar = Manager.CharacterReference[data[0]];
			Manager.NameTextOn(ActiveChar.Name);
		}
	}

	virtual public void Continue(){
		Timer -= Time.deltaTime;
	}

	virtual public void End(){
		if (ActiveChar != null)
			Manager.NameTextOff();
	}

	virtual public bool StillRunning(){
		if (Timer > 0) return true;
		return false;
	}

	//Here's a list of types of Events and the data they want:
	//Attack: {string Character.UniqueName}
	//Walk: {string Character.UniqueName, float Destination X Coord, float Destination Y Coord}
}
