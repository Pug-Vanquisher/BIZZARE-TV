using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace BID
{
    public class DebugConsole : MonoBehaviour
    {
        public DungeonLogic logic;
        public TMP_Text text;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            text.text = DungeonLogic.keystone.ToString();
        }
    }
}
