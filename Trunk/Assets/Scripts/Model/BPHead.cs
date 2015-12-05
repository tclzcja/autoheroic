using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPHead : Bodypart
    {

        public Cub.Part_Head E;
        public string AIDesc;

        public BPHead(string name, string aidesc, int cst, Cub.Part_Head e, string desc)
        {
            Name = name;
            Description = desc;
            SpDescription = "";
            Cost = cst;
            E = e;
            AIDesc = aidesc;
        }

        public BPHead(string name, string aidesc, int cst, Cub.Part_Head e, List<Special_Effects> eff, string spDesc, string desc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Effects = eff;
            E = e;
            AIDesc = aidesc;
        }

        public BPHead(string name, string aidesc, int cst, Cub.Part_Head e, List<Cub.Action> abil, string spDesc, string desc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Special_Abilities = abil;
            E = e;
            AIDesc = aidesc;
        }

        public BPHead(string name, string aidesc, int cst, Cub.Part_Head e, List<Special_Effects> eff, List<Cub.Action> abil, string spDesc, string desc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            E = e;
            AIDesc = aidesc;
        }
    }
}
