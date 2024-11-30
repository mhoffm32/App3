using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target; // Player or target
    [SerializeField] float detectionRadius = 10f; // Detection range
    NavMeshAgent agent;
    private Vector3 previousPosition;
    private Animator animator;
    private int hits = 0;
    public bool isDead = false;
    private int completedCycles = 0; 
    
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        previousPosition = transform.position;
    }
    
    private void Update()
    {
        if (!isDead && !animator.GetBool("Dead"))
        {
            Run();
        }
    }

    void Run()
    {
        // Check if the target is within the detection radius
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        
        if (distanceToTarget <= detectionRadius)
        {
            
            agent.SetDestination(target.position);

            // Calculate the movement direction
            Vector3 movementDirection = transform.position - previousPosition;

            if (movementDirection.x != 0)  // Check if there's movement in the x-axis
            {
                // Flip the character to face the direction it's moving
                transform.localScale = new Vector3(
                    Mathf.Sign(movementDirection.x), 
                    transform.localScale.y, 
                    transform.localScale.z
                );
            }

            // Update previous position for the next frame
            previousPosition = transform.position;
        }
        else
        {
            // Stop the agent if the target is out of range
            agent.ResetPath();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
            other.gameObject.SetActive(false);
        } 
        else if (other.CompareTag("Melee")) 
        {
            Animator shovelAnimator = other.gameObject.GetComponent<Animator>();
            
            Debug.Log("Colided with meloo");
            if (!shovelAnimator.GetBool("IsIdle"))
            {
                Debug.Log("isnt idle");
                TakeDamage();
                Die();
            }
        }
    }

    void TakeDamage()
    {
        hits += 1;
        animator.SetBool("Hit", true);
         
        if (hits >= 2)
        {
           Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        isDead = true;  
        agent.isStopped = true; 
        agent.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
