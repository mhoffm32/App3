using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // The player or target to follow
    public float smoothSpeed = 0.125f; // How smooth the camera follows
    public Vector3 offset;      // Offset from the player
    
    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera position
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}