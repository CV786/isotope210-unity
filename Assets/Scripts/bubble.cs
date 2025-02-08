using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
    [Header("Collision Settings")]
    public string playerTag = "Player";
    public string victimTag = "Victim";

    [Header("Audio Settings")]
    public float volume = .8f; // Volume of the sound effect
    private AudioSource audioSource; // Audio source for playing sound
    public AudioClip selectedClip; // Sound effect to play
    public AudioClip[] bounceSoundList;

    private Rigidbody2D rb;

    private Vector2 lastVelocity; // Store the last velocity in FixedUpdate

    // To slow down the velocity
    public float dampFactor = 0.99f; // Damping factor to reduce speed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Attach audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void FixedUpdate()
    {
        // Store the current velocity for the next collision
        lastVelocity = rb.linearVelocity;

        // Reapply the stored velocity, adjusting it for damping
        Vector2 newVelocity = lastVelocity * dampFactor;

        // Apply the damped velocity to the bubble
        rb.linearVelocity = newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        // Check if the bubble collides with Player or Victim
        if ((coll.gameObject.CompareTag(victimTag)) || (coll.gameObject.CompareTag(playerTag)))
        {

            // Ignore the collision between the bubble and the organic object
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());

            // Trigger a function on the organic object
            Organics organic = coll.gameObject.GetComponent<Organics>();
            if (organic != null)
            {
                organic.ContactAcid();  // Call the function on the organic object
            }
        }
        else
        {
            // Handle the bouncing effect if it's not an organic object
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

            // Reapply the reflected velocity to the bubble
            rb.linearVelocity = direction * Mathf.Max(speed, 3f);
        }
    }

    void PlayRandomSound()
    {
        if (bounceSoundList.Length > 0)
        {
            selectedClip = bounceSoundList[Random.Range(0, bounceSoundList.Length)];
            audioSource.PlayOneShot(selectedClip, volume);
        }
        else
        {
            Debug.LogWarning("No effect assigned");
        }
    }
}
