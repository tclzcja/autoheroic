using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Model;

namespace Cub.Model.Action
{
    public class Follow_Enemy : Cub.Model.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        //public Charge(List<object> _Info)
        //{
        //    this.Info = _Info;
        //}

        public Follow_Enemy()
        {
            Name = "Follow Enemy";
            Description = "I will advance towards an enemy";
            SpecialAbility = false;
            ActionType = Cub.Action.Follow_Enemy;
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
        }

        public override List<object> Confirm(Character who)
        {
            List<object> data = new List<object>();
            foreach (Character enemy in who.FindEnemies())
                data.Add(enemy);
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
            
            Debug.Log("Follow: " + who.Name + " >" + path[TravelDistance].ToString());
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            Cub.Position2 where = who.Stat.Position;
            for (int n = 0; n <= TravelDistance; n++){
                r.Add(new Cub.View.Eventon(Cub.Event.Move, who.FindColorName() + ": Following " + target.FindColorName(),
                    true, new List<object>() { who.ID, path[n].X, path[n].Y }));
                where = path[n];
                if (Cub.Tool.Pathfinder.Distance(path[n], target.Stat.Position) <= who.Info.Range)
                    break;
            }
                who.SetLocation(where);
            return r;
        }
    }
}
