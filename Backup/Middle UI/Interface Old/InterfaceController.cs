using UnityEngine;
using System.Collections;
using Cub.View;

public class InterfaceController : MonoBehaviour {

    public TeamPickerController TeamPicker;
    public TeamEditorController TeamEditor;
    public GameObject CharacterPicker;

	// Use this for initialization
	void Awake () {
        //Cub.Tool.Main.Initialization(null,null);
        Cub.Model.Library.Initialization();
        TeamPicker = (TeamPickerController)GameObject.Find("Team Picker Menu").GetComponent("TeamPickerController");
        TeamEditor = (TeamEditorController)GameObject.Find("Team Editor Menu").GetComponent("TeamEditorController");
        TeamEditor.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }

}
