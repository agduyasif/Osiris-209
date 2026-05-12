using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer !=6 ) return;

        CheckpointManager.Instance.passCheck(index);
    }
}
