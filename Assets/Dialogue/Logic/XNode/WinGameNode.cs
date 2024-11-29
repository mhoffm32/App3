using UnityEngine;
using XNode;

public class WinGameNode : BaseNode
{
	[Input] public int entry;

	public override string GetString() {
		return "WinGameNode";
	}
}
