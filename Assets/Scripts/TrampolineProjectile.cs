using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    [SerializeField] private bool startMovement;

    [SerializeField] private float angleRight=90, angleLeft=270;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startMovement = false;
        speed = 0f;
    }

   
    public float Speed { 
        get => speed; 
        set { 
            //TODO: OBJETO DEVE DESEPARECER SOMENTE QUANDO ESTIVER A UMA DISTÂNCIA X DO PLAYER
            speed = value;
            this.gameObject.transform.eulerAngles =  speed > 0 ?  new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, angleRight) : new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, angleLeft);
            StartMovement = true;  
        } 
    }
    public bool StartMovement { get => startMovement; set => startMovement = value; }


    void Update()
    {
        if(!StartMovement) return;
        //transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        rb.velocity = new Vector2(speed, rb.velocity.y);

    }
}
