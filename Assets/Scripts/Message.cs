using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System;

public class Message : MonoBehaviour
{
    public GameObject window;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        var onFade = new Subject<Unit>();

        FindObjectOfType<Player>()
            .ObserveEveryValueChanged(p => p.message)
            .Subscribe(m => {
                window.SetActive(true);
                window.transform.DOPunchScale(Vector3.one * .3f, 0.3f);
                text.text = m;

                Observable
                    .Timer(TimeSpan.FromSeconds(1))
                    .Subscribe(_ =>
                    {
                        onFade.OnNext(new Unit());
                    });
            });

        onFade
            .Throttle(TimeSpan.FromSeconds(3))
            .Subscribe(_ =>
            {
                window.transform.localScale = Vector3.one;
                window.SetActive(false);
            });
    }
}
