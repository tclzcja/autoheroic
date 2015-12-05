using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Model;

public class TeamPickerController : MonoBehaviour {

    //InterfaceController IC;
    UIGrid Grid;
    public List<TeamSave> Teams = new List<TeamSave>();
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

    List<TeamSave> LoadTeams()
    {
        string name = typeof(List<TeamSave>).AssemblyQualifiedName;
        return (List<TeamSave>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/Team_Saves.xml");
    }

    public void SaveTeams()
    {
        Cub.Tool.Xml.Serialize(Teams, "Data/Team_Saves.xml");
    }

    public void AddNewTeam()
    {
        TeamEditorButton teb = (TeamEditorButton)NGUITools.AddChild(Grid.gameObject, TeamButtonType).GetComponent("TeamEditorButton");
        Buttons.Add(teb);

        TeamSave team = new TeamSave();
        team.Name = "";
        team.Owner_Name = "";
        team.Chars = new List<Character_Save>();
        Teams.Add(team);

        teb.Setup(Teams.Count - 1);

        Grid.repositionNow = true;
    }
}
