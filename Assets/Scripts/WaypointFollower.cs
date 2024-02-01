using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private int currentWayPoint = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform target;
    [SerializeField] private bool canMove;

    private void Start()
    {
        if(target == null) { target = transform; }
    }

    void Update()
    {
        if(Vector2.Distance(wayPoints[currentWayPoint].transform.position, target.position) < .1f)
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        if (canMove)
        {
            target.position = Vector2.MoveTowards(target.position, wayPoints[currentWayPoint].transform.position, Time.deltaTime * speed);
        }
        
    }

    //only moves when it is visible
    private void OnBecameVisible()
    {
        canMove = true;
    }
}
