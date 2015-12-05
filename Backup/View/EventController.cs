using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub;
using Cub.View.Event;

namespace Cub.View
{
    public class EventController : MonoBehaviour
    {

        //	public IDictionary<string,GameObject> Classes;
        Dictionary<Cub.Class, ClassController> ClassReference = new Dictionary<Cub.Class, ClassController>();
        public List<GameObject> Classes;
        Dictionary<Cub.Terrain, TerrainController> TerrainReference = new Dictionary<Cub.Terrain, TerrainController>();
        public List<GameObject> Terrains;
        TerrainController[,] TerrainMap;
        List<ClassController> Characters = new List<ClassController>();
        public Dictionary<System.Guid, ClassController> CharacterReference = new Dictionary<System.Guid, ClassController>();
        public List<GameObject> Projectiles;
        public Dictionary<string, GameObject> ProjectileReference = new Dictionary<string, GameObject>();
        Dictionary<Cub.Event, EventParent> Events = new Dictionary<Cub.Event, EventParent>();
        public EventParent CurrentEvent = null;
        List<Eventon> EventStack = new List<Eventon>();
        TextMesh NameText;
        bool SetupComplete = false;
        List<string> Teams = new List<string>();
        public List<Material> TeamColors = new List<Material>();
        public GameObject InterfaceControllerType;
        InterfaceController InterfaceController = null;
        public bool InterfaceTestMode;
        bool InterfacePhaseOver = false;
        //public EMInterface Interface;



        // Use this for initialization
        void Start()
        {
            NameText = (TextMesh)GameObject.Find("Name").GetComponent("TextMesh");
        }

        // Update is called once per frame
        void Update()
        {
            if (InterfaceTestMode && !SetupComplete)
            {
                Cub.Tool.Main.Initialization(true);
                SetupInterfaceManager();
            }
            if (!SetupComplete || (InterfaceTestMode && !InterfacePhaseOver))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //if (InterfaceTestMode && InterfaceController != null && InterfaceController.InterfaceComplete)
                    //{
                    //    Setup(InterfaceController.Data);
                    //    Destroy(InterfaceController.gameObject);
                    //    InterfacePhaseOver = true;
                    //}
                    //else if (!InterfaceTestMode)
                    //{
                    Cub.Tool.Main.Initialization(false);
                    Setup(ForgeSetupData());
                    //}
                }
                return;
            }
            List<Cub.View.Eventon> GEL = null;
            //if (CurrentEvent == null)
            GEL = Cub.Tool.Main.Go();
            if (GEL != null)
                foreach (Cub.View.Eventon e in GEL)
                {
                    QueueEvent(e);
                }
            if (CurrentEvent != null)
            {
                if (CurrentEvent.StillRunning())
                {
                    CurrentEvent.Continue();
                }
                else
                {
                    CurrentEvent.End();
                    GotoNextEvent();
                }
            }
            //            if (Input.GetKeyDown(KeyCode.Space))
            //            {

