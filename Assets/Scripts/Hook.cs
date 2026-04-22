using UnityEngine;

public class Hook : MonoBehaviour
{
    Rigidbody rb;
    bool hit = false;
    public bool isAttached = false;
    public bool isHeavier;
    public Rigidbody grabbedRb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hit) return;

        Rigidbody objectRb = collision.gameObject.GetComponent<Rigidbody>();
        if (collision.gameObject.CompareTag("Player")) { return;}
        

        if (objectRb != null) 
        {
            
            if (objectRb.mass > 0.99f)
            {
                isHeavier = false;
                stopHook();
            }
            else { isHeavier = true; stopHook(); grabbedRb = objectRb; }
        }
    }

    void stopHook()
    {
        hit = true;
        isAttached = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        gameObject.SetActive(false);

    }

}
