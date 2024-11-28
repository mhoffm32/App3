using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour {
    public float interactionRange = 2f; // range for detecting interactable objects
    public LayerMask interactableLayer; // layer to filter interactable objects

    public GameObject interactPrompt;

    private void Update() {
        DetectInteractable();

        if (Input.GetKeyDown(KeyCode.E)) {
            InteractWithObject();
        }
    }

    private void InteractWithObject() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        Debug.Log($"Objects detected: {hits.Length}");
        foreach (Collider2D hit in hits) {
            Debug.Log($"Hit: {hit.name}");
            NodeParser nodeParser = hit.GetComponent<NodeParser>();
            if (nodeParser != null) {
                nodeParser.Interact(); // trigger dialogue for this specific object
                return;
            }
        }
    }

    private void DetectInteractable() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        if (hits.Length > 0) {
            interactPrompt.SetActive(true);
        } else {
            interactPrompt.SetActive(false);
        }
    }


    public void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
