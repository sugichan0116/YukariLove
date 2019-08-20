using UnityEngine;
using UniRx;
using System.Linq;
using System.Collections.Generic;

public class CardList : MonoBehaviour
{
    public class CardAggregate
    {
        public Card[] cards, selects;
    }

    public Subject<int> onUpdateEffect = new Subject<int>();
    public List<CardEffect> foreignEffects = new List<CardEffect>();

    // Start is called before the first frame update
    void Start()
    {
        var onChangeCard = new Subject<CardAggregate>();

        Observable
            .EveryUpdate()
            .Select(_ => GetComponentsInChildren<Card>())
            .Select(cards => new CardAggregate {
                cards = cards,
                selects = cards.Where(card => card.selected).ToArray()
            })
            .DistinctUntilChanged(a => (a.cards.Count(), a.selects.Count()))
            .Subscribe(result => {
                onChangeCard.OnNext(result);
            })
            .AddTo(this);

        onChangeCard
            .Subscribe(aggregate =>
            {
                foreignEffects.Clear();
                onUpdateEffect.OnNext(0);

                var cards = aggregate.cards;
                var selectedCards = aggregate.selects;

                var innerEffectors = selectedCards.Select(card => 
                    {
                        return new CardEffector()
                        {
                            effect = card.cardData.effect,
                            self = card.cardData,
                            cards = cards,
                            selectedCards = selectedCards
                        };
                    });

                var foreignEffectors = foreignEffects.Select(effect =>
                    {
                        return new CardEffector()
                        {
                            effect = effect,
                            cards = cards,
                            selectedCards = selectedCards
                        };
                    });

                //Debug.Log($"[Card List] inner({innerEffectors.Count()}) " +
                //    $"foreign({foreignEffectors.Count()}) in {this}");

                var effectors = innerEffectors.Concat(foreignEffectors)
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

    public void PushEffect(CardEffect effect)
    {
        foreignEffects.Add(effect);
    }
}
