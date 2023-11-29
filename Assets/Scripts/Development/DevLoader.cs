using UnityEngine;
using UnityEngine.SceneManagement;

public class DevLoader : MonoBehaviour
{
    public static bool loaded;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Initial")
        {
            loaded = true;
        }

        if (!loaded)
        {
            SceneManager.LoadScene("Initial");
        }
    }
}
