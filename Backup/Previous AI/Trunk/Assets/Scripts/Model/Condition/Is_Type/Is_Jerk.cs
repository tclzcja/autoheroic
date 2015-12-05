using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Jerk : Is_Type
    {

        public Is_Jerk()
        {
            Name = "Is A Jerk";
            Description = "...if they are a Jerk.";
            ConditionType = Cub.Condition.Is_Jerk;
            TypeOfUnit = Class.Jerk;
            ConditionGenre = Cub.ConditionGenre.Character;
        }
    }
}
