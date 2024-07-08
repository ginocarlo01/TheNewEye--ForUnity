using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    float timer = 0;

    [SerializeField]
    UIManager uiManager;

    public static Action<float> saveTimeDataAction;
    public static Action<float> saveCompleteLevelTimeDataAction;

    public static Action finishLevelAction;

    public bool paused;

    private void Start()
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        timer = JsonReadWriteSystem.INSTANCE.GetCurrentLevelTime();
    }

    void Update()
    {
        if (paused) { return; }

        timer += Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

        string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        uiManager.timerCounter.text = formattedTime;
    }

    public void PauseStartGame()
    {
        paused = !paused;
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        CheckPoint.saveCheckPointAction += SaveTimeData;
        LoadNextLevel.finishLevelAction += SaveCompleteLevelTimeData;
    }

    private void OnDisable()
    {
        CheckPoint.saveCheckPointAction -= SaveTimeData;
        LoadNextLevel.finishLevelAction -= SaveCompleteLevelTimeData;
    }
    #endregion

    private void SaveTimeData(Vector3 uselessData)
    {
        saveTimeDataAction?.Invoke(timer);
    }

    private void SaveTimeData()
    {
        saveTimeDataAction?.Invoke(timer);
    }

    private void SaveCompleteLevelTimeData()
    {
        saveCompleteLevelTimeDataAction?.Invoke(timer);

    }
}
