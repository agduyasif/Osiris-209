using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        OnEnter(other);
    }

    protected abstract void OnEnter(Collider other);
}