            //			ClassController randomGuy = Characters[Random.Range(0,Characters.Count)];
            //			QueueEvent(new GameEvent(GEventType.Walk,new List<string>{randomGuy.UniqueName,
            //				Random.Range(0,10).ToString(),Random.Range(0,10).ToString()}));
            //			  //randomGuy, new Vector2(Random.Range(0,10),Random.Range(0,10))));
            //            }
            //		if (Input.GetKeyDown(KeyCode.Z)){
            //			ClassController randomGuy = Characters[Random.Range(0,Characters.Count)];
            //			QueueEvent(new GameEvent(GEventType.Attack,new List<string>{randomGuy.UniqueName}));
            //		}
            //		if (Input.GetKeyDown(KeyCode.X)){
            //			ClassController randomGuy = Characters[Random.Range(0,Characters.Count)];
            //			QueueEvent(new GameEvent(GEventType.Die,new List<string>{randomGuy.UniqueName}));
            //		}
        }

        Cub.View.SetupData ForgeSetupData()
        {
            Cub.View.SetupData SData = new Cub.View.SetupData();
            SData.StageData();
            foreach (Cub.Tool.Team team in Cub.Tool.Main.List_Team)
            {
                List<Cub.Tool.Character> CL = team.Return_List_Character();
                foreach (Cub.Tool.Character man in CL)
                {
                    SData.AddCharacter(man.Name, man.ID, man.Info.Class,
                         new Vector2(man.Stat.Position.X, man.Stat.Position.Y), man.Stat.Team.Name);
                }
            }
            return SData;
        }

        void SetupInterfaceManager()
        {
            Cub.View.SetupData SData = new Cub.View.SetupData();
            SData.StageData();
            InterfaceController = (InterfaceController)((GameObject)Instantiate(InterfaceControllerType, Vector3.zero, Quaternion.identity))
                .GetComponent("InterfaceController");
            //InterfaceController.Setup(this, SData);
        }

        public void Setup(SetupData data)
        {
            //First off, how do we know what to build when we're told that a class/obj/whatever is needed?

            //These will be largely asked for as strings, so let's set up some easy ways to convert those strings into objects.
            SetupRefs();
            TerrainMap = new TerrainController[data.TerrainMap.GetLength(0), data.TerrainMap.GetLength(1)];
            for (int i = 0; i < data.TerrainMap.GetLength(0); i++)
                for (int j = 0; j < data.TerrainMap.GetLength(1); j++)
                {
                    TerrainController tt = GetTerrain(data.TerrainMap[i, j]);
                    GameObject go = (GameObject)Instantiate(tt.gameObject, new Vector3(i, 0, j), Quaternion.identity);
                    TerrainMap[i, j] = (TerrainController)go.GetComponent("TerrainController");
                }
            //int n = Random.Range(0,999999);
            foreach (CharController c in data.Characters)
            {
                //n++;
                //ClassController cl = null;
                if (ClassReference.ContainsKey(c.Class))
                {
                    GameObject go = (GameObject)Instantiate(GetClass(c.Class).gameObject,
                       new Vector3(c.Location.x, 0.5f, c.Location.y), Quaternion.identity);
                    ClassController cc = (ClassController)go.GetComponent("ClassController");
                    cc.ImprintFrom(c);
                    Characters.Add(cc);
                    //cc.UniqueName = cc.Name + n;
                    CharacterReference.Add(cc.Id, cc);
                    if (!Teams.Contains(cc.Team))
                        Teams.Add(cc.Team);
                    Material teamColor = TeamColors[0];
                    if (Teams.IndexOf(cc.Team) < TeamColors.Count)
                        teamColor = TeamColors[Teams.IndexOf(cc.Team)];
                    ((MeshRenderer)go.GetComponent("MeshRenderer")).material = teamColor;
                }
            }
            SetupComplete = true;
        }

        void SetupRefs()
        {
            foreach (GameObject c in Classes)
            {
                if (c.GetComponent("ClassController") == null)
                    continue;
                ClassController cont = (ClassController)c.GetComponent("ClassController");
                ClassReference.Add(cont.Class, cont);
            }
            foreach (GameObject c in Terrains)
            {
                if (c.GetComponent("TerrainController") == null)
                    continue;
                TerrainController cont = (TerrainController)c.GetComponent("TerrainController");
                TerrainReference.Add(cont.TerrainType, cont);
            }
            foreach (GameObject p in Projectiles)
            {
                ProjectileReference.Add(p.name,
                                        (GameObject)Instantiate(p, new Vector3(9999, 1999, 9999), Quaternion.identity));
            }
            Events.Add(Cub.Event.Move, (EventParent)GetComponent("WalkEvent"));
            Events.Add(Cub.Event.Attack_Range, (EventParent)GetComponent("AttackEvent"));
            Events.Add(Cub.Event.Be_Attacked, (EventParent)GetComponent("TakeDamageEvent"));
            Events.Add(Cub.Event.Die, (EventParent)GetComponent("DeathEvent"));
            Events.Add(Cub.Event.Attack_Rocket, (EventParent)GetComponent("MissileEvent"));
            Events.Add(Cub.Event.Attack_Snipe, (EventParent)GetComponent("SnipeEvent"));
            Events.Add(Cub.Event.Attack_Heal, (EventParent)GetComponent("HealEvent"));
            Events.Add(Cub.Event.Be_Healed, (EventParent)GetComponent("BeHealedEvent"));
            Events.Add(Cub.Event.Win, (EventParent)GetComponent("GameOverEvent"));
        }

        ClassController GetClass(Cub.Class c)
        {
            if (ClassReference.ContainsKey(c))
                return ClassReference[c];
            return null;
        }

        TerrainController GetTerrain(Cub.Terrain name)
        {
            if (TerrainReference.ContainsKey(name))
                return TerrainReference[name];
            Debug.Log("UHOH: " + name);
            return null;
        }

        EventParent GetEvent(Cub.Event name)
        {
            if (Events.ContainsKey(name))
                return Events[name];
            return null;
        }

        public void QueueEvent(Eventon e)
        {
            EventStack.Add(e);
            if (CurrentEvent == null)
                StartEvent(EventStack[0]);
        }

        public void StartEvent(Eventon e)
        {
            EventParent manager = GetEvent(e.Type);
            if (manager == null)
                return;
            CurrentEvent = manager;
            manager.Begin(e.Description, e.Data);
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

        void GotoNextEvent()
        {
            CurrentEvent = null;
            EventStack.RemoveAt(0);
            if (EventStack.Count > 0)
                StartEvent(EventStack[0]);
        }

        public void RemoveCharacter(ClassController who)
        {
            Characters.Remove(who);
            CharacterReference.Remove(who.Id);
            who.gameObject.transform.position = new Vector3(9999, 9999, 9999);
        }

        public void NameTextOn(string text)
        {
            NameText.text = text;
        }

        public void NameTextOff()
        {
            NameText.text = "";
        }
    }
}