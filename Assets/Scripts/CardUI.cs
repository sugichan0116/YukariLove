using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI product;
    public Image icon;
    public TextMeshProUGUI type;
    public TextMeshProUGUI love;
    public TextMeshProUGUI price;
    public TextMeshProUGUI effect;

    // Start is called before the first frame update
    void Start()
    {
        var card = GetComponent<Card>();
        var cardData = card.cardData;

        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                product.text = cardData.product;
                icon.sprite = cardData.icon;
                type.text = $"{cardData.type}";
                var loveDelta = card.GetLove() - cardData.love;
                love.text = $"{card.GetLove()}" +
                    ((loveDelta > 0) ? $"\n<size=6>(+{loveDelta})</size>" : "");
                price.text = $"${card.GetPrice()}";
                effect.text = $"{cardData.effect}";
            })
            .AddTo(gameObject);
    }
}
