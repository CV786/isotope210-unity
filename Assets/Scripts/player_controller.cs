using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
public class Player_Controller : MonoBehaviour
{
    public InputAction MoveAction;
    public Camera cam;
    Rigidbody2D rb;

    Vector2 move;
    Vector2 mousePos;


    private Quaternion targetRotation;
    public Transform CharacterTransform;
    public float RotationSmoothingCoef = 0.01f;

    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        /*
        float horizontal = 0.0f;
        if (LeftAction.IsPressed())
        {
            horizontal = -1.0f;
        }
        else if (RightAction.IsPressed())
        {
            horizontal = 1.0f;
        }
        Debug.Log(horizontal);


        float vertical = 0.0f;
        if (UpAction.IsPressed())
        {
            vertical = 1.0f;
        }
        else if (DownAction.IsPressed())
        {
            vertical = -1.0f;
        }
        Debug.Log(vertical);
        */



        //movement input
        //Vector2 move = MoveAction.ReadValue<Vector2>();

        //reads MoveAction bindings
        move = MoveAction.ReadValue<Vector2>();



        //point at mouse
        /*
        var groundPlane = new Plane(Vector3.up, CharacterTransform.position.y);
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance;

        if (groundPlane.Raycast(mouseRay, out hitDistance))
        {
            var lookAtPosition = mouseRay.GetPoint(hitDistance);
            targetRotation = Quaternion.LookRotation(lookAtPosition - CharacterTransform.position, Vector3.up);
        }
        */

        //get mouse position to point
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {

        //position of rigidbody
        Vector2 position = (Vector2)rb.position + move * moveSpeed * Time.deltaTime;
        rb.MovePosition(position);

        //point at mouse TURN

        /*
        var rotation = Quaternion.Lerp(CharacterTransform.rotation, targetRotation, RotationSmoothingCoef);
        CharacterTransform.rotation = rotation;

        */

        //point at mouse NO TURN
        Vector2 lookDir = mousePos - rb.position;

        //atan2 is math function that returns angle between x axis and 2D vector starting at 0, and terminating at x,y
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

}