using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Gauge : MonoBehaviour
{
    public float volume;
    public Color error = new Color(0xD1, 0x3C, 0x3C);

    // Start is called before the first frame update
    void Start()
    {
        var elements = GetComponentsInChildren<GaugeElement>();
        var image = GetComponent<Image>();
        var baseColor = image.color;

        Observable
            .EveryUpdate()
            .Subscribe(_ => {
                float unitLength = GetComponent<RectTransform>().sizeDelta.x - GetComponent<HorizontalLayoutGroup>().padding.horizontal;

                foreach(var e in elements)
                {
                    e.Length = e.volume / volume * unitLength;
                }

                image.color = elements.Select(e => e.volume).Sum() > volume ? error : baseColor;
            })
            .AddTo(gameObject);
    }
}
