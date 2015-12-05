using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cub.Tool
{
    public class Tactic
    {
        public Cub.Condition C { get; private set; }
        public Cub.Action A { get; private set; }
        public bool Free = false;
        public List<object> Data { get; private set; }

        //XmlSerializer needs a non-parameter constructor for the class
        public Tactic() 
        {
            this.C = Cub.Condition.None;
            this.A = Cub.Action.None;
        }

        public Tactic(Cub.Condition _C, Cub.Action _A)
        {
            this.C = _C;
            this.A = _A;
        }

        public Tactic(Cub.Condition _C, Cub.Action _A, List<object> _D)
        {
            this.C = _C;
            this.A = _A;
            this.Data = _D;
        }

        public void SetAction(Cub.Action a)
        {
            this.A = a;
        }

        public void SetCondition(Cub.Condition c)
        {
            this.C = c;
        }
    }
}