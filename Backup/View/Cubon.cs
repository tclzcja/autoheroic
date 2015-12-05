using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class CubonX
    {
        public Color32 Color { get; set; }
        public List<Position3> Position { get; set; }

        public CubonX()
        {

        }

        public CubonX(Color32 _Color, List<Position3> _Position)
        {
            this.Color = _Color;
            this.Position = _Position;
        }

        public static bool operator ==(CubonX A, CubonX B)
        {
            if (A.Color.Equals(B.Color) && A.Position.Equals(B.Position))
                return true;
            else
                return false;
        }

        public static bool operator !=(CubonX A, CubonX B)
        {
            if (A.Color.Equals(B.Color) && A.Position.Equals(B.Position))
                return false;
            else
                return true;
        }

        public override bool Equals(object obj)
        {
            if (this.Color.Equals(((CubonX)obj).Color) && this.Position.Equals(((CubonX)obj).Position))
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
