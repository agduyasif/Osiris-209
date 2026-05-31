using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoothook : MonoBehaviour
{

    LineRenderer line;

    [SerializeField] Transform player;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float pullSpeed = 1f;
    Player playerScript;
    SpringJoint joint;
    Rigidbody grabbedRb;

    private void Start()
    {
        playerScript = GetComponentInParent<Player>();
        line = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (SistemaMira.Instance.IsGrappable)
            {
                StartGrapple(SistemaMira.Instance.AimPoint);
            }
            else if (SistemaMira.Instance.AimRb != null)
            {
                grabbedRb = SistemaMira.Instance.AimRb;
            }

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            
            Destroy(joint);
            joint = null;
            line.enabled = false;
            playerScript.isGrappling = true;
            grabbedRb = null;
        }

        pullRb();

        if (joint != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, joint.connectedAnchor);
        }

    }
   

    void StartGrapple(Vector3 grapplePoint)
    {
        if (joint != null) return;

        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFrom = Vector3.Distance(player.position, grapplePoint);
        joint.maxDistance = distanceFrom * 0.8f;
        joint.minDistance = distanceFrom * 0.25f;

        joint.spring = 10;
        joint.damper = 10;
        joint.massScale = 3;

        line.positionCount = 2;
        line.enabled = true;
        playerScript.isGrappling = true;
    }

    void pullRb()
    {
        if (grabbedRb != null)
        {
            pullSpeed = pullSpeed / Mathf.Max(0.1f, grabbedRb.mass);
             
            Vector3 puntoDeAgarre = transform.position + transform.forward * 1f;
            float distance = Vector3.Distance(grabbedRb.transform.position, puntoDeAgarre);

            if (distance > 3)
            {
                grabbedRb.transform.position = Vector3.MoveTowards(grabbedRb.transform.position, puntoDeAgarre, pullSpeed * Time.deltaTime);
            }
            else
            {
                grabbedRb.transform.position = puntoDeAgarre;
            }
        }
    }

}
