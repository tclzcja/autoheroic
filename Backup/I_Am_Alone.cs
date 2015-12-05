using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model.Condition
{
    public class I_Am_Alone : Base
    {
        protected Class TypeOfUnit;

        public I_Am_Alone()
        {
            Name = "I Am Alone";
            Description = "...if I’m the only one on my team left alive.";
            ConditionType = Cub.Condition.I_Am_Alone;
            ConditionGenre = Cub.ConditionGenre.Character;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            if (who.Stat.Team.Return_List_Character().Count <= 1)
                return data;
            return null;
        }
    }
}
