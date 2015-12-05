using UnityEngine;
using System.Collections;

namespace Cub.View.Event
{

    public class WalkEvent : EventParent
    {

        Vector2 EndLocation;
        Vector2 StartLocation;

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        //This wants {string Character.UniqueName, float Destination X Coord, float Destination Y Coord}
        public override void Begin(string desc, System.Collections.Generic.List<object> data)
        {
            base.Begin(desc, data);
			//Debug.Log(data);
            Vector2 whereE = new Vector2((int)data[1], (int)data[2]);
            EndLocation = whereE;
            StartLocation = new Vector2(ActiveChar.transform.position.x, ActiveChar.transform.position.z);
            ActiveChar.DoAnimation(Cub.Animation.Character_Move);
        }

        public override void Continue()
        {
            base.Continue();
            float perc = 1 - (Timer / TimerMax);
            Vector3 where = new Vector3(StartLocation.x + (EndLocation.x - StartLocation.x) * perc
               , 0.5f, StartLocation.y + (EndLocation.y - StartLocation.y) * perc);
            //Debug.Log(EndLocation + "/" + StartLocation + "/" + where);
            ActiveChar.gameObject.transform.position = where;
        }

        public override void End()
        {
            base.End();
            ActiveChar.gameObject.transform.position = new Vector3(EndLocation.x, 0.5f, EndLocation.y);
        }
    }
}