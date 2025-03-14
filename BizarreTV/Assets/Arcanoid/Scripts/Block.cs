using UnityEngine;

namespace Arcanoid
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private int hitsLeft;

        [SerializeField]
        GameObject buff;

        gameMAnager manager;

        public void Hit()
        {
            hitsLeft--;
            if (hitsLeft < 1)
            {
                Instantiate(buff, transform.position, transform.rotation);
                manager = GameObject.Find("GameObject").GetComponent<gameMAnager>();
                manager.playDestroySound();
                manager.checkWin();
                Destroy(gameObject);
            }
            
            Color nextColor = GetComponent<SpriteRenderer>().color;
            nextColor.r /= 2;
            nextColor.g /= 2;
            nextColor.b /= 2;
            GetComponent<SpriteRenderer>().color = nextColor;
        }
    }

}