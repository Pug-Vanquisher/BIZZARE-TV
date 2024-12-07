using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class MoreHealth : FallingObject
    {
        override public void DoPickingEvent()
        {
            this.remainedTime = 1;
            gm.GainLife();
        }
    }
}