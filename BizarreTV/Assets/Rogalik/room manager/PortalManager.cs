using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Для работы с UI элементами

public class PortalManager : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public GameObject entrancePortal;
        public GameObject camera;
        public Transform targetPortalPosition; // Позиция портала в следующей комнате
    }

    public List<Room> rooms; // Список всех комнат
    private List<Room> roomSequence; // Последовательность комнат для текущей игры
    private int currentRoomIndex = 0; // Индекс текущей комнаты
    public string mainMenuSceneName; // Имя сцены главного меню
    public GameObject victoryUI; // UI элемент, показывающийся при победе

    void Start()
    {
        GenerateRoomSequence();
        victoryUI.SetActive(false); // Изначально UI победы неактивен
    }

    void GenerateRoomSequence()
    {
        roomSequence = new List<Room>(rooms);

        // Сохраняем последнюю комнату и удаляем её из списка для перемешивания
        Room lastRoom = roomSequence[roomSequence.Count - 1];
        roomSequence.RemoveAt(roomSequence.Count - 1);

        // Сохраняем предпоследнюю комнату и удаляем её из списка для перемешивания
        Room penultimateRoom = roomSequence[roomSequence.Count - 1];
        roomSequence.RemoveAt(roomSequence.Count - 1);

        // Перемешиваем все комнаты, кроме первой, предпоследней и последней
        for (int i = 1; i < roomSequence.Count; i++)
        {
            Room temp = roomSequence[i];
            int randomIndex = Random.Range(1, roomSequence.Count);
            roomSequence[i] = roomSequence[randomIndex];
            roomSequence[randomIndex] = temp;
        }

        // Добавляем предпоследнюю и последнюю комнаты обратно в список
        roomSequence.Add(penultimateRoom);
        roomSequence.Add(lastRoom);

        // Отключение всех камер, кроме первой
        foreach (var room in roomSequence)
        {
            room.camera.SetActive(false);
        }
        if (roomSequence.Count > 0)
        {
            roomSequence[0].camera.SetActive(true); // Убеждаемся, что камера первой комнаты активирована
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

            // Проверяем, является ли это последней комнатой
            if (currentRoomIndex == roomSequence.Count - 1)
            {
                victoryUI.SetActive(true); // Активируем UI победы
                StartCoroutine(EndGame());
            }
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3); // Предположим, что показываем UI победы 3 секунды
        SceneManager.LoadScene(mainMenuSceneName); // Переход на сцену главного меню
    }
}