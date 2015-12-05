using System;
using System.Collections.Generic;
using UnityEngine;
namespace AM.Actions
{
    public class Explore : AM.Actions.Action
    {
        public override string Name { get { return "Explore"; } }

        public override void Start()
        {
            AM.Character C = this.Target_List[0].GetComponent<AM.Character>();
            int Direction = UnityEngine.Random.Range(1, 4);
            Vector2 New = new Vector2();
            switch (Direction)
            {
                case 1:
                    {
                        if ((int)C.Stats_Position.x > 0)
                        {
                            New = new Vector2(C.Stats_Position.x - 1, C.Stats_Position.y);
                        }
                        break;
                    }
                case 2:
                    {
                        if ((int)C.Stats_Position.x < Field.Units.Length)
                        {
                            New = new Vector2(C.Stats_Position.x + 1, C.Stats_Position.y);
                        }
                        break;
                    }
                case 3:
                    {
                        if (C.Stats_Position.y > 0)
                        {
                            New = new Vector2(C.Stats_Position.x, C.Stats_Position.y - 1);
                        }
                        break;
                    }
                case 4:
                    {
                        if (C.Stats_Position.y < Field.Units[(int)C.Stats_Position.x].Length)
                        {
                            New = new Vector2(C.Stats_Position.x, C.Stats_Position.y + 1);
                        }
                        break;
                    }
            }
            Field.Units[(int)C.Stats_Position.x][(int)C.Stats_Position.y] = null;
            Field.Units[(int)New.x][(int)New.y] = C.gameObject;
            C.Stats_Position = New;
            C.gameObject.transform.position = new Vector3(New.x, 1, New.y);

            Destroy(this.gameObject, 1.0F);
        }

        public override void Update()
        {

        }
    }
}
