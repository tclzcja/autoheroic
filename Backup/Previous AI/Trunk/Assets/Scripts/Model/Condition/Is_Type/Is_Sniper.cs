using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Sniper : Is_Type
    {

        public Is_Sniper()
        {
            Name = "Is A Sniper";
            Description = "...if they are a Sniper.";
            ConditionType = Cub.Condition.Is_Sniper;
            TypeOfUnit = Class.Sniper;
            ConditionGenre = Cub.ConditionGenre.Character;
        }
    }
}
