using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public void ToggleMusic()
    {
        AudioManagerTower.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManagerTower.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManagerTower.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManagerTower.Instance.SFXVolume(sfxSlider.value);
    }

}
