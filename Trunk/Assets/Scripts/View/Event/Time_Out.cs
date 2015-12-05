using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Time_Out : Base
    {
        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.NarratorController.DisplayText(Desc, 2.0f);
            return 3.0F;
        }
    }
}
