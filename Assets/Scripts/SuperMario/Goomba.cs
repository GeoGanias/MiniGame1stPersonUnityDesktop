using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasStarted;
    public float startingDistance;
    [SerializeField] GameObject[] waypoints;
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    private bool turnAround = false;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 1f;

    void Update()
    {
        if(!hasStarted)
        {
            if(Vector3.Distance(player.position, transform.position) < startingDistance)
            {
                hasStarted = true;
            }
        }
        else if(hasStarted)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                TurnAround();
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        }
    }

    private void TurnAround()
    {
        if(!turnAround)
        {
            animator.Play("TurnAround");
        }
        else
        {
            animator.Play("TurnAround2");
        }
        turnAround = !turnAround;
    }

}
