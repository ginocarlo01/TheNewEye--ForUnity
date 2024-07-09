using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour, IThrowAction
{
    PlayerMovement pm;

    [SerializeField] Transform spawnObjectPos;
    
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

        Transform newPosition = spawnObjectPos;

        newPosition.transform.position = new Vector3(spawnObjectPos.position.x * lookingRight, spawnObjectPos.position.y,spawnObjectPos.position.z);    

        GameObject newObject = Instantiate(spawnObject, newPosition.position, newPosition.rotation);

        newObject.GetComponent<TrampolineProjectile>().Speed =  objectSpeed * lookingRight;
    }
}
