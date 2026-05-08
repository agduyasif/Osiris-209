using UnityEngine;

public class MuertePorAgua : MonoBehaviour
{
    public Transform puntoDeRespawn;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (puntoDeRespawn != null)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                other.transform.position = puntoDeRespawn.position;
            }

        }
    }
}