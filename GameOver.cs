using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Description: Handles the Game Over screen and player interactions.
public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score; // UI element to display the score.

    void Start()
    {
        Cursor.visible = true; // Show the cursor.
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor.
        score.text = "Score: " + PlayerPrefs.GetInt("score"); // Display the saved score.
    }

    public void playAgain()
    {
        SceneManager.LoadScene("mygame"); // Restart the game.
    }
}
