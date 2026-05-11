using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float openHeight = 5f;
    [SerializeField] float smooth = 0.4f;
    

    Vector3 closedPos;
    Vector3 openPos;
    Vector3 target;
    Vector3 velocity;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Vector3.down * openHeight;
        target = closedPos;
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smooth);
    }

    public void open()
    {
        target = openPos;
    }

    public void close() 
    {
        target = closedPos;
    }

}
