using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
	
	float MAX_TIME = 0.0f;	// time limit on finding treasures in s
	const float WAIT_TIME = 3.0f;	// time for waiting at end game
	
	const int MAX_TREASURES = 5;	// # of treasures in game
	const int MIN_TREASURES = 3;	// # of treasures needed to win
	
	// treasure IDs
	public const int TREASURE_HILLS = 1;
	public const int TREASURE_LABYRINTH = 2;
	public const int TREASURE_PARKOUR = 3;
	public const int TREASURE_PICKNICK = 4;
	public const int TREASURE_TOWER = 5;
	
	// treasure names
	public const string TREASURE_HILLS_NAME = "TreasureHills";
	public const string TREASURE_LABYRINTH_NAME = "TreasureLabyrinth";
	public const string TREASURE_PARKOUR_NAME = "TreasureParkour";
	public const string TREASURE_PICKNICK_NAME = "TreasurePicknick";
	public const string TREASURE_TOWER_NAME = "TreasureTower";
	
	float timeGameStart;	// time of game start in s
	float timeLeft; 		// time left to find treasures in s
	float timeGameEnd;		// calculated time of game end in s
	
	int numTreasuresFound;
	bool[] treasures;
	
	// Use this for initialization
	void Start () {
		// init treasures
		numTreasuresFound = 0;	// initially no treasures found
		updateTextNumOfTreasures();	// show no treasures found yet
		
		// init array with treasure status
		treasures = new bool[MAX_TREASURES];
		for (int i = 0; i < MAX_TREASURES; i++)
			treasures[i] = false;
		
		// roll which treasures spawn
		int t = 0;
		while (t < 3)
		{
			int n = randomNumber(0,MAX_TREASURES-1);
			if (treasures[n] == false)
			{
				treasures[n] = true;
				t++;
			}
		}
		
		// set treasure visibility / collision accordingly
		string treasuresSpawnedInfo = "";
		for (int i = 1; i <= MAX_TREASURES; i++)
		{
			if (treasures[i-1] == true)
			{	// treasure[i-1] DID spawn
				switch (i)
				{
				case TREASURE_HILLS:
					MAX_TIME += 120.0f;
					break;
				case TREASURE_LABYRINTH:
					MAX_TIME += 240.0f;
					break;
				case TREASURE_PARKOUR:
					MAX_TIME += 240.0f;
					break;
				case TREASURE_PICKNICK:
					MAX_TIME += 120.0f;
					break;
				case TREASURE_TOWER:
					MAX_TIME += 180.0f;
					break;
				default:
					print("This shouldn't happen :D");
					break;
				}
			}
			else
			{	// treasure[i-1] did NOT spawn => destroy it
				switch (i)
				{
				case TREASURE_HILLS:
					Destroy (GameObject.Find (TREASURE_HILLS_NAME));
					break;
				case TREASURE_LABYRINTH:
					Destroy (GameObject.Find (TREASURE_LABYRINTH_NAME));
					break;
				case TREASURE_PARKOUR:
					Destroy (GameObject.Find (TREASURE_PARKOUR_NAME));
					break;
				case TREASURE_PICKNICK:
					Destroy (GameObject.Find (TREASURE_PICKNICK_NAME));
					break;
				case TREASURE_TOWER:
					Destroy (GameObject.Find (TREASURE_TOWER_NAME));
					break;
				default:
					print("This shouldn't happen :D");
					break;
				}
			}
		}
		
		// display hints in GUI
		updateTextTreasureHints();
		
		// init times
		timeGameStart = Time.time;	// Gametimer starts NOW
		timeLeft = MAX_TIME; // all time is left at the beginning
		timeGameEnd = timeGameStart + MAX_TIME;	// calculate GameEnd time
	}
	
	// Update is called once per frame
	void Update () {
		// calculate time left
		timeLeft = Mathf.Round (timeGameEnd - Time.time);	// time left in s
		
		// calculate minutes and second, format nicely for display <mm:ss>
		float minutesLeft = Mathf.Floor(timeLeft / 60);
		float secondsLeft = timeLeft % 60;
		
		string minutes = minutesLeft.ToString();
		string seconds = secondsLeft.ToString();
		if (minutesLeft < 10.0f)
			minutes = "0" + minutesLeft.ToString();
		if (secondsLeft < 10.0f)
			seconds = "0" + secondsLeft.ToString();
		
		// print time left
		GUIText textTimeLeft = (GUIText)GameObject.Find ("TextTimeLeft").GetComponent<GUIText>();
		textTimeLeft.text = "Time left: " + minutes + ":" + seconds;
		
		// check if player ran out of time
		if (timeLeft <= 0)
		{
			StartCoroutine ("waitAndGameOver");	// trigger game over
			updateTextTreasureHints ("Your time is up, you lose :-(");
		}
	}
	
	void treasureFoundTrigger(int t) {
		// set found treasure to disabled
		treasures[t-1] = false;
		
		// check which treasure was found by the player
		switch (t)
		{
		case TREASURE_HILLS:
			break;
		case TREASURE_LABYRINTH:
			break;
		case TREASURE_PARKOUR:
			break;
		case TREASURE_PICKNICK:
			break;
		case TREASURE_TOWER:
			break;
		default:
			print("This shouldn't happen :D");
			break;
		}
		
		// trigger treasure found event
		foundTreasure ();
	}
	
	void updateTextNumOfTreasures() {
		GUIText textNumOfTreasures = (GUIText)GameObject.Find ("TextNumOfTreasures").GetComponent<GUIText>();
		textNumOfTreasures.text = "Treasures found: " + numTreasuresFound + "/" + MIN_TREASURES;
	}
	
	void updateTextTreasureHints(string hints) {
		GUIText textTreasureHints = (GUIText)GameObject.Find ("TextTreasureHints").GetComponent<GUIText>();
		textTreasureHints.text = hints;
	}
	
	void updateTextTreasureHints() {
		GUIText textTreasureHints = (GUIText)GameObject.Find ("TextTreasureHints").GetComponent<GUIText>();
		
		string treasuresInfo = "";
		for (int i = 1; i <= MAX_TREASURES; i++)
		{
			if (treasures[i-1] == true)
			{	// this treasure spawned and has not been found => prepare GUI info
				switch (i)
				{
				case TREASURE_HILLS:
					treasuresInfo += "In the hills of dark despair you will discover a treasure...\n";
					break;
				case TREASURE_LABYRINTH:
					treasuresInfo += "Find your way through the labyrinth to find a hidden treasure...\n";
					break;
				case TREASURE_PARKOUR:
					treasuresInfo += "Complete the parkour between the dark forest and the tower...\n";
					break;
				case TREASURE_PICKNICK:
					treasuresInfo += "Follow the lights in the dark forest to find a treasure...\n";
					break;
				case TREASURE_TOWER:
					treasuresInfo += "A secret treasure is hidden at the base of the highest tower...\n";
					break;
				default:
					print("This shouldn't happen :D");
					break;
				}
			}
		}
		textTreasureHints.text = treasuresInfo;
	}
	
	void foundTreasure()
	{
		numTreasuresFound++;
		updateTextNumOfTreasures();
		
		// update treasure hints GUI
		updateTextTreasureHints();
		
		// check if finished
		if (numTreasuresFound == MIN_TREASURES)
		{
			StartCoroutine ("waitAndGameWon");	// trigger game won
			updateTextTreasureHints ("You found all treasures and win :-)");
		}
	}
	
	IEnumerator waitAndGameOver()
	{
		Time.timeScale = 0.00001f;
		Screen.showCursor = true;
		Screen.lockCursor = false;
		yield return new WaitForSeconds(WAIT_TIME * Time.timeScale);	// wait a few seconds
		Time.timeScale = 1.0f;
		Application.LoadLevel ("GameOverScene");	// load game over screen
	}
	
	IEnumerator waitAndGameWon()
	{
		Time.timeScale = 0.00001f;
		Screen.showCursor = true;
		Screen.lockCursor = false;
		yield return new WaitForSeconds(WAIT_TIME * Time.timeScale);	// wait a few seconds
		Time.timeScale = 1.0f;
		Application.LoadLevel ("GameWonScene");	// load game over screen
	}
	
	private int randomNumber(int min, int max)
	{
		System.Random random = new System.Random();
		return random.Next(min, max+1);	// Random.next generates values from min..max-1 (max itself not included)
	}
}
