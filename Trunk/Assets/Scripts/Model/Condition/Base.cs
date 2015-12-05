using Cub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model.Condition
{
    public abstract class Base
    {
        public Cub.Condition ConditionType;
        public string Name;
        public string Description;
        public Cub.ConditionGenre ConditionGenre;

        public Base()
        {

        }

        public abstract List<object> Confirm(Character who, List<object> data);
    }
}
