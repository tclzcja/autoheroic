using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.View.Event
{
    public abstract class Base
    {
        public abstract float Process(List<object> _Data, string Desc);
    }
}
