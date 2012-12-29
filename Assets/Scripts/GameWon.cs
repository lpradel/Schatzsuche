using UnityEngine;
using System.Collections;

public class GameWon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// display Game Won Text
		GUIText textGameWon = (GUIText)GameObject.Find ("TextGameWon").GetComponent<GUIText>();
		textGameWon.text = "Y O U   W I N\n\nYou found all treasures!\nPress any key to quit...";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
		{	// on key press, quit game
			Application.Quit ();
		}
	}
}
