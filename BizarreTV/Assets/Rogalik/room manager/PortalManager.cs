using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��� ������ � UI ����������

public class PortalManager : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public GameObject entrancePortal;
        public GameObject camera;
        public Transform targetPortalPosition; // ������� ������� � ��������� �������
    }

    public List<Room> rooms; // ������ ���� ������
    private List<Room> roomSequence; // ������������������ ������ ��� ������� ����
    private int currentRoomIndex = 0; // ������ ������� �������
    public string mainMenuSceneName; // ��� ����� �������� ����
    public GameObject victoryUI; // UI �������, �������������� ��� ������

    void Start()
    {
        GenerateRoomSequence();
        victoryUI.SetActive(false); // ���������� UI ������ ���������
    }

    void GenerateRoomSequence()
    {
        roomSequence = new List<Room>(rooms);

        // ��������� ��������� ������� � ������� � �� ������ ��� �������������
        Room lastRoom = roomSequence[roomSequence.Count - 1];
        roomSequence.RemoveAt(roomSequence.Count - 1);

        // ��������� ������������� ������� � ������� � �� ������ ��� �������������
        Room penultimateRoom = roomSequence[roomSequence.Count - 1];
        roomSequence.RemoveAt(roomSequence.Count - 1);

        // ������������ ��� �������, ����� ������, ������������� � ���������
        for (int i = 1; i < roomSequence.Count; i++)
        {
            Room temp = roomSequence[i];
            int randomIndex = Random.Range(1, roomSequence.Count);
            roomSequence[i] = roomSequence[randomIndex];
            roomSequence[randomIndex] = temp;
        }

        // ��������� ������������� � ��������� ������� ������� � ������
        roomSequence.Add(penultimateRoom);
        roomSequence.Add(lastRoom);

        // ���������� ���� �����, ����� ������
        foreach (var room in roomSequence)
        {
            room.camera.SetActive(false);
        }
        if (roomSequence.Count > 0)
        {
            roomSequence[0].camera.SetActive(true); // ����������, ��� ������ ������ ������� ������������
        }
    }

    public void MoveToNextRoom()
    {
        currentRoomIndex++;

        if (currentRoomIndex >= 0 && currentRoomIndex < roomSequence.Count)
        {
            Room currentRoom = roomSequence[currentRoomIndex];
            currentRoom.camera.SetActive(true);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = currentRoom.targetPortalPosition.position;

            if (currentRoomIndex > 0)
            {
                roomSequence[currentRoomIndex - 1].camera.SetActive(false);
            }

            // ���������, �������� �� ��� ��������� ��������
            if (currentRoomIndex == roomSequence.Count - 1)
            {
                victoryUI.SetActive(true); // ���������� UI ������
                StartCoroutine(EndGame());
            }
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3); // �����������, ��� ���������� UI ������ 3 �������
        SceneManager.LoadScene(mainMenuSceneName); // ������� �� ����� �������� ����
    }
}