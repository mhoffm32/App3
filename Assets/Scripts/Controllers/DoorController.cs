using Cinemachine;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public BoxCollider2D innerDoor;
    private Animator animator;
    public Transform player;
    public Transform innerSpawnPosition;
    public Transform outerSpawnPosition;
    public Camera mainCamera;
    public CinemachineVirtualCamera virtualCamera;
    public Camera tempCamera;
    public bool inCottage = false;

    void Start()
    {
        tempCamera.enabled = false;
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (innerDoor.OverlapPoint(player.position))
        { 
            ExitCottage();   
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("isOpen", true);  // Ensure you have an "OpenDoor" trigger in your Animator
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("isOpen", false);  
    }
    
    public void OnDoorOpenAnimationComplete()
    {
        EnterCottage();
    }
    

    void EnterCottage()
    {
        tempCamera.enabled = true;
        mainCamera.enabled = false;
        virtualCamera.Priority = 0;
        //virtualCamera.enabled = false;
        player.position = innerSpawnPosition.position;
        inCottage = true;
    }
    
    void ExitCottage()
    {
        mainCamera.enabled = true;
        virtualCamera.Priority = 10;
        tempCamera.enabled = false;
        
        //virtualCamera.enabled = false;
        player.position = outerSpawnPosition.position;
        inCottage = false;
    }

}