using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public enum AnimationType
    {
        SHAKE_ROTATION,
        SHAKE_SCALE,
    }

    public float duration = 0.2f;
    public AnimationType type;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>()
            .ObserveEveryValueChanged(t => t.text)
            .Subscribe(_ =>
            {
                Sequence seq = DOTween.Sequence();

                switch (type)
                {
                    case AnimationType.SHAKE_ROTATION:
                        seq
                        .Append(transform.DOShakeRotation(duration))
                        .OnComplete(() => {
                            transform.rotation = Quaternion.Euler(Vector3.zero);
                        });
                        
                        break;
                    case AnimationType.SHAKE_SCALE:
                        seq
                        .Append(transform.DOShakeScale(duration))
                        .OnComplete(() => {
                            transform.localScale = Vector3.one;
                        });

                        break;
                }
            });
    }
}
