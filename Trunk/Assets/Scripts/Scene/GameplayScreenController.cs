using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayScreenController : MonoBehaviour
{

    //GameMode CurrentMode;
    //GameplayTeamPickerController Tpc;
    //public ScoreCardManager Scm;
    public MasterGameController GM;
    public DescriptionManager Desc;
    public Cub.Model.Team TeamOne = null;
    public Cub.Model.Team TeamTwo = null;
    public Cub.View.Runtime RT;
    bool RTStarted = false;
    //Cub.Position2 StageSize;
    public bool CurrentlyActive = false;

    public UITexture LeftScore;
    public UILabel LeftScoreNum;
    int T1Score;
    public UITexture RightScore;
    public UILabel RightScoreNum;
    int T2Score;
    public GameObject PHCType;
    public UIGrid T1Grid;
    public Dictionary<System.Guid, PlayerHealthController> PHCs;
    PlayerHealthController CurrentCharacter = null;
    public UIGrid T2Grid;
    //public GameObject CurrentPlayerMarker;

    // Use this for initialization
    void Start()
    {
        //StageSize = new Cub.Position2(10, 10);
        RT.GSC = this;
        Cub.View.Runtime.AddGSC(this);
        //Tpc = (GameplayTeamPickerController)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("GameplayTeamPickerController"));
        //Scm = (ScoreCardManager)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("ScoreCardManager"));
        //Desc = (DescriptionManager)GameObject.Find("UI Root").GetComponentInChildren(System.Type.GetType("DescriptionManager"));
        Cub.View.NarratorController.Initialize(Desc);
        //SwitchModes(GameMode.TeamPick);
        //Cub.View.Library.Initialization();
        //Cub.Model.Library.Initialization();
        //BuildMap();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    Application.LoadLevel("Main Menu");
        //switch (CurrentMode)
        //{
        //    case GameMode.TeamPick:
        //        TeamPickUpdate();
        //        break;
        //    case GameMode.Gameplay:
        //        GameplayUpdate();
        //        break;
        //    case GameMode.Postgame:
        //        PostGameUpdate();
        //        break;
        //}
        if (CurrentlyActive)
            GameplayUpdate();
    }

    //void TeamPickUpdate()
    //{

    //}

    void GameplayUpdate()
    {
        if (Cub.Model.Main.GameStillRunning())
            MatchBuildUpdate();
    }

    void MatchBuildUpdate()
    {
        List<Cub.View.Eventon> events = Cub.Model.Main.Go();
        foreach (Cub.View.Eventon e in events)
        {
            Cub.View.Runtime.Add_Eventon(e);
        }
        if (!RTStarted && events.Count > 0)
        {
            RT.Run_Eventon();
            RTStarted = true;
        }
    }

    //void PostGameUpdate()
    //{
    //    if (Input.GetKey(KeyCode.Return))
    //        Application.LoadLevel("Main Menu");
    //}

    //public void SwitchModes(GameMode mode)
    //{
    //    CurrentMode = mode;
    //    switch (mode)
    //    {
    //        case GameMode.TeamPick:
    //            Scm.gameObject.SetActive(false);
    //            Desc.gameObject.SetActive(false);
    //            break;
    //        case GameMode.Gameplay:
    //            Scm.gameObject.SetActive(false);
    //            Tpc.gameObject.SetActive(false);
    //            Desc.gameObject.SetActive(false);
    //            break;
    //        case GameMode.Postgame:
    //            Scm.gameObject.SetActive(true);
    //            Tpc.gameObject.SetActive(false);
    //            Desc.gameObject.SetActive(false);
    //            break;
    //    }
    //}

    public void StartGame(Cub.Model.Team T1, Cub.Model.Team T2)
    {
        RTStarted = false;
        TeamOne = T1;
        TeamTwo = T2;
        T1Score = 0;
        T2Score = 0;
        if (TeamOne.Colour_Primary.r == TeamTwo.Colour_Primary.r &&
            TeamOne.Colour_Primary.g == TeamTwo.Colour_Primary.g && TeamOne.Colour_Primary.b == TeamTwo.Colour_Primary.b)
        {
            Color32 p = TeamTwo.Colour_Primary;
            TeamTwo.Colour_Primary = TeamTwo.Colour_Secondary;
            TeamTwo.Colour_Secondary = p;
        }

        TeamOne.Colour_Primary = Color.red;
        TeamOne.Colour_Secondary = Color.yellow;
        TeamTwo.Colour_Primary = Color.blue;
        TeamTwo.Colour_Secondary = Color.green;
        //TeamOne.MakeUnique();
        //TeamTwo.MakeUnique();
        foreach (Transform t in T1Grid.transform)
            Destroy(t.gameObject);
        foreach (Transform t in T2Grid.transform)
            Destroy(t.gameObject);

        PHCs = new Dictionary<System.Guid, PlayerHealthController>();
        foreach (Cub.Model.Character c in TeamOne.List_Character)
        {
            c.Stat.Position = TranslateStartPosition(c.Stat.Position, true);
            Cub.View.Character ch = Cub.View.Runtime.Add_Character(c);
            PlayerHealthController phc = (PlayerHealthController)((GameObject)Instantiate(
                PHCType, Vector3.zero, Quaternion.identity)).GetComponent("PlayerHealthController");
            phc.Setup(ch, TeamOne, true, this);
            PHCs.Add(ch.Stat.ID, phc);
            phc.gameObject.transform.parent = T1Grid.gameObject.transform;
            phc.transform.localScale = new Vector3(1, 1, 1);
        }
        T1Grid.repositionNow = true;
        foreach (Cub.Model.Character c in TeamTwo.List_Character)
        {
            c.Stat.Position = TranslateStartPosition(c.Stat.Position, false);
            Cub.View.Character ch = Cub.View.Runtime.Add_Character(c);

            Cub.View.Character view = Cub.View.Runtime.Get_Character(c.ID);

            Quaternion rot = view.gameObject.transform.rotation;
            rot.y = 180;
            view.gameObject.transform.rotation = rot;

            PlayerHealthController phc = (PlayerHealthController)((GameObject)Instantiate(
                PHCType, Vector3.zero, Quaternion.identity)).GetComponent("PlayerHealthController");
            phc.Setup(ch, TeamTwo,false,this);
            PHCs.Add(ch.Stat.ID, phc);
            phc.gameObject.transform.parent = T2Grid.gameObject.transform;
            phc.transform.localScale = new Vector3(1, 1, 1);
        }
        T2Grid.repositionNow = true;
        Cub.Model.Main.Initialization(TeamOne, TeamTwo);
        //SwitchModes(GameMode.Gameplay);
        CurrentlyActive = true;
        Desc.gameObject.SetActive(false);
    }

    public void SetHealth(System.Guid id, int health, int maxHealth)
    {
        PlayerHealthController p = PHCs[id];
        p.SetHealth(health, maxHealth);
    }

    public void SetScore(bool teamOne, int bonus)
    {
        UITexture text;
        UILabel lab;
        int score;
        if (teamOne)
        {
            text = LeftScore;
            lab = LeftScoreNum;
            T1Score += bonus;
            score = T1Score;
        }
        else
        {
            text = RightScore;
            lab = RightScoreNum;
            T2Score += bonus;
            score = T2Score;
        }
        if (!text.gameObject.activeSelf)
            text.gameObject.SetActive(true);
        int ht = 150 * score / 400;
        text.SetDimensions(5, ht);
        lab.text = score.ToString();
    }

    public void SetCurrentCharacter(Cub.View.Eventon e)
    {
        //CurrentPlayerMarker.transform.parent = gameObject.transform;
        //CurrentPlayerMarker.transform.localPosition = new Vector3(-500, -500, 0);
        if (!e.MainAction || !(e.Data[0] is System.Guid))
            return;
        System.Guid who = (System.Guid)e.Data[0];
        //Debug.Log(who + " / " + PHCs[who]);
        if (PHCs.ContainsKey(who) && PHCs[who] != null)
        {
            if (CurrentCharacter != null)
            {
                Vector3 oldWhere = CurrentCharacter.transform.localPosition;
                oldWhere.x = 0;
                CurrentCharacter.transform.localPosition = oldWhere;
            }
            CurrentCharacter = PHCs[who];
            //CurrentPlayerMarker.transform.parent = PHCs[who].transform;
            //Vector3 where = Vector3.zero;
            float offset = 10;
            //if (PHCs[who].TeamOne)
            //    where.x += 35;
            //else
            //    where.x -= 35;
            if (!CurrentCharacter.TeamOne)
                offset *= -1;
            //CurrentPlayerMarker.transform.localPosition = where;
            Vector3 where = CurrentCharacter.transform.localPosition;
            where.x = offset;
            CurrentCharacter.transform.localPosition = where;
        }
    }

    public void EndGame()
    {
        //SwitchModes(GameMode.Postgame);
        Vector3 where = CurrentCharacter.transform.localPosition;
        where.x = 0;
        CurrentCharacter.transform.localPosition = where;
        CurrentCharacter = null;
        //CurrentPlayerMarker.transform.parent = gameObject.transform;
        //CurrentPlayerMarker.transform.position = new Vector3(-500, -500, 0);
        GM.TurnOnScoreCard(TeamOne, TeamTwo);
        Cub.Tool.Xml.Serialize(GM.Teams, "Data/Team_Saves.xml");
    }

    public void Clear()
    {
        ClearMap();
        //CurrentPlayerMarker.transform.parent = gameObject.transform;
        //CurrentPlayerMarker.transform.position = new Vector3(-500, -500, 0);
        foreach (System.Guid g in PHCs.Keys)
        {
            Destroy(PHCs[g].gameObject);
        }
        PHCs.Clear();
        LeftScore.gameObject.SetActive(false);
        RightScore.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ClearMap()
    {
        foreach (Cub.Model.Character c in TeamOne.List_Character)
        {
            Destroy(Cub.View.Runtime.Remove_Character(c.ID).gameObject);
        }
        foreach (Cub.Model.Character c in TeamTwo.List_Character)
        {
            Destroy(Cub.View.Runtime.Remove_Character(c.ID).gameObject);
        }
    }

    //void BuildMap()
    //{
    //    Cub.Terrain[][] map = Cub.Model.Library.Stage_Terrain;
    //    for (int y = 0; y < map.Length; y++)
    //    {
    //        for (int x = 0; x < map[0].Length; x++)
    //        {

    //            GameObject t = Cub.View.Library.Get_Terrain(map[y][x]);
    //            if (t != null)
    //                Instantiate(t, new Vector3(x, -0.5f, y), Quaternion.identity);
    //        }
    //    }
    //}

    public Cub.Position2 TranslateStartPosition(Cub.Position2 pos, bool teamOne)
    {
        int x = pos.X;
        int y = pos.Y;
        Cub.Position2 r = new Cub.Position2(x, y);
        if (!teamOne)
            r = new Cub.Position2(GM.xMapSize - x, GM.yMapSize - y);
        if (r.X > GM.xMapSize || r.X < 0 || r.Y > GM.yMapSize || r.Y < 0)
            Debug.Log("TRANSLATE ERROR");
        return r;
    }

    public void Reset()
    {
        RTStarted = false;
        TeamOne = null;
        TeamTwo = null;
    }
}

//public enum GameMode
//{
//    TeamPick,
//    Gameplay,
//    Postgame
//}
