using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowObjectState
{
    public IThrowObjectState ChangeState();
    public void OnBeginState();
    public void OnUpdate();
}
