using UnityEngine;
using TMPro;

public class RecipeViewer : MonoBehaviour
{
    public GameObject receptPanel;
    void Start()
    {
        receptPanel.SetActive(false);
    }
    public void ToggleRecipePanel()
    {
        receptPanel.SetActive(!receptPanel.activeSelf);
    }
}
