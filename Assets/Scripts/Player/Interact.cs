using UnityEngine;

public class Interact : MonoBehaviour {
    public NodeParser nodeParser;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            nodeParser.Interact();
        }
    }
}
