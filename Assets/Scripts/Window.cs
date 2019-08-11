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
    // Start is called before the first frame update
    void Start()
    {
        target
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                gameObject.SetActive(false);
            });
    }
}
