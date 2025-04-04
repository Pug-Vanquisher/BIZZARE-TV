using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

namespace Achievements
{
    public class MedalsCounter : MonoBehaviour
    {
        [SerializeField] private RectTransform _view;
        [SerializeField] private TMP_Text _counterView;

        [Space]

        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;
        [SerializeField] private float _counterChangesDelay;

        private int _medalsCount;
        private float _height;
        private Tweener _openCloseTweener;
        private Coroutine _changeCounterRoutine;
        private ViewStates _viewState;

        public int Count => _medalsCount;
        public ViewStates CurrentViewState => _viewState;

        private void Awake()
        {
            _height = _view.rect.height;

            _view.gameObject.SetActive(false);
            Close();
        }

        public void SetMedals(int value)
        {
            _medalsCount = value;
            _counterView.text = _medalsCount.ToString();
        }

        public Observable<bool> ChangeCounterView(int from, int to)
        {
            var onCompleted = new Observable<bool>();

            if (_changeCounterRoutine != null)
                StopCoroutine(_changeCounterRoutine);

            _changeCounterRoutine = StartCoroutine(ChangeCounterView(from, to, onCompleted));
            return onCompleted;
        }

        public Observable<bool> Open()
        {
            var onCompleted = new Observable<bool>();
            _viewState = ViewStates.Open;
            _openCloseTweener?.Kill();

            _view.gameObject.SetActive(true);
            _openCloseTweener = _view.DOAnchorPosY(0, _openDuration)
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
            _viewState = ViewStates.Close;
            _openCloseTweener?.Kill();

            _openCloseTweener = _view.DOAnchorPosY(_height, _openDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _view.gameObject.SetActive(false);
                    onCompleted.OnNext(true);
                });

            return onCompleted;
        }

        public Observable<bool> SetState(ViewStates state)
        {
            if (state == ViewStates.Open)
                return Open();
            else if (state == ViewStates.Close)
                return Close();

            return Observable<bool>.Return(false);
        }

        private IEnumerator ChangeCounterView(int from, int to, Observable<bool> onCompleted)
        {
            var value = from;
            var step = from == to ? 0 : from < to ? 1 : -1;

            do
            {
                value += step;
                _counterView.text = value.ToString();

                yield return new WaitForSeconds(_counterChangesDelay);
            } while (value != to);

            onCompleted.OnNext(true);
        }

        public enum ViewStates
        {
            Open,
            Close
        }
    }
}
