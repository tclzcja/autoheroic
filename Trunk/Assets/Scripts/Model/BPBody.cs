using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cub.Model
{
    public class BPBody : Bodypart
    {
        public int Health;
        public Cub.Part_Body E;
        
        public BPBody(string name, string desc, int hp, int cst, Cub.Part_Body e)
        {
            Name = name;
            Description = desc;
            SpDescription = "";
            Health = hp;
            Cost = cst;
            E = e;
        }

        public BPBody(string name, string desc, int hp, int cst, Cub.Part_Body e, List<Special_Effects> eff, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Health = hp;
            Cost = cst;
            Effects = eff;
            E = e;
        }

        public BPBody(string name, string desc, int hp, int cst, Cub.Part_Body e, List<Cub.Action> abil, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Health = hp;
            Cost = cst;
            Special_Abilities = abil;
            E = e;
        }

        public BPBody(string name, string desc, int hp, int cst, Cub.Part_Body e, List<Special_Effects> eff, List<Cub.Action> abil, string spDesc)
        {
            Name = name;
            Description = desc;
            SpDescription = spDesc;
            Health = hp;
            Cost = cst;
            Effects = eff;
            Special_Abilities = abil;
            E = e;
        }
    }
}
