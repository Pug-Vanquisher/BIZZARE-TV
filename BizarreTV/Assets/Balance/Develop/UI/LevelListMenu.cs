using UnityEngine;

namespace Balance
{
    public class LevelListMenu : MonoBehaviour
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private Transform _levelsContainer;

        private void Awake()
        {
            CreateLevels();

            LevelButton.OnClicked.AddListener(OpenLevel);
        }

        private void CreateLevels()
        {
            for (int i = 0; i < 10; i++)
            {
                LevelButton button = Instantiate(_levelButtonPrefab);
                button.transform.SetParent(_levelsContainer, false);
                button.Init(i + 1);
            }
        }

        private void OpenLevel(int number)
        {

        }
    }
}
