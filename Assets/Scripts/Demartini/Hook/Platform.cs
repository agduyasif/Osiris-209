using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Shoothook shoothook;
    bool arriba = false;
    float pullOriginal;
    void Update()
    {
        if (arriba && shoothook.yendoDirecto)
        {
            Vector3 pos = transform.position;
            pos.x = player.position.x;
            pos.z = player.position.z;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            arriba = true;
            shoothook.ActivarModoDirecto();
            pullOriginal = shoothook.pullSpeed;
            shoothook.pullSpeed = 1f;
        }
        

    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.layer == 6)
        {
            arriba = false;
            shoothook.DesactivarModoDirecto();
            shoothook.pullSpeed = pullOriginal;
        }
    }
}
