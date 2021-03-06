﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;
	private float modifier = 0.0f;
	private float challenge;
	private GameObject hazard;

	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while(true){
			modifier += 0.15f;
			for (int i = 0; i < hazardCount + score/500; i++) {
				challenge = Random.Range (-1.7f, 1.3f);
				if (modifier < 3.0f) {
					challenge += modifier;
				} else {
					challenge += 3.0f;
				}
				if (challenge <= 1.0f) {
					hazard = hazards [Random.Range (0, 3)];
				} else if (challenge <= 2.0f) {
					hazard = hazards [3];
				} else {
					hazard = hazards [4];
				}
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity; 
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver) {
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}
		}
	}

	void Start(){
		score = 0;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		UpdateScore ();
		StartCoroutine( SpawnWaves ());
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}
			
	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	void Update(){
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}
