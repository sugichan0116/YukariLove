using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(ObservableEventTrigger))]
public class StoryManager : MonoBehaviour
{
    public MessageBox master, yukari, blank;
    public GameObject[] portraits;
    public Stack<Story> stories = new Stack<Story>();

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
                    UpdatePortrait(message.portrait);
                    onNextMessage.OnNext(Unit.Default);
                }
                else
                {
                    index = 0;
                    stories.Pop();
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
        stories.Push(story);
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
            default:
                Debug.LogError("error");
                return null;
        }
    }

    private void UpdatePortrait(Speaking.Portrait index)
    {
        //Enumerable
        //    .Range(0, portraits.Length)
        //    .Zip(portraits, )
        //    .ToObservable()
        //    .Select(i => (i, item:portraits[i]))
        //    .Subscribe(p =>
        //    {
        //        p.item.SetActive(p.i == (int)index);
        //    });

        for(int i = 0; i < portraits.Length; i++)
        {
            portraits[i].SetActive(i == (int)index);
        }
    }


}