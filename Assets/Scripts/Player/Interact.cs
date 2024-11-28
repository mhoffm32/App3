using UnityEngine;

public class Interact : MonoBehaviour {
    public float interactionRange = 2f; // range for detecting interactable objects
    public LayerMask interactableLayer; // layer to filter interactable objects

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            InteractWithObject();
        }
    }

    private void InteractWithObject() {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);
        foreach (Collider hit in hits) {
            NodeParser nodeParser = hit.GetComponent<NodeParser>();
            if (nodeParser != null) {
                nodeParser.Interact(); // trigger dialogue for this specific object
                return;
            }
        }
    }
}
