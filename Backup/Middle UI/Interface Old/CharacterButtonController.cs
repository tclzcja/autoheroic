using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterButtonController : MonoBehaviour {

    InterfaceController IC;
    public Cub.Model.Character_Save Who = null;
    int Number;
    UILabel NumTxt;
    UILabel Name;
    UILabel Class;
    UILabel Cost;

	// Use this for initialization
	void Awake () {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Character Class":
                    Class = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Character Name":
                    Name = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Character Points":
                    Cost = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Number":
                    NumTxt = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Imprint(int num, Cub.Model.Character_Save who)
    {
        if (num != -1)
        {
            Number = num;
            NumTxt.text = (Number + 1).ToString();
        }
        if (who != null)
        {
            Who = who;
            Name.text = who.Name;
            Class.text = "???";
            Cost.text = who.Value.ToString() + "pts";
        }
        else
        {
            Who = null;
            Name.text = "-Empty-";
            Class.text = "--";
            Cost.text = "0pts";
        }
    }

    public void GetClicked()
    {
        IC.TeamEditor.CharEditor.Imprint(Who,this);
    }
}
