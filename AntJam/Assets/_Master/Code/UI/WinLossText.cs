using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLossText : MonoBehaviour
{
	public static void SetWinState(bool didWin)
	{
		Instance.GetComponent<Text> ().text = didWin ? "You won" : "You lost";
	}

	private static WinLossText Instance;

	// Use this for initialization
	void Awake () 
	{
		if (Instance != null) 
		{
			Debug.LogError ("Multiple instances of WinLossText found");
			return;
		}

		Instance = this;
	}
}
