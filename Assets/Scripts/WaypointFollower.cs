using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private int currentWayPoint = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool canMove;

    void Update()
    {
        if(Vector2.Distance(wayPoints[currentWayPoint].transform.position, transform.position) < .1f)
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPoints.Length)
            {
                currentWayPoint = 0;
            }
        }
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position, Time.deltaTime * speed);
        }
        
    }

    //only moves when it is visible
    private void OnBecameVisible()
    {
        canMove = true;
    }
}
