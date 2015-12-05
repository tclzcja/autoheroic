using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Tool.Condition
{
    public class Is_Type : Base
    {
        protected Class TypeOfUnit;

        public Is_Type()
        {
            
        }

        public override List<object> Confirm(Character who, List<object> data)
        {
            List<object> r = new List<object>();
            foreach (object obj in data)
            {
                Character e = (Character)obj;
                if (e.Info.Class == TypeOfUnit)
                    r.Add(e);
            }
            if (r.Count == 0)
                return null;
            else
                return r;
        }
    }
}
