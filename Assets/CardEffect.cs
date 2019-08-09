using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
  fileName = "CardEffect",
  menuName = "ScriptableObject/CardEffect",
  order = 0)
]
[System.Serializable]
public class CardEffect : ScriptableObject
{
    public enum Priority
    {
        LOW,
        MID,
        HIGH
    }

    public enum Target
    {
        NONE,
        ALL,
        PREV,
        NEXT,
        CARDTYPE
    }

    public Priority priority;
    public Target target;
    public CardData.CardType targetType;
    public float volume;

    public string TargetToString()
    {
        if (target == Target.CARDTYPE) return $"{targetType}";
        return $"{target}";
    }

    public override string ToString()
    {
        return $"{TargetToString()} x{volume}";
    }
}
