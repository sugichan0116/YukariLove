using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class GaugeVolumeReverse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var root = transform.parent.GetComponent<GaugeElement>();
        var self = GetComponent<GaugeElement>();

        Observable
            .EveryUpdate()
            .Select(_ => root.GetComponentsInChildren<GaugeElement>()
                    .Where(e => e != self && e != root)
                    .Sum(e => e.volume))
            .DistinctUntilChanged()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(fillVolume => {
                self.volume = Mathf.Max(0, root.volume - fillVolume);
            })
            .AddTo(gameObject);

    }
}
