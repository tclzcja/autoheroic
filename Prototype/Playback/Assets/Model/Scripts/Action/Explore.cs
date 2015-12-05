using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Action
{
    public class Explore : Cub.Action.Base
    {
        public override string Name { get { return "Explorer"; } }

        public override float Time_Casting { get { return 0F; } }
        public override float Time_Cooldown { get { return 0F; } }

		public override void Body(EventController con)
        {
            List<Vector2> L = new List<Vector2>();

            int NowX = (int)Cub.Main.Info_Position[this.Target[0]].x;
            int NowY = (int)Cub.Main.Info_Position[this.Target[0]].y;
            int M = this.Target[0].Mobility;

            for (int x = NowX - M; x < NowX + M; x++)
                for (int y = NowY - (M - System.Math.Abs(x - NowX)); y < NowY + (M - System.Math.Abs(x - NowX)); y++)
                {
                    Vector2 V = new Vector2(x, y);
                    if (x >= 0 && x < Cub.Library.Stage_Terrain.Length && y >= 0 && y < Cub.Library.Stage_Terrain[x].Length)
                        if (!Cub.Main.Info_Position.ContainsValue(V))
                        {
                            L.Add(V);
                        }
                }

            int Index = UnityEngine.Random.Range(0, L.Count - 1);

            Cub.Main.Move(this.Target[0], L[Index]);
			con.QueueEvent(new AM.GameEvent(Cub.Type.GEventType.Walk,new List<string>{
				Target[0].UName,L[Index].x.ToString(),L[Index].y.ToString()}));
            Debug.Log("Move: " + this.Target[0].Class + ">" + L[Index].ToString());
        }
    }
}
