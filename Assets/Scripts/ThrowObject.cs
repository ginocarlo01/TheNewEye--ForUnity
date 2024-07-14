using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour, IThrowAction, IReceiveAction
{
    PlayerMovement pm;

    [SerializeField] Transform spawnObjectPosLeft, spawnObjectPosRight;
    
    [SerializeField] GameObject spawnObject;

    [SerializeField] float objectSpeed = 5f;

    bool canSpawnObject = true;

    

    private void Start()
    {
        try
        {
            pm = GetComponentInParent<PlayerMovement>();
        }
        catch(MissingComponentException e)
        {
            Debug.LogError($"There is a missing component: {e}");
        }
        canSpawnObject = true;
    }

    void IThrowAction.ThrowObject()
    {
        if (!canSpawnObject) return;

        if(spawnObject ==  null) { Debug.Log("No object selected"); }

        int lookingRight = pm.LookingRight;

        Vector3 newPosition = lookingRight > 0 ? spawnObjectPosRight.position : spawnObjectPosLeft.position;

        GameObject newObject = Instantiate(spawnObject, newPosition, this.transform.rotation);

        TrampolineProjectileController tp = newObject.GetComponent<TrampolineProjectileController>();

        tp.Init(objectSpeed * lookingRight, this.gameObject);

        canSpawnObject = false;


    }

    public void ReceiveObject()
    {
        canSpawnObject = true;
    }
}
