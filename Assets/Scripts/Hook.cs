using UnityEngine;

public class Hook : MonoBehaviour
{
    Rigidbody rb;
    bool hit = false;
    public bool isAttached = false;
    public bool isPlayer;
    public Rigidbody grabbedRb;
    public bool isGrapabble;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hit) return;

        
        if (collision.gameObject.layer == 6) { return;}

        if (collision.gameObject.layer == 7)
        { 
            isPlayer = false; 
            isGrapabble = true;
            stopHook();
            return;
        }
        Rigidbody objectRb = collision.gameObject.GetComponent<Rigidbody>();
        if (objectRb != null) 
        {      
            isPlayer = false;
            isGrapabble = false;
            stopHook(); 
            grabbedRb = objectRb; 
        }
    }

    void stopHook()
    {
        hit = true;
        isAttached = true;

        InHook.inHook(rb);
        rb.isKinematic = true;
        gameObject.SetActive(false);

    }

}
