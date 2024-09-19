using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] enemies; // ������ ������ � �������
    public GameObject exitPortal; // �������� ������
    private bool isActive = false; // ����, ����������� �� ��������� �������

    private void Start()
    {
        exitPortal.SetActive(false); // ���������� �������� ������ ���������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true; // �������� ������� ��� ��������������
            StartCoroutine(ActivateEnemiesWithDelay(1.0f)); // ���������� ������ � ���������
            StartCoroutine(CheckEnemies()); // �������� �������� ��������� ������
        }
    }

    IEnumerator ActivateEnemiesWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� �������� ��������
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true); // ���������� ������� �����
        }
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f); // ��������� �������� ����� ���������

        while (true)
        {
            bool allEnemiesDefeated = true;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null && enemy.activeSelf)
                {
                    allEnemiesDefeated = false; // ���� ���� �� ���� ���� �������, ������� ��� �� �������
                    break;
                }
            }

            if (allEnemiesDefeated)
            {
                exitPortal.SetActive(true); // ���������� �������� ������, ���� ��� ����� ����������
                DestroyEnemies(); // ���������� ������ ��� ������� �������
                break; // ������� �� �����
            }

            yield return new WaitForSeconds(0.5f); // ��������� ����� ����� ��������� �����
        }
    }

    void DestroyEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // ���������� ����� ��� ������� ������
        }
    }
}