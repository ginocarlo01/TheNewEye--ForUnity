using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnterLevelUI : MonoBehaviour
{

    [SerializeField] private GameObject enterLevelUI;
    [SerializeField] private GameObject bestTimeUI;

    [SerializeField] private TMP_Text enterLevelText;
    [SerializeField] private TMP_Text bestTimeText;

    private void Start()
    {
        enterLevelUI.SetActive(false);
        bestTimeUI.SetActive(false);
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        InitLevelLoad.playerEnteredDoor += SetLevelText;
        InitLevelLoad.playerEnteredDoorAlreadyCompleted += SetTimerText;
    }

    private void OnDisable()
    {
        InitLevelLoad.playerEnteredDoor -= SetLevelText;
        InitLevelLoad.playerEnteredDoorAlreadyCompleted -= SetTimerText;
    }
    #endregion

    public void SetLevelText(int level)
    {
        if(level == -1) { enterLevelUI.SetActive(false); }

        else
        {
            enterLevelUI.SetActive(true);
            enterLevelText.text = "Level " + level.ToString();
        }
    }

    public void SetTimerText(float time)
    {
        if (time == -1f) { bestTimeUI.SetActive(false); }

        else
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            bestTimeUI.SetActive(true);
            bestTimeText.text = "Best time: " + string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
