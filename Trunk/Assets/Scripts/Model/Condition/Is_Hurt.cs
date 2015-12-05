using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model.Condition
{
    public class Is_Hurt : Base
    {
        //protected Class TypeOfUnit;

        public Is_Hurt()
        {
            Name = "Is Hurt";
            Description = "...if they’ve been hurt.";
            ConditionType = Cub.Condition.Is_Hurt;
            ConditionGenre = Cub.ConditionGenre.Character;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            List<object> r = new List<object>();
            foreach (object obj in data)
            {
                Character e = (Character)obj;
                if (e.Stat.HP < e.Info.MHP)
                    r.Add(e);
            }
            if (r.Count == 0)
                return null;
            else
                return r;
        }
    }
}
