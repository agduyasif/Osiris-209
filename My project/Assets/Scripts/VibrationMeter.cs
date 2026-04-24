using System.Threading;
using UnityEngine;

public class VibrationMeter : MonoBehaviour
{
    public static VibrationMeter Instance;
    public float currentVibration = 0f;
    public float maxVibration = 100f;
    public float decaySpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentVibration -= decaySpeed * Time.deltaTime;
        currentVibration = Mathf.Clamp(currentVibration, 0, maxVibration);
    } 

    public void AddVibration(float amount)
    {
        currentVibration += amount;
        currentVibration = Mathf.Clamp(currentVibration, 0, maxVibration);

        Debug.Log("SUMANDO: " + amount);
    }
}
