using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScriptAnimations
{
    public class ZoomOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _zoomScale = 1.15f;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        private Vector3 _initialScale;
        private Tween _currentTween;
        private bool _isPointerDown;

        private void Awake()
        {
            _initialScale = _target.localScale;
        }

        private void OnDisable()
        {
            _target.localScale = _initialScale;
            _currentTween?.Kill();
        }

        private void Update()
        {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            if (Input.touchCount == 0 && _target.localScale != _initialScale)
            {
                ZoomOut();
            }
#endif
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ZoomIn();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ZoomOut();
        }

        public void ZoomIn()
        {
            _currentTween?.Kill();
            _currentTween = _target.DOScale(_initialScale * _zoomScale, _duration).SetEase(_ease);
        }

        public void ZoomOut()
        {
            _currentTween?.Kill();
            _currentTween = _target.DOScale(_initialScale, _duration).SetEase(_ease);
        }
    }
}
