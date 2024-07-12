using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineProjectileController : MonoBehaviour
{
    //STATES
    public TrampolineProjectile_ThrownState thrownState;
    public IThrowObjectState currentState;

    //VARIABLES
    [SerializeField] private float speed = 1f;

    [SerializeField] private bool startMovement;

    [SerializeField] private float angleRight=90, angleLeft=270;

    [SerializeField] private float maxDistanceToPlayer;

    [SerializeField] private float canMove;

    public Rigidbody2D rb;

    private GameObject player;

    //ENCAPSULATIONS
    public bool StartMovement { get => startMovement; set => startMovement = value; }
    public GameObject Player { get => player; set => player = value; }
    public float CanMove { get => canMove; set => canMove = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AngleRight { get => angleRight; set => angleRight = value; }
    public float AngleLeft { get => angleLeft; set => angleLeft = value; }
    public float MaxDistanceToPlayer { get => maxDistanceToPlayer; set => maxDistanceToPlayer = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startMovement = false;
        Speed = 0f;
        thrownState = new TrampolineProjectile_ThrownState(this);
    }

    void Update()
    {
        

        if (currentState != null)
            currentState.OnUpdate();

    }

    public void Init(float speed, GameObject player)
    {
        Speed = speed;
        Player = player;
        currentState = thrownState;
        currentState.OnBeginState();

    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
