using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cub.Interface
{
    public class TeamPickerManager : OptionsListController
    {
        bool AlreadySetup = false;
        public bool Chosen = false;
        public Cub.Model.TeamSave SelectedTeam { get { return ((TeamButtonController)Selected).Team; } }
        int TeamsOffset = 0;
        public GameObject UpMarker;
        public GameObject DownMarker;
        public GameObject MyInstructions;

        //CharacterEditorManager MyCEditor;
        //TeamEditorManager MyTEditor;

        public void Setup()
        {
            if (AlreadySetup)
                return;
            //if (PlayerOne)
            //{
            //    MyCEditor = GM.LeftCEditor;
            //    MyTEditor = GM.LeftEditor;
            //}
            //else
            //{
            //    MyCEditor = GM.RightCEditor;
            //    MyTEditor = GM.RightEditor;
            //}
            AlreadySetup = true;
            MarkTeamButtons();
        }

        void Update()
        {
            if (!CurrentlyActive)
                return;
            if (Timer > 0)
                Timer -= Time.deltaTime;

            if (GetInput("Vertical") > 0.1f && Timer <= 0)
            {
                ChangeSelection(-1);
                Timer = SelectTimer;
            }
            else if (GetInput("Vertical") < -0.1f && Timer <= 0)
            {
                ChangeSelection(1);
                Timer = SelectTimer;
            }
            else if (Timer > 0 && Mathf.Abs(GetInput("Vertical")) < 0.1f)
                Timer = 0;
            MoreUpdate();
            if (GetInput("Click") > 0.5f)
            {
                if (!Clicking)
                {
                    Clicking = true;
                    Click();
                    //GM.PlaySound(MenuSound.Click);
                }
            }
            else if (GetInput("Escape") > 0.8f)
            {
                if (!Clicking)
                {
                    Clicking = true;
                    GM.MainMenu.gameObject.SetActive(true);
                    Cub.View.Alphabet.Box.SetActive(true);
                    GM.ClearAll();
                }
            }
            else if (GetInput("Move") > 0.8f)
            {
                if (!Clicking)
                {
                    Clicking = true;
                    AddTeam();
                }
            }
            else if (GetInput("Delete") > 0.8f)
            {
                if (!Clicking)
                {
                    Clicking = true;
                    DeleteTeam();
                }
            }
            else
                Clicking = false;
        }

        public void Clear()
        {
            GM.LeftPicker.transform.localPosition =
                        new Vector3(-300, transform.localPosition.y, transform.localPosition.z);
            GM.RightPicker.transform.localPosition =
                new Vector3(300, transform.localPosition.y, transform.localPosition.z);
            GM.LeftPicker.CurrentlyActive = false;
            GM.RightPicker.CurrentlyActive = false;
            MyInstructions.SetActive(false);
        }

        public void MarkTeamButtons()
        {
            int n = 0 + TeamsOffset;
            foreach (MenuChoiceController choice in Options)
            {
                TeamButtonController tbc = (TeamButtonController)choice;
                if (n == -1)
                    tbc.SetupAdder();
                else if (n >= GM.Teams.Count || n < -1)
                    tbc.Setup(null);
                else
                    tbc.Setup(GM.Teams[n]);
                n++;
            }
            if (TeamsOffset > 0)
                UpMarker.SetActive(true);
            else
                UpMarker.SetActive(false);
            if (TeamsOffset + Options.Count - 1 < Mathf.Max(3, GM.Teams.Count - 1))
                DownMarker.SetActive(true);
            else
                DownMarker.SetActive(false);
        }

        protected override void ChangeSelection(int n)
        {
            int current = Options.IndexOf(Selected);
            current += n;
            if (TeamsOffset == 0 && current < Options.Count && current > GM.Teams.Count)
            {
                current = GM.Teams.Count;
            }
            else if (current >= Options.Count)
            {
                TeamsOffset++;
                if (TeamsOffset + Options.Count - 2 >= Mathf.Max(3, GM.Teams.Count))
                {
                    TeamsOffset--;
                }
                GM.LeftPicker.MarkTeamButtons();
                GM.RightPicker.MarkTeamButtons();
                current = Options.Count - 1;
            }
            else if (current < 0)
            {
                TeamsOffset--;
                if (TeamsOffset < 0)
                    TeamsOffset = 0;
                GM.LeftPicker.MarkTeamButtons();
                GM.RightPicker.MarkTeamButtons();
                current = 0;
            }
            //-15,-7
            Vector3 offset = Selected.gameObject.transform.position - SelectMarker.transform.position;
            Selected = Options[current];
            SelectMarker.transform.position = Selected.gameObject.transform.position - offset;
            OnSelectChange();
        }

        protected override void Click()
        {
            if (((TeamButtonController)Selected).Team != null)
            {
                //((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.05f, 0.9f, 0.05f, 0.8f);
                //Chosen = true;
                GM.EditTeam(SelectedTeam, this);
                MyInstructions.SetActive(false);
            }
            else
            {
                AddTeam();
            }
        }

        public void AddTeam()
        {
            Model.TeamSave t = new Model.TeamSave(Cub.Model.Library.TeamName(), "");
            GM.Teams.Add(t);
            GM.LeftPicker.MarkTeamButtons();
            GM.RightPicker.MarkTeamButtons();
            int slide = GM.Teams.IndexOf(t) - GM.Teams.IndexOf(SelectedTeam);
            for (int n = slide; n > 0; n--)
                ChangeSelection(1);
            GM.EditTeam(t, this);
            MyInstructions.SetActive(false);
        }

        public void DeleteTeam()
        {
            GM.Teams.Remove(SelectedTeam);
            
            if (TeamsOffset > 0)
                TeamsOffset--;
            else
                ChangeSelection(-1);
            GM.LeftPicker.MarkTeamButtons();
            GM.RightPicker.MarkTeamButtons();
        }

        protected override void OnSelectChange()
        {
            ((UISprite)SelectMarker.GetComponent("UISprite")).color = new Color(0.9f, 0.05f, 0.05f, 0.8f);
            Chosen = false;
        }
    }
}