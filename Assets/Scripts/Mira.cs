using UnityEngine;
using UnityEngine.UI;
public class Mira : MonoBehaviour
{
    
    
    void Update()
    {
        Ray rayo = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }
}
