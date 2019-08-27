using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;
using System;
using System.Collections.Generic;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    public Subject<Unit> onEnding = new Subject<Unit>();
    public Window resultWindow;
    public Transform scoreContainer;
    public ScoreItem prefab;
    public ScoreItem total;
    public float timespan = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        var player = FindObjectOfType<Player>();
        int sumlove = 0, maxlove = 0;

        player
            .ObserveEveryValueChanged(p => p.love)
            .Pairwise()
            .Select(item => item.Current - item.Previous)
            .Where(delta => delta > 0)
            .Subscribe(delta => {
                sumlove += delta;
                maxlove = Mathf.Max(maxlove, delta);
            });
        
        onEnding
            .Subscribe(_ =>
            {
                resultWindow.ActiveWindow();

                var items = new Queue<(string, int)>();

                items.Enqueue(("累計の愛情", sumlove));
                items.Enqueue(($"一回で与えた愛情 {maxlove}", maxlove * 10));

                var level = player.level;
                if (level >= 5)
                {
                    items.Enqueue(($"エンディング Aルート", level * level * 10000));
                }
                else
                {
                    items.Enqueue(($"エンディング Bルート", level * level * 100));
                }

                int count = items.Count;
                int score = items.Sum(item => item.Item2);

                Observable
                    .Interval(TimeSpan.FromSeconds(timespan))
                    .Take(count)
                    .Subscribe(__ =>
                    {
                        var item = items.Dequeue();
                        var obj = Instantiate(prefab);
                        obj.Init(item.Item1, item.Item2, timespan);
                        obj.transform.SetParent(scoreContainer, false);
                    })
                    .AddTo(this);

                Observable
                    .Timer(TimeSpan.FromSeconds(timespan * count))
                    .Take(1)
                    .Subscribe(__ =>
                    {
                        total.Init(null, score, timespan * count);
                    })
                    .AddTo(this);

            })
            .AddTo(this);
    }
}
