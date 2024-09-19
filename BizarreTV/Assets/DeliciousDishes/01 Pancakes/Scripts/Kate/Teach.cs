using UnityEngine;

public class Teach : MonoBehaviour
{
    GameObject[] Pages;
    public int i = 0;
    void Start()
    {
        Pages = new GameObject[transform.childCount];
        for (int j = 0; j < transform.childCount; j++)
        {
            Pages[j] = transform.GetChild(j).gameObject;
        }
        Pages[0].SetActive(true);
        for (int j = 1; j < Pages.Length; j++) Pages[j].SetActive(false);
    }
    public void Next()
    {
        i++;
        teachNext();
    }
    public void Prev()
    {
        teachPrev();
        i--;
    }
    void teachNext()
    {
        if (i <= Pages.Length)
        {
            Pages[i - 1].SetActive(false);
            if (i < Pages.Length) Pages[i].SetActive(true);
        }
    }
    void teachPrev()
    {
        if (i > 0)
        {
            Pages[i].SetActive(false);
            if (i > 0) Pages[i - 1].SetActive(true);
        }
    }
}