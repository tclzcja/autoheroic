using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPArms : Bodypart
    {

        public Cub.Part_Arms E;

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e)
        {
            Name = name;
            Description = desc;
            SpDescription = "";
            Cost = cst;
            E = e;
        }

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e, List<Special_Effects> eff, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Effects = eff;
            E = e;
        }

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e, List<Cub.Action> abil, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Special_Abilities = abil;
            E = e;
        }

        public BPArms(string name, string desc, int cst, Cub.Part_Arms e, List<Special_Effects> eff, List<Cub.Action> abil, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            E = e;
        }
    }
}
