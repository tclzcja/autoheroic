using System;
using System.Collections.Generic;
using UnityEngine;
using Cub.Model;

namespace Cub.Model.Action
{
    public class Attack : Cub.Model.Action.Base
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
                if (Cub.Tool.Pathfinder.Distance(who.Stat.Position, enemy.Stat.Position) <= who.Info.Range)
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
            if (data.Count == 0) return new List<View.Eventon>();
            Character target = null;
            if (data.Count == 1)
                target = (data[0] as Cub.Model.Character);
            else
                target = (data[UnityEngine.Random.Range(0, data.Count)] as Cub.Model.Character);
            who.Stat.Cooldown += this.Turn_Cooldown;

            int Adv = FindAdvantage(who,target);
            int Dis = FindDisadvantage(who, target);
            bool RerollsGood = true;
            if (Dis > Adv)
                RerollsGood = false;
            int BestRoll = -1;
            for (int n = 1 + Mathf.Abs(Adv - Dis); n > 0; n--)
            {
                //Debug.Log("Test");
                int roll = UnityEngine.Random.Range(0,10);
                if (BestRoll == -1 || (RerollsGood && roll > BestRoll) || (!RerollsGood && roll < BestRoll))
                    BestRoll = roll;
            }
            r.AddRange(who.Info.Weapon.Make_Attack(who, target,BestRoll));
            return r;
        }

        public int FindAdvantage(Character who, Character target)
        {
            int r = 0;



            return r;
        }

        public int FindDisadvantage(Character who, Character target)
        {
            int r = 0;
            if (target.Info.Arms == Part_Arms.Sword)
                r++;
            return r;
        }
    }
}
