using UnityEngine;

public class VientoCiclico3D : MonoBehaviour
{
    public float tiempoActivo = 2f;
    public float tiempoEspera = 3f;
    public Vector3 fuerzaViento = new Vector3(10, 0, 0);

    private bool vientoPrendido = true;
    private ParticleSystem particulas;

    void Start()
    {
        particulas = GetComponentInChildren<ParticleSystem>();
        InvokeRepeating("AlternarViento", tiempoEspera, tiempoActivo + tiempoEspera);
    }

    void AlternarViento()
    {
        vientoPrendido = true;
        if (particulas) particulas.Play();
        Invoke("ApagarViento", tiempoActivo);
    }

    void ApagarViento()
    {
        vientoPrendido = false;
        if (particulas) particulas.Stop();
    }

    void OnTriggerStay(Collider other)
    {
        if (vientoPrendido && other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(fuerzaViento, ForceMode.Acceleration);
            }
        }
    }
}