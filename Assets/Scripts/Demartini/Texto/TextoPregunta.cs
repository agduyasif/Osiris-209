using UnityEngine;
using TMPro;
using DG.Tweening;
public class TextoPregunta : Trigger
{
    [SerializeField] TextMeshProUGUI texto;
    bool yaMostrado = false;

    private void Start()
    {
        texto.alpha = 0f;
    }

    protected override void OnEnter(Collider other)
    {
        if (yaMostrado) return;
        yaMostrado = true;
        texto.DOFade(1f, 1f).OnComplete(() => texto.DOFade(0f, 1f).SetDelay(3f));
    }
}
