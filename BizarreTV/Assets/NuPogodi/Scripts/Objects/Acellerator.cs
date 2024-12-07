using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class Acellerator : FallingObject
    {
        override public void DoPickingEvent()
        {
            this.remainedTime = 1;
            gm.BuffPlayerSpeed();
        }
    }
}