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
    public Color error = new Color(0xD1, 0x3C, 0x3C);

    // Start is called before the first frame update
    void Start()
    {
        var elements = GetComponentsInChildren<GaugeElement>()
            .Where(g => g.gameObject != gameObject);
        var image = GetComponent<Image>();
        var baseColor = image.color;

        Observable
            .EveryUpdate()
            .Subscribe(_ => {
                float unitLength = GetComponent<RectTransform>().sizeDelta.x - GetComponent<HorizontalLayoutGroup>().padding.horizontal;
                var volume = GetComponent<GaugeElement>().volume;
                if (volume <= 0f) return;
                var resource = volume;

                foreach (var e in elements)
                {
                    if (resource < 0) break;
                    if(resource - e.volume < 0) {
                        e.Length = resource / volume * unitLength;
                    }
                    else
                    {
                        e.Length = e.volume / volume * unitLength;
                        resource -= e.volume;
                    }
                }
                
                image.color = elements.Sum(e => e.volume) > volume ? error : baseColor;
            })
            .AddTo(gameObject);
    }
}
