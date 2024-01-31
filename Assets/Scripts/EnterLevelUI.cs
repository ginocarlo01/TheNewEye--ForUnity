using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterLevelUI : MonoBehaviour
{

    [SerializeField] private GameObject enterLevelUI;

    [SerializeField] private TMP_Text enterLevelText;

    private void Start()
    {
        enterLevelUI.SetActive(false);
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        InitLevelLoad.playerEnteredDoor += SetLevelText;
    }

    private void OnDisable()
    {
        InitLevelLoad.playerEnteredDoor -= SetLevelText;
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
}
