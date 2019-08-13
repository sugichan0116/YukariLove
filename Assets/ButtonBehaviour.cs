using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;

public class ButtonBehaviour : MonoBehaviour
{
    public Image stroke, fill;
    public TextMeshProUGUI label;
    public Color main, sub;

    // Start is called before the first frame update
    void Start()
    {
        var trigger = GetComponent<ObservableEventTrigger>();

        trigger
            .OnPointerEnterAsObservable()
            .Subscribe(_ => {
                stroke.color = main;
                fill.color = main;
                label.color = sub;
                //EazySoundManager.PlayUISound(sfx_hover);
            });

        trigger
            .OnPointerExitAsObservable()
            .Subscribe(_ => {
                stroke.color = main;
                fill.color = sub;
                label.color = main;
            });
        
        trigger
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                var offset = transform.localScale;
                DOTween.Sequence()
                    .Append(transform.DOScale(Vector3.one * 0.95f, 0.1f))
                    .OnComplete(() => {
                        transform.localScale = offset;
                    });
                //if (card.selected) EazySoundManager.PlayUISound(sfx_click);
            });
    }
}
