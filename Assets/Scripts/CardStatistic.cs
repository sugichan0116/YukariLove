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
        CARDS,
        @_OBSERVER
    }

    public CardProperty property;
    public float price, love;

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
                price = SumPrice();
                love = SumLove();

                switch (property)
                {
                    case CardProperty.PRICE:
                        element.volume = price;
                        break;
                    case CardProperty.LOVE:
                        element.volume = love;
                        break;
                    case CardProperty.MONEY:
                        element.volume = Mathf.Max(0, player.money - price);
                        break;
                    case CardProperty.DEFICIT:
                        element.volume = Mathf.Max(0, -player.money + price);
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
