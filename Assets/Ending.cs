using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;
using System;

public class Ending : BooleanByField
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        var manager = FindObjectOfType<ResultManager>();

        onJudge
            .Where(result => result)
            .Take(1)
            .Subscribe(_ =>
            {
                manager
                    .onEnding
                    .OnNext(Unit.Default);
            })
            .AddTo(this);
    }
}
