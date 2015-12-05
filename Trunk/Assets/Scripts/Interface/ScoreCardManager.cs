using UnityEngine;
using System.Collections;

public class ScoreCardManager : MonoBehaviour
{

    public MasterGameController GM;
    //ScoreNameController TeamOneNamer;
    //ScoreNameController TeamTwoNamer;
    //ScoreScrController TeamOneScores;
    //ScoreScrController TeamTwoScores;
    public UILabel WinnerName;

    // Use this for initialization
    //void Awake()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        switch (child.gameObject.name)
    //        {
    //            case "Team One Name":
    //                TeamOneNamer = (ScoreNameController)child.gameObject.GetComponent("ScoreNameController");
    //                break;
    //            case "Team Two Name":
    //                TeamTwoNamer = (ScoreNameController)child.gameObject.GetComponent("ScoreNameController");
    //                break;
    //            case "Team One Score":
    //                TeamOneScores = (ScoreScrController)child.gameObject.GetComponent("ScoreScrController");
    //                break;
    //            case "Team Two Score":
    //                TeamTwoScores = (ScoreScrController)child.gameObject.GetComponent("ScoreScrController");
    //                break;
    //            case "Winner Name":
    //                WinnerName = (UILabel)child.gameObject.GetComponent("UILabel");
    //                break;
    //        }
    //    }
    //}

    void Update()
    {
        if (Input.GetAxis("Escape P1") > 0.5f || Input.GetAxis("Escape P2") > 0.5f ||
            Input.GetAxis("Click P1") > 0.5f || Input.GetAxis("Click P2") > 0.5f ||
            Input.GetAxis("Ready P1") > 0.5f || Input.GetAxis("Ready P2") > 0.5f)
        {
            GM.ClearAll();
            //GM.GSC.ClearMap();
            //GM.GSC.gameObject.SetActive(false);
            //foreach (GameObject tile in GM.Blocks)
            //    tile.transform.position += new Vector3(0, 20, 0);
            GM.MainMenu.gameObject.SetActive(true);
            //GM.ResetCamera();
            //GM.GSC.Reset();
            //gameObject.SetActive(false);
        }
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

    public void Imprint(Cub.Model.Team t1, Cub.Model.Team t2)
    {
        //TeamOneNamer.Imprint(t1, "FF0000");
        //TeamOneScores.Imprint(t1);
        //TeamTwoNamer.Imprint(t2, "00FF00");
        //TeamTwoScores.Imprint(t2);
        int S1 = t1.Score;
        int S2 = t2.Score;
        string winner = "Tie!";
        Color32 color = Color.white;

        if (S1 > S2){
            winner = t1.Name + " Wins!";
            color = t1.Colour_Primary;
        }
        else if (S1 < S2){
            winner = t2.Name + " Wins!";
            color = t2.Colour_Primary;
        }
        string c = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");

        WinnerName.text = "[" + c + "]" + winner + "[-]";
    }
}
