using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Tool;

namespace Cub.Tool.Action
{
    public class Charge : Cub.Tool.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        //public Charge(List<object> _Info)
        //{
        //    this.Info = _Info;
        //}

        public Charge()
        {
            Name = "Charge";
            Description = "I will charge at and attack an enemy within my move range";
            SpecialAbility = true;
            ActionType = Cub.Action.Charge;
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
        }

        public override List<object> Confirm(Character who)
        {
            List<object> data = new List<object>();
            foreach (Character enemy in who.FindEnemies())
            {
                int dist = Pathfinder.Distance(who.Stat.Position, enemy.Stat.Position);
                if (dist <= who.Info.Speed + 1)
                    data.Add(enemy);
            }
            if (data.Count == 0)
                return null;

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

            List<Cub.Position2> path = Pathfinder.findPath(who.Stat.Position, target.Stat.Position);
            int TravelDistance = Math.Min(who.Info.Speed, path.Count) - 1;
            if (TravelDistance < 0) return new List<View.Eventon>();
            who.SetLocation(path[TravelDistance]);

            Debug.Log("Charge: " + who.Name + " (" + who.Info.Class + ") >" + path[TravelDistance].ToString());
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            for (int n = 0; n <= TravelDistance; n++){
                r.Add(new Cub.View.Eventon(Cub.Event.Move, who.FindColorName() + ": Charging " + target.FindColorName(), new List<object>() { who.ID, path[n].X, path[n].Y }));
            }
            if (Pathfinder.Distance(who.Stat.Position,target.Stat.Position) <= who.Info.Range)
                r.AddRange(Library.Get_Action(Cub.Action.Attack).Body(who, new List<object>{target}));
            return r;
        }
    }
}
