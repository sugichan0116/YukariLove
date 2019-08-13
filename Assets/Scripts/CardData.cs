using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[CreateAssetMenu(
  fileName = "CardData",
  menuName = "ScriptableObject/CardData",
  order = 0)
]
[System.Serializable]
public class CardData : ScriptableObject
{
    public enum CardType
    {
        ANY,
        FOOD,
        GAME,
        BOOK
    }

    public enum RareLity
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPER_RARE
    }

    public string product;
    [ShowAssetPreview(128, 128)]
    public Sprite icon;

    [Header("Property")]
    public CardType type;
    public int love;
    public int price;
    public RareLity rarelity;
    public CardEffect effect;
    
}
