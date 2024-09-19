using System;
using UnityEngine;
using UnityEngine.Events;

namespace Rogalik.Scripts
{
    public class EventBus : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnHealthChanged = new UnityEvent();
    
        public static EventBus Instance;
    
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}
