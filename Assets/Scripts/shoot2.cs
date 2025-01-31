using UnityEngine;

public class Shoot2 : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed; // Speed of the projectile
    public Transform firepoint; // Firepoint (child) from where the projectile will spawn

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

        // Calculate direction from firepoint to mouse position
        Vector2 direction = (mousePosition - firepoint.position).normalized;

        // Instantiate the projectile at the firepoint's position
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);

        // Apply velocity to the projectile's Rigidbody2D
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
