using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToLevel : MonoBehaviour
{
    [SerializeField] string level;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            SceneManager.LoadScene(level);
        }
    }
}
