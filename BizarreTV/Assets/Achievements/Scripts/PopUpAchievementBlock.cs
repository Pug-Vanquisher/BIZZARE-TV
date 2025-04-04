using DG.Tweening;
using UnityEngine;
using Utils;

namespace Achievements
{
    public class PopUpAchievementBlock : AchievementBlock
    {
        [Space]

        [SerializeField] private RectTransform _view;

        [Space]

        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;
        [SerializeField] private float _yPositionAtOpenig;

        private Tweener _openCloseTweener;

        private void Awake()
        {
            _view.gameObject.SetActive(false);
            Close();
        }

        public new void Init(AchievementConfig config = null, float progress = 0f)
        {
            throw new System.Exception("PopUpAchievementBlock cannot be initialized");
        }

        public Observable<bool> Open(AchievementConfig config)
        {
            SetView(config);

            var onCompleted = new Observable<bool>();
            _openCloseTweener?.Kill();

            _view.gameObject.SetActive(true);
            _openCloseTweener = _view.DOAnchorPosY(_yPositionAtOpenig, _openDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    onCompleted.OnNext(true);
                });

            return onCompleted;
        }

        public Observable<bool> Close()
        {
            var onCompleted = new Observable<bool>();
            _openCloseTweener?.Kill();

            _openCloseTweener = _view.DOAnchorPosY(0, _openDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _view.gameObject.SetActive(false);
                    onCompleted.OnNext(true);
                });

            return onCompleted;
        }

        private void SetView(AchievementConfig config)
        {
            _iconView.sprite = config.Icon;
            _titleView.text = config.Title;
        }
    }
}
