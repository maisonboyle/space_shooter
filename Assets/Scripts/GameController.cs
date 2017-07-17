using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public GameObject restartButton;
	public Text gameOverText;
	public Text highScoreText;

	private bool gameOver;
	private bool restart;
	private int score;
	private float modifier = 0.0f;
	private float challenge;
	private GameObject hazard;
	private int highScore;

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
				restartButton.SetActive (true);
				restart = true;
				break;
			}
		}
	}

	void Start(){
		score = 0;
		highScore = PlayerPrefs.GetInt ("Highscore");
		highScoreText.text = "Highscore: " + highScore;
		gameOver = false;
		restart = false;
		restartButton.SetActive (false);
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
		if (score > highScore && gameOver) {
			highScore = score;
			highScoreText.text = "Highscore: " + highScore;
			PlayerPrefs.SetInt ("Highscore", highScore);
		}
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

//	void Update(){
//		if (restart) {
//			if (Input.GetKeyDown (KeyCode.R)) {
//				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
//			}
//		}
//	}
}
