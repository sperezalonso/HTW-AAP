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
			if (Input.GetKeyDown(KeyCode.R)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			}
		}
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRot = Quaternion.identity;
                Instantiate(asteroid, spawnPos, spawnRot);
                yield return new WaitForSeconds(spawnWait);
            }
            asteroidCount += 2;             // make every new wave bigger
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
				break;
            }
        }
    }

    public void AddScore()
    {
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
        scoreText.text = "SCORE: " + score.ToString();
    }
}
