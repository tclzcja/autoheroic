using UnityEngine;
using System.Collections.Generic;

namespace Cub.Model.Action
{
    public abstract class Base
    {
        public string Name;
        public string Description;
        public Cub.Action ActionType { get; protected set; }
        public bool SpecialAbility;
        public List<Cub.ConditionGenre> ValidConditions = new List<ConditionGenre>();

        public abstract int Turn_Casting { get; }
        public abstract int Turn_Cooldown { get; }

        //public List<object> Info { get; protected set; }

        public virtual List<object> Confirm(Character who)
        {
            return new List<object>();
        }

        public abstract List<Cub.View.Eventon> Body(Character who, List<object> data);
    }
}