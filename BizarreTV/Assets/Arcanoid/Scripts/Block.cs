using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private int hitsLeft = 3;

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
        /*float r, g, b;
        r = Random.Range(0f, 1f);
        g = Random.Range(0f, 1f);
        b = Random.Range(0f, 1f);*/
        Color nextColor = GetComponent<SpriteRenderer>().color;
        nextColor.r /= 2;
        nextColor.g /= 2;
        nextColor.b /= 2;
        GetComponent<SpriteRenderer>().color = nextColor;
    }
}
