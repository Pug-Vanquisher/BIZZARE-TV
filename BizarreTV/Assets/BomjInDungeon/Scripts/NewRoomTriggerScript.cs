using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class NewRoomTriggerScript : MonoBehaviour
    {
        private bool entered = false;
        public string type;
        private void OnTriggerEnter(Collider other)
        {
            if (!entered)
            {
                if (other.gameObject.tag == "Player" && other.gameObject.name == "Bomj")
                {
                    EventManager.Instance.TriggerEvent(type);
                    entered = true;
                }
            }
        }
    }
}
