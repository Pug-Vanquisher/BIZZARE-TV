using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogalik.Scripts.Buff
{
    public class TempBuffLogger : MonoBehaviour
    {
        [SerializeField] private GameObject _buffTextPrefab;
        
        public static TempBuffLogger Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        
        public void LogBuff(string buffInfo, float duration)
        {
            GameObject buffText = Instantiate(_buffTextPrefab, transform);
            buffText.GetComponentInChildren<TextMeshProUGUI>().text = buffInfo;
            
            buffText.GetComponent<TempBuffUI>().Init(duration);
        }

    }
}