using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Cubon
    {
        public CubeType CubeType { get; set; }
        public Position3 Position { get; set; }

        public Cubon()
        {

        }

        public Cubon(CubeType _Color, Position3 _Position)
        {
            this.CubeType = _Color;
            this.Position = _Position;
        }

        public static bool operator ==(Cubon A, Cubon B)
        {
            if (A.CubeType.Equals(B.CubeType) && A.Position.Equals(B.Position))
                return true;
            else
                return false;
        }

        public static bool operator !=(Cubon A, Cubon B)
        {
            if (A.CubeType.Equals(B.CubeType) && A.Position.Equals(B.Position))
                return false;
            else
                return true;
        }

        public override bool Equals(object obj)
        {
            if (this.CubeType.Equals(((Cubon)obj).CubeType) && this.Position.Equals(((Cubon)obj).Position))
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
