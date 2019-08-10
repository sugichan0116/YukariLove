using UnityEngine;
using UniRx;
using System.Linq;
using System.Collections.Generic;

public class CardList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                var cards = FindObjectsOfType<Card>();
                var selectedCards = cards.Where(card => card.selected);
                var effectors = selectedCards.Select(card => {
                    return new CardEffector() {
                        effect = card.cardData.effect,
                        self = card.cardData,
                        cards = cards,
                        selectedCards = selectedCards
                    };
                    })
                    .OrderBy(effector => effector.effect.priority);



                foreach (var card in cards)
                {
                    card.effects = new List<CardEffect>();
                }

                foreach (var effector in effectors)
                {
                    foreach(var card in cards)
                    {
                        card.Apply(effector);
                    }
                }
            })
            .AddTo(gameObject);
    }
}
