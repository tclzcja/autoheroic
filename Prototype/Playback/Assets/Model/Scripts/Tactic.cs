using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cub
{
    public class Tactic
    {
        public Type.Target T { get; private set; }
        public List<Type.Condition> C { get; private set; }
        public Type.Action A { get; private set; }

        //XmlSerializer needs a non-parameter constructor for the class
        public Tactic() 
        {
            this.T = Type.Target.None;
            this.C = new List<Type.Condition>() { Type.Condition.None };
            this.A = Type.Action.None;
        }

        public Tactic(Type.Target _T, List<Type.Condition> _C, Type.Action _A)
        {
            this.T = _T;
            this.C = _C;
            this.A = _A;
        }
    }
}