using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class CardStatistic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var element = GetComponent<GaugeElement>();

        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                var cards = FindObjectsOfType<Card>().Where(card => card.selected);
                element.volume = cards.Select(card => card.GetPrice()).Sum();
            })
            .AddTo(gameObject);
    }
}
