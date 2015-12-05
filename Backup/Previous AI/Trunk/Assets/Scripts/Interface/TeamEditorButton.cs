using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamEditorButton : MonoBehaviour {

    Cub.Tool.Team Team = null;
    InterfaceController IC;
    int TeamNum;
    //string TeamName = "-Empty-";
    //string OwnerName = "";
    UILabel Num;
    UILabel Name;
    UILabel Owner;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Setup(int num)
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Player Name":
                    Owner = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Team Name":
                    Name = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Team Number":
                    Num = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
        TeamNum = num;
        Team = LoadTeam(TeamNum);
        UpdateNames();
    }

    public void PickTeam()
    {
        IC.TeamEditor.gameObject.SetActive(true);
        IC.TeamEditor.ImprintTeam(Team,this);
        IC.TeamPicker.gameObject.SetActive(false);
    }

    public void UpdateNames()
    {
        Owner.text = Team.Owner_Name;
        Name.text = Team.Name;
        Num.text = "Team " + (TeamNum + 1).ToString();
    }

    Cub.Tool.Team LoadTeam(int num)
    {
        List<Cub.Tool.Team> teams = IC.TeamPicker.Teams;
        if (num >= teams.Count) 
            return new Cub.Tool.Team();
        return teams[num];
    }
}
