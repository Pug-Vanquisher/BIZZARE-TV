using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PotionScript : MonoBehaviour
    {
        private DungeonLogic dlog;
        private HpManager playerhealth;

        public string room = "";

        private void Start()
        {
            dlog = GameObject.FindGameObjectWithTag("Brain").GetComponent<DungeonLogic>();

            if (room == "keystone" && dlog.keyStonePotionUsed)
            {
                Destroy(gameObject);
            }
            if (room == "boss" && dlog.bossPotionUsed)
            {
                Destroy(gameObject);
            }
        }

        public void TakeAPotion()
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.gameObject.name != "HitCollider")
                {
                    playerhealth = player.GetComponent<HpManager>();
                }
            }


            playerhealth.TakeDamage(-(playerhealth.maxhp - playerhealth.currenthp));

            if (room == "keystone")
            {
                dlog.keyStonePotionUsed = true;
            }
            if (room == "boss")
            {
                dlog.bossPotionUsed = true;
            }

            Destroy(gameObject);
        }
    }

}