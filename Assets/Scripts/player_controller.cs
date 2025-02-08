using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public InputAction MoveAction;
    public Camera cam;
    private Rigidbody2D rb;
    private Vector2 move;
    private Vector2 velocity = Vector2.zero;
    private Vector2 mousePos;
    public Animator animator;

    public float moveSpeed = 5f;
    public float movementSmoothing = 0.1f; // Lower values = smoother movement

    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Read movement input
        move = MoveAction.ReadValue<Vector2>();

        // Get mouse position in world space
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        FlipCharacter(mousePos);
    }

    void FixedUpdate()
    {
        SmoothMove();
        UpdateAnimation();
    }

    void SmoothMove()
    {
        // Target movement position
        Vector2 targetVelocity = move * moveSpeed;

        // Smoothly interpolate towards target velocity
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, movementSmoothing);
    }

    void UpdateAnimation()
    {
        bool isMoving = move.magnitude > 0;
        bool isShooting = Input.GetMouseButton(0);

        animator.SetBool("Idle", !(isMoving || isShooting));
    }

    void FlipCharacter(Vector2 mousePosition)
    {
        // Compare mouse position with player's position
        if (mousePosition.x < transform.position.x)
        {
            // Mouse is to the left → flip character left
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            // Mouse is to the right → flip character right
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
