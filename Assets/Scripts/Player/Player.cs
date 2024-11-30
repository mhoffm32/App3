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

    public GameObject cottageItem;

    public bool hasCottageItem = false;
    public bool allEnemiesKilled = false;
    public bool hasMazeItem = false;

    public bool eastQuestReceived = false;
    public bool eastQuestComplete = false;
    public bool northQuestComplete = false;
    public bool westQuestComplete = false;
    
    public GameObject eastBoulder;
    public GameObject northBoulder;
    public GameObject westBoulder;
    
    public Shooter shooter;
    public GameObject melee;
    public Transform gunTransform;
    private MeleeController meleeController;
    private Vector3 previousMousePosition = Vector3.zero;
    
    
    void Start()
    {
        meleeController = melee.GetComponent<MeleeController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerName = PlayerPrefs.GetString("PlayerName", "Default Player");
        playerNo = PlayerPrefs.GetInt("PlayerNo", 1);
        animator.SetBool("isPlayer1", playerNo == 1);
        animator.SetBool("isPlayer2",playerNo == 2);
    }

    void Update()
    {
         if (!animator.GetBool("isDead"))
         {
             MovePlayer();
             AimGun();
         }
         
         if (hasCottageItem == true) {
            Debug.Log("You have the cottage item!");
            cottageItem.SetActive(true);
         }
         if (eastQuestReceived  == true) {
            Debug.Log("You have received the first quest!");
            eastBoulder.SetActive(false);
         }
         if (eastQuestComplete == true) {
            Debug.Log("You have completed the east quest!");
            northBoulder.SetActive(false);
         }
        if (northQuestComplete == true) {
            Debug.Log("You have completed the north quest!");
            westBoulder.SetActive(false);
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
    
    
    private void AimGun()
    {
        // Get the current mouse position
        Vector3 currentMousePosition = Input.mousePosition;

        // Check if the mouse position has changed
        if (currentMousePosition != previousMousePosition)
        {
            // Update the previous mouse position
            previousMousePosition = currentMousePosition;

            // Convert the mouse position to world position
            currentMousePosition.z = 0f; // Assuming the camera is orthographic and objects are at z = 0
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(currentMousePosition);

            // Calculate the direction to the mouse
            Vector2 aimDirection = (mouseWorldPosition - transform.position).normalized;

            // Calculate the angle to the mouse
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            // Flip the gun based on the aiming direction
            if (angle > 90 || angle < -90)
            {
                gunTransform.localScale = new Vector3(x_direction, -1, 1);
            }
            else
            {
                gunTransform.localScale = new Vector3(x_direction, 1, 1);
            }

            // Rotate the gun
            gunTransform.rotation = Quaternion.Euler(0, 0, angle);

            // Adjust gun direction for the shooter
            Vector2 gunDirection = new Vector2(
                aimDirection.x * x_direction, // Flip the x-direction if facing left
                aimDirection.y
            );

            shooter.gunDirection = gunDirection;
        }
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !meleeController.isAttacking) 
        {
            Die();
        } 
    }
    
    public void Die()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isDead", true);
        shooter.enabled = false;
        
        foreach (Transform child in transform)
        {
            // Get the Renderer component of each child and disable it
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = false; // Make the child invisible
            }
        }
        GameController.isGameOver = true;
    }
}