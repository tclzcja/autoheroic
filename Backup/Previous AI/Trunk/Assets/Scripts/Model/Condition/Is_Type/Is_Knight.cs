using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Knight : Is_Type
    {

        public Is_Knight()
        {
            Name = "Is A Knight";
            Description = "...if they are a Knight.";
            ConditionType = Cub.Condition.Is_Knight;
            TypeOfUnit = Class.Knight;
            ConditionGenre = Cub.ConditionGenre.Character;
        }
    }
}
