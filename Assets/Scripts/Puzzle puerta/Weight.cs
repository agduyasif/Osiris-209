using UnityEngine;
using System.Collections.Generic;
public class Weight : MonoBehaviour
{
    private List<Rigidbody> inPlaform = new List<Rigidbody>();
    float totalMass = 0f;
    [SerializeField] Transform platform;

    [SerializeField] float weightRequiered = 10;
    [SerializeField] float pressDepth = 2;
    [SerializeField] float smooth = 0.4f;
    [SerializeField] Door door;
    

    Vector3 restPos;
    Vector3 pressedPos;
    Vector3 velocity;
    private void Awake()
    {
        restPos = platform.position;
        pressedPos = restPos + Vector3.down * pressDepth;

    }

    private void FixedUpdate()
    {
        Vector3 target = totalMass >= weightRequiered ? pressedPos : restPos;
        platform.position = Vector3.SmoothDamp(platform.position, target, ref velocity, smooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb == null) return; 
        if (inPlaform.Contains(rb)) return;

        inPlaform.Add(rb);
        CalculateTotalMass();
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        inPlaform.Remove(rb);
        CalculateTotalMass();

    }


    private void CalculateTotalMass()
    {
        totalMass = 0f;

        foreach (Rigidbody rb in inPlaform)
        {
            if (rb != null)
                totalMass += rb.mass;
        }

        
    }

    void doorState()
    {
        bool active = totalMass >= weightRequiered;

       
    }
}
