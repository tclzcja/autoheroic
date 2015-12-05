using UnityEngine;
using System.Collections;

public class DescriptionManager : MonoBehaviour {

    UILabel Label;

	// Use this for initialization
	void Start () {
        Label = (UILabel)gameObject.GetComponent("UILabel");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayImage(string text, float time)
    {
        Label.text = text;
        Invoke("TurnOffImage", time);
    }

    void TurnOffImage()
    {
        gameObject.SetActive(false);
    }
}
