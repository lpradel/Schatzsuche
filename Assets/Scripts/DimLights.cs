using UnityEngine;
using System.Collections;

public class DimLights : MonoBehaviour {
	
	Light spotlight;
	
	// Use this for initialization
	void Start () {
		spotlight = (Light)GameObject.Find ("Spotlight").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "Player")
		{
			spotlight.intensity = 0.0f;	// dim light on collision
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.gameObject.name == "Player")
		{
			spotlight.intensity = 0.73f;	// light on end collision
		}
	}
}
