using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class GameplayTeamPickerController : MonoBehaviour {

    GameplayScreenController GSC;
    //##----------------------------------Needs one per team selector!!!!!!
    Team TeamOne = null;
    Team TeamTwo = null;
    List<Cub.Tool.Team> TeamsOne;
    List<Cub.Tool.Team> TeamsTwo;
    List<UIPopupList> Selectors = new List<UIPopupList>();
    Dictionary<string, Team> TeamDictionaryOne = new Dictionary<string, Team>();
    Dictionary<string, Team> TeamDictionaryTwo = new Dictionary<string, Team>();
    UIPopupList SelOne;
    UIPopupList SelTwo;
    UIButton ReadyOne;
    UIButton ReadyTwo;
    bool T1Ready = false;
    bool T2Ready = false;
    bool GameStarted = false;

	// Use this for initialization
	void Start () {
        GSC = (GameplayScreenController)GameObject.Find("Scene Master").GetComponent("GameplayScreenController");
        TeamsOne = LoadTeams();
        TeamsTwo = LoadTeams();
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team Picker One":
                    SelOne = (UIPopupList)child.gameObject.GetComponentInChildren(System.Type.GetType("UIPopupList"));
                    Selectors.Add(SelOne);
                    break;
                case "Team Picker Two":
                    SelTwo = (UIPopupList)child.gameObject.GetComponentInChildren(System.Type.GetType("UIPopupList"));
                    Selectors.Add(SelTwo);
                    break;
                case "Team One Ready":
                    ReadyOne = (UIButton)child.gameObject.GetComponent("UIButton");
                    break;
                case "Team Two Ready":
                    ReadyTwo = (UIButton)child.gameObject.GetComponent("UIButton");
                    break;
            }
        }
        List<string> teamNames = new List<string> { "-Select a Team-" };
        foreach (Team t in TeamsOne)
        {
            string name = t.Name;
            while (teamNames.Contains(name))
                name += "+";
            teamNames.Add(name);
            TeamDictionaryOne.Add(name, t);
        }
        teamNames = new List<string> { "-Select a Team-" };
        foreach (Team t in TeamsTwo)
        {
            string name = t.Name;
            while (teamNames.Contains(name))
                name += "+";
            teamNames.Add(name);
            TeamDictionaryTwo.Add(name, t);
        }
        foreach (UIPopupList pop in Selectors)
        {
            pop.items = teamNames;
            pop.value = teamNames[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStarted) return;
        if (T1Ready && SelOne.value == SelOne.items[0])
            TeamOneButton();
        if (T2Ready && SelTwo.value == SelTwo.items[0])
            TeamTwoButton();
        if (T1Ready && T2Ready && TeamOne != null && TeamTwo != null)
        {
            GameStarted = true;
            GSC.StartGame(TeamOne, TeamTwo);
        }
            
	}

    List<Team> LoadTeams()
    {
        string name = typeof(List<Team>).AssemblyQualifiedName;
		List<Team> r = (List<Team>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/TeamSaves.xml");
		foreach (Team t in r)
			foreach (Character c in t.List_Character)
				c.Stat.SetTeam(t);
		return r;
    }

    public void TeamOneButton()
    {
        if (T1Ready)
        {
            T1Ready = false;
            ReadyOne.defaultColor = Color.white;
        }
        else if (SelOne.value != SelOne.items[0])
        { 
            T1Ready = true;
            ReadyOne.defaultColor = Color.green;
        }
        ((UISprite)ReadyOne.GetComponent("UISprite")).color = ReadyOne.defaultColor;
    }

    public void TeamTwoButton()
    {
        if (T2Ready)
        {
            T2Ready = false;
            ReadyTwo.defaultColor = Color.white;
        }
        else if (SelTwo.value != SelTwo.items[0])
        {
            T2Ready = true;
            ReadyTwo.defaultColor = Color.green;
        }
        ((UISprite)ReadyTwo.GetComponent("UISprite")).color = ReadyTwo.defaultColor;
    }

    public void TeamOneSelect()
    {
        if (SelOne != null && TeamDictionaryOne.ContainsKey(SelOne.value))
        {
            TeamOne = TeamDictionaryOne[SelOne.value];
        }
        else
        {
            TeamOne = null;
        }
    }

    public void TeamTwoSelect()
    {
        if (SelTwo != null && TeamDictionaryTwo.ContainsKey(SelTwo.value))
        {
            TeamTwo = TeamDictionaryTwo[SelTwo.value];
        }
        else
        {
            TeamTwo = null;
        }
    }
}
