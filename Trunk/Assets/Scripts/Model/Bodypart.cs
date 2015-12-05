using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class Bodypart
    {
        public string Name;
        public string Description;
        public string SpDescription;
        
        public int Cost;

        public List<Special_Effects> Effects = new List<Special_Effects>();
        public List<Cub.Action> Special_Abilities = new List<Cub.Action>();

        public Bodypart()
        {

        }
    }
}
