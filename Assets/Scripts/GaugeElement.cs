using UnityEngine;
using UniRx;
using DG.Tweening;
using TMPro;

public class GaugeElement : MonoBehaviour
{
    public float volume;
    public float Length { get; set; }
    public bool IsGauge;

    // Start is called before the first frame update
    void Start()
    {
        if (IsGauge) return;

        var rect = GetComponent<RectTransform>();
        var text = GetComponentInChildren<TextMeshProUGUI>();
        var offset = rect.sizeDelta;
        
        this.ObserveEveryValueChanged(_ => Length)
            .Subscribe(_ => {
                rect.DOSizeDelta(new Vector2(Length, offset.y), 0.1f);
                text.text = (volume > 0) ? $"{volume}" : "";
            });
    }
}
