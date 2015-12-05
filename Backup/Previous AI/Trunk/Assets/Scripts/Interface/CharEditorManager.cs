using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Tool;

public class CharEditorManager : MonoBehaviour {

    InterfaceController IC;
    UIInput Name;
    //UILabel Class;
    UILabel Cost;
    UILabel HP;
    UILabel Range;
    UILabel Speed;
    UIGrid Grid;
    public List<TacticBoxController> Tactics = new List<TacticBoxController>();
    public GameObject TacticBoxType;
    CharacterButtonController CharButton;
    public Cub.Tool.Character Who;
    UIPopupList ClassList;
    
    void Awake()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Character Name":
                    Name = (UIInput)child.gameObject.GetComponent("UIInput");
                    break;
                case "Class List":
                    ClassList = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                case "Points Total":
                    Cost = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "HPReadout":
                    HP = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "RangeReadout":
                    Range = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "SpeedReadout":
                    Speed = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
        Grid = (UIGrid)gameObject.GetComponentInChildren(System.Type.GetType("UIGrid"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(Cub.Tool.Character who, CharacterButtonController button)
    {
        CharButton = button;
        foreach (Transform tran in Grid.transform)
            if (tran.gameObject.name == "AI Panel(Clone)")
                DestroyObject(tran.gameObject);
        Tactics = new List<TacticBoxController>();
        IC.TeamEditor.SPButtonColors(who);
        if (who != null)
        {
            Who = who;
            Name.value = who.Name;
            ClassList.value = who.Info.Class.ToString();
            ClassList.items = new List<string> { };
            foreach (Cub.Tool.Character_Info ci in Library.List_Classes())
                if (ci.Class != Cub.Class.None)
                    ClassList.items.Add(ci.Class.ToString());
            Cost.text = who.Value.ToString() + "pts";
            HP.text = who.Info.MHP.ToString();
            Range.text = who.Info.Range.ToString();
            Speed.text = who.Info.Speed.ToString();
            //int n = 0;
            //foreach (Tactic tac in who.Tactics)
            for (int n = 0; n < who.Tactics.Count; n++)
            {
                TacticBoxController tbc = (TacticBoxController)NGUITools.AddChild(Grid.gameObject, TacticBoxType).GetComponent("TacticBoxController");
                Tactics.Add(tbc);
                tbc.Imprint(n, who, who.Tactics[n]);
            }
        }
        else
        {
            Who = null;
            Name.value = "---";
            ClassList.value = "";
            Cost.text = "";
            HP.text = "-";
            Range.text = "-";
            Speed.text = "-";
        }
        Grid.repositionNow = true;
        //Grid.Reposition();
    }

    public void Refresh()
    {
        Imprint(Who, CharButton);
        IC.TeamEditor.PointsReadoutUpdate();
    }

    public void AddEmptyTactic()
    {
        if (Who == null) return;
        TacticBoxController tbc = (TacticBoxController)NGUITools.AddChild(Grid.gameObject, TacticBoxType).GetComponent("TacticBoxController");
        Tactics.Add(tbc);
        Tactic tac = Who.BuyTactic(Cub.Condition.Any, Cub.Action.Explore);
        tbc.Imprint(Tactics.Count - 1, Who, tac);
        Grid.repositionNow = true;
        IC.TeamEditor.PointsReadoutUpdate();
    }

    public void UpdateName()
    {
        if (Who == null) return;
        Who.SetName(Name.value);
        CharButton.Imprint(-1, Who);
    }

    public void UpdateClass()
    {
        if (Who == null) return;
        string clss = ClassList.value;
        Cub.Class newClass = (Cub.Class)System.Enum.Parse(typeof(Cub.Class), clss);
        if (newClass == Who.Info.Class)
            return;
        Who.SetClass(newClass);
        CharButton.Imprint(-1, Who);
        //foreach (Cub.Action act in Who.Info.List_Special_Ability)
        //{
        //    Tactic special = AddEmptyTactic();
        //    special.SetAction(act);
        //}
        foreach (TacticBoxController tbc in Tactics)
        {
            tbc.Refresh();
        }
        IC.TeamEditor.PointsReadoutUpdate();
    }
}
