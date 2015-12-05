using UnityEngine;
using System.Collections.Generic;
using System;

namespace AM
{
    [Serializable]
    public class Tactic
    {
        public AM.Type.TTarget Target { get; private set; }
        public List<AM.Type.TSituation> Situation { get; private set; }
        public AM.Type.TAction Action { get; private set; }
        public int Priority { get; private set; }

        public Tactic(AM.Type.TTarget T, List<AM.Type.TSituation> S, AM.Type.TAction A, int P)
        {
            this.Target = T;
            this.Situation = S;
            this.Action = A;
            this.Priority = P;
        }

    }
}

