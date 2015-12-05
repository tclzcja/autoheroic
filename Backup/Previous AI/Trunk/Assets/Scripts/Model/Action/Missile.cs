using System;
using System.Collections.Generic;
using Cub.Tool;
using UnityEngine;

namespace Cub.Tool.Action
{
    class Missile : Cub.Tool.Action.Base
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
                if (Pathfinder.Distance(who.Stat.Position, enemy.Stat.Position) <= Range)
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
                target = (data[0] as Cub.Tool.Character);
            else
                target = (data[UnityEngine.Random.Range(0, data.Count)] as Cub.Tool.Character);
            who.Stat.Cooldown += this.Turn_Cooldown;
            who.ExhaustedActions.Add(ActionType);
            Debug.Log("Missile: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ")");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Rocket, who.FindColorName() + " <MISSILE> " + target.FindColorName(), new List<object>() { who.ID, target.ID }));
            foreach (Character guy in Main.AllCharacters())
                if (Pathfinder.Distance(target.Stat.Position, guy.Stat.Position) <= 1.5f)
                {
                    guy.Damage(Damage, who,r);
                    //if (kill)
                    //{
                    //    r.Add(new Cub.View.GameEvent(Cub.Event.Die, "R.I.P. " + guy.Name, new List<object> { guy.ID }));
                    //}
                }
            return r;
        }
    }
}
