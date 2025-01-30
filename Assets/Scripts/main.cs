using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI elements

public class Main : MonoBehaviour
{
    public Text dialogueBox; // Reference to a UI Text element for displaying messages
    private bool gameEnded = false; // Flag to prevent multiple triggers

    void Update()
    {
        if (gameEnded) return;

        // Check win condition
        if (GameObject.FindGameObjectsWithTag("Victim").Length == 0)
        {
            ShowDialogue("WIN");
            gameEnded = true;
        }

        // Check lose condition
        else if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            ShowDialogue("DEAD");
            gameEnded = true;
            Invoke("ResetGame", 60f); // Reset the game after 60 seconds
        }
    }

    void ShowDialogue(string message)
    {
        if (dialogueBox != null)
        {
            dialogueBox.text = message; // Set the dialogue text
            dialogueBox.gameObject.SetActive(true); // Ensure the dialogue box is visible
        }
        else
        {
            Debug.LogWarning("dialogue box not set in controller.");
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}