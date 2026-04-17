using UnityEngine;

public class Hook : MonoBehaviour
{
    Rigidbody rb;
    bool hit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hit) return;

        if (collision.gameObject.GetComponent<Rigidbody>() != null) { stopHook(); }
    }

    void stopHook()
    {
        hit = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

    }

}
