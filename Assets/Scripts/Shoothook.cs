using UnityEngine;
using UnityEngine.InputSystem;

public class Shoothook : MonoBehaviour
{
    [SerializeField] GameObject hookPre;
    [SerializeField] Transform point;
    [SerializeField] float speed = 15f;

    GameObject hook;
    [SerializeField]Transform player;
    [SerializeField] float pullSpeed = 1f;



    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            shootHook();
        }

        if (hook != null)
        {
            Hook hookScript = hook.GetComponent<Hook>();

            if (hookScript.isAttached)
            {
                float distance = Vector3.Distance(player.position, hook.transform.position);

                if (distance > 2)
                {
                    player.position = Vector3.MoveTowards(player.position, hook.transform.position, pullSpeed * Time.deltaTime);
                }
            }
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
