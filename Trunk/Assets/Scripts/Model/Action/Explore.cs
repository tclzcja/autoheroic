using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Model;

namespace Cub.Model.Action
{
    public class Explore : Cub.Model.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        //public Explore(List<object> _Info)
        //{
        //    this.Info = _Info;
        //}

        public Explore()
        {
            Name = "Explore";
            Description = "I will wander randomly";
            SpecialAbility = false;
            ActionType = Cub.Action.Explore;
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Location);
        }

        public override List<object> Confirm(Character who)
        {
            int NowX = who.Stat.Position.X;
            int NowY = who.Stat.Position.Y;
            int M = who.Info.Speed;

            List<object> L = new List<object>();

            for (int x = NowX - M; x < NowX + M; x++)
                for (int y = NowY - (M - System.Math.Abs(x - NowX)); y < NowY + (M - System.Math.Abs(x - NowX)); y++)
                {
                    Cub.Position2 V = new Cub.Position2(x, y);
                    if (Cub.Tool.Pathfinder.CheckAccessable(V,true))
                        L.Add(V);
                }

            return L;
        }

        public override List<Cub.View.Eventon> Body(Character who, List<object> data)
        {
            //Cooldown
            //Cub.Model.Character who = this.Info[0] as Cub.Model.Character;
            who.Stat.Cooldown += this.Turn_Cooldown;

            Cub.Position2 where;
            if (data.Count == 0) return new List<View.Eventon>();
            if (data.Count == 1) where = (Cub.Position2)data[0];
            else where = (Cub.Position2)data[UnityEngine.Random.Range(0, data.Count)];
            List<Cub.Position2> path = Cub.Tool.Pathfinder.findPath(who.Stat.Position, where,who.Info.Blockable);
            int TravelDistance = Math.Min(who.Info.Speed, path.Count) - 1;
            if (TravelDistance < 0)
                return new List<Cub.View.Eventon>() { };

            //who.Stat.Position_X = (int)L[Index].x;
            //who.Stat.Position_Y = (int)L[Index].y;

            who.SetLocation(path[TravelDistance]);

            Debug.Log("Explore: " + who.Name + " >" + path[TravelDistance].ToString());
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            for (int n = 0; n <= TravelDistance; n++)
            {
                r.Add(new Cub.View.Eventon(Cub.Event.Move, who.FindColorName() + ": Exploring", true,
                    new List<object>() { who.ID, path[n].X, path[n].Y }));
            }
            return r;
        }
    }
}
