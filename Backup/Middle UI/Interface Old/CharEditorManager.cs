using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.Model;

public class CharEditorManager : MonoBehaviour {

    InterfaceController IC;
    UIInput Name;
    //UILabel Class;
    UILabel Cost;
    UILabel HP;
    UILabel Range;
    UILabel Speed;
    CharacterButtonController CharButton;
    public Cub.Model.Character_Save Who;
    HeadMenuController Head;
    ArmsMenuController Arms;
    BodyMenuController Body;
    LegsMenuController Legs;
    
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
                case "Head Menu":
                    Head = (HeadMenuController)child.gameObject.GetComponent("HeadMenuController");
                    break;
                case "Arms Menu":
                    Arms = (ArmsMenuController)child.gameObject.GetComponent("ArmsMenuController");
                    break;
                case "Body Menu":
                    Body = (BodyMenuController)child.gameObject.GetComponent("BodyMenuController");
                    break;
                case "Legs Menu":
                    Legs = (LegsMenuController)child.gameObject.GetComponent("LegsMenuController");
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(Cub.Model.Character_Save who, CharacterButtonController button)
    {
        CharButton = button;
        IC.TeamEditor.SPButtonColors(who);
        if (who != null)
        {
            Who = who;
            Name.value = who.Name;
            //ClassList.value = who.Info.Class.ToString();
            //ClassList.items = new List<string> { };
            //foreach (Cub.Tool.Character_Info ci in Library.List_Classes())
            //    if (ci.Class != Cub.Class.None)
            //        ClassList.items.Add(ci.Class.ToString());
            Cost.text = who.Value.ToString() + "pts";
            HP.text = who.Health.ToString();
            Range.text = who.Weapon.Range.ToString();
            Speed.text = who.Speed.ToString();
            Head.Imprint(who);
            Arms.Imprint(who);
            Body.Imprint(who);
            Legs.Imprint(who);
            ////int n = 0;
            ////foreach (Tactic tac in who.Tactics)
            //for (int n = 0; n < who.Tactics.Count; n++)
            //{
            //    TacticBoxController tbc = (TacticBoxController)NGUITools.AddChild(Grid.gameObject, TacticBoxType).GetComponent("TacticBoxController");
            //    Tactics.Add(tbc);
            //    tbc.Imprint(n, who, who.Tactics[n]);
            //}
        }
        else
        {
            Who = null;
            Name.value = "---";
            Cost.text = "";
            HP.text = "-";
            Range.text = "-";
            Speed.text = "-";
        }
        //Grid.Reposition();
    }

    public void Refresh()
    {
        Imprint(Who, CharButton);
        IC.TeamEditor.PointsReadoutUpdate();
    }

    public void UpdateName()
    {
        if (Who == null) return;
        Who.Name = Name.value;
        CharButton.Imprint(-1, Who);
    }
}
