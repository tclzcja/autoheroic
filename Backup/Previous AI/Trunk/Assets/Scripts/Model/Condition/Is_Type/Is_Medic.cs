using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Medic : Is_Type
    {

        public Is_Medic()
        {
            Name = "Is A Medic";
            Description = "...if they are a Medic.";
            ConditionType = Cub.Condition.Is_Medic;
            TypeOfUnit = Class.Medic;
            ConditionGenre = Cub.ConditionGenre.Character;
        }
    }
}
