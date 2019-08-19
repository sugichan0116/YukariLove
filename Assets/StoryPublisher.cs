using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;

public class StoryPublisher : MonoBehaviour
{
    public StoryManager manager;
    public Window storyWindow;
    public Subject<Unit> onClosed = new Subject<Unit>();
    public Subject<Story> onTrigger = new Subject<Story>();

    // Start is called before the first frame update
    void Start()
    {
        onTrigger
            .Subscribe(story =>
            {
                //Debug.Log($"Story Publishing... {this} {story}");

                storyWindow.gameObject.SetActive(true);
                manager.PostStory(story);
                manager
                    .onFinishStory
                    .Subscribe(_ => {
                        storyWindow.gameObject.SetActive(false);
                        onClosed.OnNext(Unit.Default);
                    })
                    .AddTo(this);
            })
            .AddTo(this);

        storyWindow
            .onClose
            .Subscribe(_ => onClosed.OnNext(Unit.Default))
            .AddTo(this);
    }
}
