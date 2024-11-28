using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;
using System.Linq;

public class NodeParser : MonoBehaviour {
    public DialogueGraph graph;
    Coroutine _parser;
    public TMP_Text speaker;
    public TMP_Text dialogue;
    public Image speakerImage;
    public GameObject choiceButtonPrefab; // Reference to the button prefab
    public Transform choiceButtonsPanel;  // Panel that holds the choice buttons

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
            foreach (Transform child in choiceButtonsPanel) {
                Destroy(child.gameObject);
            }

            // Dialogue processing
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImage.sprite = b.GetSprite();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }
        if (dataParts[0] == "ChoiceNode") {
            // Choice processing
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
                string response = choiceNode.responses[i]; // Get the actual response string
                NodePort p = choiceNode.Outputs.ElementAt(i+1); // Get corresponding output port for response

                // Instantiate a new button from the prefab
                GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceButtonsPanel); 
                choiceButton.GetComponentInChildren<TMP_Text>().text = response; // Set button text to response
                choiceButton.GetComponent<Button>().onClick.AddListener(() => NextNode(p.fieldName)); // Add listener to handle response
            }
        }
        if (dataParts[0] == "End") {
            Debug.Log("End of dialogue");
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