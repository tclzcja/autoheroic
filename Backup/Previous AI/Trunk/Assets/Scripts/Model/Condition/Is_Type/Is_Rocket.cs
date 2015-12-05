using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Rocket : Is_Type
    {

        public Is_Rocket()
        {
            Name = "Is A Rocket-Man";
            Description = "...if they are a Rocket-Man.";
            ConditionType = Cub.Condition.Is_Rocket;
            TypeOfUnit = Class.Rocket;
            ConditionGenre = Cub.ConditionGenre.Character;
        }
    }
}
