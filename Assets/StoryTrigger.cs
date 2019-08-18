using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;
using System;


public class StoryTrigger : BooleanByField
{
    public Story story;
    public int take = 1;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        var publisher = FindObjectOfType<StoryPublisher>();

        //TODO: フレームをずらさないとStoryPublisherが発火しない模様
        //      原因は不明
        Observable
            .NextFrame()
            .Subscribe(__ =>
            {
                onJudge
                    .Where(result => result)
                    .Take(take)
                    .Subscribe(_ =>
                    {
                        Debug.Log($"Story Fired {this} // {_} // {take}");

                        publisher
                            .onTrigger
                            .OnNext(story);
                    });
            })
            .AddTo(this);
        
    }
}
