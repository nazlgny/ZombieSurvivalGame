using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Description: Controls the movement and behavior of zombies in the game.

public class ZombieMovement : MonoBehaviour
{
    private GameObject player; // Reference to the player.
    public GameObject kalp; // Prefab for the heart item.
    private int zombieLife = 3; // Zombie's health.
    private float distance; // Distance to the player.
    private GameControl control; // Reference to the GameControl script.
    private int scoreFromDeadZombies = 10; // Score given when a zombie is killed.
    private AudioSource AudioSource; // Audio source for playing sounds.
    private bool zombieDies; // Tracks if the zombie is dead.

    void Start()
    {
        AudioSource = GetComponent<AudioSource>(); // Initialize the audio source.
        player = GameObject.Find("Player"); // Find the player in the scene.
        control = GameObject.Find("Scriptss")?.GetComponent<GameControl>();
        if (control == null)
        {
            Debug.LogError("'Scriptss' GameObject does not have a GameControl component.");
        }
    }

    void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position; // Set zombie's destination to the player.
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 10f)
        {
            if (!AudioSource.isPlaying)
                AudioSource.Play(); // Play the attack sound.
            if (!zombieDies)
                GetComponentInChildren<Animation>().Play("Zombie_Attack_01"); // Play the attack animation.
        }
        else
        {
            if (AudioSource.isPlaying)
                AudioSource.Stop(); // Stop the attack sound.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (control == null)
        {
            Debug.LogError("'control' is null. Check if 'Scriptss' GameObject has GameControl.");
            return;
        }
        if (collision.collider.gameObject.tag.Equals("bullet"))
        {
            Debug.Log("Zombie hit by bullet.");
            zombieLife--; // Decrease zombie health.
            if (zombieLife == 0)
            {
                control.ScoreIncrease(scoreFromDeadZombies); // Increase player score.
                if (kalp != null)
                {
                    Instantiate(kalp, transform.position + Vector3.up * 2f, Quaternion.identity); // Spawn a heart item.
                }
                else
                {
                    Debug.LogError("Heart prefab is not assigned.");
                }
                GetComponentInChildren<Animation>().Play("Zombie_Death_01"); // Play the death animation.
                Destroy(this.gameObject, 1.667f); // Destroy the zombie after animation.
            }
        }
    }
}
