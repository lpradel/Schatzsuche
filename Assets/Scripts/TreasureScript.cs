using UnityEngine;
using System.Collections;

public class TreasureScript : MonoBehaviour {
	
	// spawn position
	const float SPAWN_X = 350.0f;
	const float SPAWN_Y = 1.05f;
	const float SPAWN_Z = 150.0f;
	
	// teleport sound AudioClip
	public AudioClip teleport;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void DoActivateTrigger (int t) {
		// teleport player to spawn position
		GameObject.Find ("Player").transform.position = new Vector3(SPAWN_X, SPAWN_Y, SPAWN_Z);
		AudioSource.PlayClipAtPoint(teleport, Camera.main.transform.position);	// teleport sound
		
		// notify GameLogic that a treasure was found
		GameObject.Find ("Player").SendMessage ("treasureFoundTrigger", t, SendMessageOptions.DontRequireReceiver);
	}
	
	void OnTriggerEnter (Collider other) {
		// check if collided with player
		if (other.gameObject.name == "Player")
		{
			int found = 0;
			
			// check which treasure was found
			switch (this.gameObject.name)
			{
			case GameLogic.TREASURE_HILLS_NAME:
				found = GameLogic.TREASURE_HILLS;
				break;
			case GameLogic.TREASURE_LABYRINTH_NAME:
				found = GameLogic.TREASURE_LABYRINTH;
				break;
			case GameLogic.TREASURE_PARKOUR_NAME:
				found = GameLogic.TREASURE_PARKOUR;
				break;
			case GameLogic.TREASURE_PICKNICK_NAME:
				found = GameLogic.TREASURE_PICKNICK;
				break;
			case GameLogic.TREASURE_TOWER_NAME:
				found = GameLogic.TREASURE_TOWER;
				break;
			default:
				print ("Wait, this shouldn't happen!");
				break;
			}
			
			// destroy the found treasure
			Destroy (this.gameObject);
			
			// fire treasure found event
			DoActivateTrigger(found);
		}
	}
}
