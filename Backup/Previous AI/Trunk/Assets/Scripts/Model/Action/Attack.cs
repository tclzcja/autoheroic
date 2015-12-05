using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Tool;

namespace Cub.Tool.Action
{
    public class Attack : Cub.Tool.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        public Attack()
        {
            ActionType = Cub.Action.Attack;
            Name = "Attack";
            SpecialAbility = false;
            Description = "I will attack an enemy within my range for 2 damage";
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
        }

        public override List<object> Confirm(Character who)
        {
            List<object> data = new List<object>();
            bool anyone = false;
            foreach (Character enemy in who.FindEnemies())
                if (Pathfinder.Distance(who.Stat.Position, enemy.Stat.Position) <= who.Info.Range)
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

            Debug.Log("Attack: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ")");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Range, who.FindColorName() + " vs. " + target.FindColorName(), new List<object>() { who.ID, target.ID }));
            target.Damage(2, who, r);
            //if (kill){
            //    r.Add(new Cub.View.GameEvent(Cub.Event.Die, "R.I.P. " + target.Name, new List<object>{target.ID }));
            //}
            return r;
        }
    }
}
