using UnityEngine;
using UnityEngine.SceneManagement;
public static class ResetScene
{
    public static void Reset()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
