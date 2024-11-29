using UnityEngine;
using XNode;

public class GainItemNode : BaseNode {

	public string speakerName;
	public Sprite sprite;
	[Input] public int entry;

	public override string GetString() {
		return "GainItemNode";
	}
}