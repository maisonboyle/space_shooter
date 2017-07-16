using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterManeuver : MonoBehaviour {

	public Vector2 startWait;
	public float dodge;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public float smoothing;
	public Boundary boundary;
	public float tilt;

	private Transform playerTransform;
	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		rb = GetComponent<Rigidbody> ();
		StartCoroutine (Evade ());
		currentSpeed = rb.velocity.z;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax));
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

	}

	IEnumerator Evade(){
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
		while (true) {
			if (playerTransform != null) {
				targetManeuver = playerTransform.position.x - transform.position.x;
			} else {
				targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			}
//			yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));
			yield return new WaitForSeconds (1.0f);
			targetManeuver = 0;
//			yield return new WaitForSeconds (Random.Range(maneuverWait.x, maneuverWait.y));
			yield return new WaitForSeconds (0.3f);
		}
	}

}
