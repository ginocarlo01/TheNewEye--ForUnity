using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour, IThrowAction
{
    PlayerMovement pm;

    [SerializeField] Transform spawnObjectPosLeft, spawnObjectPosRight;
    
    [SerializeField] GameObject spawnObject;

    [SerializeField] float objectSpeed = 5f;

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
        
    }

    void IThrowAction.ThrowObject()
    {
        if(spawnObject ==  null) { Debug.Log("No object selected"); }

        int lookingRight = pm.LookingRight;

        Vector3 newPosition = lookingRight > 0 ? spawnObjectPosRight.position : spawnObjectPosLeft.position;

        GameObject newObject = Instantiate(spawnObject, newPosition, this.transform.rotation);

        newObject.GetComponent<TrampolineProjectile>().Speed =  objectSpeed * lookingRight;
    }
}
