using UnityEngine;

public class MainNPC : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public DialogueGraph mainDialogue1; // The default dialogue graph
    public DialogueGraph mainDialogue2; // The dialogue graph if the player has a specific item
    public DialogueGraph mainDialogue3; // The dialogue graph if the player has a specific item
    public DialogueGraph mainDialogue4; // The dialogue graph if the player has a specific item

    private NodeParser nodeParser; // Reference to the NodeParser script

    private void Start()
    {
        nodeParser = GetComponent<NodeParser>();
        if (nodeParser == null)
        {
            Debug.LogError("NodeParser script is missing on this NPC!");
        }
    }

    private void Update()
    {
        // Check if the player has the item and update the graph
        if (player.GetComponent<Player>().hasMazeItem)
        {
            nodeParser.graph = mainDialogue4; // Assign the alternate graph
        } else if (player.GetComponent<Player>().allEnemiesKilled) {
            nodeParser.graph = mainDialogue3; // Assign the alternate graph
        }
        else if (player.GetComponent<Player>().hasCottageItem) {
            nodeParser.graph = mainDialogue2; // Assign the alternate graph
        } else
        {
            nodeParser.graph = mainDialogue1; // Assign the default graph
        }
    }
}
