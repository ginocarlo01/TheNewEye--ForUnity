using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterLevelUI : MonoBehaviour
{

    [SerializeField] private GameObject enterLevelUI;

    [SerializeField] private TMP_Text enterLevelText;

    private string text = "";

    public static EnterLevelUI instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(text == "")
        {
            enterLevelUI.SetActive(false);
        }
        else{
            enterLevelUI.SetActive(true);
            enterLevelText.text = text;
        }
    }

    public void SetLevelText(string text)
    {
        this.text = text;
    }
}
