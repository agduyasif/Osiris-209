using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoothook : MonoBehaviour
{
    [SerializeField] GameObject hookPre;
    [SerializeField] Transform point;
    [SerializeField] float speed = 15f;
    bool isDestroyed;


    GameObject hook;
    [SerializeField]Transform player;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float pullSpeed = 1f;



    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            shootHook();
           
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            
            Destroy(hook);
            isDestroyed = true;
        
        }

        if (hook != null)
        {
            Hook hookScript = hook.GetComponent<Hook>();

            if (hookScript.isAttached)
            {
                
                if (hookScript.isPlayer == false && hookScript.isGrapabble == true)
                {
                    float distance = Vector3.Distance(player.position, hook.transform.position);
                    playerRb.useGravity = false;
                    InHook.inHook(playerRb);
                    if (distance > 2)
                    {
                        player.position = Vector3.MoveTowards(player.position, hook.transform.position, pullSpeed * Time.deltaTime);
                    }
                }
                else 
                {
                    if (hookScript.grabbedRb != null)
                    {
                        float objetctMass = hookScript.grabbedRb.mass;
                        float realPullSpeed = pullSpeed / Mathf.Max(0.1f, objetctMass);
                        InHook.inHook(hookScript.grabbedRb);

                        float distance = Vector3.Distance(hookScript.grabbedRb.transform.position, player.position);
                        Vector3 puntoDeAgarre = transform.position + (transform.forward * 1f);

                        if (distance > 2.2f)
                        {
                            hookScript.grabbedRb.transform.position = Vector3.MoveTowards(hookScript.grabbedRb.transform.position, puntoDeAgarre, realPullSpeed * Time.deltaTime);
                            
                        }else 
                        {
                            hookScript.grabbedRb.transform.position = puntoDeAgarre;
                            
                        }
                    }

                }

                
                
            }
        }
        if (isDestroyed)
        {           
            playerRb.useGravity = true;
            isDestroyed = false;
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
