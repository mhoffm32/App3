using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 40f;      
    private Rigidbody2D rb;
    public int playerNo = 1;
    public string playerName = "Default Player";
    private Animator animator;
    private Collider2D bodyCollider;
    public static int x_direction = 1;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        playerName = PlayerPrefs.GetString("PlayerName", "Default Player");
        playerNo = PlayerPrefs.GetInt("PlayerNo", 1);
        
        // animator.SetBool("isPlayer1", playerNo == 1);
        // animator.SetBool("isPlayer2", playerNo == 2);
        
        animator.SetBool("isPlayer1", true);
        animator.SetBool("isPlayer2", false);
        
    }

    void Update()
    {
         if (!animator.GetBool("isDead"))
         {
                MovePlayer();
         }
    }
    
    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("HorizontalMove");
        float vertical = Input.GetAxis("VerticalMove"); 

        // Calculate the movement direction
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        if (direction.magnitude > 0f)
        {
            animator.SetBool("isRunning", true);

            if (horizontal < 0)
            {
                x_direction = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontal > 0)
            {
                x_direction = 1;
                transform.localScale = new Vector3(1, 1, 1); 
            }
            
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
