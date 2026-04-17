using UnityEngine;
using UnityEngine.InputSystem;

public class Shoothook : MonoBehaviour
{
    [SerializeField] GameObject hookPre;
    [SerializeField] Transform point;
    [SerializeField] float speed = 15f;

    GameObject hook;


    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            shootHook();
        }
    }

    void shootHook()
    {
        if (hook != null)
        {
            Destroy(hook);
        }

        hook = Instantiate(hookPre, point.position, point.rotation);
        Rigidbody rb = hook.GetComponent<Rigidbody>();
        if (rb != null) 
        {
            rb.AddForce(point.forward * speed, ForceMode.Impulse);
        }

    }
}
