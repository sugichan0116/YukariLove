using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>()
            .ObserveEveryValueChanged(t => t.text)
            .Subscribe(_ =>
            {
                transform.DOShakeRotation(0.2f);
            });
    }
}
