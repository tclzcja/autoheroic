using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub;

public class TeamEditorController : MonoBehaviour {

    InterfaceController IC;
    UIInput TeamName;
    UIInput OwnerName;
    UILabel NumChars;
    UILabel TotalPts;
    UILabel SaveButt;
    UIGrid CLGrid;
    public CharEditorManager CharEditor;
    GameObject CharList;
    public GameObject CharButtonType;
    List<CharacterButtonController> CButtons = new List<CharacterButtonController>();
    public Cub.Model.TeamSave Team = null;
    TeamEditorButton TeamButton = null;
    Dictionary<Position2, StartingPositionController> SPButtons = new Dictionary<Position2, StartingPositionController>();

	// Use this for initialization
    void Awake()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team Name":
                    TeamName = (UIInput)child.gameObject.GetComponent("UIInput");
                    break;
                case "Owner Name":
                    OwnerName = (UIInput)child.gameObject.GetComponent("UIInput");
                    break;
                case "Num Chars":
                    NumChars = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Total Points":
                    TotalPts = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Char List":
                    CharList = child.gameObject;
                    CLGrid = (UIGrid)CharList.gameObject.GetComponentInChildren(System.Type.GetType("UIGrid"));
                    break;
                case "Char Editor":
                    CharEditor = (CharEditorManager)child.gameObject.GetComponent("CharEditorManager");
                    break;
                case "Dude Placer":
                    foreach (Transform dudeB in child.transform)
                    {
                        StartingPositionController spc = (StartingPositionController)dudeB.gameObject.GetComponent("StartingPositionController");
                        SPButtons.Add(new Position2(spc.X, spc.Y), spc);
                    }
                    break;
                case "Save Button":
                    SaveButt = (UILabel)child.gameObject.GetComponentInChildren(System.Type.GetType("UILabel"));
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseWindow();
	}

    public void ImprintTeam(Cub.Model.TeamSave team, TeamEditorButton button)
    {
        TeamButton = button;
        foreach (Transform tran in CLGrid.transform)
            if (tran.gameObject.name == "CharacterBox(Clone)")
                DestroyObject(tran.gameObject);
        Team = team;
        TeamName.value = Team.Name;
        OwnerName.value = Team.Owner_Name;
        System.Collections.Generic.List<Cub.Model.Character_Save> chars = team.Chars;
        PointsReadoutUpdate();
        int n = 0;
        CButtons = new List<CharacterButtonController>();
        foreach (Cub.Model.Character_Save c in chars)
        {
            CharacterButtonController cbc = (CharacterButtonController)NGUITools.AddChild(CLGrid.gameObject, CharButtonType).GetComponent("CharacterButtonController");
            CButtons.Add(cbc);
            cbc.Imprint(n, c);
            n++;
        }
        CLGrid.Reposition();
        //Find Grid
        //foreach (Transform child in CharList.transform)
        //    if (child.gameObject.name == "CharacterBox")
        //        ((CharacterButtonController)child.gameObject.GetComponent("CharacterButtonController")).Imprint(chars);
        //CharEditor.Imprint(null);
        CLGrid.repositionNow = true;
        foreach (StartingPositionController spc in SPButtons.Values)
        {
            spc.Imprint(null);
        }
        List<Cub.Model.Character_Save> unplaced = new List<Cub.Model.Character_Save>();
        foreach (Cub.Model.Character_Save c in Team.Chars)
        {
            if (SPButtons.ContainsKey(c.Position))
                SPButtons[c.Position].Imprint(c);
            else
                unplaced.Add(c);
        }
        foreach (Cub.Model.Character_Save c in unplaced)
        {
            bool placed = false;
            foreach (StartingPositionController spc in SPButtons.Values)
            {
                if (spc.Who != null)
                    continue;
                spc.Imprint(c);
                c.Position = new Position2(spc.X, spc.Y);
                break;
            }
            if (placed) continue;
        }
        SPButtonColors(null);
        //if (CButtons.Count > 0)
         //   CharEditor.Imprint(CButtons[0].Who, CButtons[0]);
    }

    public void Refresh()
    {
        ImprintTeam(Team, TeamButton);
    }

    public void CloseWindow()
    {
        IC.TeamPicker.gameObject.SetActive(true);
        IC.TeamEditor.gameObject.SetActive(false);
    }

    public void SaveButton()
    {
        if (Team.TotalValue > Cub.Model.Library.PointCap)
        {
            Debug.Log("TRYING TO SAVE WHEN YOU SHOULDN'T");
            //return;
        }
        IC.TeamPicker.gameObject.SetActive(true);
        IC.TeamEditor.gameObject.SetActive(false);
        IC.TeamPicker.SaveTeams();
    }

    public void DeleteButton()
    {
        IC.TeamPicker.gameObject.SetActive(true);
        IC.TeamEditor.gameObject.SetActive(false);
        IC.TeamPicker.Teams.Remove(Team);
        IC.TeamPicker.BuildButtons();
        IC.TeamPicker.SaveTeams();
    }

    public void UpdateTeamName()
    {
        Team.Name = TeamName.value;
        TeamButton.UpdateNames();
    }

    public void UpdateOwnerName()
    {
        Team.Owner_Name = OwnerName.value;
        TeamButton.UpdateNames();
    }

    public void AddNewCharacter()
    {
        if (Team == null || Team.Chars.Count >= 16) return;
        CharacterButtonController cbc = (CharacterButtonController)NGUITools.AddChild(CLGrid.gameObject, CharButtonType).GetComponent("CharacterButtonController");
        CButtons.Add(cbc);
        Cub.Model.Character_Save cha = new Cub.Model.Character_Save("---",Part_Head.Soldier,Part_Arms.Rifle,Part_Body.Medium,Part_Legs.Humanoid,0,0);
        Team.Add_Character(cha);
        cbc.Imprint(CButtons.Count - 1,cha);
        CLGrid.repositionNow = true;
        foreach (StartingPositionController spc in SPButtons.Values)
            if (spc.Who == null)
            {
                spc.Imprint(cha);
                break;
            }
        PointsReadoutUpdate();
        CharEditor.Imprint(cha, cbc);
    }

    public void RemoveCharacter()
    {
        Cub.Model.Character_Save who = CharEditor.Who;
        Team.Remove_Character(who);
        Refresh();
    }

    public void SPButtonClick(StartingPositionController spc)
    {
        if (spc.Who != null) return;
        Cub.Model.Character_Save who = IC.TeamEditor.CharEditor.Who;
        if (SPButtons.ContainsKey(who.Position))
            SPButtons[who.Position].Imprint(null);
        spc.Imprint(IC.TeamEditor.CharEditor.Who);
        who.Position = new Position2(spc.X, spc.Y);
    }

    public void SPButtonColors(Cub.Model.Character_Save who)
    {
        foreach (StartingPositionController spc in SPButtons.Values)
        {
            UIButton butt = (UIButton)spc.gameObject.GetComponent("UIButton");
            UISprite spr = (UISprite)spc.gameObject.GetComponent("UISprite");
            if (spc.Who == who && who != null)
            {
                butt.defaultColor = Color.red;
                spr.color = Color.red;
            }
            else
            {
                butt.defaultColor = Color.white;
                spr.color = Color.white;
            }
        }
    }

    public void PointsReadoutUpdate()
    {
        System.Collections.Generic.List<Cub.Model.Character_Save> chars = Team.Chars;
        NumChars.text = chars.Count.ToString() + " Characters";
        int pts = Team.TotalValue;
        TotalPts.text = pts.ToString() + "pts (" + (Cub.Model.Library.PointCap - pts).ToString() + "pts left)";
        Color col = Color.black;
        if (pts > Cub.Model.Library.PointCap)
            col = Color.red;
        TotalPts.color = col;
        SaveButt.color = col;
    }
}
