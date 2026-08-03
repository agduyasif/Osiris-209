using UnityEditor.Compilation;
using UnityEngine;

public abstract class agarrable : MonoBehaviour
{
    [SerializeField] protected Transform player;
    protected Rigidbody rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                float distancia = Vector3.Distance(transform.position, player.position);
                if (distancia < 3)
                {
                    alAgarrar();
                    Destroy(gameObject);
                }
            }
        
    }

    protected abstract void alAgarrar();
    

    

}
