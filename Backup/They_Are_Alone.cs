using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model.Condition
{
    public class They_Are_Alone : Base
    {
        protected Class TypeOfUnit;

        public They_Are_Alone()
        {
            Name = "They Are Alone";
            Description = "...if they’re the only one on their team left alive.";
            ConditionType = Cub.Condition.They_Are_Alone;
            ConditionGenre = Cub.ConditionGenre.Character;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            if (who.FindEnemies().Count <= 1)
                return data;
            return null;
        }
    }
}
