using UnityEngine;
using UniRx;
using System.Linq;

public class CardStatistic : MonoBehaviour
{
    public enum CardProperty
    {
        NONE,
        PRICE,
        LOVE,
        MONEY,
        DEFICIT,
        CARDS
    }

    public CardProperty property;

    // Start is called before the first frame update
    void Start()
    {
        if (property == CardProperty.NONE) return;

        var element = GetComponent<GaugeElement>();
        var player = FindObjectOfType<Player>();

        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                switch(property)
                {
                    case CardProperty.PRICE:
                        element.volume = SumPrice();
                        break;
                    case CardProperty.LOVE:
                        element.volume = SumLove();
                        break;
                    case CardProperty.MONEY:
                        element.volume = Mathf.Max(0, player.money - SumPrice());
                        break;
                    case CardProperty.DEFICIT:
                        element.volume = Mathf.Max(0, -player.money + SumPrice());
                        break;
                }
                
            })
            .AddTo(gameObject);
    }

    public int SumPrice()
    {
        return FindObjectsOfType<Card>()
            .Where(card => card.selected)
            .Sum(card => card.GetPrice());
    }
    
    public int SumLove()
    {
        return FindObjectsOfType<Card>()
            .Where(card => card.selected)
            .Sum(card => card.GetLove());
    }
}
