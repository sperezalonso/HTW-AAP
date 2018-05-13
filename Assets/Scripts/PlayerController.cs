using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void Update() 
	{
		if ((Input.GetButton("Fire1") || Input.GetKeyDown("space")) && Time.time > nextFire)		// Fire a blast with the main mouse key or the spacebar
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
	
	void FixedUpdate () {
		// axis in opposite directions since the game moves sideways
		float moveHorizontal = Input.GetAxis("Vertical");
		float moveVertical = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(-moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		// Limit the spaceship position within the screen
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);		// Have a tilt movement for the spaceship when moving sideways
	}
}
