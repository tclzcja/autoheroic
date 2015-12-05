using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AM.Actions
{
    public class Observe : AM.Actions.Action
    {
        public override string Name { get { return "Observe"; } }

        public Observe(List<GameObject> CL)
        {
            this.Target_List = CL;
        }

        public override void Start()
        {
            AM.Character C = this.Target_List[0].GetComponent<AM.Character>();
            C.Input(this.gameObject);
            C.Stats_Sight = new List<Vector2>();
            for (int x = (int)(C.Stats_Position.x - 2); x <= (int)(C.Stats_Position.x + 2); x++)
                for (int y = (int)(C.Stats_Position.y - 2 + x); y <= (int)(C.Stats_Position.y + 2 - x); y++)
                {
                    C.Stats_Sight.Add(new Vector2(x, y));
                }
            Destroy(this.gameObject);
        }

        public override void Update()
        {

        }
    }
}
