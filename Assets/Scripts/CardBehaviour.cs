﻿using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;

public class CardBehaviour : MonoBehaviour
{
    private float delta = 10;
    public GameObject cover;

    public AudioClip sfx_hover;
    public AudioClip sfx_click;

    private Card card;
    
    // Start is called before the first frame update
    void Start()
    {
        card = GetComponent<Card>();
        var cardData = card.cardData;
        var trigger = GetComponent<ObservableEventTrigger>();

        float offset = transform.localPosition.y;
        
        trigger
            .OnPointerEnterAsObservable()
            .Subscribe(_ => {
                transform.DOLocalMoveY(delta + offset, 0.1f);
                EazySoundManager.PlayUISound(sfx_hover);
            })
            .AddTo(this);

        trigger
            .OnPointerExitAsObservable()
            .Subscribe(_ => {
                transform.DOLocalMoveY(offset, 0.1f);
            })
            .AddTo(this);

        trigger
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                card.selected = !card.selected;
                cover.SetActive(card.selected);
                if(card.selected) EazySoundManager.PlayUISound(sfx_click);
            })
            .AddTo(this);

    }
    
    public void Select()
    {
        card.selected = !card.selected;
        cover.SetActive(card.selected);
    }
}
