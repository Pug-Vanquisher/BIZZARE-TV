using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpViewer : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject Page;
    // public GameObject[] BookMarks;
    void Start()
    {
        // Page.SetActive(false);
        helpPanel.SetActive(false);
        // for (int i = 0; i < BookMarks.Length; i++){
        //     BookMarks[i].SetActive(false);
        // }
    }
    public void ToggleHelpPanel()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
        // Page.SetActive(!Page.activeSelf);
        // for (int i = 0; i < BookMarks.Length; i++){
        //     BookMarks[i].SetActive(!BookMarks[i].activeSelf);
        // }
    }
    public void ChangePage(GameObject page)
    {
        Page = page;
    }
}
