using UniRx;
using System.Linq;
using System.Collections.Generic;

public class CardEffector
{
    //effect & context
    public CardEffect effect;
    public CardData self;
    public IEnumerable<Card> cards, selectedCards;

    public bool CanApply(CardData target)
    {
        //targetType
        if (target.type != effect.targetType &&
            (int)target.type * (int)effect.targetType > 0) return false;

        //condition
        switch (effect.condition)
        {
            case CardEffect.Conditions.ALL:
                return true;
            case CardEffect.Conditions.OTHER:
                return self != target;
            case CardEffect.Conditions.GROUP:
                var targetCount = selectedCards
                    .Where(card => card.cardData.type == effect.targetType
                        || effect.targetType == CardData.CardType.ANY)
                    .Count();

                return targetCount >= effect.conditionVolume;
            case CardEffect.Conditions.PRICE:
                return target.price >= effect.conditionVolume;
        }

        return false;
    }
}