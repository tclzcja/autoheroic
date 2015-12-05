using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;

namespace Cub
{
    public static class Xml
    {
        public static void Serialize(object Object, string Path)
        {
            XmlSerializer XS = new XmlSerializer(Object.GetType());
            StreamWriter SW = new StreamWriter(Path);
            XS.Serialize(SW, Object);
            SW.Close();
        }

        public static object Deserialize(System.Type Type, string Path)
        {
            XmlSerializer XS = new XmlSerializer(Type);
            StreamReader SR = new StreamReader(Path);
            object O = XS.Deserialize(SR);
            SR.Close();
            return O;
        }
    }
}