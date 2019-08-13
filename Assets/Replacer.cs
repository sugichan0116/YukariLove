using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System.Linq;
using System;

public class Replacer : MonoBehaviour
{
    public CardList cardCase;
    
    public void ApplyReplacement()
    {
        var deck = FindObjectOfType<Deck>();
        var selectedHolder = cardCase.GetComponentsInChildren<CardHolder>()
            .Where(holder => holder.GetComponentInChildren<Card>().selected);
        var selectedCount = selectedHolder.Count();

        if(selectedCount <= 0)
        {
            var holders = cardCase.GetComponentsInChildren<CardHolder>();

            foreach(var holder in holders)
            {
                holder.GetComponentInChildren<CardBehaviour>().Select();
            }
        }
        else
        {
            foreach (var holder in selectedHolder)
            {
                Destroy(holder.gameObject);
            }

            Observable
                .Interval(TimeSpan.FromMilliseconds(250))
                .Take(selectedCount)
                .Subscribe(l =>
                {
                    var holder = deck.DrawCard();
                    holder.transform.SetParent(cardCase.transform, false);
                }).AddTo(this);

            gameObject.SetActive(false);
        }
    }
}
