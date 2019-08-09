using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
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
                var effectors = selectedCards.Select(card => card.cardData.effect)
                    .Select(effect => new CardEffector() { effect = effect});

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

public class CardEffector
{
    //effect & context
    public CardEffect effect;

}