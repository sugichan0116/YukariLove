using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;

public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        var manager = transform.parent.GetComponent<StoryManager>();
        var scale = transform.localScale;

        transform.localScale = Vector3.zero;
        transform.DOScale(scale, 0.2f);

        manager
            .onNextMessage
            .Take(1)
            .Subscribe(_ =>
            {
                transform.DOScale(scale * 0.8f, 0.2f);
            })
            .AddTo(this);

        manager
            .onNextMessage
            .Skip(2)
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            })
            .AddTo(this);

        manager
            .onFinishStory
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            })
            .AddTo(this);
    }
}
