using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToLevel : MonoBehaviour
{
    [SerializeField] Scene level;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            SceneManager.LoadScene(level);
        }
    }
}
