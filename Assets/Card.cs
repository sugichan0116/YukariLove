using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public bool selected;
    public CardData cardData;
    public List<CardEffect> effects;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Apply(CardEffector effector)
    {
        var effect = effector.effect;
        if(effect.target == CardEffect.Target.CARDTYPE)
        {
            if(effect.targetType == cardData.type)
            {
                effects.Add(effect);
            }
        }
    }

    public int GetPrice()
    {
        return cardData.price;
    }

    public int GetLove()
    {
        float love = cardData.love;

        foreach(var effect in effects)
        {
            love *= effect.volume;
        }

        return (int)love;
    }
}
