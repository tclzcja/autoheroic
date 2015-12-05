using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model.Condition
{
    public class Almost_Dead : Base
    {
        //protected Class TypeOfUnit;

        public Almost_Dead()
        {
            Name = "Almost Dead";
            Description = "...if I can kill them in one attack.";
            ConditionType = Cub.Condition.Almost_Dead;
            ConditionGenre = Cub.ConditionGenre.Character;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            List<object> r = new List<object>();
            foreach (object obj in data)
            {
                Character e = (Character)obj;
                if (e.Stat.HP <= 2)
                    r.Add(e);
            }
            if (r.Count == 0)
                return null;
            else
                return r;
        }
    }
}
