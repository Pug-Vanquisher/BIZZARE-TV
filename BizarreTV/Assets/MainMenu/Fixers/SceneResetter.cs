using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    // ��� �����, ������� ����� ��������� ����� �������
    public string targetSceneName;

    void Start()
    {
        // ������� ��� ������� �� �����
        ClearScene();

        // ���������� �����
        ResetTime();

        // ��������� ������ ����� �� �����
        LoadTargetScene();
    }

    void ClearScene()
    {
        // �������� ��� ������� �� �����
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // �� ���������� ��� ������
            if (obj != gameObject)
            {
                Destroy(obj);
            }
        }
    }

    void ResetTime()
    {
        // ��������������� ���������� �����
        Time.timeScale = 1f;
    }

    void LoadTargetScene()
    {
        // ���������, ��� ������ � ��������� ����� �� �����
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // ��������� ������ �����
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("�������� ����� �� ������!");
        }
    }
}
