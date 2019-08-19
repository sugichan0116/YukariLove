using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(ObservableEventTrigger))]
public class StoryManager : MonoBehaviour
{
    public MessageBox master, yukari, blank;
    //public Story story;
    public Queue<Story> stories = new Queue<Story>();

    public Subject<Unit> onNextMessage = new Subject<Unit>();
    public Subject<Unit> onFinishStory = new Subject<Unit>();
    
    // Start is called before the first frame update
    void Start()
    {
        var trigger = GetComponent<ObservableEventTrigger>();
        int index = 0;

        Observable
            .EveryUpdate()
            .Where(_ => index == 0 || Input.GetMouseButtonDown(0))
            .Where(_ => stories.Count > 0)
            .Subscribe(_ =>
            {
                if(stories.Peek().messages.Count > index)
                {
                    var message = stories.Peek().messages[index++];
                    var box = PushMessageBox(MessageBox(message.speaker));
                    box.text.text = message.text;
                    onNextMessage.OnNext(Unit.Default);
                }
                else
                {
                    index = 0;
                    stories.Dequeue();
                    PushMessageBox(blank);
                    if (stories.Count == 0)
                    {
                        onFinishStory.OnNext(Unit.Default);
                    }
                }
            })
            .AddTo(this);
    }

    private MessageBox PushMessageBox(MessageBox prefab)
    {
        var box = Instantiate(prefab);
        box.transform.SetParent(transform, false);
        box.transform.SetAsFirstSibling();

        return box;
    }

    public void PostStory(Story story)
    {
        stories.Enqueue(story);
        //this.story = story;
    }

    private MessageBox MessageBox(Speaking.Speaker speaker)
    {
        switch(speaker)
        {
            case Speaking.Speaker.MASTER:
                return master;
            case Speaking.Speaker.YUKARI:
                return yukari;
        }

        Debug.LogError("error");
        return null;
    }


}