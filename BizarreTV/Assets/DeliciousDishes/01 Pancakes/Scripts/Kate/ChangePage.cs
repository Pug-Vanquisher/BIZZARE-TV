using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    public GameObject Page;
    public void Change(GameObject page){
        if (Page != page) {
        Page.SetActive(false);
        page.SetActive(true);
        Page = page;
        }
    }
}
