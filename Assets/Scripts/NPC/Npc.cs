using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine;

public class Npc : MonoBehaviour
{
    public Transform[] waypoints; // Assign in Inspector
    public float speed = 2f;
    public bool loop = true; // Whether to loop back to the first waypoint

    private int currentWaypointIndex = 0;
    private Animator animator; // Animator reference
    private Vector3 lastPosition; // Track previous position for direction

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Check if the character is close to the waypoint
        if (direction.magnitude < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                if (loop)
                {
                    currentWaypointIndex = 0; // Loop back to the first waypoint
                }
                else
                {
                    SetIdleAnimation(); // Stop and set idle animation
                    enabled = false; // Stop movement
                }
            }
        }

        // Update animations based on movement direction
        UpdateAnimations(direction);
    }

    private void UpdateAnimations(Vector3 direction)
    {
        if (direction.magnitude > 0.1f) // If moving
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Horizontal movement
                animator.SetBool("isWalkingLR", true);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isIdle", false);

                // Flip sprite if moving left
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // Flip
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1); // Default
                }
            }
            else if (direction.y > 0)
            {
                // Moving up
                animator.SetBool("isWalkingLR", false);
                animator.SetBool("isWalkingUp", true);
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isIdle", false);
            }
            else
            {
                // Moving down
                animator.SetBool("isWalkingLR", false);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalkingDown", true);
                animator.SetBool("isIdle", false);
            }
        }
        else
        {
            SetIdleAnimation();
        }
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("isWalkingLR", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
        animator.SetBool("isIdle", true);
    }
}

