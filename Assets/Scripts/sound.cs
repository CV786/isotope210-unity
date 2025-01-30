using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip soundtrack; // Assign your soundtrack in the Inspector
    public float volume = 1.0f;  // Set the volume level

    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundtrack;
        audioSource.loop = true; // Enable looping
        audioSource.volume = volume;
        audioSource.playOnAwake = false; // Disable play on awake if controlling via script

        // Play the soundtrack
        audioSource.Play();
    }
}
