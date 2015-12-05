using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Cub.Interface
{

    public class OptionsListController : MonoBehaviour
    {
        public bool PlayerOne;
        public bool PlayerTwo;
        public bool CurrentlyActive = false;
        public bool Clicking = false;

        public GameObject SelectMarker;
        public MasterGameController GM;

        public List<GameObject> OptionsRaw;
        protected List<MenuChoiceController> Options = new List<MenuChoiceController>();

        protected MenuChoiceController Selected;

        protected float SelectTimer = 0.2f;
        protected float Timer;

        // Use this for initialization
        void Awake()
        {
            foreach (GameObject obj in OptionsRaw)
                Options.Add((MenuChoiceController)obj.GetComponent("MenuChoiceController"));
            GM = (MasterGameController)GameObject.Find("Game Master").GetComponent("MasterGameController");
            Selected = Options[0];
        }

        // Update is called once per frame
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
                }
            }
            else
                Clicking = false;
        }

        protected float GetInput(string axis)
        {
            float r = 0;
            if (PlayerOne)
                r += Input.GetAxis(axis + " P1");
            if (PlayerTwo)
                r += Input.GetAxis(axis + " P2");
            return r;
        }

        protected virtual void MoreUpdate()
        {

        }

        protected virtual void Click()
        {

        }

        //void AddButton(GameObject obj, MenuOptions mmo)
        //{
        //    Options.Add(mmo);
        //    OptionsDict.Add(mmo, obj);
        //}

        protected virtual void ChangeSelection(int n)
        {
            int current = Options.IndexOf(Selected);
            current += n;
            if (current >= Options.Count)
                current = 0;
            else if (current < 0)
                current = Options.Count - 1;
            //-15,-7
            if (SelectMarker != null)
            {
                Vector3 offset = Selected.gameObject.transform.position - SelectMarker.transform.position;
                Selected = Options[current];
                SelectMarker.transform.position = Selected.gameObject.transform.position - offset;
            }
            else
                Selected = Options[current];
            OnSelectChange();
        }

        protected virtual void OnSelectChange()
        {

        }
    }

    public enum MenuOptions
    {
        MMFight,
        MMCredits,
        MMQuit,
        PickerTeam,
        Head,
        Arms,
        Body,
        Legs
    }
}