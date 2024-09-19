using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenManager : MonoBehaviour
{
    public void SwitchScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
