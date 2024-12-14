using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class Furball : FallingObject
    {
        override public void DoPickingEvent()
        {
            Debug.Log("A");
            this.remainedTime = 1;
            gm.LoseLife();
        }
        override public void DoMissingEvent()
        {
            Debug.Log("B");
            this.remainedTime = 1;
        }
    }
}