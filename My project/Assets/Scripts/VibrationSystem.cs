using UnityEngine;
using System;

public class VibrationSystem : MonoBehaviour
{
    public static VibrationSystem Instance;
    public Action<Vector3, float> OnVibration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    } 

    public void CreateVibration(Vector3 pos, float radius)
    {
        if(OnVibration != null)
        {
            OnVibration.Invoke(pos, radius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
