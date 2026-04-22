using UnityEngine.SceneManagement;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;

           SceneManager.LoadScene(currentScene);
        }
    }
}
