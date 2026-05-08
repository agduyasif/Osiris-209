using System.Threading;
using UnityEngine;

public class VibrationMeter : MonoBehaviour
{
    public static VibrationMeter Instance;
    public float currentVibration = 0f;
    public float maxVibration = 100f;
    public float decaySpeed = 10f;
    

    private void Awake()
    {
        Instance = this;
    }

    
    void Update()
    {
        currentVibration -= decaySpeed * Time.deltaTime;
        currentVibration = Mathf.Clamp(currentVibration, 0, maxVibration);
    } 

    public void AddVibration(float amount)
    {
        currentVibration += amount;
        currentVibration = Mathf.Clamp(currentVibration, 0, maxVibration);
    }
}
