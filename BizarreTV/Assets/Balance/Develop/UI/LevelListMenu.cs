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
            int lastCompletedLevel = DIContainer.Resolve<Storage>().GameData.LastCompletedLevel;

            for (int i = 0; i < 10; i++)
            {
                LevelButton button = Instantiate(_levelButtonPrefab);
                button.transform.SetParent(_levelsContainer, false);

                int number = i + 1;
                button.Init(number);

                if (number > lastCompletedLevel + 1)
                    button.Lock();
                else
                    button.Unlock();
            }
        }

        private void OpenLevel(int number)
        {
            DIContainer.Resolve<LevelTracker>().SetCurrentLevelNumber(number);
            DIContainer.Resolve<SceneLoader>().LoadGameplay();
        }
    }
}
