using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AM;

public class EventController : MonoBehaviour {


//	public IDictionary<string,GameObject> Classes;
	Dictionary<Cub.Type.Class,ClassController> ClassReference = new Dictionary<Cub.Type.Class, ClassController>();
	public List<GameObject> Classes;
	Dictionary<Cub.Type.Terrain,TerrainController> TerrainReference = new Dictionary<Cub.Type.Terrain, TerrainController>();
	public List<GameObject> Terrains;
	TerrainController[,] TerrainMap;
	List<ClassController> Characters = new List<ClassController>();
	public Dictionary<string,ClassController> CharacterReference = new Dictionary<string, ClassController>();
	Dictionary<Cub.Type.GEventType,EventParent> Events = new Dictionary<Cub.Type.GEventType, EventParent>();
	public EventParent CurrentEvent = null;
	List<GameEvent> EventStack = new List<GameEvent>();
	TextMesh NameText;
	//public EMInterface Interface;
	Cub.Main Model;


	// Use this for initialization
	void Start () {
		NameText = (TextMesh)GameObject.Find("Name").GetComponent("TextMesh");
	}
	
	// Update is called once per frame
	void Update () {
		if (Model == null){
			if (Input.GetKeyDown(KeyCode.Space)) {
				Model = new Cub.Main(this);
				Model.Awake();
				Model.Start();
			}
			return;
		}
		Model.Go();
		if (CurrentEvent != null){
			if (CurrentEvent.StillRunning()){
				CurrentEvent.Continue();
			}
			else{
				CurrentEvent.End();
				GotoNextEvent();
			}
		}
		if (Input.GetKeyDown(KeyCode.Space)) {

//			ClassController randomGuy = Characters[Random.Range(0,Characters.Count)];
//			QueueEvent(new GameEvent(GEventType.Walk,new List<string>{randomGuy.UniqueName,
//				Random.Range(0,10).ToString(),Random.Range(0,10).ToString()}));
//			  //randomGuy, new Vector2(Random.Range(0,10),Random.Range(0,10))));
		}
//		if (Input.GetKeyDown(KeyCode.Z)){
//			ClassController randomGuy = Characters[Random.Range(0,Characters.Count)];
//			QueueEvent(new GameEvent(GEventType.Attack,new List<string>{randomGuy.UniqueName}));
//		}
//		if (Input.GetKeyDown(KeyCode.X)){
//			ClassController randomGuy = Characters[Random.Range(0,Characters.Count)];
//			QueueEvent(new GameEvent(GEventType.Die,new List<string>{randomGuy.UniqueName}));
//		}
	}

	public void Setup(SetupData data){
		//First off, how do we know what to build when we're told that a class/obj/whatever is needed?
		
		//These will be largely asked for as strings, so let's set up some easy ways to convert those strings into objects.
		SetupRefs();
		Debug.Log(data.TerrainMap);
		TerrainMap = new TerrainController[data.TerrainMap.GetLength(0),data.TerrainMap.GetLength(1)];
		for (int i = 0; i < data.TerrainMap.GetLength(0); i++)
			for (int j = 0; j < data.TerrainMap.GetLength(1); j++){
			TerrainController tt = GetTerrain(data.TerrainMap[i,j]);
			GameObject go =	(GameObject)Instantiate(tt.gameObject, new Vector3(i, 0, j), Quaternion.identity);
			TerrainMap[i,j] = (TerrainController)go.GetComponent("TerrainController");
		}
		//int n = Random.Range(0,999999);
		foreach (CharController c in data.Characters){
			//n++;
			ClassController cl = null;
			Debug.Log(c.Name);
			if (ClassReference.ContainsKey(c.Class)){
				GameObject go = (GameObject)Instantiate(GetClass(c.Class).gameObject,
				   new Vector3(c.Location.x, 0.5f, c.Location.y), Quaternion.identity);
				ClassController cc = (ClassController)go.GetComponent("ClassController");
				cc.ImprintFrom(c);
				Characters.Add(cc);
				//cc.UniqueName = cc.Name + n;
				CharacterReference.Add(cc.UniqueName,cc);
			}
		}
	}

	void SetupRefs(){
		foreach (GameObject c in Classes){
			if (c.GetComponent("ClassController") == null)
				continue;
			ClassController cont = (ClassController)c.GetComponent("ClassController");
			ClassReference.Add(cont.Class, cont);
		}
		foreach (GameObject c in Terrains){
			if (c.GetComponent("TerrainController") == null)
				continue;
			TerrainController cont = (TerrainController)c.GetComponent("TerrainController");
			TerrainReference.Add(cont.TerrainType, cont);
		}
		Events.Add(Cub.Type.GEventType.Walk,(EventParent)GetComponent("WalkEvent"));
		Events.Add(Cub.Type.GEventType.Attack,(EventParent)GetComponent("AttackEvent"));
		Events.Add(Cub.Type.GEventType.TakeDamage,(EventParent)GetComponent("TakeDamageEvent"));
		Events.Add(Cub.Type.GEventType.Die,(EventParent)GetComponent("DeathEvent"));
	}

	ClassController GetClass(Cub.Type.Class c){
		if (ClassReference.ContainsKey(c))
			return ClassReference[c];
		return null;
	}

	TerrainController GetTerrain(Cub.Type.Terrain name){
		if (TerrainReference.ContainsKey(name))
			return TerrainReference[name];
		Debug.Log("UHOH: " + name);
		return null;
	}

	EventParent GetEvent(Cub.Type.GEventType name){
		if (Events.ContainsKey(name))
			return Events[name];
		return null;
	}

	public void QueueEvent(GameEvent e){
		EventStack.Add(e);
		if (CurrentEvent == null)
			StartEvent(EventStack[0]);
	}

	public void StartEvent(GameEvent e){
		EventParent manager = GetEvent(e.Type);
		if (manager == null) 
			return;
		CurrentEvent = manager;
		manager.Begin(e.Data);
//		switch (e.Type)
//		{
//		case GEventType.Walk:
//		{
//			((WalkEvent)manager).Begin(e.Data);
//			break;
//		}
//		case GEventType.Attack:
//		{
//			((AttackEvent)manager).Begin(e.MainChar);
//			break;
//		}
//		}
	}

	void GotoNextEvent(){
		CurrentEvent = null;
		EventStack.RemoveAt(0);
		if (EventStack.Count > 0)
			StartEvent(EventStack[0]);
	}

	public void RemoveCharacter(ClassController who){
		Characters.Remove(who);
		CharacterReference.Remove(who.UniqueName);
		who.gameObject.transform.position = new Vector3(9999,9999,9999);
	}

	public void NameTextOn(string text){
		NameText.text = text;
	}

	public void NameTextOff(){
		NameText.text = "";
	}
}
