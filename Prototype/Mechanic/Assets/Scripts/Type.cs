using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM.Type
{
    public enum TTarget
    {
        Any,
        Self,
        Ally,
        Enemy,
        Terrain
    }

    public enum TSituation
    {
        Any,
        No_Enemy_Insight,
        No_Enemy_In_3,
        No_Enemy_In_2,
        No_Enemy_In_1,
        Being_Surrounded,
        Being_Controlled,
        Closest,
        Farest,
        Not_In_Melee_Range,
        In_Melee_Range
    }

    public enum TAction
    {
        Explore,
        Approach,
        Observe, //Observe is also an action. A pure model.
        Attack,
        Fireball,
    }

    public enum TTerrain
    {
        Grass = 1,
        Desert = 2
    }
}