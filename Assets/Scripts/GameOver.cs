using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// display Game Over Text
		GUIText textGameOver = (GUIText)GameObject.Find ("TextGameOver").GetComponent<GUIText>();
		textGameOver.text = "G A M E   O V E R\n\nYour time ran out!\nPress any key to quit...";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
		{	// on key press, quit game
			Application.Quit ();
		}
	}
}
