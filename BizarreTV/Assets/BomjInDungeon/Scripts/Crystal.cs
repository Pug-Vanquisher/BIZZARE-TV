using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BID {
    public class Crystal : MonoBehaviour
    {
        private Transform crystal;
        private Vector3 startPos;
        public List<Vector3> holdPoints;
        private List<ChainScript> chains = new List<ChainScript>();
        public GameObject chainBracket;
        public ParticleSystem explosion;
        void Start()
        {
            crystal = transform.GetChild(0);
            startPos = crystal.position;
            if (DungeonLogic.keystone != "destroyed")
            {
                for (int i = 2; i < transform.childCount; i++)
                {
                    chains.Add(transform.GetChild(i).GetComponent<ChainScript>());
                    if (DungeonLogic.keystone[i - 2] == '1')
                    {
                        chains[i - 2].destroyed = true;
                    }
                }
            }
            else
            {
                for (int i = 2; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<ChainScript>().destroyed = true;
                }
                Destroy(crystal.gameObject);
            }
        }

        void Update()
        {
            if (DungeonLogic.keystone != "destroyed")
            {
                crystal.transform.position = startPos + Vector3.down * Mathf.Sin(Time.time / 2f);
                DungeonLogic.keystone = "";
                for (int i = 0; i < chains.Count; i++)
                {
                    chains[i].holdPoint = holdPoints[i] + crystal.position;
                    if (chains[i].destroyed)
                    {
                        DungeonLogic.keystone += "1";
                    }
                    else
                    {
                        DungeonLogic.keystone += "0";
                    }
                }
                if (!DungeonLogic.keystone.Contains('0'))
                {
                    EventManager.Instance.TriggerEvent("CrystalDestroyed");
                    DungeonLogic.keystone = "destroyed";
                    Instantiate(chainBracket, crystal.transform.position, Quaternion.identity);
                    explosion.Play();
                    Destroy(crystal.gameObject);
                }
            }
        }

    }

}
