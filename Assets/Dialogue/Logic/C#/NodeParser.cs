using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class NodeParser : MonoBehaviour {
    public DialogueGraph graph;
    Coroutine _parser;
    public TMP_Text speaker;
    public TMP_Text dialogue;
    public Image speakerImage;
    public GameObject choiceButtonPrefab; // Reference to the button prefab
    public Transform choiceButtonsPanel;  // Panel that holds the choice buttons

    public Player player;

    public GameObject dialogueBox;
    public GameObject spaceNext;

    private bool isInteracting = false;

    private void Start() {
        foreach (BaseNode b in graph.nodes) {
            if (b.GetString() == "Start") {
                graph.current = b;
                break;
            }
        }
    }

    public void Interact() {
        if (isInteracting) return; // Prevent multiple interactions
        isInteracting = true;

        // Reset graph to "Start" node
        foreach (BaseNode b in graph.nodes) {
            if (b.GetString() == "Start") {
                graph.current = b;
                break;
            }
        }

        if (gameObject.name == "Npc3") {
            player.eastQuestReceived = true;
        } 
        
        if (gameObject.name == "Npc3" && player.hasCottageItem == true) {
            player.eastQuestComplete = true;
        }
        
        if (gameObject.name == "Npc3" && player.allEnemiesKilled == true) {
            player.northQuestComplete = true;
        } 
        
        if (gameObject.name == "Npc3" && player.hasMazeItem == true) {
            player.westQuestComplete = true;
        }

        Debug.Log("Interacting with " + gameObject.name);
        dialogueBox.SetActive(true);
        _parser = StartCoroutine(ParseNode());
    }


    IEnumerator ParseNode() {

        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');
        
        if (dataParts[0] == "Start") {
            NextNode("exit");
        } 
        if (dataParts[0] == "DialogueNode") {
            spaceNext.SetActive(true);
            foreach (Transform child in choiceButtonsPanel) {
                Destroy(child.gameObject);
            }

            // Dialogue processing
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => Input.GetKeyDown("space"));
            yield return new WaitUntil(() => Input.GetKeyUp("space"));
            NextNode("exit");
        }
        if (dataParts[0] == "ChoiceNode") {
            // Choice processing
            spaceNext.SetActive(false);

            ChoiceNode choiceNode = b as ChoiceNode; // Cast to ChoiceNode
            speaker.text = choiceNode.speakerName;
            dialogue.text = choiceNode.dialogueLine;
            speakerImage.sprite = choiceNode.GetSprite();

            // Clear any previous buttons
            foreach (Transform child in choiceButtonsPanel) {
                Destroy(child.gameObject); // Destroy existing buttons
            }

            // Create new buttons based on the current choices
            for (int i = 0; i < choiceNode.responses.Length; i++) {

                Debug.Log("Creating button for response " + choiceNode.responses[i]);

                string response = choiceNode.responses[i]; // Get the actual response string
                NodePort p = choiceNode.Outputs.ElementAt(i+1); // Get corresponding output port for response

                // Instantiate a new button from the prefab
                GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceButtonsPanel); 
                choiceButton.GetComponentInChildren<TMP_Text>().text = response; // Set button text to response
                choiceButton.GetComponent<Button>().onClick.AddListener(() => NextNode(p.fieldName)); // Add listener to handle response
            }
        }
        if (dataParts[0] == "DeathNode") {
            Debug.Log("Player died. Game Over.");
            dialogueBox.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
        if (dataParts[0] == "GainItemNode") {
            Debug.Log("Player gains an item.");
            dialogue.text = "You gained a powerful item!";
            spaceNext.SetActive(false);
            if (player.hasCottageItem == true) {
                player.hasMazeItem = true;
            } else {
                player.hasCottageItem = true;
            }            // Add logic to give the player an item here
            yield return new WaitForSeconds(2f); // Show message briefly
            dialogueBox.SetActive(false);
            isInteracting = false; // Allow future interactions  
            Destroy(gameObject);      
        }
        if (dataParts[0] == "WinGameNode") {
            Debug.Log("Player wins the game.");
            dialogue.text = "Congratulations! You have saved the village!";
            spaceNext.SetActive(false);
            yield return new WaitForSeconds(2f); // Show message briefly
            dialogueBox.SetActive(false);
            SceneManager.LoadScene("Win");
        }
        if (dataParts[0] == "End") {
            Debug.Log("End of dialogue");
            spaceNext.SetActive(false);
            dialogueBox.SetActive(false);
            isInteracting = false; // Allow future interactions
        }

        // Ensure buttons are displayed one on top of the other
        LayoutRebuilder.ForceRebuildLayoutImmediate(choiceButtonsPanel as RectTransform);
    }

    public void NextNode(string fieldName) {
        if (_parser != null) {
            StopCoroutine(_parser);
            _parser = null;
        }
        isInteracting = false;

        foreach (NodePort p in graph.current.Ports) {
            // Find port with this name
            if (p.fieldName == fieldName) {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
}