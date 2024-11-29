using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 4f;
    private Rigidbody2D rb;
    private bool isInUse = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.right * speed;  // Move in the "right/left" direction at set speed
        Invoke("DestroyBullet", lifetime);   // Destroy bullet after lifetime
    }

    private void DestroyBullet()
    {
        // This just makes the bullet inactive instead of destroying it, allowing for reuse
        gameObject.SetActive(false);  // Deactivate the bullet object
    }

    public void ResetBullet(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);  // Re-enable the bullet object
    }
    
    
}