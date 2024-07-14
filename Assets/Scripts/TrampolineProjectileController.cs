using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineProjectileController : MonoBehaviour
{
    //STATES
    public TrampolineProjectile_ThrownState thrownState;
    public TrampolineProjectile_OnWallState wallState;
    public TrampolineProjectile_BackToPlayerState backToPlayerState;
    public IThrowObjectState currentState;

    //VARIABLES
    private float speed = 1f;
    [SerializeField] private float followPlayerSpeed;
    [SerializeField] private float incrementPlayerSpeedRate = 0.1f;

    [SerializeField] private float angleRight=90, angleLeft=270;

    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float minDistanceToPlayer;

    private bool isPointingToRightDirection;

    //COMPONENTS
    [SerializeField] private GameObject surfaceCollider;
    [SerializeField] private GameObject blockers;

    [HideInInspector]
    public Rigidbody2D rb;

    private GameObject player;

    //ENCAPSULATIONS
    public GameObject Player { get => player; set => player = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AngleRight { get => angleRight; set => angleRight = value; }
    public float AngleLeft { get => angleLeft; set => angleLeft = value; }
    public float MaxDistanceToPlayer { get => maxDistanceToPlayer; set => maxDistanceToPlayer = value; }
    public GameObject SurfaceCollider { get => surfaceCollider; set => surfaceCollider = value; }
    public bool IsPointingToRightDirection { get => isPointingToRightDirection; set => isPointingToRightDirection = value; }
    public GameObject Blockers { get => blockers; set => blockers = value; }
    public float MinDistanceToPlayer { get => minDistanceToPlayer; set => minDistanceToPlayer = value; }
    public float FollowPlayerSpeed { get => followPlayerSpeed; set => followPlayerSpeed = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SurfaceCollider.SetActive(false);
        Blockers.SetActive(false);
        Speed = 0f;
        thrownState = new TrampolineProjectile_ThrownState(this);
        wallState = new TrampolineProjectile_OnWallState(this);
        backToPlayerState = new TrampolineProjectile_BackToPlayerState(this);
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

    public void BackToPlayer()
    {
        Player.GetComponent<ThrowObject>().ReceiveObject();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeState();
    }

    public void ChangeState()
    {
        currentState = currentState.ChangeState();
        currentState.OnBeginState();
    }

    public void IncreaseProjectileSpeed()
    {
        FollowPlayerSpeed += incrementPlayerSpeedRate * Time.deltaTime;
    }
}
