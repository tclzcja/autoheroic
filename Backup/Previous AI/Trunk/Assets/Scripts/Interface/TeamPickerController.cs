using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class TeamPickerController : MonoBehaviour {

    //InterfaceController IC;
    UIGrid Grid;
    public List<Team> Teams = new List<Team>();
    List<TeamEditorButton> Buttons = new List<TeamEditorButton>();
    public GameObject TeamButtonType;
    
    // Use this for initialization
	void Start () {
        //IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        Grid = (UIGrid)gameObject.GetComponentInChildren(System.Type.GetType("UIGrid"));
        Teams = LoadTeams();
        BuildButtons();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel("Main Menu");
	}

    public void BuildButtons()
    {
        foreach (Transform tran in Grid.transform)
            if (tran.gameObject.name == "Team Button(Clone)")
                DestroyObject(tran.gameObject);
        //int n = 0;
        //foreach (Team team in Teams)
        for (int n = 0; n < Teams.Count;n++)   
        {
            TeamEditorButton teb = (TeamEditorButton)NGUITools.AddChild(Grid.gameObject, TeamButtonType).GetComponent("TeamEditorButton");
            Buttons.Add(teb);
            teb.Setup(n);
        }
        Grid.repositionNow = true;
    }

    List<Team> LoadTeams()
    {
        string name = typeof(List<Team>).AssemblyQualifiedName;
        return (List<Team>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/TeamSaves.xml");
    }

    public void SaveTeams()
    {
        Cub.Tool.Xml.Serialize(Teams,"Data/TeamSaves.xml");
    }

    public void AddNewTeam()
    {
        TeamEditorButton teb = (TeamEditorButton)NGUITools.AddChild(Grid.gameObject, TeamButtonType).GetComponent("TeamEditorButton");
        Buttons.Add(teb);

        Team team = new Team();
        Teams.Add(team);

        teb.Setup(Teams.Count - 1);

        Grid.repositionNow = true;
    }
}
