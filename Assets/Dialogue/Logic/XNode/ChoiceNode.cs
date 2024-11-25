using UnityEngine;
using XNode;

public class ChoiceNode : BaseNode {

	[Input] public int entry;
	public string speakerName;
	public string dialogueLine;
	public Sprite sprite;
	[Output(dynamicPortList = true)] public string[] responses; // Responses to this dialogue


	public override string GetString() {
		return "ChoiceNode/" + speakerName + "/" + dialogueLine;
	}

	public override Sprite GetSprite() {
		return sprite;
	}
}