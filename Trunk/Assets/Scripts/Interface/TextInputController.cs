using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextInputController : MonoBehaviour
{

    public List<UILabel> Letters;
    public UILabel Desc;
    //public string Mode;
    TeamEditorManager TEM = null;
    public bool PlayerOne;
    public bool PlayerTwo;
    CharacterEditorManager CEM = null;

    public bool CurrentlyActive = false;
    public bool Clicking = false;

    public GameObject SelectMarker;
    public MasterGameController GM;

    protected UILabel Selected;

    protected float SelectTimer = 0.2f;
    protected float Timer;


    // Use this for initialization
    void Start()
    {
        GM = (MasterGameController)GameObject.Find("Game Master").GetComponent("MasterGameController");
        Selected = Letters[0];
    }

    void Update()
    {
        if (!CurrentlyActive)
            return;
        if (Timer > 0)
            Timer -= Time.deltaTime;

        if (GetInput("Horizontal") > 0.1f && Timer <= 0)
        {
            ChangeSelection(1);
            Timer = SelectTimer;
        }
        else if (GetInput("Horizontal") < -0.1f && Timer <= 0)
        {
            ChangeSelection(-1);
            Timer = SelectTimer;
        }
        else if (GetInput("Vertical") > 0.1f && Timer <= 0)
        {
            ChangeText(-1);
            Timer = SelectTimer;
        }
        else if (GetInput("Vertical") < -0.1f && Timer <= 0)
        {
            ChangeText(1);
            Timer = SelectTimer;
        }
        else if (Timer > 0 && Mathf.Abs(GetInput("Vertical")) < 0.1f && Mathf.Abs(GetInput("Horizontal")) < 0.1f)
            Timer = 0;

        if (GetInput("Click") > 0.5f)
        {
            if (!Clicking)
            {
                Clicking = true;
                Debug.Log("A");
                if (TEM != null)
                {
                    Debug.Log("B");
                    if (Desc.text == "Select Team Name")
                    {
                        Debug.Log("C");
                        TEM.TeamNameUpdate(ReadName());
                        TEM.gameObject.SetActive(true);
                        TEM = null;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        TEM.OwnerNameUpdate(ReadName());
                        TEM.gameObject.SetActive(true);
                        TEM = null;
                        gameObject.SetActive(false);
                    }
                }
                else if (CEM != null)
                {
                    CEM.NameUpdate(ReadName());
                    CEM.gameObject.SetActive(true);
                    CEM = null;
                    gameObject.SetActive(false);
                }
            }
        }
        else
            Clicking = false;
    }

    float GetInput(string axis)
    {
        float r = 0;
        if (PlayerOne)
            r += Input.GetAxis(axis + " P1");
        if (PlayerTwo)
            r += Input.GetAxis(axis + " P2");
        return r;
    }

    public void SetupTeam(TeamEditorManager tem, Cub.Model.TeamSave team)
    {
        TEM = tem;
        Desc.text = "Select Team Name";
        WriteName(team.Name);
        CurrentlyActive = true;
    }

    public void SetupOwner(TeamEditorManager tem, Cub.Model.TeamSave team)
    {
        TEM = tem;
        Desc.text = "Select Owner Name";
        WriteName(team.Owner_Name);
        CurrentlyActive = true;
    }

    public void SetupCharacter(CharacterEditorManager cem, Cub.Model.Character_Save who)
    {
        CEM = cem;
        Desc.text = "Select Character Name";
        WriteName(who.Name);
        CurrentlyActive = true;
    }

    public void WriteName(string name)
    {
        for (int n = 0; n < Letters.Count; n++)
        {
            string c = "";
            if (n < name.Length)
                c = name[n].ToString();
            Letters[n].text = c;
        }
    }

    public string ReadName()
    {
        string r = "";
        for (int n = 0; n < Letters.Count; n++)
        {
            string l = Letters[n].text;
            if (l == "_")
                l = " ";
            r += l;
        }
        return r;
    }

    protected virtual void ChangeSelection(int n)
    {
        int current = Letters.IndexOf(Selected);
        current += n;
        if (current >= Letters.Count)
            current = 0;
        else if (current < 0)
            current = Letters.Count - 1;
        //-15,-7
        if (SelectMarker != null)
        {
            Vector3 offset = Selected.gameObject.transform.position - SelectMarker.transform.position;
            Selected = Letters[current];
            SelectMarker.transform.position = Selected.gameObject.transform.position - offset;
        }
        else
            Selected = Letters[current];
    }

    protected virtual void ChangeText(int n)
    {
        string l = Selected.text;
        int index = Mathf.Max(0, GM.Letters.IndexOf(l));
        index += n;
        if (index < 0)
            index = GM.Letters.Count - 1;
        else if (index >= GM.Letters.Count)
        {
            index = 0;
        }
        Selected.text = GM.Letters[index];
    }
}
