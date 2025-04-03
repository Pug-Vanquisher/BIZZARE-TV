using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Achievements
{
	public class AchievementsMenu : MonoBehaviour
    {
        [SerializeField] private Transform _view;
        [SerializeField] private CanvasGroup _fadeView;
        [SerializeField] private RectTransform _blocksContainer;
        [SerializeField] private AchievementBlock _blockPrefab;

        [Space]

        [SerializeField] private List<Button> _closeButtons;

        [Space]

        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;
        [SerializeField] private float _fadeDuration;

        private Tweener _openCloseTweener;
        private Tweener _fadeTweener;

        private void Awake()
        {
            _closeButtons.ForEach(b => b.onClick.AddListener(() => Close()));

            _view.transform.localScale = new Vector2(1, 0);
            _view.gameObject.SetActive(false);
            _fadeView.gameObject.SetActive(false);
        }

        public Observable<bool> Open()
        {
            var onCompleted = new Observable<bool>();

            _openCloseTweener?.Kill();
            _view.gameObject.SetActive(true);
            _openCloseTweener = _view.DOScaleY(1, _openDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                _blocksContainer.sizeDelta = new Vector2(_blocksContainer.sizeDelta.x, 0);
                onCompleted.OnNext(true);
            });

            FadeScreen(true);

            return onCompleted;
        }

        public Observable<bool> Close()
        {
            var onCompleted = new Observable<bool>();

            _openCloseTweener?.Kill();
            _openCloseTweener = _view.DOScaleY(0, _closeDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                _view.gameObject.SetActive(false);
                onCompleted.OnNext(true);
            });

            FadeScreen(false);

            return onCompleted;
        }

        public void CreateBlocks(IEnumerable<AchievementConfig> configs)
        {
            ClearBlocksContainer();

            foreach (var config in configs)
            {
                CreateBlock(config);
            }
        }

        private void CreateBlock(AchievementConfig config)
        {
            var block = Instantiate(_blockPrefab);
            block.Init(config);
            block.transform.SetParent(_blocksContainer, false);
        }

        private void ClearBlocksContainer()
        {
            var childCount = _blocksContainer.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(_blocksContainer.GetChild(i).gameObject);
            }
        }

        private void FadeScreen(bool value)
        {
            var alpha = value ? 1 : 0;

            if (value)
                _fadeView.gameObject.SetActive(true);

            _fadeTweener?.Kill();
            _fadeTweener = _fadeView.DOFade(alpha, _fadeDuration).OnComplete(() =>
            {
                if (!value)
                    _fadeView.gameObject.SetActive(false);
            });
            
        }
    }
}