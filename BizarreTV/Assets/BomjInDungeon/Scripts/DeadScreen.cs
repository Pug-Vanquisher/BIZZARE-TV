using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace BID
{
    public class DeadScreen : MonoBehaviour
    {
        public string words;

        public Image Screen;
        public TMP_Text death;
        public Animator animator;
        public Image ButtonImage;
        public GameObject button;


        public float duration;
        void Start()
        {
            EventManager.Instance.Subscribe("GameRestarted", Spawned);

            Screen.color = new Color(0, 0, 0, 0);
            death.text = "";
            StartCoroutine("_fadein");
            StartCoroutine("_texting");
        }
        public void Retry()
        {
            EventManager.Instance.TriggerEvent("Retry");
        }
        public void Spawned()
        {
            Debug.Log("fadeout");
            death.text = "";
            button.SetActive(false);
            StartCoroutine("_fadeout");
        }
        IEnumerator _fadein()
        {
            for (float i = 0; i < 100; i++)
            {
                Screen.color = new Color(0, 0, 0, i/100f);
                yield return new WaitForSeconds(duration/100f);
            }
            Screen.color = new Color(0, 0, 0, 1f);
            yield return 0;
        }
        IEnumerator _fadeout()
        {
            for(int i = 100; i > 0; i-=1)
            {
                Screen.color = new Color(0, 0, 0, i / 100f);
                yield return new WaitForSeconds(duration / 100f);
            }
            Destroy(gameObject);
            yield return 0;
        }
        IEnumerator _texting()
        {
            string str = words;
            foreach (char ch in str)
            {
                death.text += ch;
                yield return new WaitForSeconds(duration / str.Length);
            }
            ButtonImage.color = new Color(1, 1, 1, 1);
            animator.Play("BAppear");
            yield return new WaitForSeconds(0.3f);
            ButtonImage.gameObject.SetActive(false); 
            button.SetActive(true);
            yield return 0;
        }
    }

}