using System;
using System.Collections.Generic;
using Cub.Tool;
using UnityEngine;

namespace Cub.Tool.Action
{
    class Heal : Cub.Tool.Action.Base
    {
        public override int Turn_Casting { get { return 0; } }
        public override int Turn_Cooldown { get { return 2; } }

        public int Range = 3;
        public int HealAmt = 1;

        public Heal()
        {
            Name = "Heal";
            Description = "I will heal an ally for 1HP";
            SpecialAbility = true;
            ActionType = Cub.Action.Heal;
            ValidConditions.Add(Cub.ConditionGenre.Generic);
            ValidConditions.Add(Cub.ConditionGenre.Character);
        }

        public override List<object> Confirm(Character who)
        {
            if (who.ExhaustedActions.Contains(ActionType)) return null;
            List<object> data = new List<object>();
            bool anyone = false;
            foreach (Character friend in who.Stat.GetTeam().Return_List_Character())
                if (Pathfinder.Distance(who.Stat.Position, friend.Stat.Position) <= Range)
                {
                    data.Add(friend);
                    anyone = true;
                }
            if (!anyone)
                return null;
            return data;
        }

        public override List<Cub.View.Eventon> Body(Character who, List<object> data)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            if (data.Count == 0) return new List<View.Eventon>();
            Character target = null;
            if (data.Count == 1)
                target = (data[0] as Cub.Tool.Character);
            else
                target = (data[UnityEngine.Random.Range(0, data.Count)] as Cub.Tool.Character);
            who.Stat.Cooldown += this.Turn_Cooldown;
            Debug.Log("Heal: " + who.Name + " (" + who.Info.Class + ") > " + target.Name + " (" + target.Info.Class + ")");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Heal, who.FindColorName() + " <HEAL> " + target.FindColorName(), new List<object>() { who.ID, target.ID }));
            target.Heal(HealAmt, who, r);
            return r;
        }
    }
}
