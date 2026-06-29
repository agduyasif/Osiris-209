using UnityEngine;
using UnityEngine.Events;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] UnityEvent EventLoadCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer !=6 ) return;

        CheckpointManager.Instance.passCheck(index);
    }

    public void LoadCheckpoint()
    {
        if(EventLoadCheckpoint != null) 
            EventLoadCheckpoint.Invoke();
    }
}
