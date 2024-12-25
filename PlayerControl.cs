using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Description: Handles player actions, health, and interactions with objects.
public class NewBehaviourScript : MonoBehaviour
{
    public AudioClip shotVoice, dieVoice, takeLifeVoice, hurtVoice; // Audio clips for actions.
    public Transform bulletPosition; // Position to spawn bullets.
    public GameObject bullet; // Prefab for bullets.
    public Image lifeBar; // UI element for displaying health.
    private float lifeCount = 100f; // Player's initial health.
    public GameObject explosion; // Prefab for explosion effect.
    public GameControl control; // Reference to the GameControl script.
    private AudioSource AudioSource; // Audio source for playing sounds.

    void Start()
    {
        AudioSource = GetComponent<AudioSource>(); // Initialize the audio source.
    }

    void Update()
    {
        // Shoot a bullet when the player presses the F key.
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioSource.PlayOneShot(shotVoice, 1f); // Play the shot sound.
            GameObject go = Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
            GameObject goExplosion = Instantiate(explosion, bulletPosition.position, bulletPosition.rotation);
            go.GetComponent<Rigidbody>().velocity = bulletPosition.transform.forward * 10f;
            Destroy(go.gameObject, 2f); // Destroy the bullet after 2 seconds.
            Destroy(goExplosion.gameObject, 2f); // Destroy the explosion effect after 2 seconds.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collisions with zombies.
        if (collision.collider.gameObject.tag.Equals("zombi"))
        {
            AudioSource.PlayOneShot(hurtVoice, 1f); // Play the hurt sound.
            lifeCount -= 10f; // Decrease health.
            float k = lifeCount / 100f; // Calculate health percentage.
            lifeBar.fillAmount = k; // Update the health bar.
            lifeBar.color = Color.Lerp(Color.red, Color.green, k); // Change color based on health.

            if (lifeCount <= 0)
            {
                AudioSource.PlayOneShot(dieVoice, 1f); // Play the death sound.
                control.gameOver(); // Trigger game over.
            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        // Handle collisions with health items.
        if (c.gameObject.tag.Equals("heart"))
        {
            AudioSource.PlayOneShot(takeLifeVoice, 1f); // Play the health pickup sound.
            if (lifeCount < 100)
                lifeCount += 10f; // Increase health.
            float k = lifeCount / 100f; // Calculate health percentage.
            lifeBar.fillAmount = k; // Update the health bar.
            lifeBar.color = Color.Lerp(Color.red, Color.green, k); // Change color based on health.
            Destroy(c.gameObject); // Destroy the heart object.
        }
    }
}
