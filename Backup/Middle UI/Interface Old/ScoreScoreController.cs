using UnityEngine;
using System.Collections;

public class ScoreScoreController : MonoBehaviour {

    UILabel Kills;
    UILabel Survival;
    UILabel Total;
    
    // Use this for initialization
	void Awake () {
        foreach (Transform child in transform)
        {
            switch (child.gameObject.name)
            {
                case "Kills":
                    Kills = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Survival":
                    Survival = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
                case "Total":
                    Total = (UILabel)child.gameObject.GetComponent("UILabel");
                    break;
            }
        }
	}

    public void Imprint(Cub.Model.Team t)
    {
        Kills.text = GetScore("Kills", t);
        Survival.text = GetScore("Survival", t);
        Total.text = t.Score.ToString() + "pts";
    }

    string GetScore(string name, Cub.Model.Team t)
    {
        int amt = 0;
        foreach (Cub.Model.Score s in t.ScoreThings)
            if (s.Name == name)
            {
                amt = s.Value;
                break;
            }
        return amt.ToString() + "pts";
    }
}
