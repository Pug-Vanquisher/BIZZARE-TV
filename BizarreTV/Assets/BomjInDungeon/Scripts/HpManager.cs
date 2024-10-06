using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class HpManager : MonoBehaviour
    {
        public int maxhp;
        public int currenthp;

        void Start()
        {
            currenthp = maxhp;
        }

        private void Update()
        {
            if (currenthp <= 0) 
            {
                if (gameObject.name != "Bomj") { Destroy(gameObject); }

                else
                {
                    Debug.Log("я здох :(");
                    currenthp = maxhp;
                }
            }
        }
    }
}
