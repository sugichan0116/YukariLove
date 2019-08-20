using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card card;
    public CardHolder holder;
    public CardData[] cardLibrary;

    // Start is called before the first frame update
    void Start()
    {
        cardLibrary = Resources.LoadAll<CardData>("cards/");
    }

    public GameObject DrawCard()
    {
        var newHolder = Instantiate(holder);
        newHolder.GetComponentInChildren<Card>().cardData = BashCardData();
        return newHolder.gameObject;
    }

    public CardData BashCardData()
    {
        var player = FindObjectOfType<Player>();
        var box = cardLibrary.Where(card => card.price <= player.salary);
        var totalWeight = box.Sum(card => RarelityWeight(card.rarelity));
        var dropValue = totalWeight * Random.value;

        float sum = 0;
        foreach(var card in box)
        {
            sum += RarelityWeight(card.rarelity);

            if (sum >= dropValue) return card;
        }

        Debug.LogError($"error {totalWeight} // {dropValue} // {box.Count()}");
        return null;
    }

    public float RarelityWeight(CardData.RareLity rare)
    {
        return 5 - (int)rare;
    }
}
