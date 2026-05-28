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
                
            }

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            
            Destroy(joint);
            joint = null;
            line.enabled = false;
            playerScript.isGrappling = true;
        }

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
}
