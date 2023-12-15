using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject canvasObject;

    private void Start()
    {
        canvasObject.SetActive(false);
    }


    public void ChangePauseCanvas()
    {
        canvasObject.SetActive(!canvasObject.activeSelf);
    }
}
