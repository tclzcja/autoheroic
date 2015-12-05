using System;
using System.Collections.Generic;
using Cub.Model;
using UnityEngine;

namespace Cub.Model.Action
{
    class Missile : Cub.Model.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        public int Range = 5;
        public int Damage = 3;

        public Missile()
        {
            Name = "Missile";
            Description = "I will fire a rocket at the first enemy I see within 5 squares, doing 3 damage to them and anyone next to them";
            SpecialAbility = true;
            ActionType = Cub.Action.Missile;
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
            ValidConditions.Add(Cub.ConditionGenre.Missile);
        }

        public override List<object> Confirm(Character who)
        {
            if (who.ExhaustedActions.Contains(ActionType)) return null;
            List<object> data = new List<object>();
            bool anyone = false;
            foreach (Character enemy in who.FindEnemies())
                if (Cub.Tool.Pathfinder.Distance(who.Stat.Position, enemy.Stat.Position) <= Range)
                {
                    data.Add(enemy);
                    anyone = true;
                }
            if (!anyone)
                return null;
            return data;
        }

        public override List<Cub.View.Eventon> Body(Character who, List<object> data)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            //Cooldown
            //Character who = (this.Info[0] as Cub.Model.Character);
            if (data.Count == 0) return new List<View.Eventon>();
            Character target = null;
            if (data.Count == 1)
                target = (data[0] as Cub.Model.Character);
            else
                target = (data[UnityEngine.Random.Range(0, data.Count)] as Cub.Model.Character);
            who.Stat.Cooldown += this.Turn_Cooldown;
            who.ExhaustedActions.Add(ActionType);
            Debug.Log("Missile: " + who.Name +" > " + target.Name);
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Rocket, who.FindColorName() + " <MISSILE> " + target.FindColorName(),
                new List<object>() { who.ID, target.Stat.Position.ToVector2() }));
            foreach (Character guy in Main.AllCharacters())
                if (Cub.Tool.Pathfinder.Distance(target.Stat.Position, guy.Stat.Position) <= 1.5f)
                {
                    guy.Damage(Damage, who,r,Cub.Attack_Result.Hit);
                }
            return r;
        }
    }
}
