using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FightButton()
    {
        Application.LoadLevel("Gameplay Screen");
    }

    public void EditButton()
    {
        Application.LoadLevel("Team Editor");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
