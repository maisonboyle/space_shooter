using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		Invoke ("Fire", delay);
	}

	void Fire(){
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		float randomTime = Random.Range (fireRate, 2 * fireRate);
		audioSource.Play ();
		Invoke ("Fire", randomTime);
	}

}
