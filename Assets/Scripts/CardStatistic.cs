using UnityEngine;
using UniRx;
using System.Linq;

public class CardStatistic : MonoBehaviour
{
    public enum CardProperty
    {
        PRICE,
        LOVE
    }

    public CardProperty property;

    // Start is called before the first frame update
    void Start()
    {
        var element = GetComponent<GaugeElement>();

        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                var cards = FindObjectsOfType<Card>().Where(card => card.selected);
                switch(property)
                {
                    case CardProperty.PRICE:
                        element.volume = cards.Select(card => card.GetPrice()).Sum();
                        break;
                    case CardProperty.LOVE:
                        element.volume = cards.Select(card => card.GetLove()).Sum();
                        break;
                }
                
            })
            .AddTo(gameObject);
    }
}
