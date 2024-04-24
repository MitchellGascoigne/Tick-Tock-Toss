using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenu : MonoBehaviour
{
    // This method is called when the button is clicked.
    public void LoadMainMenu()
    {
        // Load the scene named "Menu".
        SceneManager.LoadScene("Menu");
    }
}
