using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoothook : MonoBehaviour
{

    LineRenderer line;

    [SerializeField] Transform player;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float pullSpeed = 1f;

    SpringJoint joint;
    
    private void Start()
    {
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
                Debug.Log("Atraer Rigidbody");
            }

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            
            Destroy(joint);
            joint = null;
            line.enabled = false;
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

        joint.spring = 50;
        joint.damper = 14;
        joint.massScale = 4;

        line.positionCount = 2;
        line.enabled = true;
       
    }
}
