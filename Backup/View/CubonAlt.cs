using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class CubonAlt
    {
        public CubeType CubeType { get; set; }
        public List<Position3> Position { get; set; }

        public CubonAlt()
        {

        }

        public CubonAlt(CubeType _Color, List<Position3> _Position)
        {
            this.CubeType = _Color;
            this.Position = _Position;
        }

        public static bool operator ==(CubonAlt A, CubonAlt B)
        {
            if (A.CubeType.Equals(B.CubeType) && A.Position.Equals(B.Position))
                return true;
            else
                return false;
        }

        public static bool operator !=(CubonAlt A, CubonAlt B)
        {
            if (A.CubeType.Equals(B.CubeType) && A.Position.Equals(B.Position))
                return false;
            else
                return true;
        }

        public override bool Equals(object obj)
        {
            if (this.CubeType.Equals(((CubonAlt)obj).CubeType) && this.Position.Equals(((CubonAlt)obj).Position))
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
