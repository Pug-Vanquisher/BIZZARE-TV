using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabsMenu : MonoBehaviour
{
    public GameObject[] Tabs;
    public Button[] Buttons;
    public Color ActiveColor, DeactiveColor;

    public void SwitchToTab(int TabId)
    {
        foreach(GameObject go in Tabs)
        {
            go.SetActive(false);
        }
        Tabs[TabId].SetActive(true);

        foreach (Button im in Buttons)
        {
            im.GetComponent<Image>().color = new Color(0.38f, 0.25f, 0.11f, 1);
        }
        Buttons[TabId].GetComponent<Image>().color = new Color(0.68f, 0.45f, 0.20f, 1);
    }
}
