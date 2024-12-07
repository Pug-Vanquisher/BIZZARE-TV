using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class Coin : FallingObject
    {
        override public void DoPickingEvent()
        {
            this.remainedTime = 1;
            gm.AddScore();
        }
        override public void DoMissingEvent()
        {
            this.remainedTime = 1;
            gm.LoseLife();
        }
    }
}