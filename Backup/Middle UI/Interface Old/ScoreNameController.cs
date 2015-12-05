using UnityEngine;
using System.Collections;

public class ScoreNameController : MonoBehaviour {

    UILabel Name;
    UILabel OwnerName;

	// Use this for initialization
	void Awake () {
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Team Name":
                    Name = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Owner Name":
                    OwnerName = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}

    public void Imprint(Cub.Model.Team t)
    {
        Name.text = t.Name;
        OwnerName.text = t.Owner_Name;
    }
}
