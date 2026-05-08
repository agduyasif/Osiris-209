using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class VibrationUI : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (VibrationMeter.Instance == null) return;

        float current = VibrationMeter.Instance.currentVibration;
        float max = VibrationMeter.Instance.maxVibration;

        slider.value = Mathf.Lerp(slider.value, current, 10f * Time.deltaTime);

        float normalized = current / max;

        if (normalized < 0.5f)
        {
            fill.color = Color.Lerp(Color.green, Color.yellow, normalized * 2f);
        }

        else
        {
            fill.color = Color.Lerp(Color.yellow, Color.red, (normalized - 0.5f) * 2f);
        }
            
    }
}
