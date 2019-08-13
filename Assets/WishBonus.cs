using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System;

public class WishBonus : MonoBehaviour
{
    public Sprite[] sprites;
    public CardEffect[] effects;
    public int index;

    public TextMeshProUGUI text;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        var cardlist = FindObjectOfType<CardList>();
        
        cardlist
            .onUpdateEffect
            .Subscribe(_ =>
            {
                cardlist.foreignEffects.Add(effects[index]);
            });

        this
            .ObserveEveryValueChanged(wb => wb.index)
            .Subscribe(index => {
                text.text = $"{effects[index]}";
                image.sprite = sprites[index];
            });
    }
}
