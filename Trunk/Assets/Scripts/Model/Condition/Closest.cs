using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model.Condition
{
    public class Closest : Base
    {
        public Closest()
        {
            Name = "Closest";
            Description = "...if they’re the closest to me.";
            ConditionType = Cub.Condition.Closest;
            ConditionGenre = Cub.ConditionGenre.Character;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            List<object> r = new List<object>();
            int bDist = 99999;
            foreach (object obj in data)
            {
                Character e = (Character)obj;
                int dist = Cub.Tool.Pathfinder.Distance(e.Stat.Position, who.Stat.Position);
                if (dist < bDist)
                    r.Clear();
                if (dist <= bDist)
                    r.Add(e);
            }
            if (r.Count == 0)
                return null;
            else
                return r;
        }
    }
}
