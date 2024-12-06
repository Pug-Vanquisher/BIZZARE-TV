using UnityEngine;
using TMPro; // ��� ������������� TextMeshPro

namespace Rogalik
{
    public class Chest : MonoBehaviour
    {
        public Sprite openSprite; // ������ ��������� �������
        public Sprite closedSprite; // ������ ��������� �������
        public GameObject[] buffItems; // ������ ������
        public Transform spawnPoint; // ����� ������ �������
        public TextMeshProUGUI pressEText; // ����� � ����������

        private SpriteRenderer spriteRenderer;
        private bool isOpen = false;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = closedSprite;
            pressEText.gameObject.SetActive(false); // �������� ����� ��� ������
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
            spriteRenderer.sprite = openSprite; // ������ ������ �� �������� ������
            pressEText.gameObject.SetActive(false); // �������� �����

            int randomIndex = Random.Range(0, buffItems.Length);
            Instantiate(buffItems[randomIndex], spawnPoint.position, Quaternion.identity); // ������ ��������� ����
        }
    }

}