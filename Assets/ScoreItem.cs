using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;

public class ScoreItem : MonoBehaviour
{
    public TextMeshProUGUI label, valueText;

    public void Init(string l, float v, float timespan)
    {
        if(l != null) label.text = l;

        DOVirtual.Float(0f, v, timespan, value =>
        {
            valueText.text = $"{(int)value}";
        });
    }
}
