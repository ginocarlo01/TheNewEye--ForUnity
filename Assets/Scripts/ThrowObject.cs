using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowObject : MonoBehaviour, IThrowAction, IReceiveAction
{
    PlayerMovement pm;

    [SerializeField] GameObject spawnObjectLeft, spawnObjectRight;

    BoxCollider2D colliderLeft, colliderRight;

    [SerializeField] GameObject spawnObject;

    [SerializeField] LayerMask maskToBlock;

    TrampolineProjectileController tp;

    [SerializeField] float objectSpeed = 5f;

    bool canSpawnObject = true;

    

    private void Start()
    {
        try
        {
            pm = GetComponentInParent<PlayerMovement>();
            colliderLeft = spawnObjectLeft.GetComponent<BoxCollider2D>();
            colliderRight = spawnObjectRight.GetComponent<BoxCollider2D>();
            colliderLeft.enabled = false;
            colliderRight.enabled = false;
        }
        catch(MissingComponentException e)
        {
            Debug.LogError($"There is a missing component: {e}");
        }
        canSpawnObject = true;
    }

    void IThrowAction.ThrowObject()
    {
        if (canSpawnObject && !IsColliding())
        { 
            if(spawnObject ==  null) { Debug.Log("No object selected"); }

            int lookingRight = pm.LookingRight;

            Vector3 newPosition = lookingRight > 0 ? spawnObjectRight.transform.position : spawnObjectLeft.transform.position;

            GameObject newObject = Instantiate(spawnObject, newPosition, this.transform.rotation);

            tp = newObject.GetComponent<TrampolineProjectileController>();

            tp.Init(objectSpeed * lookingRight, this.gameObject);

            canSpawnObject = false;
        }
        else
        {
            if (tp)
            {
                tp.CallProjectileBack();
            }
        }

    }

    public void ReceiveObject()
    {
        canSpawnObject = true;
    }

    private bool IsColliding()
    {
        bool left = Physics2D.BoxCast(colliderLeft.bounds.center, new Vector3(1.19f, 0.16f, 0.0f), 0f, Vector2.left, .5f, maskToBlock);
        bool right = Physics2D.BoxCast(colliderRight.bounds.center, new Vector3(1.19f, 0.16f, 0.0f), 0f, Vector2.right, .5f, maskToBlock);
        return pm.LookingRight > 0 ? right : left;
    }
}
