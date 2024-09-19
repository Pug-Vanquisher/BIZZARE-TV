using UnityEngine;

namespace Rogalik.Scripts.Player.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RoguelikePlayer player;
        [SerializeField] private GameObject heartPrefab;

        private void Start()
        {
            EventBus.Instance.OnHealthChanged.AddListener(SetHealth);
        }

        private void SetHealth()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            int playerHealth = player.currentStats.health;
            int playerMaxHealth = player.currentStats.maxHealth;

            for (int i = 0; i < playerMaxHealth; i+=2)
            {
                GameObject heart = Instantiate(heartPrefab, transform);
                HealthHeart healthHeart = heart.GetComponent<HealthHeart>();

                if(i+1 == playerHealth)
                    healthHeart.SetHeart(HealthHeart.HeartState.Half);
                else if (i < playerHealth)
                    healthHeart.SetHeart(HealthHeart.HeartState.Full);
                else 
                    healthHeart.SetHeart(HealthHeart.HeartState.Empty);
            }
        }
    }
}