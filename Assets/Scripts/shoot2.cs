using UnityEngine;

public class shoot2 : MonoBehaviour

    //TEST
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed; // Speed of the projectile

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button to shoot
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set z-axis to 0 (2D plane)

        // Calculate direction from player to mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Apply velocity to the projectile's Rigidbody2D
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
