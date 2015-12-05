using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.Tool
{
    public static class Library
    {
        private static bool Trigger = true;

        public static int PointCap = 600;

        private static Dictionary<Cub.Condition, Cub.Tool.Condition.Base> Dictionary_Condition { get; set; }
        private static Dictionary<Cub.Action, Cub.Tool.Action.Base> Dictionary_Action { get; set; }
        private static Dictionary<string, Cub.Condition> Condition_Strings { get; set; }
        private static Dictionary<string, Cub.Action> Action_Strings { get; set; }
        private static Dictionary<Cub.Class, Cub.Tool.Character_Info> Dictionary_Class_Info { get; set; }

        public static Cub.Terrain[][] Stage_Terrain { get; set; }
        public static Cub.Class[][] Stage_Unit { get; set; }

        public static void Initialization()
        {
            if (Trigger)
            {
                Dictionary_Class_Info = new Dictionary<Cub.Class, Cub.Tool.Character_Info>();
                Dictionary_Class_Info[Class.None] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_None.xml") as Cub.Tool.Character_Info;
                Dictionary_Class_Info[Class.Soldier] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_Soldier.xml") as Cub.Tool.Character_Info;
                Dictionary_Class_Info[Class.Knight] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_Knight.xml") as Cub.Tool.Character_Info;
                Dictionary_Class_Info[Class.Rocket] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_Rocket.xml") as Cub.Tool.Character_Info;
                Dictionary_Class_Info[Class.Sniper] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_Sniper.xml") as Cub.Tool.Character_Info;
                Dictionary_Class_Info[Class.Jerk] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_Jerk.xml") as Cub.Tool.Character_Info;
                Dictionary_Class_Info[Class.Medic] = Cub.Tool.Xml.Deserialize(typeof(Cub.Tool.Character_Info), "Data/Character_Info_Medic.xml") as Cub.Tool.Character_Info;

                Dictionary_Condition = new Dictionary<Cub.Condition, Cub.Tool.Condition.Base>();
                Dictionary_Condition[Cub.Condition.Any] = new Cub.Tool.Condition.Any();
                Dictionary_Condition[Cub.Condition.Adjacent_2] = new Cub.Tool.Condition.Adjacent_2();
                Dictionary_Condition[Cub.Condition.Is_Soldier] = new Cub.Tool.Condition.Is_Soldier();
                Dictionary_Condition[Cub.Condition.Is_Sniper] = new Cub.Tool.Condition.Is_Sniper();
                Dictionary_Condition[Cub.Condition.Is_Rocket] = new Cub.Tool.Condition.Is_Rocket();
                Dictionary_Condition[Cub.Condition.Is_Medic] = new Cub.Tool.Condition.Is_Medic();
                Dictionary_Condition[Cub.Condition.Is_Knight] = new Cub.Tool.Condition.Is_Knight();
                Dictionary_Condition[Cub.Condition.Is_Jerk] = new Cub.Tool.Condition.Is_Jerk();
                Dictionary_Condition[Cub.Condition.Almost_Dead] = new Cub.Tool.Condition.Almost_Dead();
                Dictionary_Condition[Cub.Condition.Is_Hurt] = new Cub.Tool.Condition.Is_Hurt();
                Dictionary_Condition[Cub.Condition.I_Am_Alone] = new Cub.Tool.Condition.I_Am_Alone();
                Dictionary_Condition[Cub.Condition.They_Are_Alone] = new Cub.Tool.Condition.They_Are_Alone();
                Dictionary_Condition[Cub.Condition.Closest] = new Cub.Tool.Condition.Closest();

                Dictionary_Action = new Dictionary<Cub.Action, Cub.Tool.Action.Base>();
                Dictionary_Action[Cub.Action.Attack] = new Cub.Tool.Action.Attack();
                Dictionary_Action[Cub.Action.Explore] = new Cub.Tool.Action.Explore();
                Dictionary_Action[Cub.Action.Charge] = new Cub.Tool.Action.Charge();
                Dictionary_Action[Cub.Action.Missile] = new Cub.Tool.Action.Missile();
                Dictionary_Action[Cub.Action.Heal] = new Cub.Tool.Action.Heal();
                Dictionary_Action[Cub.Action.Snipe] = new Cub.Tool.Action.Snipe();
                Dictionary_Action[Cub.Action.Follow_Ally] = new Cub.Tool.Action.Follow_Ally();
                Dictionary_Action[Cub.Action.Follow_Enemy] = new Cub.Tool.Action.Follow_Enemy();

                Action_Strings = new Dictionary<string, Cub.Action>();
                foreach (Cub.Tool.Action.Base act in Dictionary_Action.Values)
                    Action_Strings.Add(act.Name, act.ActionType);
                Condition_Strings = new Dictionary<string, Cub.Condition>();
                foreach (Cub.Tool.Condition.Base con in Dictionary_Condition.Values)
                    Condition_Strings.Add(con.Name, con.ConditionType);

                Cub.Tool.Library.Stage_Terrain = Xml.Deserialize(typeof(Cub.Terrain[][]), "Data/Stage_Terrain.xml") as Cub.Terrain[][];
                Cub.Tool.Library.Stage_Unit = Xml.Deserialize(typeof(Cub.Class[][]), "Data/Stage_Unit.xml") as Cub.Class[][];

                Trigger = false;
            }
        }

        public static Cub.Tool.Condition.Base Get_Condition(Cub.Condition _Condition)
        {
            if (Dictionary_Condition.ContainsKey(_Condition))
                return Dictionary_Condition[_Condition];
            else
                return null;
        }

        public static Cub.Tool.Action.Base Get_Action(Cub.Action _Action)
        {
            if (Dictionary_Action.ContainsKey(_Action))
                return Dictionary_Action[_Action];
            else
                return null;
        }

        public static Cub.Tool.Character_Info Get_Character_Info(Cub.Class _Class)
        {
            if (Dictionary_Class_Info.ContainsKey(_Class))
                return Dictionary_Class_Info[_Class];
            else
                return null;
        }

        public static List<Cub.Tool.Action.Base> List_Actions()
        {
            return List_Actions(Cub.Class.None);
        }
        public static List<Cub.Tool.Action.Base> List_Actions(Cub.Class c)
        {
            List<Cub.Tool.Action.Base> r = new List<Action.Base>();
            foreach (Cub.Tool.Action.Base act in Dictionary_Action.Values)
                if (!act.SpecialAbility || Get_Character_Info(c).List_Special_Ability.Contains(act.ActionType))
                    r.Add(act);
            return r;
        }

        public static List<Cub.Tool.Condition.Base> List_Conditions()
        {
            return List_Conditions(Cub.Action.None);
        }
        public static List<Cub.Tool.Condition.Base> List_Conditions(Cub.Action a)
        {
            List<Cub.Tool.Condition.Base> r = new List<Condition.Base>();
            foreach (Cub.Tool.Condition.Base con in Dictionary_Condition.Values)
                if (Cub.Tool.Library.Get_Action(a).ValidConditions.Contains(con.ConditionGenre))
                    r.Add(con);
            return r;
        }

        public static List<Cub.Tool.Character_Info> List_Classes()
        {
            List<Cub.Tool.Character_Info> r = new List<Cub.Tool.Character_Info>();
            foreach (Cub.Tool.Character_Info cls in Dictionary_Class_Info.Values)
                r.Add(cls);
            return r;
        }

        public static Cub.Action String_Action(string str)
        {
            if (Action_Strings.ContainsKey(str))
                return Action_Strings[str];
            return Cub.Action.None;
        }

        public static Cub.Condition String_Condition(string str)
        {
            if (Condition_Strings.ContainsKey(str))
                return Condition_Strings[str];
            return Cub.Condition.None;
        }
    }


}
