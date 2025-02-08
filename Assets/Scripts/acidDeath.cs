using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class acidDeath : MonoBehaviour

{
    [Header("Audio Settings")]
    public float volume = 1; // volume of the sound effect
    public AudioSource audioSource; // audio source for playing sound
    private AudioClip selectedClip; // selection from list to play a sound from
    public AudioClip[] deathVoiceList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //attach audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        PlayRandomSound();
    }



    void PlayRandomSound()
    {
        if (deathVoiceList.Length > 0)
        {
            //randomly selects and stores a random voice from the list, then plays it
            selectedClip = deathVoiceList[Random.Range(0, deathVoiceList.Length)];
            audioSource.PlayOneShot(selectedClip, volume);
        }
        else
        {
            Debug.LogWarning("no effect assigned");
        }
    }
}
