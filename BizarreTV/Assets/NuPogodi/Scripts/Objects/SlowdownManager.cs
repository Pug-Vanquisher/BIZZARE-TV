using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class SlowdownManager : MonoBehaviour
    {
        [SerializeField] GameManager gm;
        public List<GameObject> affectedGameObjects = new List<GameObject>();
        public GameObject slowDownTrigger;
        void Awake()
        {
            gm.OnObjectsSpeedSlowed += OnObjectsSpeedSlowed;
        }
        void OnObjectsSpeedSlowed()
        {
            slowDownTrigger.SetActive(true);
            Timer.StartTimer(this, 10f, () =>
            {
                slowDownTrigger.SetActive(false);
                foreach (GameObject fallingObject in affectedGameObjects)
                {
                    if (fallingObject != null)
                    {
                        fallingObject.GetComponent<FallingObject>().fallingSpeed *= 2;
                    }
                }
                affectedGameObjects.Clear();
            });
        }

        void Update()
        {

        }
    }
}