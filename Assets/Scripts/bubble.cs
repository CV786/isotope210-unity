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
        //PlayRandomSound();


        //collision w/ organics - dissolve
        if ((coll.gameObject.CompareTag(victimTag)) || (coll.gameObject.CompareTag(playerTag)))
        {

            //pass over the organic object
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());

            // Trigger a function on the organic object.
            Organics organic = coll.gameObject.GetComponent<Organics>();
            if (organic != null)
            {
                organic.ContactAcid();  // Call the function on the organic object.
            }

        } else
        {


            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

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
            Debug.LogWarning("no effect assigned");
        }
    }

}
