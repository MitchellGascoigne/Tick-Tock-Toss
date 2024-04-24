using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoader : MonoBehaviour
{
    [SerializeField] string initialSceneName;
    [SerializeField] string mainSceneName;

    private void Start()
    {
        // Check if the initial scene has already been loaded
        if (!string.IsNullOrEmpty(mainSceneName) && SceneManager.GetSceneByName(mainSceneName).isLoaded)
        {
            // The initial scene has already been loaded, so cancel the auto-load
            Debug.LogWarning("The Initial scene has already been loaded. This is not allowed. Cancelling main menu auto-load.");
            gameObject.SetActive(false);
            return;
        }

        // Load the main menu scene
        SceneManager.LoadScene(mainSceneName);
    }

    public void LoadInitialScene()
    {
        SceneManager.LoadScene(initialSceneName);
    }
}