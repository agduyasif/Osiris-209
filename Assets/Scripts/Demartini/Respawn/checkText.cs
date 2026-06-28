using UnityEngine;
using TMPro;
using DG.Tweening;

public class checkText : MonoBehaviour
{
    public static checkText Instance {  get; private set; }


    [SerializeField] TextMeshProUGUI text;
    int displayDuration = 2;
    int fadeDuration = 1;

    Tween currentTween;

    private void Awake()
    {
        Instance = this;
        text.alpha = 0;
    }


    public void showText()
    {
        if (currentTween  != null) currentTween.Kill();

        text.alpha = 1f;
        currentTween = text.DOFade(0f,fadeDuration).SetDelay(displayDuration);
    }

}
