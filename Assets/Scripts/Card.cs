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
        if(effector.CanApply(cardData)) effects.Add(effector.effect);
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
            love = effect.ApplyBonus(love);
        }

        return (int)love;
    }
}
