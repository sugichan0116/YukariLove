using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;
using System;
using System.Linq;
using System.Collections.Generic;

public class StoryTalk : MonoBehaviour
{
    public Story[] stories;

    private Stack<Story> shuffled = new Stack<Story>();
    private StoryPublisher publisher;

    // Start is called before the first frame update
    void Start()
    {
        publisher = FindObjectOfType<StoryPublisher>();
    }

    public void SendStory()
    {
        if(shuffled.Count == 0)
            shuffled = new Stack<Story>(stories.OrderBy(_ => Guid.NewGuid()));
        
        publisher
            .onTrigger
            .OnNext(shuffled.Pop());
    }
}
