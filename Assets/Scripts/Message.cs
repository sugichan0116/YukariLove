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
        FindObjectOfType<Player>()
            .ObserveEveryValueChanged(p => p.message)
            .Subscribe(m => {
                window.SetActive(true);
                text.text = m;

                Observable
                    .Timer(TimeSpan.FromSeconds(6))
                    .Subscribe(_ =>
                    {
                        window.SetActive(false);
                    });
            });
    }
}
