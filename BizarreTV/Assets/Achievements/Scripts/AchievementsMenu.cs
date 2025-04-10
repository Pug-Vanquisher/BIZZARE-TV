using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public Observable<bool> OnCloseButtonClicked = new();

        private void Awake()
        {
            _closeButtons.ForEach(b => b.onClick.AddListener(() => OnCloseButtonClicked.OnNext(true)));

            _view.gameObject.SetActive(false);
            _fadeView.gameObject.SetActive(false);
            Close();
        }

        public Observable<bool> Open()
        {
            var onCompleted = new Observable<bool>();

            _openCloseTweener?.Kill();
            _view.gameObject.SetActive(true);
            _openCloseTweener = _view.DOScaleY(1, _openDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
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
            _openCloseTweener = _view.DOScaleY(0, _closeDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _view.gameObject.SetActive(false);
                    onCompleted.OnNext(true);
                });

            FadeScreen(false);

            return onCompleted;
        }

        public void CreateBlocks(AchievementProjects project, IEnumerable<AchievementConfig> configs, 
                                 AchievementsStateProxy stateProxy)
        {
            ClearBlocksContainer();

            var sortedConfigs = new List<AchievementConfig>(configs)
                .OrderBy(c => !stateProxy.IsObtained(AchievementsMapper.GetId(project, c.Id)));

            foreach (var config in sortedConfigs)
            {
                var mappedId = AchievementsMapper.GetId(project, config.Id);
                var progress = stateProxy.GetProgress(mappedId);

                CreateBlock(config, progress);
            }
        }

        private void CreateBlock(AchievementConfig config, float progress)
        {
            var block = Instantiate(_blockPrefab);
            block.Init(config, progress);
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