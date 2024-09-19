using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChannelSwitcher : MonoBehaviour
{
    private int _currentChannelNum;

    [SerializeField] private Image _tvImage;
    [SerializeField] private List<ChannelData> _channelsList = new();
    [SerializeField] private TextMeshProUGUI _channelNameText;

    private void Start()
    {
        _currentChannelNum = 0;
        SwitchChannel(_currentChannelNum);
    }

    public void GoToNextChannel()
    {
        _currentChannelNum = _currentChannelNum + 1 == _channelsList.Count ? 0 : _currentChannelNum + 1;
        SwitchChannel(_currentChannelNum);
    }

    public void GoToPreviousChannel()
    {
        _currentChannelNum = _currentChannelNum - 1 < 0 ? _channelsList.Count - 1 : _currentChannelNum - 1;
        SwitchChannel(_currentChannelNum);
    }

    public void GoToCurrentMinigame()
    {
        SceneManager.LoadScene(_channelsList[_currentChannelNum].sceneName);
    }

    public void GoToMinigameByName()
    {
        SceneManager.LoadScene(_channelsList[_currentChannelNum].sceneName);
    }

    private void SwitchChannel(int needChannelNumber)
    {
        _tvImage.sprite = _channelsList[needChannelNumber].channelSprite;
        _channelNameText.text = _channelsList[needChannelNumber].channelName;
    }
}


[Serializable]
public sealed class ChannelData
{
    public Sprite channelSprite;
    public string sceneName;
    public string channelName;
}
