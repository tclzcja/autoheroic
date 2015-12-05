using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Cubon
    {
        public Colour Colour { get; set; }
        public List<Vector3> Position { get; set; }

        public Cubon()
        {

        }

        public Cubon(Colour _Color, List<Vector3> _Position)
        {
            this.Colour = _Color;
            this.Position = _Position;
        }

        public static bool operator ==(Cubon A, Cubon B)
        {
            if (A.Colour.Equals(B.Colour) && A.Position.Equals(B.Position))
                return true;
            else
                return false;
        }

        public static bool operator !=(Cubon A, Cubon B)
        {
            if (A.Colour.Equals(B.Colour) && A.Position.Equals(B.Position))
                return false;
            else
                return true;
        }

        public override bool Equals(object obj)
        {
            if (this.Colour.Equals(((Cubon)obj).Colour) && this.Position.Equals(((Cubon)obj).Position))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
