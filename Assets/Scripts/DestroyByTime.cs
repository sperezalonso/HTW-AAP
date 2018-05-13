using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifetime;

	// Life time defined in editor. Game Objects destroyed after specified time
	void Start () {
		Destroy(gameObject, lifetime);
	}
}
