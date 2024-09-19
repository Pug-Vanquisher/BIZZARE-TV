using DG.Tweening;
using Rogalik.Scripts.Buff.Base;
using TMPro;
using UnityEngine;

namespace Rogalik.Scripts
{
    public class BuffLogger : MonoBehaviour
    {
        [SerializeField] private GameObject _buffLogPrefab;
        [SerializeField] private float _logDuration = 1f;

        public static BuffLogger Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void LogBuff(string buffInfo)
        {
            GameObject buffLog = Instantiate(_buffLogPrefab, transform);
            buffLog.GetComponentInChildren<TextMeshProUGUI>().text = buffInfo;

            AnimateBuff(buffLog);
        }

        private void AnimateBuff(GameObject buffLog)
        {
            buffLog.transform.DOScale(Vector3.zero, 0);

            buffLog.transform
                .DOScale(Vector3.one, _logDuration)
                .SetEase(Ease.OutBounce)
                .onComplete += () =>
            {
                buffLog.transform.DOScale(Vector3.one, _logDuration).onComplete += () =>
                {
                    buffLog.transform
                            .DOScale(Vector3.zero, _logDuration)
                            .SetEase(Ease.InBack)
                            .onComplete +=
                        () => { Destroy(buffLog); };
                };
            };
        }
    }
}