using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    [CreateAssetMenu(menuName = "Managers/Tutorial Manager")]
    public class TutorialManager : ScriptableObject
    {
        public delegate void PanelCallBack(int screenNum);
        public PanelCallBack OnPanelChanged;
        public void ChangePanel(int screenNum)
        {
            OnPanelChanged.Invoke(screenNum);
        }

        public delegate void SceneCallBack();
        public SceneCallBack OnSceneChanged;
        public void ChangeScene()
        {
            OnSceneChanged.Invoke();
        }
    }
}