using Rogalik.Scripts.Buff;
using Rogalik.Scripts.Buff.Base;
using Rogalik.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Rogalik.Scripts.Player.Attack;

namespace Rogalik.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public RoguelikePlayer player;
        public Animator playerAnimator;
        
        public static GameManager Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                player.AddBuff(new DamageBuff(10));
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                player.AddBuff(new SpeedBuff(2));
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                player.AddBuff(new TemporaryBuff(player, new SpeedBuff(2), 2));
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                player.RemoveBuff(player.buffs.Find(buff => buff is DamageBuff));
            }
            
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                player.AddBuff(new TemporaryBuff(player, new ImmortalBuff(), 3));
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.GetDamage(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                player.Heal(1);
            }
        }

        public void Lose()
        {
            StartCoroutine(LoseSequence());
        }

        IEnumerator LoseSequence()
        {
            Debug.Log("You lose");

            playerAnimator.SetTrigger("Die");
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            
            yield return new WaitForSeconds(3); 

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}