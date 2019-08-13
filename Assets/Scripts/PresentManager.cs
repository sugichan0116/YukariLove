using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System.Linq;
using System;
using Hellmade.Sound;

public class PresentManager : MonoBehaviour
{
    public Window present;
    public GameObject cardCase;
    public GameObject resultCardCase;
    public bool IsClicked;

    public AudioClip sfx_click;
    public AudioClip sfx_click2;

    public void AwakePresentWindow()
    {
        EazySoundManager.PlayUISound(sfx_click);

        var player = FindObjectOfType<Player>();

        //show result
        var selectedCards = cardCase.GetComponentsInChildren<CardHolder>()
            .Where(holder => holder.GetComponentInChildren<Card>().selected);
        
        if (selectedCards.Count() <= 0)
        {
            IsClicked = true;

            Observable
                .Timer(TimeSpan.FromSeconds(3))
                .Subscribe(_ => {
                    IsClicked = false;
                })
                .AddTo(this);

            return;
        }

        if (player.GetComponent<CardStatistic>().SumPrice() > player.money) return;

        present.gameObject.SetActive(true);


        //reset
        foreach (Transform child in resultCardCase.transform)
        {
            Destroy(child.gameObject);
        }
        
        //clone
        foreach(var holder in selectedCards)
        {
            var clone = Instantiate(holder);
            var func = clone.GetComponentInChildren<CardBehaviour>();
            Destroy(func.cover);
            Destroy(func);
            Destroy(clone.GetComponentInChildren<Card>());
            Destroy(clone.GetComponentInChildren<CardUI>());
            clone.transform.SetParent(resultCardCase.transform, false);
        }
    }

    public void ApplyPresent()
    {
        EazySoundManager.PlayUISound(sfx_click2);

        var player = FindObjectOfType<Player>();
        var statics = player.GetComponent<CardStatistic>();
        player.money -= statics.SumPrice();
        player.love += statics.SumLove();
        player.day++;
        player.message = "ありがとう！";
        player.IsNotWorked = true;

        //reset
        foreach (Transform child in cardCase.transform)
        {
            Destroy(child.gameObject);
        }
        
        Observable
            .Interval(TimeSpan.FromMilliseconds(250))
            .Take(6)
            .Subscribe(l =>
            {
                //Debug.Log($"ok {l}");
                var deck = FindObjectOfType<Deck>();
                var holder = deck.DrawCard();
                holder.transform.SetParent(cardCase.transform, false);
            }).AddTo(this);

        present.gameObject.SetActive(false);
    }
}
