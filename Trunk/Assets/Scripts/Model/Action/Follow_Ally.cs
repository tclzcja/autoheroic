using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Model;

namespace Cub.Model.Action
{
    public class Follow_Ally : Cub.Model.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        //public Charge(List<object> _Info)
        //{
        //    this.Info = _Info;
        //}

        public Follow_Ally()
        {
            Name = "Follow Ally";
            Description = "I will follow/go to an ally";
            SpecialAbility = false;
            ActionType = Cub.Action.Follow_Ally;
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
        }

        public override List<object> Confirm(Character who)
        {
            List<object> data = new List<object>();
            foreach (Character friend in who.Stat.Team.Return_List_Character())
                if (friend != who)
                    data.Add(friend);
            if (data.Count == 0) return null;
            return data;
        }

        public override List<Cub.View.Eventon> Body(Character who, List<object> data)
        {
            who.Stat.Cooldown += this.Turn_Cooldown;
            if (data.Count == 0) return new List<View.Eventon>();
            Character target;
            if (data.Count == 1)
                target = (Character)data[0];
            else
                target = (Character)data[UnityEngine.Random.Range(0, data.Count)];

            List<Cub.Position2> path = Cub.Tool.Pathfinder.findPath(who.Stat.Position, target.Stat.Position, who.Info.Blockable);
            int TravelDistance = Math.Min(who.Info.Speed,path.Count) - 1;
            if (TravelDistance < 0) return new List<View.Eventon>();
            who.SetLocation(path[TravelDistance]);
            Debug.Log("Follow: " + who.Name + " >" + path[TravelDistance].ToString());
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            for (int n = 0; n <= TravelDistance; n++){
                r.Add(new Cub.View.Eventon(Cub.Event.Move, who.FindColorName() + ": Following " + target.FindColorName(),
                    true,new List<object>() { who.ID, path[n].X, path[n].Y }));
            }
            return r;
        }
    }
}
