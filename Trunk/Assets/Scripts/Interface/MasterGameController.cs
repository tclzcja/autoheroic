using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameController : MonoBehaviour
{
    public Cub.Interface.TitleScreenController MainMenu;
    public GameObject TeamPickers;
    public GameplayScreenController GSC;
    //public GameObject MainMenu;
    //public GameObject TeamPickers;
    public Camera MainCamera;
    //public GameObject PersonalCamera;

    public Cub.View.Runtime Runtime;

    public List<Cub.Model.TeamSave> Teams;

    MasterStage Stage = MasterStage.Waiting;

    Vector3 CameraStart;
    Vector3 CameraDestination;
    float CDTimerMax;
    float CDTimer;

    float TimerMax;
    float Timer;

    //bool BlockStuff;
    Dictionary<GameObject, float> BlockTimersMax;
    Dictionary<GameObject, float> BlockTimers;
    public List<GameObject> Blocks = new List<GameObject>();

    public Cub.Interface.TeamPickerManager LeftPicker;
    public Cub.Interface.TeamPickerManager RightPicker;
    public TeamEditorManager LeftEditor;
    public TeamEditorManager RightEditor;
    public CharacterEditorManager LeftCEditor;
    public CharacterEditorManager RightCEditor;
    public TextInputController LeftNameEditor;
    public TextInputController RightNameEditor;
    public ConfirmerController LeftConfirm;
    public ConfirmerController RightConfirm;

    Vector3 CameraWhere;
    Quaternion CameraRot;

    public ScoreCardManager SCM;

    public int xMapSize { get; private set; }
    public int yMapSize { get; private set; }
    public int PlacementSize;

    public List<Color32> Colors;
    public List<string> Letters;

    public GameObject PickerInstructionsLeft;
    public GameObject PickerInstructionsRight;

    public AudioClip ClickSound;
    public AudioClip ErrorSound;

    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;
        Cub.View.Library.Initialization();
        Cub.Model.Library.Initialization();
        BuildMap();
        string name = typeof(List<Cub.Model.TeamSave>).AssemblyQualifiedName;
        Teams = (List<Cub.Model.TeamSave>)Cub.Tool.Xml.Deserialize(System.Type.GetType(name), "Data/Team_Saves.xml");
        GSC.gameObject.SetActive(false);
        LeftCEditor.PersonalCamera.SetActive(false);
        RightCEditor.PersonalCamera.SetActive(false);
        LeftEditor.gameObject.SetActive(false);
        RightEditor.gameObject.SetActive(false);
        LeftCEditor.gameObject.SetActive(false);
        RightCEditor.gameObject.SetActive(false);
        LeftNameEditor.gameObject.SetActive(false);
        RightNameEditor.gameObject.SetActive(false);
        PickerInstructionsLeft.SetActive(false);
        PickerInstructionsRight.SetActive(false);
        LeftConfirm.gameObject.SetActive(false);
        RightConfirm.gameObject.SetActive(false);
        SCM.gameObject.SetActive(false);
        Application.targetFrameRate = 60;
        CameraWhere = MainCamera.transform.position;
        CameraRot = MainCamera.transform.rotation;
        Letters = new List<string> {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","_",""};
    }


    // Update is called once per frame
    void Update()
    {
        if (CDTimer > 0)
        {
            CDTimer = Mathf.Max(0, CDTimer - Time.deltaTime);
            float x = Mathf.Lerp(CameraStart.x, CameraDestination.x, (CDTimerMax - CDTimer) / CDTimerMax);
            float y = Mathf.Lerp(CameraStart.y, CameraDestination.y, (CDTimerMax - CDTimer) / CDTimerMax);
            float z = Mathf.Lerp(CameraStart.z, CameraDestination.z, (CDTimerMax - CDTimer) / CDTimerMax);
            MainCamera.transform.position = new Vector3(x, y, z);
        }
        if (Stage != MasterStage.Waiting)
        {
            HandleStage(Stage);
        }
        else if (TeamPickers.activeSelf)
        {
            if (LeftPicker.Chosen && RightPicker.Chosen && LeftPicker.SelectedTeam != null && RightPicker.SelectedTeam != null)
                StartGameplay();
        }
    }

    void HandleStage(MasterStage stage)
    {
        switch (stage)
        {
            case MasterStage.CameraMoving:
                if (CDTimer <= 0)
                    SetStage(MasterStage.Waiting);
                break;
            case MasterStage.BlockBuilding:
                bool keepGoing = false;
                foreach (GameObject block in Blocks)
                {
                    BlockTimers[block] = Mathf.Max(0, BlockTimers[block] - Time.deltaTime);
                    Vector3 where = block.transform.position;
                    where.y = Mathf.Lerp(20.5f, -0.5f, (BlockTimersMax[block] - BlockTimers[block]) / BlockTimersMax[block]);
                    block.transform.position = where;
                    if (BlockTimers[block] > 0)
                        keepGoing = true;
                }
                if (!keepGoing)
                {
                    Cub.View.Kamera.Unlock();
                    Timer = TimerMax = 1;
                    LeftPicker.gameObject.SetActive(true);
                    RightPicker.gameObject.SetActive(true);
                    SetStage(MasterStage.TeamPickersSlideIn);
                }
                break;
            case MasterStage.TeamPickersSlideIn:
                Timer = Mathf.Max(0, Timer - Time.deltaTime);
                float x = Mathf.Lerp(300f, 202f, (TimerMax - Timer) / TimerMax);
                LeftPicker.transform.localPosition =
                    new Vector3(-x, LeftPicker.transform.localPosition.y, LeftPicker.transform.localPosition.z);
                RightPicker.transform.localPosition =
                    new Vector3(x, LeftPicker.transform.localPosition.y, LeftPicker.transform.localPosition.z);
                if (Timer <= 0)
                {
                    PickerInstructionsLeft.SetActive(true);
                    PickerInstructionsRight.SetActive(true);
                    SetStage(MasterStage.Waiting);
                    LeftPicker.CurrentlyActive = true;
                    RightPicker.CurrentlyActive = true;
                }
                break;
        }
    }


    public void GotoFightScreen()
    {
        MainMenu.gameObject.SetActive(false);
        Cub.View.Alphabet.Box.SetActive(false);
        LeftPicker.Setup();
        RightPicker.Setup();
        //CameraToPoint(new Vector3(-2, 3, -2),1);
        BlockTimers = new Dictionary<GameObject, float>();
        BlockTimersMax = new Dictionary<GameObject, float>();
        foreach (GameObject block in Blocks)
        {
            float n = Random.Range(0.8f, 1.2f);
            BlockTimers.Add(block, n);
            BlockTimersMax.Add(block, n);
        }
        SetStage(MasterStage.BlockBuilding);
    }

    public void GotoCreditsScreen()
    {
        Application.LoadLevel(1);
    }

    void CameraToPoint(Vector3 where, float time)
    {
        CameraStart = MainCamera.transform.position;
        CameraDestination = where;
        CDTimer = CDTimerMax = time;
    }

    void BuildMap()
    {
        yMapSize = Cub.Model.Library.Stage_Terrain.Length - 1;
        xMapSize = Cub.Model.Library.Stage_Terrain[0].Length - 1;
        Cub.Terrain[][] map = Cub.Model.Library.Stage_Terrain;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {

                GameObject t = Cub.View.Library.Get_Terrain(map[y][x]);
                if (t != null)
                    Blocks.Add((GameObject)Instantiate(t, new Vector3(x, 20.5f, y), Quaternion.identity));
            }
        }
    }

    void SetStage(MasterStage stage)
    {
        Stage = stage;
    }

    void StartGameplay()
    {
        LeftEditor.ClearMarkers();
        RightEditor.ClearMarkers();
        Cub.Model.TeamSave TS1 = LeftPicker.SelectedTeam;
        Cub.Model.TeamSave TS2 = RightPicker.SelectedTeam;
        Cub.Model.Team TeamOne = TS1.Extract_Team();
        Cub.Model.Team TeamTwo = TS2.Extract_Team();
        LeftEditor.gameObject.SetActive(false);
        RightEditor.gameObject.SetActive(false);
        GSC.gameObject.SetActive(true);
        GSC.StartGame(TeamOne, TeamTwo);
    }

    public Cub.Position2 TranslateStartPosition(Cub.Position2 pos, bool teamOne)
    {
        int x = pos.X;
        int y = pos.Y;
        Cub.Position2 r = new Cub.Position2(x, y);
        if (!teamOne)
            r = new Cub.Position2(xMapSize - x, yMapSize - y);
        if (r.X > xMapSize || r.X < 0 || r.Y > yMapSize || r.Y < 0)
            Debug.Log("TRANSLATE ERROR");
        return r;
    }

    public void EditTeam(Cub.Model.TeamSave team, Cub.Interface.TeamPickerManager picker)
    {
        picker.gameObject.SetActive(false);
        if (picker.PlayerOne)
        {
            LeftEditor.Setup(team);
            LeftEditor.gameObject.SetActive(true);
            LeftEditor.CurrentlyActive = true;
            LeftPicker.MyInstructions.SetActive(false);
        }
        else
        {
            RightEditor.Setup(team);
            RightEditor.gameObject.SetActive(true);
            RightEditor.CurrentlyActive = true;
            RightPicker.MyInstructions.SetActive(false);
        }


    }

    public void EditCharacter(TeamEditorManager tem, Cub.View.Character VChar, Cub.Model.Character_Save SChar)
    {
        CharacterEditorManager cem;
        if (tem.PlayerOne)
            cem = LeftCEditor;
        else
            cem = RightCEditor;
        tem.TurnOff();
        cem.gameObject.SetActive(true);
        cem.Setup(VChar, SChar, tem.Team);
        cem.Clicking = true;
    }

    public void CheckMatchReady()
    {
        bool ready = true;
        if (!LeftEditor.Ready)
            ready = false;
        if (!RightEditor.Ready)
            ready = false;
        if (ready)
        {
            LeftEditor.Ready = false;
            LeftEditor.ReadyButton.color = Color.white;
            RightEditor.Ready = false;
            RightEditor.ReadyButton.color = Color.white;
            Cub.Tool.Xml.Serialize(Teams, "Data/Team_Saves.xml");
            StartGameplay();
        }
    }

    public void TurnOnScoreCard(Cub.Model.Team teamOne, Cub.Model.Team teamTwo)
    {
        SCM.gameObject.SetActive(true);
        SCM.Imprint(teamOne, teamTwo);
    }

    public void ResetCamera()
    {
        MainCamera.transform.position = CameraWhere;
        MainCamera.transform.rotation = CameraRot;
    }

    public void ClearAll()
    {
        if (LeftPicker.gameObject.activeSelf)
            LeftPicker.Clear();
        if (RightPicker.gameObject.activeSelf)
            RightPicker.Clear();
        if (LeftEditor.gameObject.activeSelf)
            LeftEditor.Clear();
        if (RightEditor.gameObject.activeSelf)
            RightEditor.Clear();
        if (LeftCEditor.gameObject.activeSelf)
            LeftCEditor.Clear();
        if (RightCEditor.gameObject.activeSelf)
            RightCEditor.Clear();
        if (GSC.gameObject.activeSelf)
            GSC.Clear();
        if (SCM.gameObject.activeSelf)
            SCM.Clear();
        foreach (GameObject tile in Blocks)
            tile.transform.position += new Vector3(0, 20, 0);
        ResetCamera();
        Cub.Tool.Xml.Serialize(Teams, "Data/Team_Saves.xml");
    }

    public void PlaySound(MenuSound ms)
    {
        AudioClip s = null;
        switch (ms)
        {
            case MenuSound.Click:
                s = ClickSound;
                break;

            case MenuSound.Error:
                s = ErrorSound;
                break;
        }
        if (s != null)
            audio.PlayOneShot(s);
    }
}

public enum MasterStage
{
    Waiting,
    CameraMoving,
    BlockBuilding,
    TeamPickersSlideIn
}

public enum MenuSound
{
    Click,
    Error
}
