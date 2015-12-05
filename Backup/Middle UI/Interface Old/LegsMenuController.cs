using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegsMenuController : MonoBehaviour
{

    Cub.Model.Character_Save Who;
    InterfaceController IC;

    UIPopupList Options;
    UILabel Cost;
    UILabel Effects;
    UILabel Desc;
    UISprite Portrait;

    // Use this for initialization
    void Start()
    {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Body Picker":
                    Options = (UIPopupList)child.gameObject.GetComponent("UIPopupList");
                    break;
                case "Cost Label":
                    Cost = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Effects Label":
                    Effects = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Description Label":
                    Desc = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Portrait":
                    Portrait = (UISprite)child.gameObject.GetComponent("UISprite");
                    break;
            }
        }
        Options.items = new List<string> { };
        foreach (Cub.Model.Bodypart bp in Cub.Model.Library.List_Legs())
            Options.items.Add(bp.Name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Imprint(Cub.Model.Character_Save who)
    {
        Who = who;
        Cub.Model.BPLegs H = who.Legs_Part;
        if (Options.value != H.Name)
            Options.value = H.Name;
        Cost.text = "Cost: " + H.Cost.ToString() + "pts";
        Effects.text = "Effects: " + H.Speed + " Speed";
        Desc.text = H.Description;
        Portrait.color = Portrait.color;
    }

    public void UpdateBodypart()
    {
        if (Who == null) return;
        //Cub.Part_Legs current = Who.Legs;
        string bp = Options.value;
        Who.Legs = Cub.Model.Library.Get_Legs(bp);
        //Imprint(Who);
        IC.TeamEditor.Refresh();
        IC.TeamEditor.CharEditor.Refresh();
    }
}
