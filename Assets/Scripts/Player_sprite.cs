using UnityEngine;

public class player_sprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Quaternion initialLocalRotation; // Store the initial local rotation

    void Start()
    {
        // Save the initial local rotation so it remains fixed
        initialLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // Reset the child's local rotation every frame
        transform.localRotation = initialLocalRotation;
    }

}
