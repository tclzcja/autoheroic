using UnityEngine;
using System.Collections.Generic;

namespace Cub.Action
{
    public abstract class Base
    {
        public abstract string Name { get; }

        public abstract float Time_Casting { get; }
        public abstract float Time_Cooldown { get; }

        public List<Cub.Character> Target { get; set; }

		public abstract void Body(EventController con);
    }
}