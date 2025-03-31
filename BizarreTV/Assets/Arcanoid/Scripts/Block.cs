using UnityEngine;

namespace Arcanoid
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private int hitsLeft;

        [SerializeField]
        GameObject buff;
        public SpriteRenderer dsprite;
        public Sprite damaged;
        public Sprite bdamaged;

        gameMAnager manager;

        public void Hit()
        {
            Debug.Log(gameObject.name);
            hitsLeft--;
            if (hitsLeft < 1)
            {
                if (!gameObject.name.Contains("Dirt Block")) Instantiate(buff, transform.position, transform.rotation).GetComponent<plusSize>().type = gameObject.name;
                manager = GameObject.Find("GameObject").GetComponent<gameMAnager>();
                manager.playDestroySound();
                manager.checkWin();
                Destroy(gameObject);
            }
            
            if (hitsLeft == 2) { dsprite.sprite = damaged; }
            if (hitsLeft == 1) { dsprite.sprite = bdamaged; }
        }
    }

}