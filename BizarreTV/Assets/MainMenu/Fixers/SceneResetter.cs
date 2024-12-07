using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    // Имя сцены, которую нужно загрузить после очистки
    public string targetSceneName;

    void Start()
    {
        // Очищаем все объекты на сцене
        ClearScene();

        // Сбрасываем время
        ResetTime();

        // Загружаем нужную сцену по имени
        LoadTargetScene();
    }

    void ClearScene()
    {
        // Получаем все объекты на сцене
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Не уничтожаем сам скрипт
            if (obj != gameObject)
            {
                Destroy(obj);
            }
        }
    }

    void ResetTime()
    {
        // Восстанавливаем нормальное время
        Time.timeScale = 1f;
    }

    void LoadTargetScene()
    {
        // Проверяем, что строка с названием сцены не пуста
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // Загружаем нужную сцену
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("Название сцены не задано!");
        }
    }
}
