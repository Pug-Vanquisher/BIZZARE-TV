using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BID
{
    public class Timer : MonoBehaviour
    {

        public float lifetime;
        private float gametime;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            var text = gameObject.GetComponent<TextMeshProUGUI>();

            if (Mathf.Floor(lifetime / 60) == lifetime / 60) { text.text = (Mathf.Floor(lifetime / 60)).ToString() + ":0" + (lifetime % 60).ToString(); }
            else if (lifetime % 60 >= 10) { text.text = "0" + (Mathf.Floor(lifetime / 60)).ToString() + ":" + (lifetime % 60).ToString(); }
            else { text.text = "0" + (Mathf.Floor(lifetime / 60)).ToString() + ":0" + (lifetime % 60).ToString(); }

            gametime += 1 * Time.deltaTime;
            if (gametime >= 1)
            {
                lifetime -= 1;
                gametime = 0;
            }
            if (lifetime < 300) { text.color = Color.yellow; }
            if (lifetime < 120) { text.color = Color.red; }
        }
    }
}
