using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.View.Event
{
    public class Be_Healed : Base
    {
        public override float Process(List<object> _Data, string Desc)
        {
            return 1.5F;
        }
    }
}
