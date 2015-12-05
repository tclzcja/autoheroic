using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Any : Base
    {
        public Any()
        {
            Name = "None";
            Description = "";
            this.ConditionType = Cub.Condition.Any;
            ConditionGenre = Cub.ConditionGenre.Generic;
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            return data;
        }
    }
}
