using UnityEngine;
using System.Collections;
using Cub;

public class StartingPositionController : MonoBehaviour {

    InterfaceController IC;
    public int X;
    public int Y;
    UILabel Label;
    public Cub.Model.Character_Save Who = null;

	// Use this for initialization
	void Awake () {
        IC = (InterfaceController)GameObject.Find("UI Root").GetComponent("InterfaceController");
        Label = (UILabel)gameObject.GetComponentInChildren(System.Type.GetType("UILabel"));
        Label.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ButtonClick()
    {
        IC.TeamEditor.SPButtonClick(this);
        
    }

    public void Imprint(Cub.Model.Character_Save who)
    {
        Who = who;
        if (who == null)
        {
            Label.text = "";
            return;
        }
        Label.text = "x";
        //Label.text = Who.Info.Class.ToString()[0].ToString();
        IC.TeamEditor.SPButtonColors(who);
    }
}
