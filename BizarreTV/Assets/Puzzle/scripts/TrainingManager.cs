using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour
{
    public GameObject trainingPanel;  // Ссылка на ваш объект Panel, содержащий обучение
    public Button closeButton;        // Кнопка закрытия
    public Button openButton;         // Кнопка открытия
    public GameObject retryButton;
    public GameObject backButton;
    public GameObject musicButton;

    void Start()
    {
        OpenTraining();  // Показать обучение при запуске уровня
    }

    public void OpenTraining()
    {
        trainingPanel.SetActive(true);
        closeButton.onClick.AddListener(CloseTraining);
        openButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        musicButton.gameObject.SetActive(false);
    }

    public void CloseTraining()
    {
        trainingPanel.SetActive(false);
        openButton.onClick.AddListener(OpenTraining);
        openButton.gameObject.SetActive(true);
        closeButton.onClick.RemoveAllListeners();
        retryButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        musicButton.gameObject.SetActive(true);
    }

}
