using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;

public class Window : MonoBehaviour
{
    public ObservableEventTrigger target;
    public Subject<Unit> onClose = new Subject<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        target
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                onClose.OnNext(Unit.Default);
                gameObject.SetActive(false);
            })
            .AddTo(this);
    }
}
