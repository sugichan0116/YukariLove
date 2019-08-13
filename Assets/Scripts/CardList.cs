using UnityEngine;
using UniRx;
using System.Linq;
using System.Collections.Generic;

public class CardList : MonoBehaviour
{
    public Subject<int> onUpdateEffect = new Subject<int>();
    public List<CardEffect> foreignEffects;

    // Start is called before the first frame update
    void Start()
    {
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                foreignEffects = new List<CardEffect>();
                onUpdateEffect.OnNext(0);

                var cards = FindObjectsOfType<Card>();
                var selectedCards = cards.Where(card => card.selected);
                var innerEffectors = selectedCards.Select(card => {
                    return new CardEffector()
                    {
                        effect = card.cardData.effect,
                        self = card.cardData,
                        cards = cards,
                        selectedCards = selectedCards
                    };
                });

                var foreign = foreignEffects.Select(effect =>
                    {
                        return new CardEffector()
                        {
                            effect = effect,
                            cards = cards,
                            selectedCards = selectedCards
                        };
                    });

                var effectors = innerEffectors.Concat(foreign)
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
