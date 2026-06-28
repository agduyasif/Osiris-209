using UnityEngine;

public static class InHook
{
    public static void inHook(Rigidbody Rb)
    {
        Rb.linearVelocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
    }
}
