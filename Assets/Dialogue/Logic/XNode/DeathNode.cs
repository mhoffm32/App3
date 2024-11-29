using UnityEngine;
using XNode;

public class DeathNode : BaseNode {

	[Input] public int entry;

	public override string GetString() {
		return "DeathNode";
	}
}