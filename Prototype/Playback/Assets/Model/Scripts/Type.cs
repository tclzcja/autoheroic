using System;
using UnityEngine;

namespace Cub.Type
{
    public enum Target
    {
        None = 0,
        Self = 1,
        Ally = 2,
        Enemy = 3,
        Terrain = 4
    }

    public enum Condition
    {
        None,
        Any,
        Closest,
        Furthest,
        In_Range,
        Not_In_Range,
        Is_Knight,
        Is_Mage,
        Is_Archer,
        Is_Not_Knight,
        Is_Not_Mage,
        Is_Not_Archer,
        Damaged,
        Not_Damaged
    }

    public enum Action
    {
        None = 0,
        Explore,
        Approach,
        Fallback,
        Attack,
        Charge,
        Heal,
        Blizzard,
        Explosive,
        Snipe,
        Stealth
    }

    public enum Terrain
    {
        None = 0,
        Grass,
        Desert,
        Dirt
    }

    public enum Class
    {
        None,
        Knight,
        Archer,
        Mage
    }

	public enum GEventType
	{
		None,
		Walk,
		Attack,
		TakeDamage,
		Die
	}

	public enum Anim{
		None,
		Walk,
		Attack,
		TakeDamage,
		Die
	}

}
