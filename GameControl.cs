using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Description: Manages the game's core logic, such as spawning zombies and tracking the score.
public class GameControl : MonoBehaviour
{
    public GameObject zombie; // Prefab for the zombie.
    private float time; // Timer for spawning zombies.
    private float process = 10f; // Time interval for spawning zombies.
    public TextMeshProUGUI scoreText; // UI element for displaying the score.
    private int score; // Player's score.

    void Start()
    {
        time = process; // Initialize the timer.
    }

    void Update()
    {
        time -= Time.deltaTime; // Decrease the timer.
        if (time < 0)
        {
            // Spawn a zombie at a random position.
            Vector3 pos = new Vector3(Random.Range(149f, 284f), 25.6f, Random.Range(163f, 317f));
            Instantiate(zombie, pos, Quaternion.identity);
            time = process; // Reset the timer.
        }
    }

    public void ScoreIncrease(int s)
    {
        score += s; // Increase the score.
        scoreText.text = "" + score; // Update the score text.
    }

    public void gameOver()
    {
        PlayerPrefs.SetInt("score", score); // Save the score.
        SceneManager.LoadScene("gameoverscene"); // Load the Game Over scene.
    }
}
