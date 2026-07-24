using UnityEngine;

public class Frasco : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject frascoluzPre;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > 0.1f) 
        {
            float distancia = Vector3.Distance(transform.position, player.position);
            if (distancia < 3)
            {
                Instantiate(frascoluzPre, player.position, Quaternion.identity, player);
                Destroy(gameObject);
            }
        }
    }
}
