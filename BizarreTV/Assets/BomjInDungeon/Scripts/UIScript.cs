using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class UIScript : MonoBehaviour
    {
        public GameObject player;
        public UIhealth health;
        void Start()
        {
            health.playerhealth = player.GetComponent<HpManager>();
        }
    }
}
