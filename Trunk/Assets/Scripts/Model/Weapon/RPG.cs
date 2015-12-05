using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cub.Model.Weapons
{
    class RPG : Cub.Model.Weapon
    {
        public RPG(int range, int missC, int critC, int hitD, int critD)
        {
            Range = range;
            MissChance = missC;
            CritChance = critC;
            HitDam = hitD;
            CritDam = critD;
        }

        public override List<View.Eventon> Make_Attack(Character who, Character target, int roll)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            Cub.Attack_Result result = Attack_Result.Hit;
            int CritNum = 10 - CritChance;
            if (roll < MissChance)
                result = Attack_Result.Miss;
            else if (roll >= CritNum)
                result = Attack_Result.Crit;
            int dam = CritDam;
            Vector2 where = target.Stat.Position.ToVector2();
            List<Vector2> dirs = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
            if (result == Attack_Result.Hit)
                where += dirs[UnityEngine.Random.Range(0, dirs.Count)];
            else if (result == Attack_Result.Miss)
            {
                Vector2 wander1 = dirs[UnityEngine.Random.Range(0, dirs.Count)];
                Vector2 wander2 = dirs[UnityEngine.Random.Range(0, dirs.Count)];
                if (wander1 + wander2 != Vector2.zero)
                    wander1 += wander2;
                where += wander1;
            }
            Debug.Log("Attack: " + who.Name + " > " + target.Name + " ["
                + MissChance.ToString() + "/" + CritChance.ToString() + "/" + roll.ToString() + "/" + result + "/" + dam.ToString() + "]");
            r.Add(new Cub.View.Eventon(Cub.Event.Attack_Rocket, who.FindColorName() + " vs. " + target.FindColorName(),
                true,new List<object>() { who.ID, where }));
            r.AddRange(SplashDamage(where, Attack_Result.Crit,who));
            foreach (Vector2 d in dirs)
                r.AddRange(SplashDamage(where + d, Attack_Result.Hit, who));
            return r;
        }

        List<Cub.View.Eventon> SplashDamage(Vector2 whereV, Attack_Result hit, Character source)
        {
            List<Cub.View.Eventon> r = new List<Cub.View.Eventon>();
            Position2 where = new Position2((int)whereV.x,(int)whereV.y);
            int damage = HitDam;
            if (hit == Attack_Result.Crit)
                damage = CritDam;
            foreach (Character who in Cub.Model.Main.AllCharacters())
                if (where == who.Stat.Position)
                {
                    who.Damage(damage, source, r, hit);
                    break;
                }
            return r;
        }
    }
}
