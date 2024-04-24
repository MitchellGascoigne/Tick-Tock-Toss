using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] string menuSceneName = "Menu";

    // Call this method to go back to the menu scene
    public void GoBackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
