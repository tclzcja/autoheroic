using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub
{
    public static class Library
    {
        private static bool Trigger = true;

        public static Cub.Character Class_None { get; private set; }
        public static Cub.Character Class_Knight { get; private set; }
        public static Cub.Character Class_Archer { get; private set; }
        public static Cub.Character Class_Mage { get; private set; }

        public static Cub.Type.Terrain[][] Stage_Terrain { get; set; }
        public static Cub.Type.Class[][] Stage_Unit { get; set; }

        public static void Initialization()
        {
            if (Trigger)
            {
                Cub.Library.Class_None = Cub.Xml.Deserialize(typeof(Cub.Character), "Data/Class_None.xml") as Cub.Character;
                Cub.Library.Class_Knight = Cub.Xml.Deserialize(typeof(Cub.Character), "Data/Class_Knight.xml") as Cub.Character;
                Cub.Library.Class_Archer = Cub.Xml.Deserialize(typeof(Cub.Character), "Data/Class_Archer.xml") as Cub.Character;
                Cub.Library.Class_Mage = Cub.Xml.Deserialize(typeof(Cub.Character), "Data/Class_Mage.xml") as Cub.Character;

                Cub.Library.Stage_Terrain = Xml.Deserialize(typeof(Type.Terrain[][]), "Data/Stage_Terrain.xml") as Cub.Type.Terrain[][];
                Cub.Library.Stage_Unit = Xml.Deserialize(typeof(Type.Class[][]), "Data/Stage_Unit.xml") as Type.Class[][];      
                Trigger = false;
            }

        }
    }


}
