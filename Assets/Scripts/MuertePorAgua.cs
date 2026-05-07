using UnityEngine;

public class MuertePorAgua : MonoBehaviour
{
    // Ahora te va a aparecer una cajita en el Inspector para arrastrar el Respawn
    public Transform puntoDeRespawn;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ALGO TOCO EL AGUA: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (puntoDeRespawn != null)
            {
                // Frenamos al personaje
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                // Teletransportamos
                other.transform.position = puntoDeRespawn.position;
                Debug.Log("¡Jugador reseteado!");
            }
            else
            {
                Debug.LogError("¡No arrastraste el RespawnPoint al script en el Inspector!");
            }
        }
    }
}