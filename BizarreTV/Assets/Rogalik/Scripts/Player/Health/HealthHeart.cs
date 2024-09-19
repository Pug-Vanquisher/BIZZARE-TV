using UnityEngine;
using UnityEngine.UI;

namespace Rogalik.Scripts.Player.Health
{
    public class HealthHeart : MonoBehaviour
    {
        public enum HeartState
        {
            Full,
            Half,
            Empty
        }

        public HeartState heartState;

        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetHeart(HeartState state)
        {
            switch (state)
            {
                case HeartState.Full:
                    _image.sprite = fullHeart;
                    break;
                case HeartState.Half:
                    _image.sprite = halfHeart;
                    break;
                case HeartState.Empty:
                    _image.sprite = emptyHeart;
                    break;
            }

            heartState = state;
        }
    }
}