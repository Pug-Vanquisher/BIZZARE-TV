using UnityEngine;
using TMPro; // Для использования TextMeshPro

namespace Rogalik
{
    public class Chest : MonoBehaviour
    {
        public Sprite openSprite; // Спрайт открытого сундука
        public Sprite closedSprite; // Спрайт закрытого сундука
        public GameObject[] buffItems; // Массив баффов
        public Transform spawnPoint; // Точка спавна объекта
        public TextMeshProUGUI pressEText; // Текст с подсказкой

        private SpriteRenderer spriteRenderer;
        private bool isOpen = false;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = closedSprite;
            pressEText.gameObject.SetActive(false); // Скрываем текст при старте
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !isOpen)
            {
                pressEText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                pressEText.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && pressEText.gameObject.activeInHierarchy && !isOpen)
            {
                OpenChest();
            }
        }

        void OpenChest()
        {
            SoundManager.Instance.PlaySound(6);
            isOpen = true;
            spriteRenderer.sprite = openSprite; // Меняем спрайт на открытый сундук
            pressEText.gameObject.SetActive(false); // Скрываем текст

            int randomIndex = Random.Range(0, buffItems.Length);
            Instantiate(buffItems[randomIndex], spawnPoint.position, Quaternion.identity); // Создаём случайный бафф
        }
    }

}