using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class Score
    {
        public string Name = "";
        public int Value = 0;

        public Score()
        {

        }

        public Score(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
