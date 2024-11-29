using UnityEngine;
using XNode;

public class WinNode : BaseNode {

	[Input] public int entry;

	public override string GetString() {
		return "WinNode";
	}
}