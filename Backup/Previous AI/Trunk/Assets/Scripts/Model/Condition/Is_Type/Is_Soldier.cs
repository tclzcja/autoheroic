using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Soldier : Is_Type
    {

        public Is_Soldier()
        {
            Name = "Is A Soldier";
            Description = "...if they are a Soldier.";
            ConditionType = Cub.Condition.Is_Soldier;
            TypeOfUnit = Class.Soldier;
            ConditionGenre = Cub.ConditionGenre.Character;
        }
    }
}
