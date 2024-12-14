using System.Collections;
using UnityEngine;

namespace Balance
{
    [RequireComponent(typeof(Animator))]
    public class VanishingPlatform : MonoBehaviour
    {
        [SerializeField] private float _timeBeforVanish;
        [SerializeField] private float _cooldown;

        private bool _canVanish;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _canVanish = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && _canVanish)
                StartCoroutine(Vanish());
        }

        private IEnumerator Vanish()
        {
            _canVanish = false;

            yield return new WaitForSeconds(_timeBeforVanish);

            _animator.SetTrigger("Vanish");

            yield return new WaitForSeconds(_cooldown);

            _animator.SetTrigger("Appear");

            _canVanish = true;
        }
    }
}
