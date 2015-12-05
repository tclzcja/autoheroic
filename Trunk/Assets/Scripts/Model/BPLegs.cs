using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPLegs : Bodypart
    {
        public int Speed;
        public bool Blockable;
        public Cub.Part_Legs E;

        public BPLegs(string name, string desc, int sp, int cst, Cub.Part_Legs e)
        {
            Name = name;
            Description = desc;
            SpDescription = "";
            Speed = sp;
            Cost = cst;
            Blockable = true;
            E = e;
        }

        public BPLegs(string name, string desc, int sp, int cst, Cub.Part_Legs e, List<Special_Effects> eff, bool block, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Speed = sp;
            Cost = cst;
            Effects = eff;
            Blockable = block;
            E = e;
        }

        public BPLegs(string name, string desc, int sp, int cst, Cub.Part_Legs e, List<Cub.Action> abil, bool block, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Speed = sp;
            Cost = cst;
            Special_Abilities = abil;
            Blockable = block;
            E = e;
        }

        public BPLegs(string name, string desc, int sp, int cst, Cub.Part_Legs e, List<Special_Effects> eff, List<Cub.Action> abil, bool block, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Speed = sp;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            Blockable = block;
            E = e;
        }
    }
}
