using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject asteroid;
    public Vector3 spawnValues;
    public int asteroidCount;
    public float spawnWait, startWait, waveWait;

    public Text scoreText, restartText, gameOverText;
    bool gameOver, restart;
    int score;

    void Start()
    {
		// Empty text messages on screen while game is running, set the initiall score to 0
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

	void Update() {
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R))			// Restart the game with the R key 
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			}
		}
	}

    IEnumerator SpawnWaves()
    {
		// Spawn infinite waves of asteroids after a specified starting wait time
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);	// Random asteroid position along the x axis
                Quaternion spawnRot = Quaternion.identity;
                Instantiate(asteroid, spawnPos, spawnRot);
                yield return new WaitForSeconds(spawnWait);			// Wait time between different asteroid spawns
            }
            asteroidCount += 2;             // make every new wave bigger by two asteroids
            yield return new WaitForSeconds(waveWait);		// Wait time for the next wave of asteroids

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";			// Restart game option only available when game is over
                restart = true;
				break;												// Infinite spawning loop is ended when game is over
            }
        }
    }

    public void AddScore()
    {
		// Add a point to the score every time an asteroid is destroyed
        score++;
        UpdateScore();
    }

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "GAME OVER!";
	}
    void UpdateScore()
    {
		// Update the score text
        scoreText.text = "SCORE: " + score.ToString();
    }
}
