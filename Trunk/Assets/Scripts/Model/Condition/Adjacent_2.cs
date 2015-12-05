using System;
using System.Collections.Generic;
using Cub.Model;

namespace Cub.Model.Condition
{
    public class Adjacent_2 : Base
    {
        public Adjacent_2()
        {
            Name = "Two Adjacent";
            Description = "...if it can hit at least two enemies.";
            ConditionType = Cub.Condition.Adjacent_2;
            ConditionGenre = Cub.ConditionGenre.Missile;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            List<object> r = new List<object>();
            List<Character> enemies = who.FindEnemies();
            foreach (object obj in data)
            {
                Character e = (Character)obj;
                foreach (Character other in enemies)
                    if (e != other && Cub.Tool.Pathfinder.Distance(e.Stat.Position, other.Stat.Position) <= 1.5f)
                        r.Add(e);
            }
            if (r.Count == 0)
                return null;
            else
                return r;
        }
    }
}
