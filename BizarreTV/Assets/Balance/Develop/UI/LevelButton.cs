using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Balance
{
    public class LevelButton : MonoBehaviour
    {
        private int _number;
        [SerializeField] private TMP_Text _numberView;

        [Space]

        [SerializeField] private GameObject _lockView;
        private bool _isLock;

        public static UnityEvent<int> OnClicked = new();

        private void Awake()
        {
            _isLock = true;
        }

        public void Init(int number)
        {
            _number = number;
            _numberView.text = _number.ToString();
        }

        public void Lock()
        {
            _isLock = true;
            _lockView.SetActive(true);
        }

        public void Unlock()
        {
            _isLock = false;
            _lockView.SetActive(false);
        }

        public void OnClick()
        {
            if (_isLock) return;

            OnClicked.Invoke(_number);
        }
    }
}
