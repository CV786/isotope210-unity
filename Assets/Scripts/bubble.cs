using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
    [Header("Collision Settings")]
    public string playerTag = "Player";
    public string victimTag = "Victim";
    public GameObject objectToSpawn; // Prefab to spawn upon collision
    public AudioClip collisionSound; // Sound effect to play
    public AudioClip[] soundEffects;

    [Header("Audio Settings")]
    public float volume = .8f; // Volume of the sound effect
    private AudioSource audioSource; // Audio source for playing sound

    private Rigidbody2D rb;

    private Vector3 lastVelocity;

    //to slow velocity
    public float dampFactor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //attach audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;

        rb.linearVelocity = rb.linearVelocity * dampFactor;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

        rb.linearVelocity = direction * Mathf.Max(speed, 3f);


        //dissolve player
        if (coll.gameObject.CompareTag(playerTag))
        {
            Destroy(coll.gameObject);

            // Play the collision sound
            if (collisionSound != null)
            {
                PlayRandomSound();
                //audioSource.PlayOneShot(collisionSound, volume); //play bubble effect
            }

            // Spawn a new object at the collision point
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, coll.contacts[0].point, Quaternion.identity);
            }
        }

        //dissolve victim
        if (coll.gameObject.CompareTag(victimTag))
        {
            // Destroy the object with the specified tag
            Destroy(coll.gameObject);

            // Play the collision sound
            if (collisionSound != null)
            {
                PlayRandomSound();
                //audioSource.PlayOneShot(collisionSound, volume); //play bubble effect
            }

            // Spawn a new object at the collision point
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, coll.contacts[0].point, Quaternion.identity);
            }

        }
    }


    void PlayRandomSound()
    {
        if (soundEffects.Length > 0)
        {
            // Select a random sound effect from the list
            AudioClip selectedClip = soundEffects[Random.Range(0, soundEffects.Length)];

            // Play the sound effect
            audioSource.PlayOneShot(selectedClip, volume);
        }
        else
        {
            Debug.LogWarning("no effect assigned");
        }
    }

}
