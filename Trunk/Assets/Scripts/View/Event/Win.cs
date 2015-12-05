using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View.Event
{
    public class Win : Base
    {
        //_Data[0] is a list of GUIDs of the winners, [1] is a list of losers.

        public override float Process(List<object> _Data, string Desc)
        {
            Cub.View.NarratorController.DisplayText(Desc, 2.0f);
            return 3.0F;
        }
    }
}
