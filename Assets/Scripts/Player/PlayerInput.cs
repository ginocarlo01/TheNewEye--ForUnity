using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private IThrowAction throwAction;

    private void Start()
    {
        throwAction = GetComponent<IThrowAction>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(throwAction);
            throwAction?.ThrowObject();
        }
    }
}
