using UnityEngine;

public class BalanceLogic
{
    public float balanceHandle = 0f; 
    float fallSpeed = 0.5f; // 
    float recoverySpeed = 1.5f;

    public void TubeMove(Vector3 direccion, float speed, Rigidbody rb)
    {
        rb.MovePosition(rb.position + direccion * speed * Time.deltaTime);
    }

    public void UpdateLogic(float inputHorizontal)
    {
        balanceHandle += Time.deltaTime * fallSpeed;
        balanceHandle += inputHorizontal * recoverySpeed * Time.deltaTime;
        balanceHandle = Mathf.Clamp(balanceHandle, -1.1f, 1.1f);
    }

    public bool CheckIfFallen() => Mathf.Abs(balanceHandle) >= 1.0f;
    public void Reset() => balanceHandle = 0f;
}
