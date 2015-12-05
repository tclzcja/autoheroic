using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TacticBoxController : MonoBehaviour {

    InterfaceController IC;
    Cub.Model.Character Who = null;
    public Cub.Model.Tactic Tactic = null;
    public int Number;
    UILabel NumLabel;
    UILabel ActionDesc;
    UILabel ConditionDesc;
    //UILabel Between;
    UIPopupList ActionList;
    UIPopupList ConditionList;
    UIButton DeleteButton;

	// Use this for initialization
    void Awake()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Number":
                    NumLabel = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Action Description":
                    ActionDesc = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Condition Description":
                    ConditionDesc = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Action List":
                    ActionList = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                case "Condition List":
                    ConditionList = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                //case "Between Label":
                //    Between = (UILabel)child.gameObject.GetComponent("UILabel");
                //    break;
                case "Tactic Remover":
                    DeleteButton = (UIButton)child.gameObject.GetComponent("UIButton");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Imprint(int n, Cub.Model.Character who, Cub.Model.Tactic tac)
    {
        Who = who;
        Number = n;
        NumLabel.text = (Number + 1).ToString();
        if (who != null && who.Tactics.Count > Number)
        {
            Tactic = tac;
            ActionList.items = new List<string> { };
            if (Tactic.Free)
            {
                ((UILabel)ActionList.gameObject.GetComponent("UILabel")).color = new Color(0.5f, 0.5f, 0.5f);
                ((UILabel)ConditionList.gameObject.GetComponent("UILabel")).color = new Color(0.5f, 0.5f, 0.5f);
                DeleteButton.gameObject.SetActive(false);
            }
            else
            {
                //Cub.Action oldAct = Tactic.A;
                List<Cub.Model.Action.Base> acts = Cub.Model.Library.List_Actions(who.Info.Class);
                foreach (Cub.Model.Action.Base act in acts)
                    ActionList.items.Add(act.Name);
                DeleteButton.gameObject.SetActive(true);
            }
            ActionList.value = Cub.Model.Library.Get_Action(Tactic.A).Name;
            ConditionList.value = Cub.Model.Library.Get_Condition(Tactic.C).Name;
        }
        else
        {
            Tactic = null;
            ActionDesc.text = "";
            ConditionDesc.text = "";
        }
    }

    public void Refresh()
    {
        ActionList.items = new List<string> { };
        List<Cub.Model.Action.Base> acts = Cub.Model.Library.List_Actions(Who.Info.Class);
        Debug.Log(acts.Count);
        bool unlucky = true;
        foreach (Cub.Model.Action.Base act in acts)
        {
            ActionList.items.Add(act.Name);
            if (act.ActionType == Tactic.A)
                unlucky = false;
        }
        if (unlucky)
        {
            Cub.Action a = Cub.Action.Attack;
            foreach (Cub.Action sp in Who.Info.List_Special_Ability)
                a = sp;
            Tactic.SetAction(a);
            if (!Cub.Model.Library.List_Conditions(a).Contains(Cub.Model.Library.Get_Condition(Tactic.C)))
                Tactic.SetCondition(Cub.Condition.Any);
        }
        ActionList.value = Cub.Model.Library.Get_Action(Tactic.A).Name;
        ConditionList.value = Cub.Model.Library.Get_Condition(Tactic.C).Name;
    }

    public void NewActionSelected()
    {
        if (UIPopupList.current != null && Tactic != null)
        {
            string text = UIPopupList.current.isLocalized ?
                Localization.Get(UIPopupList.current.value) :
                UIPopupList.current.value;
            Tactic.SetAction(Cub.Model.Library.String_Action(text));
            SetAction();
        }
    }

    public void SetAction()
    {
        if (Tactic == null) return;
        ActionDesc.text = Cub.Model.Library.Get_Action(Tactic.A).Description;
        ConditionList.items = new List<string> { };
        List<Cub.Model.Condition.Base> cons = Cub.Model.Library.List_Conditions(Tactic.A);
        foreach (Cub.Model.Condition.Base con in cons)
            ConditionList.items.Add(con.Name);
        if (!ConditionList.items.Contains(Cub.Model.Library.Get_Condition(Tactic.C).Name))
        {
            Tactic.SetCondition(Cub.Condition.Any);
            ConditionList.value = Cub.Model.Library.Get_Condition(Tactic.C).Name;
        }
    }

    public void SetCondition()
    {
        if (Tactic == null) return;
        ConditionDesc.text = Cub.Model.Library.Get_Condition(Tactic.C).Description;
    }

    public void NewConditionSelected()
    {
        if (UIPopupList.current != null && Tactic != null)
        {
            string text = UIPopupList.current.isLocalized ?
                Localization.Get(UIPopupList.current.value) :
                UIPopupList.current.value;
            Tactic.SetCondition(Cub.Model.Library.String_Condition(text));
            //if (Tactic.C == Cub.Condition.None) return;
            SetCondition();
        }
    }

    public void RemoveTactic()
    {
        Who.Tactics.Remove(Tactic);
        IC.TeamEditor.CharEditor.Refresh();
    }

    public void MoveTacticUp()
    {
        List<Cub.Model.Tactic> tactics = Who.Tactics;
        int placement = tactics.IndexOf(Tactic);
        if (placement == 0) return;
        tactics.RemoveAt(placement);
        tactics.Insert(placement - 1, Tactic);
        IC.TeamEditor.CharEditor.Refresh();
    }

    public void MoveTacticDown()
    {
        List<Cub.Model.Tactic> tactics = Who.Tactics;
        int placement = tactics.IndexOf(Tactic);
        if (placement >= tactics.Count - 1) return;
        tactics.RemoveAt(placement);
        tactics.Insert(placement + 1, Tactic);
        IC.TeamEditor.CharEditor.Refresh();
    }
}
