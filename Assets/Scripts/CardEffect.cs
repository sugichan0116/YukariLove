using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        HIGH,
        MID,
        LOW
    }

    public enum Conditions
    {
        ALL,
        OTHER,
        GROUP
    }

    public enum BonusType
    {
        ADD,
        PRODUCT
    }
    
    [Header("Condition")]
    public Conditions condition;
    public int conditionVolume;
    public CardData.CardType targetType;

    [Header("Bonus")]
    public BonusType bonus;
    public Priority priority;
    public float bonusVolume;

    public string TargetToString()
    {
        var target = (targetType == CardData.CardType.ANY) ? "カード" : $"[{targetType}]";

        switch (condition)
        {
            case Conditions.ALL:
                return $"全ての{target}に ";
            case Conditions.OTHER:
                return $"他の{target}に ";
            case Conditions.GROUP:
                return $"もし{target}が{conditionVolume}枚以上なら";
        }

        return $"Error";
    }

    public string BonusToString()
    {
        switch (bonus)
        {
            case BonusType.ADD:
                return $"+{bonusVolume}ボーナス！";
            case BonusType.PRODUCT:
                return $"x{bonusVolume}倍ボーナス！";
        }

        return $"Error";
    }

    public float ApplyBonus(float origin)
    {
        switch (bonus)
        {
            case BonusType.ADD:
                return origin + bonusVolume;
            case BonusType.PRODUCT:
                return origin * bonusVolume;
        }

        return origin;
    }

    public override string ToString()
    {
        return $"{TargetToString()} {BonusToString()}";
    }

    [CustomEditor(typeof(CardEffect))]
    public class CardEffectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CardEffect t = target as CardEffect;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"Preview");
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextArea($"{t}", GUILayout.Height(40));
            EditorGUI.EndDisabledGroup();
        }
    }
}
