using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Rogalik.Scripts.Buff
{
    public class TempBuffUI : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        public void Init(float duration)
        {
            _slider.value = 1;

            AnimateBuff(duration);
        }

        private void AnimateBuff(float duration)
        {
            transform.DOScale(Vector3.zero, 0);
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

            _slider.DOValue(0, duration).SetEase(Ease.Linear).onComplete += () =>
            {
                transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).onComplete +=
                    () => { Destroy(gameObject); };
            };
        }
    }
}