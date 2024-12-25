using UnityEngine;

// Description: Represents a destructible cube that can drop an armor pickup when destroyed.
public class Chest : MonoBehaviour
{
    public GameObject armorPickupPrefab; // The prefab for the armor pickup to spawn.
    public int boxHealth = 5; // Health of the cube.

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a bullet.
        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(1); // Decrease health by 1.
            Destroy(collision.gameObject); // Destroy the bullet.
        }
    }

    private void TakeDamage(int damage)
    {
        boxHealth -= damage; // Reduce cube health.
        if (boxHealth <= 0)
        {
            BreakBox(); // Break the cube when health reaches 0.
        }
    }

    private void BreakBox()
    {
        // Spawn the armor pickup if it is set.
        if (armorPickupPrefab != null)
        {
            Instantiate(armorPickupPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // Destroy the cube.
    }
}
