using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pogodi
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] TutorialManager _tutorialManager;
        [SerializeField] List<GameObject> _tutorialPanels;

        void Awake()
        {
            _tutorialManager.OnPanelChanged += OnPanelChanged;
            _tutorialManager.OnSceneChanged += OnSceneChanged;
        }
        void OnPanelChanged(int panelNum)
        {
            _tutorialPanels[0].SetActive(panelNum == 0);
            _tutorialPanels[1].SetActive(panelNum == 1);
        }
        void OnSceneChanged()
        {
            SceneManager.LoadScene("MainNuPogodi");
        }
    }
}