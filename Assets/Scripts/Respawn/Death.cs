using UnityEngine.SceneManagement;
using UnityEngine;


public class Death : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ResetScene.Reset();
        }
    }
}
