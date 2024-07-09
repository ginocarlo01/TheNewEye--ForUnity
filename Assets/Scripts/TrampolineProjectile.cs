using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    [SerializeField] private bool startMovement;

    private void Awake()
    {
        startMovement = false;
        speed = 0f;
    }

   
    public float Speed { get => speed; set { speed = value; StartMovement = true; } }
    public bool StartMovement { get => startMovement; set => startMovement = value; }


    void Update()
    {
        if(!StartMovement) return;
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        
    }
}
