using UnityEngine;

public class ocscuridad : MonoBehaviour
{
    [SerializeField] ParticleSystem humo;
    [SerializeField] GameObject pared;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;

        FrascoLuz frasco = other.GetComponentInChildren<FrascoLuz>();
        if (frasco != null && frasco.luciernagas >= 2)
        {
            humo.Stop();
            Destroy(pared);
        }
    }



}
