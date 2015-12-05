using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cub.View;

namespace Cub.View.Event
{

    public class EventParent : MonoBehaviour
    {

        protected EventController Manager;
        public float TimerMax;
        protected float Timer;
        protected ClassController ActiveChar = null;
        public string Desc = "";

        // Use this for initialization
        void Start()
        {

        }

        protected virtual void Initialize()
        {
            Manager = (EventController)GetComponent("EventController");
        }

        // Update is called once per frame
        void Update()
        {

        }

        virtual public void Begin(string desc, List<object> data)
        {
            Timer = TimerMax;
            Desc = desc;
            Manager.NameTextOn(Desc);
            if (data.Count == 0) Debug.Log("0 Length Data");
			else if (data[0] is System.Guid && Manager.CharacterReference.ContainsKey((System.Guid)data[0]))
				ActiveChar = Manager.CharacterReference[(System.Guid)data[0]];
        }

        virtual public void Continue()
        {
            Timer -= Time.deltaTime;
        }

        virtual public void End()
        {
            if (ActiveChar != null)
                Manager.NameTextOff();
        }

        virtual public bool StillRunning()
        {
            if (Timer > 0) return true;
            return false;
        }

        //Here's a list of types of Events and the data they want:
        //Attack: {string Character.UniqueName}
        //Walk: {string Character.UniqueName, float Destination X Coord, float Destination Y Coord}
    }
}