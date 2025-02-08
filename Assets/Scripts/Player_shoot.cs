using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot2 : MonoBehaviour
{
    public InputAction MoveAction;

    //projectile variables
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed; // Speed of the projectile
    public Transform firepoint; // Firepoint (child) from where the projectile will spawn

    //rotating firepoint variables
    private Quaternion targetRotation;
    public float RotationSmoothingCoef = 0.01f;
    Vector2 move;
    Vector2 mousePos;

    public Camera cam;

    //rotates the firepoint

    Rigidbody2D parentRb;
    //Rigidbody2D rb;

    public Transform CharacterTransform;

    private void Start()
    {

        parentRb = GetComponentInParent<Rigidbody2D>();

        if (parentRb != null)
        {
            Debug.Log("Parent Rigidbody2D found: " + parentRb.name);
        }
        else
        {
            Debug.LogError("No Rigidbody2D found on parent!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button to shoot
        {
            Shoot();
        }

        move = MoveAction.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {

        Vector2 lookDir = mousePos - parentRb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        targetRotation = Quaternion.Euler(0, 0, angle);


        //point at mouse TURN
        var rotation = Quaternion.Lerp(CharacterTransform.rotation, targetRotation, RotationSmoothingCoef);
        CharacterTransform.rotation = rotation;

        //rotates the object toward the mouse
        //atan2 is math function that returns angle between x axis and 2D vector starting at 0, and terminating at x,y
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
