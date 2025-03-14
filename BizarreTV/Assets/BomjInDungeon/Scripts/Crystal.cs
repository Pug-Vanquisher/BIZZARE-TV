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
        public DungeonLogic dlog;
        void Start()
        {
            dlog = GameObject.FindGameObjectWithTag("Brain").GetComponent<DungeonLogic>();
            crystal = transform.GetChild(0);
            startPos = crystal.position;
            if (dlog.keystone != "destroyed")
            {
                for (int i = 2; i < transform.childCount; i++)
                {
                    chains.Add(transform.GetChild(i).GetComponent<ChainScript>());
                    if (dlog.keystone[i - 2] == '1')
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
            if (dlog.keystone != "destroyed")
            {
                crystal.transform.position = startPos + Vector3.down * Mathf.Sin(Time.time / 2f);
                dlog.keystone = "";
                for (int i = 0; i < chains.Count; i++)
                {
                    chains[i].holdPoint = holdPoints[i] + crystal.position;
                    if (chains[i].destroyed)
                    {
                        dlog.keystone += "1";
                    }
                    else
                    {
                        dlog.keystone += "0";
                    }
                }
                if (!dlog.keystone.Contains('0'))
                {
                    EventManager.Instance.TriggerEvent("CrystalDestroyed");
                    dlog.keystone = "destroyed";
                    Instantiate(chainBracket, crystal.transform.position, Quaternion.identity);
                    explosion.Play();
                    Destroy(crystal.gameObject);
                }
            }
        }

    }

}
