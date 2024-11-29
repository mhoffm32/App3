using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;      // Reference to the prefab
    public float fireRate = 0.001f;        // Rate of fire in seconds
    public int poolSize = 10;            // Size of the bullet pool

    public Transform gunTip;             
    public Vector2 gunDirection;         // Direction to fire bullets

    private Rigidbody2D player_rb;
    private float nextFireTime = 0f;

    private GameObject[] bulletPool;     // Pool of bullets
    private int currentBulletIndex = 0; // Index for reusing bullets
    
    private bool isFirstShot= true;

    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();

        // Initialize bullet pool
        bulletPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            bulletPool[i] = Instantiate(bulletPrefab);
            bulletPool[i].SetActive(false); // Deactivate all bullets initially
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && Time.time >= nextFireTime)
        {
            if (!isFirstShot)
            {
                FireBullet();
            }
            else
            {
                isFirstShot = false;
            }

            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
        // Get a bullet from the pool
        GameObject bullet = GetBulletFromPool();
        if (bullet != null)
        {
            // Reset bullet position and rotation
            bullet.transform.position = gunTip.position;
            bullet.transform.rotation = gunTip.rotation;
            bullet.SetActive(true);

            // Get the Rigidbody2D of the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                // Apply velocity in the direction of the gunTip's forward vector
                float bulletSpeed = 10f; // Adjust bullet speed as needed
                Vector2 firingDirection = gunTip.right.normalized; // Assuming gunTip's right points forward
                bulletRb.velocity = firingDirection * bulletSpeed;

                Debug.Log("Bullet fired from gunTip: " + firingDirection);
            }
            else
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody2D component.");
            }
        }
    }

    GameObject GetBulletFromPool()
    {
        // Loop through the pool to find an inactive bullet
        for (int i = 0; i < poolSize; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                currentBulletIndex = (currentBulletIndex + 1) % poolSize; // Use the next bullet in the pool
                return bulletPool[i];
            }
        }
        // If no inactive bullets are available, return null (could expand pool here if needed)
        return null;
    }
}

