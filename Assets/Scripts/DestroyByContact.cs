using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	GameController gameController;

	void Start() {
		// Find the instance of the Game Controller script
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();		
	}

	void OnTriggerEnter(Collider other) {
		// Ignore collision with the level boundary
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate(explosion, transform.position, transform.rotation);			// Instantiate the explosions
		if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();		// If an asteroid collides with the player, end the game	
		}
		gameController.AddScore();		// Add a point for every destroyed asteroid
		Destroy(other.gameObject);		// Destroy the laser and the asteroid objects
		Destroy(gameObject);
	}
}