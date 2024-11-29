using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private Animator animator;
    
    public bool isAttacking = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsIdle", true);
            isAttacking = false;
        }
    }
    
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !animator.GetBool("isIdle")) 
        {
            isAttacking = true;
        }
    }
}